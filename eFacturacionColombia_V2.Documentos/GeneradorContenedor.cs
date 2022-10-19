using System;
using eFacturacionColombia_V2.Tipos.Standard;
using eFacturacionColombia_V2.Util;

namespace eFacturacionColombia_V2.Documentos
{
    public class GeneradorContenedor
    {
        public AttachedDocumentType AttachedDocument { get; private set; }

        public GeneradorContenedor(AmbienteDestino ambiente)
        {
            AttachedDocument = new AttachedDocumentType
            {
                UBLVersionID = new UBLVersionIDType { Value = "UBL 2.1" },
                CustomizationID = new CustomizationIDType { Value = "Documentos adjuntos" },
                ProfileID = new ProfileIDType { Value = "DIAN 2.1" },
                ProfileExecutionID = new ProfileExecutionIDType { Value = ambiente.Codigo },
                ID = new IDType { Value = DateTime.Now.ToFileTimeUtc().ToString() },
                IssueDate = new IssueDateType { Value = DateTime.Now },
                IssueTime = new IssueTimeType { Value = DateTime.Now.ToString("HH:mm:ss") + "-05:00" },
                DocumentType = new DocumentTypeType { Value = "Contenedor de Factura Electrónica" }
            };
        }


        public GeneradorContenedor ConRespuesta(ApplicationResponseType ar)
        {
            var xmlApplicationResponse = ar.SerializeXmlDocument().ToXmlString();

            AttachedDocument.ParentDocumentLineReference = new LineReferenceType[]
            {
                new LineReferenceType
                {
                    LineID = new LineIDType { Value = "1"},
                    DocumentReference = new DocumentReferenceType
                    {
                        ID = new IDType { Value = ar.DocumentResponse[0].DocumentReference[0].ID.Value },
                        UUID = new UUIDType
                        {
                            schemeName = ar.DocumentResponse[0].DocumentReference[0].UUID.schemeName,
                            Value = ar.DocumentResponse[0].DocumentReference[0].UUID.Value
                        },
                        IssueDate = new IssueDateType { Value = ar.IssueDate.Value },
                        DocumentType = new DocumentTypeType { Value = "ApplicationResponse"},
                        Attachment = new AttachmentType
                        {
                            ExternalReference = new ExternalReferenceType
                            {
                                MimeCode = new MimeCodeType { Value = "text/xml" },
                                EncodingCode = new EncodingCodeType { Value = "UTF-8"},
                                Description = new DescriptionType[]
                                {
                                    new DescriptionType { Value = "<![CDATA[" + xmlApplicationResponse + "]]>" }
                                }
                            }
                        },
                        ResultOfVerification = new ResultOfVerificationType
                        {
                            ValidatorID = new ValidatorIDType { Value = "Unidad Especial Dirección de Impuestos y Aduanas Nacionales"},
                            ValidationResultCode = new ValidationResultCodeType { Value = ar.DocumentResponse[0].Response.ResponseCode.Value },
                            ValidationDate = new ValidationDateType { Value = ar.IssueDate.Value },
                            ValidationTime = new ValidationTimeType { Value = ar.IssueTime.Value }
                        }
                    }
                }
            };

            return this;
        }

