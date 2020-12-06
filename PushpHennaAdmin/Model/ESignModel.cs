using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PushpHennaAdmin.Model
{
    public class CommanClassPDF
    {
        public string ModuleName { get; set; }
        public string CreatedDate { get; set; }
        public string Aadhar_ID { get; set; }
        public string Name { get; set; }
        public string RequestID { get; set; }
        public string departmentname { get; set; }
        public string otp { get; set; }
        public string transactionid { get; set; }
        public string INVOICE { get; set; }
    }
    public class eInvoiceFile
    {
        public string INVOICE { get; set; }
    }
    public class EsignedDoc
    {
        public InputJson InputJson { get; set; }
        public string filetype { get; set; }
        public string transactionid { get; set; }
        public string docname { get; set; }
        public string designation { get; set; }
        public string status { get; set; }
        public string llx { get; set; }
        public string lly { get; set; }
        public string positionX { get; set; }
        public string positionY { get; set; }
        public string mode { get; set; }

    }
    public class InputJson
    {
        public string File { get; set; }

    }
    public class PDFLogFile : CommanClassPDF
    {

        public string PDFwithoutSign { get; set; }
        public string PDFwithSign { get; set; }

    }
    public class DMSError : CommanClassPDF
    {
        public string Request { get; set; }
        public string Response { get; set; }

    }
    public class ReGenratedPDFModel
    {
        public string TableName { get; set; }
        public string RequestId { get; set; }
    }
    public class PDFModel
    {
        public PDFModel()
        {
            GenratePDFModel = new ReGenratedPDFModel();
            PDFLogFileModel = new List<PDFLogFile>();
            DMSErrorModel = new List<DMSError>();
        }
        public ReGenratedPDFModel GenratePDFModel { get; set; }
        public List<PDFLogFile> PDFLogFileModel { get; set; }
        public List<DMSError> DMSErrorModel { get; set; }
    }

    public class ResponseModel
    {
        public int Status { get; set; }
        public string ResponseCode { get; set; }
        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }
        public string Document { get; set; }
        public string TransactionId { get; set; }
    }

}
