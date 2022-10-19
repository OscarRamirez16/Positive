using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

using eFacturacionColombia_V2.Tipos.Standard;
using eFacturacionColombia_V2.Util;

namespace eFacturacionColombia_V2.Documentos
{
    public class GeneradorNotaCredito
    {
        public CreditNoteType CreditNote { get; private set; }

        public GeneradorNotaCredito(CreditNoteType creditNote)
        {
            CreditNote = creditNote;
        }

        public GeneradorNotaCredito(AmbienteDestino ambiente, ConceptoNotaCredito concepto, ReferenciaDocumento referencia)
        {
            CreditNote = new CreditNoteType();
            CreditNote.UBLVersionID = new UBLVersionIDType { Value = "UBL 2.1" };
            CreditNote.CustomizationID = new CustomizationIDType { Value = OperacionNotaCredito.CON_REFERENCIA_FE.Codigo };
            CreditNote.ProfileID = new ProfileIDType { Value = "DIAN 2.1" };
            CreditNote.ProfileExecutionID = new ProfileExecutionIDType { Value = ambiente.Codigo };
            CreditNote.UBLExtensions = new UBLExtensionType[]
            {
                new UBLExtensionType { ExtensionContent = new ExtensionContentType { }  },
                new UBLExtensionType { ExtensionContent = new ExtensionContentType { }  }
            };

            CreditNote.CreditNoteTypeCode = new CreditNoteTypeCodeType { Value = TipoDocumento.NOTA_CREDITO.Codigo };

            CreditNote.DiscrepancyResponse = new ResponseType[]
            {
                new ResponseType
                {
                    ReferenceID = new ReferenceIDType { Value = referencia.Numero },
                    ResponseCode = new ResponseCodeType { Value = concepto.Codigo },
                    Description = new DescriptionType[]
                    {
                        new DescriptionType { Value = concepto.Descripcion }
                    }
                }
            };

            CreditNote.BillingReference = new BillingReferenceType[]
            {
                new BillingReferenceType
                {
                    InvoiceDocumentReference = new DocumentReferenceType
                    {
                        ID = new IDType { Value = referencia.Numero },
                        UUID = new UUIDType
                        {
                            schemeName = referencia.AlgoritmoCufe.Codigo,
                            Value = referencia.Cufe
                        },
                        DocumentTypeCode = new DocumentTypeCodeType { Value = referencia.TipoDocumento.Codigo },
                        IssueDate = new IssueDateType { Value = referencia.Fecha }
                    }
                }
            };
        }


        public GeneradorNotaCredito ConOperacion(OperacionNotaCredito operacion)
        {
            CreditNote.CustomizationID = new CustomizationIDType { Value = operacion.Codigo };

            return this;
        }

        public GeneradorNotaCredito ConNumero(string numero)
        {
            CreditNote.ID = new IDType { Value = numero };

            return this;
        }

        public GeneradorNotaCredito ConFecha(DateTime fecha)
        {
            CreditNote.IssueDate = new IssueDateType { Value = fecha };

            CreditNote.IssueTime = new IssueTimeType { Value = fecha.ToString("HH:mm:ss") + "-05:00" };

            return this;
        }

        public GeneradorNotaCredito ConNota(string texto)
        {
            CreditNote.Note = new NoteType[] { new NoteType { Value = texto } };

            return this;
        }

        public GeneradorNotaCredito ConMoneda(Moneda moneda)
        {
            CreditNote.DocumentCurrencyCode = new DocumentCurrencyCodeType { Value = moneda.Codigo };

            return this;
        }

        public GeneradorNotaCredito ConPeriodoFacturacion(PeriodoFacturacion periodo)
        {
            CreditNote.InvoicePeriod = new PeriodType[]
            {
                new PeriodType
                {
                    StartDate = new StartDateType { Value = periodo.FechaInicio },
                    StartTime = new StartTimeType{ Value = periodo.FechaInicio.ToString("HH:mm:ss") + "-05:00" },
                    EndDate = new EndDateType { Value = periodo.FechaFin },
                    EndTime = new EndTimeType { Value = periodo.FechaFin.ToString("HH:mm:ss") + "-05:00" }
                }
            };

            return this;
        }

        public GeneradorNotaCredito ConOrdenCompra(OrdenCompra orden)
        {
            CreditNote.OrderReference = new OrderReferenceType
            {
                ID = new IDType { Value = orden.Numero },
                IssueDate = new IssueDateType { Value = orden.Fecha }
            };

            return this;
        }

        public GeneradorNotaCredito ConExtensionDian(ExtensionDian extension)
        {
            var dianExtensions = new DianExtensionsType
            {
                InvoiceControl = new InvoiceControl
                {
                    InvoiceAuthorization = new NumericType1 { Value = extension.RangoNumeracion.NumeroResolucion },
                    AuthorizationPeriod = new PeriodType
                    {
                        StartDate = new StartDateType { Value = extension.RangoNumeracion.VigenciaDesde },
                        EndDate = new EndDateType { Value = extension.RangoNumeracion.VigenciaHasta }
                    },
                    AuthorizedInvoices = new AuthrorizedInvoices
                    {
                        Prefix = extension.RangoNumeracion.Prefijo,
                        From = extension.RangoNumeracion.Desde,
                        To = extension.RangoNumeracion.Hasta
                    }
                },
                InvoiceSource = new CountryType
                {
                    IdentificationCode = new IdentificationCodeType
                    {
                        listAgencyID = "6",
                        listAgencyName = "United Nations Economic Commission for Europe",
                        listSchemeURI = "urn:oasis:names:specification:ubl:codelist:gc:CountryIdentificationCode-2.1",
                        Value = extension.PaisOrigen.Codigo
                    }
                },
                SoftwareProvider = new SoftwareProvider
                {
                    ProviderID = new coID2Type
                    {
                        schemeAgencyID = "195",
                        schemeAgencyName = "CO, DIAN (Dirección de Impuestos y Aduanas Nacionales)",
                        schemeName = "31",
                        schemeID = NitHelper.CalcularDigitoVerificador(extension.SoftwareProveedorNit).ToString(),
                        Value = extension.SoftwareProveedorNit
                    },
                    SoftwareID = new IdentifierType1
                    {
                        schemeAgencyID = "195",
                        schemeAgencyName = "CO, DIAN (Dirección de Impuestos y Aduanas Nacionales)",
                        Value = extension.SoftwareIdentificador
                    }
                },
                SoftwareSecurityCode = new IdentifierType1
                {
                    schemeAgencyID = "195",
                    schemeAgencyName = "CO, DIAN (Dirección de Impuestos y Aduanas Nacionales)",
                    Value = CryptographyHelper.Sha384(extension.SoftwareIdentificador + extension.SoftwarePin + CreditNote.ID.Value)
                },
                AuthorizationProvider = new AuthorizationProviderType
                {
                    AuthorizationProviderID = new IdentifierType1
                    {
                        schemeAgencyID = "195",
                        schemeAgencyName = "CO, DIAN (Dirección de Impuestos y Aduanas Nacionales)",
                        schemeName = "31",
                        schemeID = NitHelper.CalcularDigitoVerificador("800197268").ToString(), // 4
                        Value = "800197268" // nit dian
                    }
                },
                QRCode = new QRCodeType { Value = "{{QR_CODE_VALUE}}" }
            };

            CreditNote.UBLExtensions[0] = new UBLExtensionType
            {
                ExtensionContent = new ExtensionContentType { Any = dianExtensions.SerializeXmlElement() }
            };

            CreditNote.InvoicePeriod = new PeriodType[]
            {
                new PeriodType
                {
                    StartDate = new StartDateType { Value = extension.RangoNumeracion.VigenciaDesde },
                    StartTime = new StartTimeType { Value = extension.RangoNumeracion.VigenciaDesde.ToString("HH:mm:ss") + "-05:00" },
                    EndDate = new EndDateType { Value = extension.RangoNumeracion.VigenciaHasta },
                    EndTime = new EndTimeType { Value = extension.RangoNumeracion.VigenciaHasta.ToString("HH:mm:ss") + "-05:00" },
                }
            };

            return this;
        }