        public GeneradorContenedor ConDocumento(InvoiceType invoice)
        {
            AttachedDocument.ParentDocumentID = new ParentDocumentIDType { Value = invoice.ID.Value };

            AttachedDocument.SenderParty = new PartyType
            {
                PartyTaxScheme = new PartyTaxSchemeType[]
                {
                    new PartyTaxSchemeType
                    {
                        RegistrationName = new RegistrationNameType { Value = invoice.AccountingSupplierParty.Party.PartyName[0].Name.Value },
                        CompanyID = new CompanyIDType
                        {
                            schemeAgencyID = "195",
                            schemeID = invoice.AccountingSupplierParty.Party.PartyTaxScheme[0].CompanyID.schemeID,
                            schemeName = invoice.AccountingSupplierParty.Party.PartyTaxScheme[0].CompanyID.schemeName,
                            Value = invoice.AccountingSupplierParty.Party.PartyTaxScheme[0].CompanyID.Value
                        },
                        TaxLevelCode = new TaxLevelCodeType
                        {
                            listName = invoice.AccountingSupplierParty.Party.PartyTaxScheme[0].TaxLevelCode.listName,
                            Value = invoice.AccountingSupplierParty.Party.PartyTaxScheme[0].TaxLevelCode.Value
                        },
                        TaxScheme = new TaxSchemeType
                        {
                            ID = new IDType { Value = invoice.AccountingSupplierParty.Party.PartyTaxScheme[0].TaxScheme.ID.Value },
                            Name = new NameType1 { Value = invoice.AccountingSupplierParty.Party.PartyTaxScheme[0].TaxScheme.Name.Value }
                        }
                    }
                }
            };

            AttachedDocument.ReceiverParty = new PartyType
            {
                PartyTaxScheme = new PartyTaxSchemeType[]
                {
                    new PartyTaxSchemeType
                    {
                        RegistrationName = new RegistrationNameType { Value = invoice.AccountingCustomerParty[0].Party.PartyName[0].Name.Value },
                        CompanyID = new CompanyIDType
                        {
                            schemeAgencyID = "195",
                            schemeID = invoice.AccountingCustomerParty[0].Party.PartyTaxScheme[0].CompanyID.schemeID,
                            schemeName = invoice.AccountingCustomerParty[0].Party.PartyTaxScheme[0].CompanyID.schemeName,
                            Value = invoice.AccountingCustomerParty[0].Party.PartyTaxScheme[0].CompanyID.Value
                        },
                        TaxLevelCode = new TaxLevelCodeType
                        {
                            listName = invoice.AccountingCustomerParty[0].Party.PartyTaxScheme[0].TaxLevelCode.listName,
                            Value = invoice.AccountingCustomerParty[0].Party.PartyTaxScheme[0].TaxLevelCode.Value
                        },
                        TaxScheme = new TaxSchemeType
                        {
                            ID = new IDType { Value = invoice.AccountingCustomerParty[0].Party.PartyTaxScheme[0].TaxScheme.ID.Value },
                            Name = new NameType1 { Value = invoice.AccountingCustomerParty[0].Party.PartyTaxScheme[0].TaxScheme.Name.Value }
                        }
                    }
                }
            };

            AttachedDocument.Attachment = new AttachmentType
            {
                ExternalReference = new ExternalReferenceType
                {
                    MimeCode = new MimeCodeType { Value = "text/xml" },
                    EncodingCode = new EncodingCodeType { Value = "UTF-8" },
                    Description = new DescriptionType[]
                    {
                        new DescriptionType { Value = "<![CDATA[" + invoice.SerializeXmlDocument().ToXmlString() + "]]>" }
                    }
                }
            };

            return this;
        }

        public GeneradorContenedor ConDocumento(CreditNoteType creditNote)
        {
            AttachedDocument.ParentDocumentID = new ParentDocumentIDType { Value = creditNote.ID.Value };

            AttachedDocument.SenderParty = new PartyType
            {
                PartyTaxScheme = new PartyTaxSchemeType[]
                {
                    new PartyTaxSchemeType
                    {
                        RegistrationName = new RegistrationNameType { Value = creditNote.AccountingSupplierParty.Party.PartyName[0].Name.Value },
                        CompanyID = new CompanyIDType
                        {
                            schemeAgencyID = "195",
                            schemeID = creditNote.AccountingSupplierParty.Party.PartyTaxScheme[0].CompanyID.schemeID,
                            schemeName = creditNote.AccountingSupplierParty.Party.PartyTaxScheme[0].CompanyID.schemeName,
                            Value = creditNote.AccountingSupplierParty.Party.PartyTaxScheme[0].CompanyID.Value
                        },
                        TaxLevelCode = new TaxLevelCodeType
                        {
                            listName = creditNote.AccountingSupplierParty.Party.PartyTaxScheme[0].TaxLevelCode.listName,
                            Value = creditNote.AccountingSupplierParty.Party.PartyTaxScheme[0].TaxLevelCode.Value
                        },
                        TaxScheme = new TaxSchemeType
                        {
                            ID = new IDType { Value = creditNote.AccountingSupplierParty.Party.PartyTaxScheme[0].TaxScheme.ID.Value },
                            Name = new NameType1 { Value = creditNote.AccountingSupplierParty.Party.PartyTaxScheme[0].TaxScheme.Name.Value }
                        }
                    }
                }
            };

            AttachedDocument.ReceiverParty = new PartyType
            {
                PartyTaxScheme = new PartyTaxSchemeType[]
                {
                    new PartyTaxSchemeType
                    {
                        RegistrationName = new RegistrationNameType { Value = creditNote.AccountingCustomerParty[0].Party.PartyName[0].Name.Value },
                        CompanyID = new CompanyIDType
                        {
                            schemeAgencyID = "195",
                            schemeID = creditNote.AccountingCustomerParty[0].Party.PartyTaxScheme[0].CompanyID.schemeID,
                            schemeName = creditNote.AccountingCustomerParty[0].Party.PartyTaxScheme[0].CompanyID.schemeName,
                            Value = creditNote.AccountingCustomerParty[0].Party.PartyTaxScheme[0].CompanyID.Value
                        },
                        TaxLevelCode = new TaxLevelCodeType
                        {
                            listName = creditNote.AccountingCustomerParty[0].Party.PartyTaxScheme[0].TaxLevelCode.listName,
                            Value = creditNote.AccountingCustomerParty[0].Party.PartyTaxScheme[0].TaxLevelCode.Value
                        },
                        TaxScheme = new TaxSchemeType
                        {
                            ID = new IDType { Value = creditNote.AccountingCustomerParty[0].Party.PartyTaxScheme[0].TaxScheme.ID.Value },
                            Name = new NameType1 { Value = creditNote.AccountingCustomerParty[0].Party.PartyTaxScheme[0].TaxScheme.Name.Value }
                        }
                    }
                }
            };

            AttachedDocument.Attachment = new AttachmentType
            {
                ExternalReference = new ExternalReferenceType
                {
                    MimeCode = new MimeCodeType { Value = "text/xml" },
                    EncodingCode = new EncodingCodeType { Value = "UTF-8" },
                    Description = new DescriptionType[]
                    {
                        new DescriptionType { Value = creditNote.SerializeXmlDocument().ToXmlString() }
                    }
                }
            };

            return this;
        }

