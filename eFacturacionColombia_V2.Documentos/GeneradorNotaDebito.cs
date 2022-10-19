using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

using eFacturacionColombia_V2.Tipos.Standard;
using eFacturacionColombia_V2.Util;

namespace eFacturacionColombia_V2.Documentos
{
    public class GeneradorNotaDebito
    {
        public DebitNoteType DebitNote { get; private set; }

        public GeneradorNotaDebito(DebitNoteType debitNote)
        {
            DebitNote = debitNote;
        }

        public GeneradorNotaDebito(AmbienteDestino ambiente, ConceptoNotaDebito concepto, ReferenciaDocumento referencia)
        {
            DebitNote = new DebitNoteType();
            DebitNote.UBLVersionID = new UBLVersionIDType { Value = "UBL 2.1" };
            DebitNote.CustomizationID = new CustomizationIDType { Value = OperacionNotaDebito.CON_REFERENCIA_FE.Codigo };
            DebitNote.ProfileID = new ProfileIDType { Value = "DIAN 2.1" };
            DebitNote.ProfileExecutionID = new ProfileExecutionIDType { Value = ambiente.Codigo };
            DebitNote.UBLExtensions = new UBLExtensionType[]
            {
                new UBLExtensionType { ExtensionContent = new ExtensionContentType { }  },
                new UBLExtensionType { ExtensionContent = new ExtensionContentType { }  }
            };

            DebitNote.DiscrepancyResponse = new ResponseType[]
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

            DebitNote.BillingReference = new BillingReferenceType[]
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


        public GeneradorNotaDebito ConOperacion(OperacionNotaDebito operacion)
        {
            DebitNote.CustomizationID = new CustomizationIDType { Value = operacion.Codigo };

            return this;
        }

        public GeneradorNotaDebito ConNumero(string numero)
        {
            DebitNote.ID = new IDType { Value = numero };

            return this;
        }

        public GeneradorNotaDebito ConFecha(DateTime fecha)
        {
            DebitNote.IssueDate = new IssueDateType { Value = fecha };

            DebitNote.IssueTime = new IssueTimeType { Value = fecha.ToString("HH:mm:ss") + "-05:00" };

            return this;
        }

        public GeneradorNotaDebito ConNota(string texto)
        {
            DebitNote.Note = new NoteType[] { new NoteType { Value = texto } };

            return this;
        }

        public GeneradorNotaDebito ConMoneda(Moneda moneda)
        {
            DebitNote.DocumentCurrencyCode = new DocumentCurrencyCodeType { Value = moneda.Codigo };

            return this;
        }

        public GeneradorNotaDebito ConPeriodoFacturacion(PeriodoFacturacion periodo)
        {
            DebitNote.InvoicePeriod = new PeriodType[]
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

        public GeneradorNotaDebito ConOrdenCompra(OrdenCompra orden)
        {
            DebitNote.OrderReference = new OrderReferenceType
            {
                ID = new IDType { Value = orden.Numero },
                IssueDate = new IssueDateType { Value = orden.Fecha }
            };

            return this;
        }

        public GeneradorNotaDebito ConExtensionDian(ExtensionDian extension)
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
                    Value = CryptographyHelper.Sha384(extension.SoftwareIdentificador + extension.SoftwarePin + DebitNote.ID.Value)
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

            DebitNote.UBLExtensions[0] = new UBLExtensionType
            {
                ExtensionContent = new ExtensionContentType { Any = dianExtensions.SerializeXmlElement() }
            };

            DebitNote.InvoicePeriod = new PeriodType[]
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

        public GeneradorNotaDebito ConEmisor(Emisor emisor)
        {
            DebitNote.AccountingSupplierParty = new SupplierPartyType
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

        public GeneradorNotaDebito ConAdquiriente(Adquiriente adquiriente)
        {
            DebitNote.AccountingCustomerParty = new CustomerPartyType[]
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

        public GeneradorNotaDebito AgregarAdquiriente(Adquiriente adquiriente, decimal porcentajeParticipacion)
        {
            var accountingCustomerList = DebitNote.AccountingCustomerParty?.ToList() ?? new List<CustomerPartyType>();

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

            DebitNote.AccountingCustomerParty = accountingCustomerList.ToArray();

            return this;
        }

        public GeneradorNotaDebito ConDetallesEntrega(DetallesEntrega entrega)
        {
            DebitNote.Delivery = new DeliveryType[]
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

        public GeneradorNotaDebito ConAnticipo(Anticipo anticipo)
        {
            DebitNote.PrepaidPayment = new PaymentType[]
            {
                new PaymentType
                {
                    ID = new IDType { Value = anticipo.Identificador },
                    PaidAmount = new PaidAmountType
                    {
                        currencyID = DebitNote.DocumentCurrencyCode.Value,
                        Value = anticipo.Valor
                    },
                    PaidDate = new PaidDateType { Value = anticipo.FechaPago },
                    PaidTime = new PaidTimeType { Value = anticipo.FechaPago.ToString("HH:mm:ss") + "-05:00" },
                    ReceivedDate = new ReceivedDateType { Value = anticipo.FechaRecepcionPago },
                    InstructionID = new InstructionIDType { Value = anticipo.Instrucciones }
                }
            };

            return this;
        }

        public GeneradorNotaDebito ConDetallePago(DetallePago pago)
        {
            DebitNote.PaymentMeans = new PaymentMeansType[]
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

        public GeneradorNotaDebito AgregarDetallePago(DetallePago pago)
        {
            var paymenMeans = (DebitNote.PaymentMeans != null) ? DebitNote.PaymentMeans.ToList() : new List<PaymentMeansType>();

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

            DebitNote.PaymentMeans = paymenMeans.ToArray();

            return this;
        }

        public GeneradorNotaDebito ConTerminosEntrega(TerminosEntrega terminos)
        {
            DebitNote.DeliveryTerms = new DeliveryTermsType[]
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

        public GeneradorNotaDebito AgregarCargo(Cargo cargo)
        {
            var list = (DebitNote.AllowanceCharge != null) ? DebitNote.AllowanceCharge.ToList() : new List<AllowanceChargeType>();

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
                    currencyID = DebitNote.DocumentCurrencyCode.Value,
                    Value = cargo.Monto
                },
                BaseAmount = (cargo.Base <= 0M) ? null : new BaseAmountType
                {
                    currencyID = DebitNote.DocumentCurrencyCode.Value,
                    Value = cargo.Base
                }
            };

            list.Add(charge);

            DebitNote.AllowanceCharge = list.ToArray();

            return this;
        }

        public GeneradorNotaDebito AgregarDescuento(Descuento descuento)
        {
            var list = (DebitNote.AllowanceCharge != null) ? DebitNote.AllowanceCharge.ToList() : new List<AllowanceChargeType>();

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
                    currencyID = DebitNote.DocumentCurrencyCode.Value,
                    Value = descuento.Monto
                },
                BaseAmount = (descuento.Base <= 0M) ? null : new BaseAmountType
                {
                    currencyID = DebitNote.DocumentCurrencyCode.Value,
                    Value = descuento.Base
                }
            };

            list.Add(allowance);

            DebitNote.AllowanceCharge = list.ToArray();

            return this;
        }

        public GeneradorNotaDebito ConTasaCambio(TasaCambio tasa)
        {
            DebitNote.PaymentExchangeRate = new ExchangeRateType
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

        public GeneradorNotaDebito AgregarResumenImpuesto(ResumenImpuesto resumen)
        {
            if (!resumen.Retenido)
                AgregarResumenImpuestoNormal(resumen);

            return this;
        }

        protected void AgregarResumenImpuestoNormal(ResumenImpuesto resumen)
        {
            var list = (DebitNote.TaxTotal != null) ? DebitNote.TaxTotal.ToList() : new List<TaxTotalType>();

            var taxTotal = new TaxTotalType
            {
                TaxAmount = new TaxAmountType
                {
                    currencyID = DebitNote.DocumentCurrencyCode.Value,
                    Value = resumen.Importe
                }
            };

            var details = new List<TaxSubtotalType>();

            foreach (var impuesto in resumen.Detalles)
            {
                var taxSubtotal = new TaxSubtotalType
                {
                    TaxableAmount = (impuesto.BaseImponible <= 0M && impuesto.Importe > 0M) ? null : new TaxableAmountType
                    {
                        currencyID = DebitNote.DocumentCurrencyCode.Value,
                        Value = impuesto.BaseImponible
                    },
                    TaxAmount = new TaxAmountType
                    {
                        currencyID = DebitNote.DocumentCurrencyCode.Value,
                        Value = impuesto.Importe
                    },
                    BaseUnitMeasure = (impuesto.Importe <= 0M || impuesto.Porcentaje > 0M || impuesto.BaseImponible > 0M) ? null : new BaseUnitMeasureType
                    {
                        unitCode = "NIU",
                        Value = 1M
                    },
                    PerUnitAmount = (impuesto.Importe <= 0M || impuesto.Porcentaje > 0M || impuesto.BaseImponible > 0M) ? null : new PerUnitAmountType
                    {
                        currencyID = DebitNote.DocumentCurrencyCode.Value,
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

            if (details.Count == 0)
            {
                var taxSubtotal = new TaxSubtotalType
                {
                    TaxableAmount = new TaxableAmountType
                    {
                        currencyID = DebitNote.DocumentCurrencyCode.Value,
                        Value = 0M
                    },
                    TaxAmount = new TaxAmountType
                    {
                        currencyID = DebitNote.DocumentCurrencyCode.Value,
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

            DebitNote.TaxTotal = list.ToArray();
        }

        public GeneradorNotaDebito ConTotales(Totales totales)
        {
            DebitNote.RequestedMonetaryTotal = new MonetaryTotalType
            {
                LineExtensionAmount = new LineExtensionAmountType
                {
                    currencyID = DebitNote.DocumentCurrencyCode.Value,
                    Value = totales.ValorBruto
                },
                TaxExclusiveAmount = new TaxExclusiveAmountType
                {
                    currencyID = DebitNote.DocumentCurrencyCode.Value,
                    Value = totales.BaseImponible
                },
                TaxInclusiveAmount = new TaxInclusiveAmountType
                {
                    currencyID = DebitNote.DocumentCurrencyCode.Value,
                    Value = totales.ValorBrutoConImpuestos
                },
                AllowanceTotalAmount = (totales.Descuentos <= 0M) ? null : new AllowanceTotalAmountType
                {
                    currencyID = DebitNote.DocumentCurrencyCode.Value,
                    Value = totales.Descuentos
                },
                ChargeTotalAmount = (totales.Cargos <= 0M) ? null : new ChargeTotalAmountType
                {
                    currencyID = DebitNote.DocumentCurrencyCode.Value,
                    Value = totales.Cargos
                },
                PrepaidAmount = new PrepaidAmountType
                {
                    currencyID = DebitNote.DocumentCurrencyCode.Value,
                    Value = totales.Anticipo
                },
                PayableAmount = new PayableAmountType
                {
                    currencyID = DebitNote.DocumentCurrencyCode.Value,
                    Value = totales.TotalPagable
                }
            };

            return this;
        }

        public GeneradorNotaDebito AgregarLinea(Linea linea)
        {
            var list = (DebitNote.DebitNoteLine != null) ? DebitNote.DebitNoteLine.ToList() : new List<DebitNoteLineType>();

            var line = new DebitNoteLineType
            {
                ID = new IDType { Value = (list.Count + 1).ToString() },
                DebitedQuantity = new DebitedQuantityType
                {
                    unitCode = (linea.Impuestos.Count > 0 && linea.Impuestos[0].PorUnidad > 0M) ? "NIU" : "EA",
                    Value = linea.Cantidad
                },
                LineExtensionAmount = new LineExtensionAmountType
                {
                    currencyID = DebitNote.DocumentCurrencyCode.Value,
                    Value = linea.CostoTotal
                },
                PricingReference = (linea.CostoTotal > 0) ? null : new PricingReferenceType
                {
                    AlternativeConditionPrice = new PriceType[]
                    {
                        new PriceType
                        {
                            PriceAmount = new PriceAmountType
                            {
                                currencyID = DebitNote.DocumentCurrencyCode.Value,
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
                        currencyID = DebitNote.DocumentCurrencyCode.Value,
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
                            currencyID = DebitNote.DocumentCurrencyCode.Value,
                            Value = cargo.Monto
                        },
                        BaseAmount = (cargo.Base <= 0M) ? null : new BaseAmountType
                        {
                            currencyID = DebitNote.DocumentCurrencyCode.Value,
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
                            currencyID = DebitNote.DocumentCurrencyCode.Value,
                            Value = descuento.Monto
                        },
                        BaseAmount = (descuento.Base <= 0M) ? null : new BaseAmountType
                        {
                            currencyID = DebitNote.DocumentCurrencyCode.Value,
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
                                currencyID = DebitNote.DocumentCurrencyCode.Value,
                                Value = impuesto.Importe
                            },
                            TaxSubtotal = new TaxSubtotalType[]
                            {
                                new TaxSubtotalType
                                {
                                    TaxableAmount = (impuesto.BaseImponible <= 0M && impuesto.Importe > 0M) ? null : new TaxableAmountType
                                    {
                                        currencyID = DebitNote.DocumentCurrencyCode.Value,
                                        Value = impuesto.BaseImponible
                                    },
                                    TaxAmount = new TaxAmountType
                                    {
                                        currencyID = DebitNote.DocumentCurrencyCode.Value,
                                        Value = impuesto.Importe
                                    },
                                    BaseUnitMeasure = (impuesto.Importe <= 0M || impuesto.Porcentaje > 0M  || impuesto.BaseImponible > 0M) ? null : new BaseUnitMeasureType
                                    {
                                        unitCode = "NIU",
                                        Value = 1M
                                    },
                                    PerUnitAmount = (impuesto.Importe <= 0M || impuesto.Porcentaje > 0M || impuesto.BaseImponible > 0M) ? null : new PerUnitAmountType
                                    {
                                        currencyID = DebitNote.DocumentCurrencyCode.Value,
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

            DebitNote.DebitNoteLine = list.ToArray();

            DebitNote.LineCountNumeric = new LineCountNumericType { Value = list.Count };

            return this;
        }

        public GeneradorNotaDebito AsignarCUDE(string pinSoftware)
        {
            DebitNote.UUID = new UUIDType
            {
                schemeID = DebitNote.ProfileExecutionID.Value,
                schemeName = AlgoritmoSeguridadUUID.CUDE_SHA384.Codigo,
                Value = CrearCUDE(DebitNote, pinSoftware)
            };

            var qrCodeValue = "https://catalogo-vpfe.dian.gov.co/document/searchqr?documentkey=" + DebitNote.UUID.Value;
            if (DebitNote.ProfileExecutionID.Value == AmbienteDestino.PRUEBAS.Codigo)
                qrCodeValue = "https://catalogo-vpfe-hab.dian.gov.co/document/searchqr?documentkey=" + DebitNote.UUID.Value;

            var ublExtension = DebitNote.UBLExtensions[0] as UBLExtensionType;
            ublExtension.ExtensionContent.Any.InnerXml = ublExtension.ExtensionContent.Any.InnerXml.Replace("{{QR_CODE_VALUE}}", qrCodeValue);

            return this;
        }

        public string GenerarTextoQR()
        {
            var qrCodeValue = "https://catalogo-vpfe.dian.gov.co/document/searchqr?documentkey=" + DebitNote.UUID.Value;
            if (DebitNote.ProfileExecutionID.Value == AmbienteDestino.PRUEBAS.Codigo)
                qrCodeValue = "https://catalogo-vpfe-hab.dian.gov.co/document/searchqr?documentkey=" + DebitNote.UUID.Value;

            return qrCodeValue;
        }

        public DebitNoteType Obtener()
        {
            return DebitNote;
        }


        public static string CrearCUDE(DebitNoteType debitNote, string pinSoftware)
        {
            var NumDeb = debitNote.ID.Value;
            var FecDeb = debitNote.IssueDate.Value.ToString("yyyy-MM-dd");
            var HorDeb = debitNote.IssueTime.Value;
            var ValBru = debitNote.RequestedMonetaryTotal.LineExtensionAmount.Value.ToString("0.00").Replace(",", ".");
            var CodImp1 = "01";
            var ValImp1 = 0M;
            if (debitNote.TaxTotal != null)
            {
                foreach (var tax in debitNote.TaxTotal)
                {
                    if (tax.TaxSubtotal[0].TaxCategory.TaxScheme.ID.Value == "01")
                        ValImp1 += tax.TaxAmount.Value;
                }
            }
            var StrValImp1 = ValImp1.ToString("0.00").Replace(",", ".");
            var CodImp2 = "04";
            var ValImp2 = 0M;
            if (debitNote.TaxTotal != null)
            {
                foreach (var tax in debitNote.TaxTotal)
                {
                    if (tax.TaxSubtotal[0].TaxCategory.TaxScheme.ID.Value == "04")
                        ValImp2 += tax.TaxAmount.Value;
                }
            }
            var StrValImp2 = ValImp2.ToString("0.00").Replace(",", ".");
            var CodImp3 = "03";
            var ValImp3 = 0M;
            if (debitNote.TaxTotal != null)
            {
                foreach (var tax in debitNote.TaxTotal)
                {
                    if (tax.TaxSubtotal[0].TaxCategory.TaxScheme.ID.Value == "03")
                        ValImp3 += tax.TaxAmount.Value;
                }
            }
            var StrValImp3 = ValImp3.ToString("0.00").Replace(",", ".");
            var ValTot = debitNote.RequestedMonetaryTotal.PayableAmount.Value.ToString("0.00").Replace(",", ".");
            var NitOFE = debitNote.AccountingSupplierParty.Party.PartyTaxScheme[0].CompanyID.Value;
            var NumAdq = debitNote.AccountingCustomerParty[0].Party.PartyTaxScheme[0].CompanyID.Value;
            var tipAmb = debitNote.ProfileExecutionID.Value;

            string cadena = NumDeb + FecDeb + HorDeb + ValBru + CodImp1 + StrValImp1 +
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