        public GeneradorNotaCredito ConEmisor(Emisor emisor)
        {
            CreditNote.AccountingSupplierParty = new SupplierPartyType
            {
                AdditionalAccountID = new AdditionalAccountIDType[]
                {
                    new AdditionalAccountIDType
                    {
                        schemeAgencyID = "195",
                        Value = emisor.TipoContribuyente.Codigo
                    }
                },
                Party = new PartyType
                {
                    PartyName = new PartyNameType[]
                    {
                        new PartyNameType
                        {
                            Name = new NameType1 { Value = emisor.Nombre }
                        }
                    },
                    PhysicalLocation = new LocationType1
                    {
                        Address = new AddressType
                        {
                            ID = (emisor.DireccionFisica.Municipio != null) ?
                                new IDType { Value = emisor.DireccionFisica.Municipio.Codigo } : null,
                            CityName = (emisor.DireccionFisica.Municipio != null) ?
                                new CityNameType { Value = emisor.DireccionFisica.Municipio.Nombre } : null,
                            CountrySubentity = (emisor.DireccionFisica.Departamento != null) ?
                                new CountrySubentityType { Value = emisor.DireccionFisica.Departamento.Nombre } : null,
                            CountrySubentityCode = (emisor.DireccionFisica.Departamento != null) ?
                                new CountrySubentityCodeType { Value = emisor.DireccionFisica.Departamento.Codigo } : null,
                            AddressLine = new AddressLineType[]
                            {
                                new AddressLineType { Line =  new LineType{ Value = emisor.DireccionFisica.Linea } }
                            },
                            Country = new CountryType
                            {
                                IdentificationCode = new IdentificationCodeType { Value = emisor.DireccionFisica.Pais.Codigo },
                                Name = new NameType1
                                {
                                    languageID = "es",
                                    Value = emisor.DireccionFisica.Pais.Nombre
                                }
                            }
                        }
                    },
                    PartyTaxScheme = new PartyTaxSchemeType[]
                    {
                        new PartyTaxSchemeType
                        {
                            RegistrationName = new RegistrationNameType { Value = emisor.Nombre },
                            CompanyID = new CompanyIDType
                            {
                                schemeAgencyName = "CO, DIAN (Dirección de Impuestos y Aduanas Nacionales)",
                                schemeAgencyID = "195",
                                schemeName = "31", // nit
                                schemeID = NitHelper.CalcularDigitoVerificador(emisor.Nit).ToString(), // dv
                                Value = emisor.Nit
                            },
                            RegistrationAddress = new AddressType
                            {
                                ID = (emisor.DireccionFisica.Municipio != null) ?
                                    new IDType { Value = emisor.DireccionFisica.Municipio.Codigo } : null,
                                CityName = (emisor.DireccionFisica.Municipio != null) ?
                                    new CityNameType { Value = emisor.DireccionFisica.Municipio.Nombre } : null,
                                CountrySubentity = (emisor.DireccionFisica.Departamento != null) ?
                                    new CountrySubentityType { Value = emisor.DireccionFisica.Departamento.Nombre } : null,
                                CountrySubentityCode = (emisor.DireccionFisica.Departamento != null) ?
                                    new CountrySubentityCodeType { Value = emisor.DireccionFisica.Departamento.Codigo } : null,
                                AddressLine = new AddressLineType[]
                                {
                                    new AddressLineType { Line =  new LineType{ Value = emisor.DireccionFisica.Linea } }
                                },
                                Country = new CountryType
                                {
                                    IdentificationCode = new IdentificationCodeType { Value = emisor.DireccionFisica.Pais.Codigo },
                                    Name = new NameType1
                                    {
                                        languageID = "es",
                                        Value = emisor.DireccionFisica.Pais.Nombre
                                    }
                                }
                            },
                            TaxLevelCode = new TaxLevelCodeType
                            {
                                listName = emisor.RegimenFiscal.Codigo,
                                Value = emisor.ResponsabilidadFiscal.Codigo
                            },
                            TaxScheme = new TaxSchemeType
                            {
                                ID = new IDType { Value = TipoImpuesto.IVA.Codigo },
                                Name = new NameType1 { Value = TipoImpuesto.IVA.Nombre }
                            }
                        }
                    },
                    PartyLegalEntity = new PartyLegalEntityType[]
                    {
                        new PartyLegalEntityType
                        {
                            RegistrationName = new RegistrationNameType { Value = emisor.Nombre },
                            CompanyID = new CompanyIDType
                            {
                                schemeAgencyName = "CO, DIAN (Dirección de Impuestos y Aduanas Nacionales)",
                                schemeAgencyID = "195",
                                schemeName = "31", // nit
                                schemeID = NitHelper.CalcularDigitoVerificador(emisor.Nit).ToString(), // dv
                                Value = emisor.Nit
                            },
                            CorporateRegistrationScheme = new CorporateRegistrationSchemeType
                            {
                                ID = new IDType { Value = emisor.PrefijoRangoNumeracion },
                                Name = new NameType1 { Value = emisor.MatriculaMercantil }
                            }
                        }
                    },
                    Contact = (emisor.NumeroTelefonico == null && emisor.CorreoElectronico == null) ? null : new ContactType
                    {
                        Telephone = new TelephoneType { Value = emisor.NumeroTelefonico },
                        ElectronicMail = new ElectronicMailType { Value = emisor.CorreoElectronico }
                    }
                }
            };

            return this;
        }

