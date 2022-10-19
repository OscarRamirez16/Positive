using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

using eFacturacionColombia_V2.Tipos.Standard;
using eFacturacionColombia_V2.Util;

namespace eFacturacionColombia_V2.Documentos
{
    public class GeneradorEvento
    {
        public ApplicationResponseType ApplicationResponse { get; private set; }

        public GeneradorEvento(ApplicationResponseType applicationResponse)
        {
            ApplicationResponse = applicationResponse;
        }

        public GeneradorEvento(AmbienteDestino ambiente, ReferenciaDocumento referencia)
        {
            ApplicationResponse = new ApplicationResponseType();
            ApplicationResponse.UBLVersionID = new UBLVersionIDType { Value = "UBL 2.1" };
            ApplicationResponse.CustomizationID = new CustomizationIDType { Value = "1" };
            ApplicationResponse.ProfileID = new ProfileIDType { Value = "DIAN 2.1" };
            ApplicationResponse.ProfileExecutionID = new ProfileExecutionIDType { Value = ambiente.Codigo };

            ApplicationResponse.UBLExtensions = new UBLExtensionType[]
            {
                new UBLExtensionType { ExtensionContent = new ExtensionContentType { }  },
                new UBLExtensionType { ExtensionContent = new ExtensionContentType { }  }
            };

            ApplicationResponse.ID = new IDType { Value = referencia.Numero + "_" + new Random().Next(10000, 99999) };

            ApplicationResponse.ReceiverParty = new PartyType
            {
                PartyTaxScheme = new PartyTaxSchemeType[]
                {
                    new PartyTaxSchemeType
                    {
                        RegistrationName = new RegistrationNameType { Value = "Unidad Especial Dirección de Impuestos y Aduanas Nacionales" },
                        CompanyID = new CompanyIDType
                        {
                            schemeName = "31",
                            schemeID = NitHelper.CalcularDigitoVerificador("800197268").ToString(), // 4
                            Value = "800197268" // nit dian
                        },
                        TaxScheme = new TaxSchemeType
                        {
                            ID = new IDType { Value = TipoImpuesto.IVA.Codigo },
                            Name = new NameType1 { Value = TipoImpuesto.IVA.Nombre }
                        }
                    }
                }
            };

            ApplicationResponse.DocumentResponse = new DocumentResponseType[]
            {
                new DocumentResponseType
                {
                    DocumentReference = new DocumentReferenceType[]
                    {
                        new DocumentReferenceType
                        {
                            ID = new IDType { Value = referencia.Numero },
                            UUID =  new UUIDType
                            {
                                //schemeID = ambiente.Codigo,
                                schemeName = referencia.AlgoritmoCufe.Codigo,
                                Value = referencia.Cufe
                            },
                            DocumentTypeCode = new DocumentTypeCodeType { Value = referencia.TipoDocumento.Codigo },
                            //IssueDate = new IssueDateType { Value = referencia.Fecha }
                        }
                    }
                }
            };
        }


        public GeneradorEvento ConFecha(DateTime fecha)
        {
            ApplicationResponse.IssueDate = new IssueDateType { Value = fecha };

            ApplicationResponse.IssueTime = new IssueTimeType { Value = fecha.ToString("HH:mm:ss") + "-05:00" };

            return this;
        }

        public GeneradorEvento ConExtensionDian(ExtensionDian extension)
        {
            var dianExtensions = new DianExtensionsType
            {
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
                    Value = CryptographyHelper.Sha384(extension.SoftwareIdentificador + extension.SoftwarePin + ApplicationResponse.ID.Value)
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
                }
            };

            ApplicationResponse.UBLExtensions[0] = new UBLExtensionType
            {
                ExtensionContent = new ExtensionContentType { Any = dianExtensions.SerializeXmlElement() }
            };

            return this;
        }

        public GeneradorEvento ConRegistrante(ParteEvento parte)
        {
            ApplicationResponse.SenderParty = new PartyType
            {
                PartyTaxScheme = new PartyTaxSchemeType[]
                {
                    new PartyTaxSchemeType
                    {
                        RegistrationName = new RegistrationNameType { Value = parte.Nombre },
                        CompanyID = new CompanyIDType
                        {
                            schemeName = parte.TipoIdentificacion.Codigo,
                            schemeID = NitHelper.CalcularDigitoVerificador(parte.Identificacion).ToString(), // dv
                            Value = parte.Identificacion
                        },
                        TaxScheme = new TaxSchemeType
                        {
                            ID = new IDType { Value = TipoImpuesto.IVA.Codigo },
                            Name = new NameType1 { Value = TipoImpuesto.IVA.Nombre }
                        }
                    }
                }
            };

            return this;
        }