        public GeneradorContenedor ConDocumento(DebitNoteType debitNote)
        {
            AttachedDocument.ParentDocumentID = new ParentDocumentIDType { Value = debitNote.ID.Value };

            AttachedDocument.SenderParty = new PartyType
            {
                PartyTaxScheme = new PartyTaxSchemeType[]
                {
                    new PartyTaxSchemeType
                    {
                        RegistrationName = new RegistrationNameType { Value = debitNote.AccountingSupplierParty.Party.PartyName[0].Name.Value },
                        CompanyID = new CompanyIDType
                        {
                            schemeAgencyID = "195",
                            schemeID = debitNote.AccountingSupplierParty.Party.PartyTaxScheme[0].CompanyID.schemeID,
                            schemeName = debitNote.AccountingSupplierParty.Party.PartyTaxScheme[0].CompanyID.schemeName,
                            Value = debitNote.AccountingSupplierParty.Party.PartyTaxScheme[0].CompanyID.Value
                        },
                        TaxLevelCode = new TaxLevelCodeType
                        {
                            listName = debitNote.AccountingSupplierParty.Party.PartyTaxScheme[0].TaxLevelCode.listName,
                            Value = debitNote.AccountingSupplierParty.Party.PartyTaxScheme[0].TaxLevelCode.Value
                        },
                        TaxScheme = new TaxSchemeType
                        {
                            ID = new IDType { Value = debitNote.AccountingSupplierParty.Party.PartyTaxScheme[0].TaxScheme.ID.Value },
                            Name = new NameType1 { Value = debitNote.AccountingSupplierParty.Party.PartyTaxScheme[0].TaxScheme.Name.Value }
                        }
                    }
                }
            };

            AttachedDocument.ReceiverParty = new PartyType
            {
                PartyTaxScheme = new PartyTaxSchemeType[]
                {
                    new PartyTaxSchemeType
                    {
                        RegistrationName = new RegistrationNameType { Value = debitNote.AccountingCustomerParty[0].Party.PartyName[0].Name.Value },
                        CompanyID = new CompanyIDType
                        {
                            schemeAgencyID = "195",
                            schemeID = debitNote.AccountingCustomerParty[0].Party.PartyTaxScheme[0].CompanyID.schemeID,
                            schemeName = debitNote.AccountingCustomerParty[0].Party.PartyTaxScheme[0].CompanyID.schemeName,
                            Value = debitNote.AccountingCustomerParty[0].Party.PartyTaxScheme[0].CompanyID.Value
                        },
                        TaxLevelCode = new TaxLevelCodeType
                        {
                            listName = debitNote.AccountingCustomerParty[0].Party.PartyTaxScheme[0].TaxLevelCode.listName,
                            Value = debitNote.AccountingCustomerParty[0].Party.PartyTaxScheme[0].TaxLevelCode.Value
                        },
                        TaxScheme = new TaxSchemeType
                        {
                            ID = new IDType { Value = debitNote.AccountingCustomerParty[0].Party.PartyTaxScheme[0].TaxScheme.ID.Value },
                            Name = new NameType1 { Value = debitNote.AccountingCustomerParty[0].Party.PartyTaxScheme[0].TaxScheme.Name.Value }
                        }
                    }
                }
            };

            AttachedDocument.Attachment = new AttachmentType
            {
                ExternalReference = new ExternalReferenceType
                {
                    MimeCode = new MimeCodeType { Value = "text/xml" },
                    EncodingCode = new EncodingCodeType { Value = "UTF-8" },
                    Description = new DescriptionType[]
                    {
                        new DescriptionType { Value = debitNote.SerializeXmlDocument().ToXmlString() }
                    }
                }
            };

            return this;
        }

        public AttachedDocumentType Obtener()
        {
            return AttachedDocument;
        }
    }
}