        public GeneradorNotaCredito ConAdquiriente(Adquiriente adquiriente)
        {
            CreditNote.AccountingCustomerParty = new CustomerPartyType[]
            {
                new CustomerPartyType
                {
                    AdditionalAccountID = new AdditionalAccountIDType[]
                    {
                        new AdditionalAccountIDType
                        {
                            schemeAgencyID = "195",
                            Value = adquiriente.TipoContribuyente.Codigo
                        }
                    },
                    Party = new PartyType
                    {
                        PartyIdentification = adquiriente.TipoContribuyente.EsPersonaJuridica ? null : new PartyIdentificationType[]
                        {
                            new PartyIdentificationType
                            {
                                ID = new IDType
                                {
                                    schemeAgencyName = "CO, DIAN (Dirección de Impuestos y Aduanas Nacionales)",
                                    schemeAgencyID = "195",
                                    schemeName = adquiriente.TipoIdentificacion.Codigo,
                                    schemeID = NitHelper.CalcularDigitoVerificador(adquiriente.Identificacion).ToString(), // dv
                                    Value = adquiriente.Identificacion
                                }
                            }
                        },
                        PartyName = new PartyNameType[]
                        {
                            new PartyNameType
                            {
                                Name = new NameType1 { Value = adquiriente.Nombre }
                            }
                        },
                        PhysicalLocation = new LocationType1
                        {
                            Address = new AddressType
                            {
                                ID = (adquiriente.DireccionFisica.Municipio != null) ?
                                    new IDType { Value = adquiriente.DireccionFisica.Municipio.Codigo } : null,
                                CityName = (adquiriente.DireccionFisica.Municipio != null) ?
                                    new CityNameType { Value = adquiriente.DireccionFisica.Municipio.Nombre } : null,
                                CountrySubentity = (adquiriente.DireccionFisica.Departamento != null) ?
                                    new CountrySubentityType { Value = adquiriente.DireccionFisica.Departamento.Nombre } : null,
                                CountrySubentityCode = (adquiriente.DireccionFisica.Departamento != null) ?
                                    new CountrySubentityCodeType { Value = adquiriente.DireccionFisica.Departamento.Codigo } : null,
                                AddressLine = new AddressLineType[]
                                {
                                    new AddressLineType { Line =  new LineType{ Value = adquiriente.DireccionFisica.Linea } }
                                },
                                Country = new CountryType
                                {
                                    IdentificationCode = new IdentificationCodeType { Value = adquiriente.DireccionFisica.Pais.Codigo },
                                    Name = new NameType1
                                    {
                                        languageID = "es",
                                        Value = adquiriente.DireccionFisica.Pais.Nombre
                                    }
                                }
                            }
                        },
                        PartyTaxScheme = new PartyTaxSchemeType[]
                        {
                            new PartyTaxSchemeType
                            {
                                RegistrationName = new RegistrationNameType { Value = adquiriente.Nombre },
                                CompanyID = new CompanyIDType
                                {
                                    schemeAgencyName = "CO, DIAN (Dirección de Impuestos y Aduanas Nacionales)",
                                    schemeAgencyID = "195",
                                    schemeName = adquiriente.TipoIdentificacion.Codigo,
                                    schemeID = NitHelper.CalcularDigitoVerificador(adquiriente.Identificacion).ToString(), // dv
                                    Value = adquiriente.Identificacion
                                },
                                TaxLevelCode = new TaxLevelCodeType
                                {
                                    listName = adquiriente.RegimenFiscal.Codigo,
                                    Value = adquiriente.ResponsabilidadFiscal.Codigo
                                },
                                TaxScheme = new TaxSchemeType
                                {
                                    ID = new IDType { Value = TipoImpuesto.IVA.Codigo },
                                    Name = new NameType1 { Value = TipoImpuesto.IVA.Nombre }
                                }
                            }
                        },
                        PartyLegalEntity = new PartyLegalEntityType[]
                        {
                            new PartyLegalEntityType
                            {
                                RegistrationName = new RegistrationNameType { Value = adquiriente.Nombre },
                                CompanyID = new CompanyIDType
                                {
                                    schemeAgencyName = "CO, DIAN (Dirección de Impuestos y Aduanas Nacionales)",
                                    schemeAgencyID = "195",
                                    schemeName = adquiriente.TipoIdentificacion.Codigo,
                                    schemeID = NitHelper.CalcularDigitoVerificador(adquiriente.Identificacion).ToString(), // dv
                                    Value = adquiriente.Identificacion
                                },
                                CorporateRegistrationScheme = new CorporateRegistrationSchemeType
                                {
                                    Name = new NameType1 { Value = adquiriente.MatriculaMercantil }
                                },
                                ShareholderParty = new ShareholderPartyType[]
                                {
                                    new ShareholderPartyType
                                    {
                                        PartecipationPercent = new PartecipationPercentType { Value = 100 }
                                    }
                                }
                            }
                        },
                        Contact = (adquiriente.NumeroTelefonico == null && adquiriente.CorreoElectronico == null) ? null : new ContactType
                        {
                            Telephone = new TelephoneType { Value = adquiriente.NumeroTelefonico },
                            ElectronicMail = new ElectronicMailType { Value = adquiriente.CorreoElectronico }
                        }
                    }
                }
            };


            return this;
        }

        public GeneradorNotaCredito AgregarAdquiriente(Adquiriente adquiriente, decimal porcentajeParticipacion)
        {
            var accountingCustomerList = CreditNote.AccountingCustomerParty?.ToList() ?? new List<CustomerPartyType>();

            bool agregarAccountingCustomer = true;
            for (int x = 0; x < accountingCustomerList.Count; x++)
            {
                if (accountingCustomerList[x].Party.PartyTaxScheme[0].CompanyID.Value == adquiriente.Identificacion)
                {
                    accountingCustomerList[x].Party.PartyLegalEntity[0].ShareholderParty[0].PartecipationPercent.Value = porcentajeParticipacion;
                    agregarAccountingCustomer = false;
                }
            }

            if (agregarAccountingCustomer)
            {
                var customerParty = new CustomerPartyType
                {
                    AdditionalAccountID = new AdditionalAccountIDType[]
                    {
                        new AdditionalAccountIDType
                        {
                            schemeAgencyID = "195",
                            Value = adquiriente.TipoContribuyente.Codigo
                        }
                    },
                    Party = new PartyType
                    {
                        PartyIdentification = adquiriente.TipoContribuyente.EsPersonaJuridica ? null : new PartyIdentificationType[]
                        {
                            new PartyIdentificationType
                            {
                                ID = new IDType
                                {
                                    schemeAgencyName = "CO, DIAN (Dirección de Impuestos y Aduanas Nacionales)",
                                    schemeAgencyID = "195",
                                    schemeName = adquiriente.TipoIdentificacion.Codigo,
                                    schemeID = NitHelper.CalcularDigitoVerificador(adquiriente.Identificacion).ToString(), // dv
                                    Value = adquiriente.Identificacion
                                }
                            }
                        },
                        PartyName = new PartyNameType[]
                        {
                            new PartyNameType
                            {
                                Name = new NameType1 { Value = adquiriente.Nombre }
                            }
                        },
                        PhysicalLocation = new LocationType1
                        {
                            Address = new AddressType
                            {
                                ID = (adquiriente.DireccionFisica.Municipio != null) ?
                                    new IDType { Value = adquiriente.DireccionFisica.Municipio.Codigo } : null,
                                CityName = (adquiriente.DireccionFisica.Municipio != null) ?
                                    new CityNameType { Value = adquiriente.DireccionFisica.Municipio.Nombre } : null,
                                CountrySubentity = (adquiriente.DireccionFisica.Departamento != null) ?
                                    new CountrySubentityType { Value = adquiriente.DireccionFisica.Departamento.Nombre } : null,
                                CountrySubentityCode = (adquiriente.DireccionFisica.Departamento != null) ?
                                    new CountrySubentityCodeType { Value = adquiriente.DireccionFisica.Departamento.Codigo } : null,
                                AddressLine = new AddressLineType[]
                                {
                                    new AddressLineType { Line =  new LineType{ Value = adquiriente.DireccionFisica.Linea } }
                                },
                                Country = new CountryType
                                {
                                    IdentificationCode = new IdentificationCodeType { Value = adquiriente.DireccionFisica.Pais.Codigo },
                                    Name = new NameType1
                                    {
                                        languageID = "es",
                                        Value = adquiriente.DireccionFisica.Pais.Nombre
                                    }
                                }
                            }
                        },
                        PartyTaxScheme = new PartyTaxSchemeType[]
                        {
                            new PartyTaxSchemeType
                            {
                                RegistrationName = new RegistrationNameType { Value = adquiriente.Nombre },
                                CompanyID = new CompanyIDType
                                {
                                    schemeAgencyName = "CO, DIAN (Dirección de Impuestos y Aduanas Nacionales)",
                                    schemeAgencyID = "195",
                                    schemeName = adquiriente.TipoIdentificacion.Codigo,
                                    schemeID = NitHelper.CalcularDigitoVerificador(adquiriente.Identificacion).ToString(), // dv
                                    Value = adquiriente.Identificacion
                                },
                                TaxLevelCode = new TaxLevelCodeType
                                {
                                    listName = adquiriente.RegimenFiscal.Codigo,
                                    Value = adquiriente.ResponsabilidadFiscal.Codigo
                                },
                                TaxScheme = new TaxSchemeType
                                {
                                    ID = new IDType { Value = TipoImpuesto.IVA.Codigo },
                                    Name = new NameType1 { Value = TipoImpuesto.IVA.Nombre }
                                }
                            }
                        },
                        PartyLegalEntity = new PartyLegalEntityType[]
                        {
                            new PartyLegalEntityType
                            {
                                RegistrationName = new RegistrationNameType { Value = adquiriente.Nombre },
                                CompanyID = new CompanyIDType
                                {
                                    schemeAgencyName = "CO, DIAN (Dirección de Impuestos y Aduanas Nacionales)",
                                    schemeAgencyID = "195",
                                    schemeName = adquiriente.TipoIdentificacion.Codigo,
                                    schemeID = NitHelper.CalcularDigitoVerificador(adquiriente.Identificacion).ToString(), // dv
                                    Value = adquiriente.Identificacion
                                },
                                CorporateRegistrationScheme = new CorporateRegistrationSchemeType
                                {
                                    Name = new NameType1 { Value = adquiriente.MatriculaMercantil }
                                },
                                ShareholderParty = new ShareholderPartyType[]
                                {
                                    new ShareholderPartyType
                                    {
                                        PartecipationPercent = new PartecipationPercentType { Value = porcentajeParticipacion }
                                    }
                                }
                            }
                        },
                        Contact = (adquiriente.NumeroTelefonico == null && adquiriente.CorreoElectronico == null) ? null : new ContactType
                        {
                            Telephone = new TelephoneType { Value = adquiriente.NumeroTelefonico },
                            ElectronicMail = new ElectronicMailType { Value = adquiriente.CorreoElectronico }
                        }
                    }
                };

                accountingCustomerList.Add(customerParty);
            }

            CreditNote.AccountingCustomerParty = accountingCustomerList.ToArray();

            return this;
        }