        public GeneradorEvento ConReceptor(ParteEvento parte)
        {
            ApplicationResponse.ReceiverParty = new PartyType
            {
                PartyTaxScheme = new PartyTaxSchemeType[]
                {
                    new PartyTaxSchemeType
                    {
                        RegistrationName = new RegistrationNameType { Value = parte.Nombre },
                        CompanyID = new CompanyIDType
                        {
                            schemeName = parte.TipoIdentificacion.Codigo,
                            schemeID = NitHelper.CalcularDigitoVerificador(parte.Identificacion).ToString(), // dv
                            Value = parte.Identificacion
                        },
                        TaxScheme = new TaxSchemeType
                        {
                            ID = new IDType { Value = TipoImpuesto.IVA.Codigo },
                            Name = new NameType1 { Value = TipoImpuesto.IVA.Nombre }
                        }
                    }
                }
            };

            return this;
        }

        public GeneradorEvento ParaAcuseRecibo()
        {
            ApplicationResponse.DocumentResponse[0].Response = new ResponseType
            {
                ResponseCode = new ResponseCodeType { Value = "030" },
                Description = new DescriptionType[]
                {
                    new DescriptionType { Value = "Acuse de Recibo" }
                }
            };

            return this;
        }

        public GeneradorEvento ParaRechazoDocumento(ConceptoEventoRechazo concepto = null)
        {
            concepto = concepto ?? ConceptoEventoRechazo.INCONSISTENCIAS_DOCUMENTO;

            ApplicationResponse.DocumentResponse[0].Response = new ResponseType
            {
                ResponseCode = new ResponseCodeType
                {
                    Value = "031",
                    listID = concepto.Codigo
                },
                Description = new DescriptionType[]
                {
                    new DescriptionType { Value = "Rechazo de Documento" }
                }
            };

            return this;
        }

        public GeneradorEvento ParaRecepcionBienes()
        {
            ApplicationResponse.DocumentResponse[0].Response = new ResponseType
            {
                ResponseCode = new ResponseCodeType { Value = "032" },
                Description = new DescriptionType[]
                {
                    new DescriptionType { Value = "Recepción de Bienes" }
                }
            };

            return this;
        }

        public GeneradorEvento ParaAceptacionDocumento()
        {
            ApplicationResponse.DocumentResponse[0].Response = new ResponseType
            {
                ResponseCode = new ResponseCodeType { Value = "033" },
                Description = new DescriptionType[]
                {
                    new DescriptionType { Value = "Aceptación de Documento" }
                }
            };

            return this;
        }

        public GeneradorEvento ParaEvento(string codigo, string descripcion)
        {
            ApplicationResponse.DocumentResponse[0].Response = new ResponseType
            {
                ResponseCode = new ResponseCodeType { Value = codigo },
                Description = new DescriptionType[]
                {
                    new DescriptionType { Value = descripcion }
                }
            };

            return this;
        }

        public GeneradorEvento AsignarCUDE(string pinSoftware = null)
        {
            ApplicationResponse.UUID = new UUIDType
            {
                schemeID = ApplicationResponse.ProfileExecutionID.Value,
                schemeName = AlgoritmoSeguridadUUID.CUFE_SHA384.Codigo,
                Value = CrearCUDE(ApplicationResponse, pinSoftware)
            };

            return this;
        }

        public ApplicationResponseType Obtener()
        {
            return ApplicationResponse;
        }


        public static string CrearCUDE(ApplicationResponseType applicationResponse, string pinSoftware = null)
        {
            var NumDE = applicationResponse.ID.Value;

            var FecEmi = applicationResponse.IssueDate.Value.ToString("yyyy-MM-dd");

            var HorEmi = applicationResponse.IssueTime.Value;

            var NitFE = applicationResponse.SenderParty.PartyTaxScheme[0].CompanyID.Value;

            var DocAdq = applicationResponse.ReceiverParty.PartyTaxScheme[0].CompanyID.Value;

            var ResCode = applicationResponse.DocumentResponse[0].Response.ResponseCode.Value;

            var ID = applicationResponse.DocumentResponse[0].DocumentReference[0].ID.Value;

            var DocType = applicationResponse.DocumentResponse[0].DocumentReference[0].DocumentTypeCode.Value;

            string cadena = NumDE + FecEmi + HorEmi + NitFE + DocAdq + ResCode + ID + DocType + pinSoftware;

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