        public GeneradorNotaCredito ConDetallesEntrega(DetallesEntrega entrega)
        {
            CreditNote.Delivery = new DeliveryType[]
            {
                new DeliveryType
                {
                    ActualDeliveryDate = new ActualDeliveryDateType { Value = entrega.Fecha},
                    ActualDeliveryTime = new ActualDeliveryTimeType { Value = entrega.Fecha.ToString("HH:mm:ss") + "-05:00"},
                    DeliveryAddress = new AddressType
                    {
                        ID = (entrega.Direccion.Municipio != null) ?
                            new IDType { Value = entrega.Direccion.Municipio.Codigo } : null,
                        CityName = (entrega.Direccion.Municipio != null) ?
                            new CityNameType { Value = entrega.Direccion.Municipio.Nombre } : null,
                        CountrySubentity = (entrega.Direccion.Departamento != null) ?
                            new CountrySubentityType { Value = entrega.Direccion.Departamento.Nombre } : null,
                        CountrySubentityCode = (entrega.Direccion.Departamento != null) ?
                            new CountrySubentityCodeType { Value = entrega.Direccion.Departamento.Codigo } : null,
                        AddressLine = new AddressLineType[]
                        {
                            new AddressLineType { Line =  new LineType{ Value = entrega.Direccion.Linea } }
                        },
                        Country = new CountryType
                        {
                            IdentificationCode = new IdentificationCodeType { Value = entrega.Direccion.Pais.Codigo },
                            Name = new NameType1
                            {
                                languageID = "es",
                                Value = entrega.Direccion.Pais.Nombre
                            }
                        }
                    },
                    DeliveryParty = (entrega.Transportador == null) ? null : new PartyType
                    {
                        PartyIdentification = entrega.Transportador.TipoContribuyente.EsPersonaJuridica ? null : new PartyIdentificationType[]
                        {
                            new PartyIdentificationType
                            {
                                ID = new IDType
                                {
                                    schemeAgencyName = "CO, DIAN (Dirección de Impuestos y Aduanas Nacionales)",
                                    schemeAgencyID = "195",
                                    schemeName = entrega.Transportador.TipoIdentificacion.Codigo,
                                    schemeID = NitHelper.CalcularDigitoVerificador(entrega.Transportador.Identificacion).ToString(), // dv
                                    Value = entrega.Transportador.Identificacion
                                }
                            }
                        },
                        PartyName = new PartyNameType[]
                        {
                            new PartyNameType
                            {
                                Name = new NameType1 { Value = entrega.Transportador.Nombre }
                            }
                        },
                        PhysicalLocation = new LocationType1
                        {
                            Address = new AddressType
                            {
                                ID = (entrega.Transportador.DireccionFisica.Municipio != null) ?
                                    new IDType { Value = entrega.Transportador.DireccionFisica.Municipio.Codigo } : null,
                                CityName = (entrega.Transportador.DireccionFisica.Municipio != null) ?
                                    new CityNameType { Value = entrega.Transportador.DireccionFisica.Municipio.Nombre } : null,
                                CountrySubentity = (entrega.Transportador.DireccionFisica.Departamento != null) ?
                                    new CountrySubentityType { Value = entrega.Transportador.DireccionFisica.Departamento.Nombre } : null,
                                CountrySubentityCode = (entrega.Transportador.DireccionFisica.Departamento != null) ?
                                    new CountrySubentityCodeType { Value = entrega.Transportador.DireccionFisica.Departamento.Codigo } : null,
                                AddressLine = new AddressLineType[]
                                {
                                    new AddressLineType { Line =  new LineType{ Value = entrega.Transportador.DireccionFisica.Linea } }
                                },
                                Country = new CountryType
                                {
                                    IdentificationCode = new IdentificationCodeType { Value = entrega.Transportador.DireccionFisica.Pais.Codigo },
                                    Name = new NameType1
                                    {
                                        languageID = "es",
                                        Value = entrega.Transportador.DireccionFisica.Pais.Nombre
                                    }
                                }
                            }
                        },
                        PartyTaxScheme = new PartyTaxSchemeType[]
                        {
                            new PartyTaxSchemeType
                            {
                                RegistrationName = new RegistrationNameType { Value = entrega.Transportador.Nombre },
                                CompanyID = new CompanyIDType
                                {
                                    schemeAgencyName = "CO, DIAN (Dirección de Impuestos y Aduanas Nacionales)",
                                    schemeAgencyID = "195",
                                    schemeName = entrega.Transportador.TipoIdentificacion.Codigo,
                                    schemeID = NitHelper.CalcularDigitoVerificador(entrega.Transportador.Identificacion).ToString(), // dv
                                    Value = entrega.Transportador.Identificacion
                                },
                                TaxLevelCode = new TaxLevelCodeType
                                {
                                    listName = "05",
                                    Value = entrega.Transportador.ResponsabilidadFiscal.Codigo
                                },
                                TaxScheme = new TaxSchemeType
                                {
                                    ID = new IDType { Value = TipoImpuesto.IVA.Codigo },
                                    Name = new NameType1 { Value = TipoImpuesto.IVA.Nombre }
                                }
                            }
                        },
                        PartyLegalEntity = new PartyLegalEntityType[]
                        {
                            new PartyLegalEntityType
                            {
                                RegistrationName = new RegistrationNameType { Value = entrega.Transportador.Nombre },
                                CompanyID = new CompanyIDType
                                {
                                    schemeAgencyName = "CO, DIAN (Dirección de Impuestos y Aduanas Nacionales)",
                                    schemeAgencyID = "195",
                                    schemeName = entrega.Transportador.TipoIdentificacion.Codigo,
                                    schemeID = NitHelper.CalcularDigitoVerificador(entrega.Transportador.Identificacion).ToString(), // dv
                                    Value = entrega.Transportador.Identificacion
                                },
                                CorporateRegistrationScheme = new CorporateRegistrationSchemeType
                                {
                                    Name = new NameType1 { Value = entrega.Transportador.MatriculaMercantil }
                                }
                            }
                        },
                        Contact = (entrega.Transportador.NumeroTelefonico == null && entrega.Transportador.CorreoElectronico == null) ? null : new ContactType
                        {
                            Telephone = new TelephoneType { Value = entrega.Transportador.NumeroTelefonico },
                            ElectronicMail = new ElectronicMailType { Value = entrega.Transportador.CorreoElectronico }
                        }
                    }
                }
            };

            return this;
        }

        public GeneradorNotaCredito ConDetallePago(DetallePago pago)
        {
            CreditNote.PaymentMeans = new PaymentMeansType[]
            {
                new PaymentMeansType
                {
                    ID = new IDType { Value = pago.Forma.Codigo },
                    PaymentMeansCode = new PaymentMeansCodeType { Value = pago.Metodo.Codigo },
                    PaymentDueDate = new PaymentDueDateType { Value = pago.Fecha },
                    PaymentID = new PaymentIDType[]
                    {
                        new PaymentIDType { Value = pago.Identificador }
                    }
                }
            };

            return this;
        }

        public GeneradorNotaCredito AgregarDetallePago(DetallePago pago)
        {
            var paymenMeans = (CreditNote.PaymentMeans != null) ? CreditNote.PaymentMeans.ToList() : new List<PaymentMeansType>();

            var payment = new PaymentMeansType
            {
                ID = new IDType { Value = pago.Forma.Codigo },
                PaymentMeansCode = new PaymentMeansCodeType { Value = pago.Metodo.Codigo },
                PaymentDueDate = new PaymentDueDateType { Value = pago.Fecha },
                PaymentID = new PaymentIDType[]
                {
                    new PaymentIDType { Value = pago.Identificador }
                }
            };

            paymenMeans.Add(payment);

            CreditNote.PaymentMeans = paymenMeans.ToArray();

            return this;
        }

        public GeneradorNotaCredito ConTerminosEntrega(TerminosEntrega terminos)
        {
            CreditNote.DeliveryTerms = new DeliveryTermsType[]
            {
                new DeliveryTermsType
                {
                    SpecialTerms = new SpecialTermsType[]
                    {
                        new SpecialTermsType { Value = terminos.TerminosEspeciales }
                    },
                    LossRiskResponsibilityCode = new LossRiskResponsibilityCodeType { Value = terminos.Condicion.Codigo },
                    LossRisk = new LossRiskType[]
                    {
                        new LossRiskType { Value = terminos.Condicion.Descripcion }
                    }
                }
            };

            return this;
        }

        public GeneradorNotaCredito AgregarCargo(Cargo cargo)
        {
            var list = (CreditNote.AllowanceCharge != null) ? CreditNote.AllowanceCharge.ToList() : new List<AllowanceChargeType>();

            var charge = new AllowanceChargeType
            {
                ID = new IDType { Value = (list.Count + 1).ToString() },
                ChargeIndicator = new ChargeIndicatorType { Value = true },
                AllowanceChargeReason = new AllowanceChargeReasonType[]
                {
                    new AllowanceChargeReasonType { Value = cargo.Razon }
                },
                MultiplierFactorNumeric = (cargo.Porcentaje <= 0M && cargo.Base <= 0M) ? null : new MultiplierFactorNumericType { Value = cargo.Porcentaje },
                Amount = new AmountType2
                {
                    currencyID = CreditNote.DocumentCurrencyCode.Value,
                    Value = cargo.Monto
                },
                BaseAmount = (cargo.Base <= 0M) ? null : new BaseAmountType
                {
                    currencyID = CreditNote.DocumentCurrencyCode.Value,
                    Value = cargo.Base
                }
            };

            list.Add(charge);

            CreditNote.AllowanceCharge = list.ToArray();

            return this;
        }

        public GeneradorNotaCredito AgregarDescuento(Descuento descuento)
        {
            var list = (CreditNote.AllowanceCharge != null) ? CreditNote.AllowanceCharge.ToList() : new List<AllowanceChargeType>();

            var allowance = new AllowanceChargeType
            {
                ID = new IDType { Value = (list.Count + 1).ToString() },
                ChargeIndicator = new ChargeIndicatorType { Value = false },
                AllowanceChargeReasonCode = new AllowanceChargeReasonCodeType { Value = descuento.Tipo.Codigo },
                AllowanceChargeReason = new AllowanceChargeReasonType[]
                {
                    new AllowanceChargeReasonType { Value = descuento.Razon }
                },
                MultiplierFactorNumeric = (descuento.Porcentaje <= 0M && descuento.Base <= 0M) ? null : new MultiplierFactorNumericType { Value = descuento.Porcentaje },
                Amount = new AmountType2
                {
                    currencyID = CreditNote.DocumentCurrencyCode.Value,
                    Value = descuento.Monto
                },
                BaseAmount = (descuento.Base <= 0M) ? null : new BaseAmountType
                {
                    currencyID = CreditNote.DocumentCurrencyCode.Value,
                    Value = descuento.Base
                }
            };

            list.Add(allowance);

            CreditNote.AllowanceCharge = list.ToArray();

            return this;
        }

        public GeneradorNotaCredito ConTasaCambio(TasaCambio tasa)
        {
            CreditNote.PaymentExchangeRate = new ExchangeRateType
            {
                SourceCurrencyCode = new SourceCurrencyCodeType { Value = tasa.Origen.Codigo },
                SourceCurrencyBaseRate = new SourceCurrencyBaseRateType { Value = 1.00M },
                TargetCurrencyCode = new TargetCurrencyCodeType { Value = tasa.Destino.Codigo },
                TargetCurrencyBaseRate = new TargetCurrencyBaseRateType { Value = 1.00M },
                CalculationRate = new CalculationRateType { Value = tasa.Cambio },
                Date = new DateType1 { Value = tasa.Fecha }
            };

            return this;
        }

        public GeneradorNotaCredito AgregarResumenImpuesto(ResumenImpuesto resumen)
        {
            bool ValidadorDetalles = false;
            foreach (Impuesto Detalle in resumen.Detalles)
            {
                if (Detalle != null && Detalle.BaseImponible > 0)
                {
                    ValidadorDetalles = true;
                }
            }
            if (ValidadorDetalles)
            {
                if (!resumen.Retenido)
                    AgregarResumenImpuestoNormal(resumen);
            }
            return this;
        }

        protected void AgregarResumenImpuestoNormal(ResumenImpuesto resumen)
        {
            var list = (CreditNote.TaxTotal != null) ? CreditNote.TaxTotal.ToList() : new List<TaxTotalType>();

            var taxTotal = new TaxTotalType
            {
                TaxAmount = new TaxAmountType
                {
                    currencyID = CreditNote.DocumentCurrencyCode.Value,
                    Value = resumen.Importe
                }
            };

            var details = new List<TaxSubtotalType>();

            foreach (var impuesto in resumen.Detalles)
            {
                if (impuesto != null && impuesto.BaseImponible > 0)
                {
                    var taxSubtotal = new TaxSubtotalType
                    {
                        TaxableAmount = (impuesto.BaseImponible <= 0M && impuesto.Importe > 0M) ? null : new TaxableAmountType
                        {
                            currencyID = CreditNote.DocumentCurrencyCode.Value,
                            Value = impuesto.BaseImponible
                        },
                        TaxAmount = new TaxAmountType
                        {
                            currencyID = CreditNote.DocumentCurrencyCode.Value,
                            Value = impuesto.Importe
                        },
                        BaseUnitMeasure = (impuesto.Importe <= 0M || impuesto.Porcentaje > 0M || impuesto.BaseImponible > 0M) ? null : new BaseUnitMeasureType
                        {
                            unitCode = "NIU",
                            Value = 1M
                        },
                        PerUnitAmount = (impuesto.Importe <= 0M || impuesto.Porcentaje > 0M || impuesto.BaseImponible > 0M) ? null : new PerUnitAmountType
                        {
                            currencyID = CreditNote.DocumentCurrencyCode.Value,
                            Value = impuesto.PorUnidad
                        },
                        TaxCategory = new TaxCategoryType
                        {
                            Percent = (impuesto.Porcentaje <= 0M && impuesto.BaseImponible <= 0M && impuesto.Importe > 0M) ? null : new PercentType1 { Value = impuesto.Porcentaje },
                            TaxScheme = new TaxSchemeType
                            {
                                ID = new IDType { Value = resumen.Tipo.Codigo },
                                Name = new NameType1 { Value = resumen.Tipo.Nombre }
                            }
                        }
                    };

                    details.Add(taxSubtotal);
                }
            }

            if (details.Count == 0)
            {
                var taxSubtotal = new TaxSubtotalType
                {
                    TaxableAmount = new TaxableAmountType
                    {
                        currencyID = CreditNote.DocumentCurrencyCode.Value,
                        Value = 0M
                    },
                    TaxAmount = new TaxAmountType
                    {
                        currencyID = CreditNote.DocumentCurrencyCode.Value,
                        Value = 0M
                    },
                    TaxCategory = new TaxCategoryType
                    {
                        Percent = new PercentType1 { Value = 0M },
                        TaxScheme = new TaxSchemeType
                        {
                            ID = new IDType { Value = resumen.Tipo.Codigo },
                            Name = new NameType1 { Value = resumen.Tipo.Nombre }
                        }
                    }
                };

                details.Add(taxSubtotal);
            }

            taxTotal.TaxSubtotal = details.ToArray();

            list.Add(taxTotal);

            CreditNote.TaxTotal = list.ToArray();
        }

        public GeneradorNotaCredito ConTotales(Totales totales)
        {
            CreditNote.LegalMonetaryTotal = new MonetaryTotalType
            {
                LineExtensionAmount = new LineExtensionAmountType
                {
                    currencyID = CreditNote.DocumentCurrencyCode.Value,
                    Value = totales.ValorBruto
                },
                TaxExclusiveAmount = new TaxExclusiveAmountType
                {
                    currencyID = CreditNote.DocumentCurrencyCode.Value,
                    Value = totales.BaseImponible
                },
                TaxInclusiveAmount = new TaxInclusiveAmountType
                {
                    currencyID = CreditNote.DocumentCurrencyCode.Value,
                    Value = totales.ValorBrutoConImpuestos
                },
                AllowanceTotalAmount = (totales.Descuentos <= 0M) ? null : new AllowanceTotalAmountType
                {
                    currencyID = CreditNote.DocumentCurrencyCode.Value,
                    Value = totales.Descuentos
                },
                ChargeTotalAmount = (totales.Cargos <= 0M) ? null : new ChargeTotalAmountType
                {
                    currencyID = CreditNote.DocumentCurrencyCode.Value,
                    Value = totales.Cargos
                },
                PrepaidAmount = new PrepaidAmountType
                {
                    currencyID = CreditNote.DocumentCurrencyCode.Value,
                    Value = totales.Anticipo
                },
                PayableAmount = new PayableAmountType
                {
                    currencyID = CreditNote.DocumentCurrencyCode.Value,
                    Value = totales.TotalPagable
                }
            };

            return this;
        }

        public GeneradorNotaCredito AgregarLinea(Linea linea)
        {
            var list = (CreditNote.CreditNoteLine != null) ? CreditNote.CreditNoteLine.ToList() : new List<CreditNoteLineType>();

            var line = new CreditNoteLineType
            {
                ID = new IDType { Value = (list.Count + 1).ToString() },
                CreditedQuantity = new CreditedQuantityType
                {
                    unitCode = (linea.Impuestos.Count > 0 && linea.Impuestos[0].PorUnidad > 0M) ? "NIU" : "EA",
                    Value = linea.Cantidad
                },
                LineExtensionAmount = new LineExtensionAmountType
                {
                    currencyID = CreditNote.DocumentCurrencyCode.Value,
                    Value = linea.CostoTotal
                },
                FreeOfChargeIndicator = new FreeOfChargeIndicatorType { Value = (linea.CostoTotal <= 0M) },
                PricingReference = (linea.CostoTotal > 0) ? null : new PricingReferenceType
                {
                    AlternativeConditionPrice = new PriceType[]
                    {
                        new PriceType
                        {
                            PriceAmount = new PriceAmountType
                            {
                                currencyID = CreditNote.DocumentCurrencyCode.Value,
                                Value = linea.PrecioUnitarioReal
                            },
                            PriceTypeCode = new PriceTypeCodeType { Value = "03" }
                        }
                    }
                },
                Item = new ItemType
                {
                    Description = new DescriptionType[] { new DescriptionType { Value = linea.Descripcion } },
                    StandardItemIdentification = (linea.CodigoProducto == null) ? null : new ItemIdentificationType
                    {
                        ID = new IDType
                        {
                            schemeID = linea.CodigoProducto.Tipo.Identificador,
                            schemeName = linea.CodigoProducto.Tipo.Nombre,
                            schemeAgencyID = linea.CodigoProducto.Tipo.Codigo,
                            Value = linea.CodigoProducto.Valor
                        }
                    }
                },
                Price = new PriceType
                {
                    PriceAmount = new PriceAmountType
                    {
                        currencyID = CreditNote.DocumentCurrencyCode.Value,
                        Value = linea.PrecioUnitario
                    },
                    BaseQuantity = new BaseQuantityType
                    {
                        unitCode = (linea.Impuestos.Count > 0 && linea.Impuestos[0].PorUnidad > 0M) ? "NIU" : "EA",
                        Value = linea.Cantidad
                    }
                }
            };

            var allowanceChargeList = new List<AllowanceChargeType>();
            if (linea.Cargos.Count > 0)
            {
                foreach (var cargo in linea.Cargos)
                {
                    var charge = new AllowanceChargeType
                    {
                        ID = new IDType { Value = "1" },
                        ChargeIndicator = new ChargeIndicatorType { Value = true },
                        AllowanceChargeReason = new AllowanceChargeReasonType[]
                        {
                            new AllowanceChargeReasonType { Value = cargo.Razon }
                        },
                        MultiplierFactorNumeric = (cargo.Porcentaje <= 0M && cargo.Base <= 0M) ? null : new MultiplierFactorNumericType { Value = cargo.Porcentaje },
                        Amount = new AmountType2
                        {
                            currencyID = CreditNote.DocumentCurrencyCode.Value,
                            Value = cargo.Monto
                        },
                        BaseAmount = (cargo.Base <= 0M) ? null : new BaseAmountType
                        {
                            currencyID = CreditNote.DocumentCurrencyCode.Value,
                            Value = cargo.Base
                        }
                    };

                    allowanceChargeList.Add(charge);
                }
            }
            if (linea.Descuentos.Count > 0)
            {
                foreach (var descuento in linea.Descuentos)
                {
                    var allowance = new AllowanceChargeType
                    {
                        ID = new IDType { Value = "1" },
                        ChargeIndicator = new ChargeIndicatorType { Value = false },
                        AllowanceChargeReasonCode = new AllowanceChargeReasonCodeType { Value = descuento.Tipo.Codigo },
                        AllowanceChargeReason = new AllowanceChargeReasonType[]
                        {
                            new AllowanceChargeReasonType { Value = descuento.Razon }
                        },
                        MultiplierFactorNumeric = (descuento.Porcentaje <= 0M && descuento.Base <= 0M) ? null : new MultiplierFactorNumericType { Value = descuento.Porcentaje },
                        Amount = new AmountType2
                        {
                            currencyID = CreditNote.DocumentCurrencyCode.Value,
                            Value = descuento.Monto
                        },
                        BaseAmount = (descuento.Base <= 0M) ? null : new BaseAmountType
                        {
                            currencyID = CreditNote.DocumentCurrencyCode.Value,
                            Value = descuento.Base
                        }
                    };

                    allowanceChargeList.Add(allowance);
                }
            }
            line.AllowanceCharge = allowanceChargeList.ToArray();

            if (linea.Impuestos.Count > 0)
            {
                var listTaxtTotal = new List<TaxTotalType>();

                foreach (var impuesto in linea.Impuestos)
                {
                    if (impuesto.Retenido == false)
                    {
                        var taxTotal = new TaxTotalType
                        {
                            TaxAmount = new TaxAmountType
                            {
                                currencyID = CreditNote.DocumentCurrencyCode.Value,
                                Value = impuesto.Importe
                            },
                            TaxSubtotal = new TaxSubtotalType[]
                            {
                                new TaxSubtotalType
                                {
                                    TaxableAmount = (impuesto.BaseImponible <= 0M && impuesto.Importe > 0M) ? null : new TaxableAmountType
                                    {
                                        currencyID = CreditNote.DocumentCurrencyCode.Value,
                                        Value = impuesto.BaseImponible
                                    },
                                    TaxAmount = new TaxAmountType
                                    {
                                        currencyID = CreditNote.DocumentCurrencyCode.Value,
                                        Value = impuesto.Importe
                                    },
                                    BaseUnitMeasure = (impuesto.Importe <= 0M || impuesto.Porcentaje > 0M  || impuesto.BaseImponible > 0M) ? null : new BaseUnitMeasureType
                                    {
                                        unitCode = "NIU",
                                        Value = 1M
                                    },
                                    PerUnitAmount = (impuesto.Importe <= 0M || impuesto.Porcentaje > 0M || impuesto.BaseImponible > 0M) ? null : new PerUnitAmountType
                                    {
                                        currencyID = CreditNote.DocumentCurrencyCode.Value,
                                        Value = impuesto.PorUnidad
                                    },
                                    TaxCategory = new TaxCategoryType
                                    {
                                        Percent = (impuesto.Porcentaje <= 0M && impuesto.BaseImponible <= 0M && impuesto.Importe > 0M) ? null : new PercentType1 { Value = impuesto.Porcentaje },
                                        TaxScheme = new TaxSchemeType
                                        {
                                            ID = new IDType { Value = impuesto.Tipo.Codigo },
                                            Name = new NameType1 { Value = impuesto.Tipo.Nombre }
                                        }
                                    }
                                }
                            }
                        };

                        listTaxtTotal.Add(taxTotal);
                    }
                }

                line.TaxTotal = listTaxtTotal.ToArray();
            }

            list.Add(line);

            CreditNote.CreditNoteLine = list.ToArray();

            CreditNote.LineCountNumeric = new LineCountNumericType { Value = list.Count };

            return this;
        }

        public GeneradorNotaCredito AgregarLineas(List<Linea> lineas)
        {
            var list = (CreditNote.CreditNoteLine != null) ? CreditNote.CreditNoteLine.ToList() : new List<CreditNoteLineType>();
            foreach (Linea linea in lineas)
            {
                var line = new CreditNoteLineType
                {
                    ID = new IDType { Value = (list.Count + 1).ToString() },
                    CreditedQuantity = new CreditedQuantityType
                    {
                        unitCode = (linea.Impuestos.Count > 0 && linea.Impuestos[0].PorUnidad > 0M) ? "NIU" : "EA",
                        Value = linea.Cantidad
                    },
                    LineExtensionAmount = new LineExtensionAmountType
                    {
                        currencyID = CreditNote.DocumentCurrencyCode.Value,
                        Value = linea.CostoTotal
                    },
                    FreeOfChargeIndicator = new FreeOfChargeIndicatorType { Value = (linea.CostoTotal <= 0M) },
                    PricingReference = (linea.CostoTotal > 0) ? null : new PricingReferenceType
                    {
                        AlternativeConditionPrice = new PriceType[]
                    {
                        new PriceType
                        {
                            PriceAmount = new PriceAmountType
                            {
                                currencyID = CreditNote.DocumentCurrencyCode.Value,
                                Value = linea.PrecioUnitarioReal
                            },
                            PriceTypeCode = new PriceTypeCodeType { Value = "03" }
                        }
                    }
                    },
                    Item = new ItemType
                    {
                        Description = new DescriptionType[] { new DescriptionType { Value = linea.Descripcion } },
                        StandardItemIdentification = (linea.CodigoProducto == null) ? null : new ItemIdentificationType
                        {
                            ID = new IDType
                            {
                                schemeID = linea.CodigoProducto.Tipo.Identificador,
                                schemeName = linea.CodigoProducto.Tipo.Nombre,
                                schemeAgencyID = linea.CodigoProducto.Tipo.Codigo,
                                Value = linea.CodigoProducto.Valor
                            }
                        }
                    },
                    Price = new PriceType
                    {
                        PriceAmount = new PriceAmountType
                        {
                            currencyID = CreditNote.DocumentCurrencyCode.Value,
                            Value = linea.PrecioUnitario
                        },
                        BaseQuantity = new BaseQuantityType
                        {
                            unitCode = (linea.Impuestos.Count > 0 && linea.Impuestos[0].PorUnidad > 0M) ? "NIU" : "EA",
                            Value = linea.Cantidad
                        }
                    }
                };

                var allowanceChargeList = new List<AllowanceChargeType>();
                if (linea.Cargos.Count > 0)
                {
                    foreach (var cargo in linea.Cargos)
                    {
                        var charge = new AllowanceChargeType
                        {
                            ID = new IDType { Value = "1" },
                            ChargeIndicator = new ChargeIndicatorType { Value = true },
                            AllowanceChargeReason = new AllowanceChargeReasonType[]
                            {
                            new AllowanceChargeReasonType { Value = cargo.Razon }
                            },
                            MultiplierFactorNumeric = (cargo.Porcentaje <= 0M && cargo.Base <= 0M) ? null : new MultiplierFactorNumericType { Value = cargo.Porcentaje },
                            Amount = new AmountType2
                            {
                                currencyID = CreditNote.DocumentCurrencyCode.Value,
                                Value = cargo.Monto
                            },
                            BaseAmount = (cargo.Base <= 0M) ? null : new BaseAmountType
                            {
                                currencyID = CreditNote.DocumentCurrencyCode.Value,
                                Value = cargo.Base
                            }
                        };

                        allowanceChargeList.Add(charge);
                    }
                }
                if (linea.Descuentos.Count > 0)
                {
                    foreach (var descuento in linea.Descuentos)
                    {
                        var allowance = new AllowanceChargeType
                        {
                            ID = new IDType { Value = "1" },
                            ChargeIndicator = new ChargeIndicatorType { Value = false },
                            AllowanceChargeReasonCode = new AllowanceChargeReasonCodeType { Value = descuento.Tipo.Codigo },
                            AllowanceChargeReason = new AllowanceChargeReasonType[]
                            {
                            new AllowanceChargeReasonType { Value = descuento.Razon }
                            },
                            MultiplierFactorNumeric = (descuento.Porcentaje <= 0M && descuento.Base <= 0M) ? null : new MultiplierFactorNumericType { Value = descuento.Porcentaje },
                            Amount = new AmountType2
                            {
                                currencyID = CreditNote.DocumentCurrencyCode.Value,
                                Value = descuento.Monto
                            },
                            BaseAmount = (descuento.Base <= 0M) ? null : new BaseAmountType
                            {
                                currencyID = CreditNote.DocumentCurrencyCode.Value,
                                Value = descuento.Base
                            }
                        };

                        allowanceChargeList.Add(allowance);
                    }
                }
                line.AllowanceCharge = allowanceChargeList.ToArray();

                if (linea.Impuestos.Count > 0)
                {
                    var listTaxtTotal = new List<TaxTotalType>();

                    foreach (var impuesto in linea.Impuestos)
                    {
                        if (impuesto.Retenido == false)
                        {
                            var taxTotal = new TaxTotalType
                            {
                                TaxAmount = new TaxAmountType
                                {
                                    currencyID = CreditNote.DocumentCurrencyCode.Value,
                                    Value = impuesto.Importe
                                },
                                TaxSubtotal = new TaxSubtotalType[]
                                {
                                new TaxSubtotalType
                                {
                                    TaxableAmount = (impuesto.BaseImponible <= 0M && impuesto.Importe > 0M) ? null : new TaxableAmountType
                                    {
                                        currencyID = CreditNote.DocumentCurrencyCode.Value,
                                        Value = impuesto.BaseImponible
                                    },
                                    TaxAmount = new TaxAmountType
                                    {
                                        currencyID = CreditNote.DocumentCurrencyCode.Value,
                                        Value = impuesto.Importe
                                    },
                                    BaseUnitMeasure = (impuesto.Importe <= 0M || impuesto.Porcentaje > 0M  || impuesto.BaseImponible > 0M) ? null : new BaseUnitMeasureType
                                    {
                                        unitCode = "NIU",
                                        Value = 1M
                                    },
                                    PerUnitAmount = (impuesto.Importe <= 0M || impuesto.Porcentaje > 0M || impuesto.BaseImponible > 0M) ? null : new PerUnitAmountType
                                    {
                                        currencyID = CreditNote.DocumentCurrencyCode.Value,
                                        Value = impuesto.PorUnidad
                                    },
                                    TaxCategory = new TaxCategoryType
                                    {
                                        Percent = (impuesto.Porcentaje <= 0M && impuesto.BaseImponible <= 0M && impuesto.Importe > 0M) ? null : new PercentType1 { Value = impuesto.Porcentaje },
                                        TaxScheme = new TaxSchemeType
                                        {
                                            ID = new IDType { Value = impuesto.Tipo.Codigo },
                                            Name = new NameType1 { Value = impuesto.Tipo.Nombre }
                                        }
                                    }
                                }
                                }
                            };

                            listTaxtTotal.Add(taxTotal);
                        }
                    }

                    line.TaxTotal = listTaxtTotal.ToArray();
                }
                list.Add(line);
            }
            CreditNote.CreditNoteLine = list.ToArray();
            CreditNote.LineCountNumeric = new LineCountNumericType { Value = list.Count };
            return this;
        }

        public GeneradorNotaCredito AsignarCUDE(string pinSoftware)
        {
            CreditNote.UUID = new UUIDType
            {
                schemeID = CreditNote.ProfileExecutionID.Value,
                schemeName = AlgoritmoSeguridadUUID.CUDE_SHA384.Codigo,
                Value = CrearCUDE(CreditNote, pinSoftware)
            };

            var qrCodeValue = "https://catalogo-vpfe.dian.gov.co/document/searchqr?documentkey=" + CreditNote.UUID.Value;
            if (CreditNote.ProfileExecutionID.Value == AmbienteDestino.PRUEBAS.Codigo)
                qrCodeValue = "https://catalogo-vpfe-hab.dian.gov.co/document/searchqr?documentkey=" + CreditNote.UUID.Value;

            var ublExtension = CreditNote.UBLExtensions[0] as UBLExtensionType;
            ublExtension.ExtensionContent.Any.InnerXml = ublExtension.ExtensionContent.Any.InnerXml.Replace("{{QR_CODE_VALUE}}", qrCodeValue);

            return this;
        }

        public string GenerarTextoQR()
        {
            var qrCodeValue = "https://catalogo-vpfe.dian.gov.co/document/searchqr?documentkey=" + CreditNote.UUID.Value;
            if (CreditNote.ProfileExecutionID.Value == AmbienteDestino.PRUEBAS.Codigo)
                qrCodeValue = "https://catalogo-vpfe-hab.dian.gov.co/document/searchqr?documentkey=" + CreditNote.UUID.Value;

            return qrCodeValue;
        }

        public CreditNoteType Obtener()
        {
            return CreditNote;
        }


        public static string CrearCUDE(CreditNoteType creditNote, string pinSoftware)
        {
            var NumCre = creditNote.ID.Value;
            var FecCre = creditNote.IssueDate.Value.ToString("yyyy-MM-dd");
            var HorCre = creditNote.IssueTime.Value;
            var ValBru = creditNote.LegalMonetaryTotal.LineExtensionAmount.Value.ToString("0.00").Replace(",", ".");
            var CodImp1 = "01";
            var ValImp1 = 0M;
            if (creditNote.TaxTotal != null)
            {
                foreach (var tax in creditNote.TaxTotal)
                {
                    if (tax.TaxSubtotal[0].TaxCategory.TaxScheme.ID.Value == "01")
                        ValImp1 += tax.TaxAmount.Value;
                }
            }
            var StrValImp1 = ValImp1.ToString("0.00").Replace(",", ".");
            var CodImp2 = "04";
            var ValImp2 = 0M;
            if (creditNote.TaxTotal != null)
            {
                foreach (var tax in creditNote.TaxTotal)
                {
                    if (tax.TaxSubtotal[0].TaxCategory.TaxScheme.ID.Value == "04")
                        ValImp2 += tax.TaxAmount.Value;
                }
            }
            var StrValImp2 = ValImp2.ToString("0.00").Replace(",", ".");
            var CodImp3 = "03";
            var ValImp3 = 0M;
            if (creditNote.TaxTotal != null)
            {
                foreach (var tax in creditNote.TaxTotal)
                {
                    if (tax.TaxSubtotal[0].TaxCategory.TaxScheme.ID.Value == "03")
                        ValImp3 += tax.TaxAmount.Value;
                }
            }
            var StrValImp3 = ValImp3.ToString("0.00").Replace(",", ".");
            var ValTot = creditNote.LegalMonetaryTotal.PayableAmount.Value.ToString("0.00").Replace(",", ".");
            var NitOFE = creditNote.AccountingSupplierParty.Party.PartyTaxScheme[0].CompanyID.Value;
            var NumAdq = creditNote.AccountingCustomerParty[0].Party.PartyTaxScheme[0].CompanyID.Value;
            var tipAmb = creditNote.ProfileExecutionID.Value;

            string cadena = NumCre + FecCre + HorCre + ValBru + CodImp1 + StrValImp1 +
                CodImp2 + StrValImp2 + CodImp3 + StrValImp3 + ValTot + NitOFE + NumAdq + pinSoftware + tipAmb;

            using (var sha384Hash = SHA384.Create())
            {
                var sourceBytes = Encoding.UTF8.GetBytes(cadena);
                var hashBytes = sha384Hash.ComputeHash(sourceBytes);
                var hash = BitConverter.ToString(hashBytes).Replace("-", string.Empty);

                return hash.ToLower();
            }
        }
    }
}