using DinkToPdf;
using DinkToPdf.Contracts;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

using PushpHennaAdmin.Model;
using PushpHennaAdmin.DataBase;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Text;
//using SautinSoft;
namespace PushpHennaAdmin.Controllers
{
    [Route("api/ESign")]
    public class ESignController : Controller
    {
        private IConverter _converter;
        DAL objDAL = DAL.GetObject(); //new DAL();
        private IConfiguration _config;
        string connection = string.Empty;

        public ESignController(IConfiguration config, IConverter converter)
        {
            _config = config;
            connection = _config.GetSection("AppSettings").GetSection("ConnectionString").Value;
            _converter = converter;
        }

        [HttpPost("[action]")]
        public string EsignTest()
        {
            CommanClassPDF objCommanClassPDF = new CommanClassPDF();
            objCommanClassPDF.Aadhar_ID = "688506502454";
            objCommanClassPDF.departmentname = "test";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(_config.GetSection("ESign_SendOTP").Value + _config.GetSection("ESign_ClientId").Value);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            //httpWebRequest.Accept = "application/json; charset=utf-8";
            try
            {
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string objVM = JsonConvert.SerializeObject(new
                    {
                        aadharid = objCommanClassPDF.Aadhar_ID,
                        departmentname = objCommanClassPDF.departmentname,                        
                    });

                    streamWriter.Write(objVM);
                    streamWriter.Flush();
                    streamWriter.Close();

                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        ResponseModel objj = new ResponseModel();
                       // var ser = JsonConvert.SerializeObject(typeof(ResponseModel));
                        objj = JsonConvert.DeserializeObject<ResponseModel>(result);

                        if (objj.Status == 1)
                        {
                            return "1";
                        }
                        else
                            return "0";
                        // pass.Text = result.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }


        }

        [HttpPost("[action]")]
        public string EsignOTPVerifyAndPdfSign([FromBody] CommanClassPDF objCommanClassPDF)
        {

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(_config.GetSection("ESign_authOTP").Value + _config.GetSection("ESign_ClientId").Value);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            //httpWebRequest.Accept = "application/json; charset=utf-8";
            try
            {
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string objVM = JsonConvert.SerializeObject(new
                    {
                        otp = objCommanClassPDF.otp,
                        transactionid = objCommanClassPDF.transactionid,
                    });

                    streamWriter.Write(objVM);
                    streamWriter.Flush();
                    streamWriter.Close();

                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        ResponseModel objj = new ResponseModel();
                        objj = JsonConvert.DeserializeObject<ResponseModel>(result);

                        if (objj.Status == 1)
                        {
                            eInvoiceFile objInvoiceFile = new eInvoiceFile();
                            objInvoiceFile = InvoiceDetailByInvoiceNo("101065", "test.pdf");

                            string PdfPath = _config.GetSection("AppSettings").GetSection("InvoicePath").Value + "\\Test.pdf";
                            #region ConvertHTML to PDF Format
                            ConvertHtmlToPDF(objInvoiceFile.INVOICE, PdfPath);
                            #endregion
                            #region Convert Pdf To Base64
                            PdfReader reader = new PdfReader(PdfPath);
                            string text = string.Empty;
                            for (int page = 1; page <= reader.NumberOfPages; page++)
                            {
                                text += PdfTextExtractor.GetTextFromPage(reader, page);
                            }
                            byte[] pdfBytes = System.IO.File.ReadAllBytes(PdfPath);
                            string pdfBase64 = Convert.ToBase64String(pdfBytes);
                            reader.Close();
                            #endregion

                            return EsignPdf(pdfBase64, objCommanClassPDF.transactionid, PdfPath);// objj.TransactionId, PdfPath);
                                                     
                        }
                        else
                            return "0";

                    }
                }
            }
            catch (Exception ex)
            {
                //return ex.Message;
                if (ex.Message.Contains("404"))
                { 
                return EsignOTPVerifyAndPdfSign(objCommanClassPDF);
                }
                else
                    return "0";
            }


        }

        public void ConvertHtmlToPDF(string htmlcontent, string pdfFilePath)
        {
            try
            {

                var globalSettings = new GlobalSettings
                {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4,
                    Margins = new MarginSettings { Top = 10 },
                    DocumentTitle = "PDF Report",
                    Out = pdfFilePath //@"D:\PDFCreator\Employee_Report.pdf"
                };
                var objectSettings = new ObjectSettings
                {
                    PagesCount = true,
                    HtmlContent = htmlcontent,
                    WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "assets", "styles.css") },
                    //HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
                    //FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "Report Footer" }
                };

                var pdf = new HtmlToPdfDocument()
                {
                    GlobalSettings = globalSettings,
                    Objects = { objectSettings }
                };
                
                _converter.Convert(pdf);              
                
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public eInvoiceFile InvoiceDetailByInvoiceNo(string Id, string FileName)
        {
            string outPut = string.Empty;
            eInvoiceFile objInvoiceFile = new eInvoiceFile();
            try
            {
                SqlParameter[] spparams = { new SqlParameter("@INVOICE_NO", Id), new SqlParameter("@FILENAME", FileName) };
                DataSet ds = objDAL.Get(connection, "RPT_COUNTERSELL_BYINVOICENO_PRINT_WOJSON", spparams);
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        objInvoiceFile.INVOICE = dt.Rows[0]["INVOICE"].ToString();
                      
                    }
                }
                else
                    return objInvoiceFile;

            }
            catch (Exception ex)
            {
                outPut = ex.Message;
            }
            return objInvoiceFile;
        }

        public string EsignPdf(string pdfBase64,string transactionid,string PdfPath)
        {
            EsignedDoc objEsignedDoc = new EsignedDoc();
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(_config.GetSection("Esign").Value + _config.GetSection("ESign_ClientId").Value);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            //httpWebRequest.Accept = "application/json; charset=utf-8";
            try
            {
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    InputJson objInputJson = new InputJson();

                    objEsignedDoc.filetype = _config.GetSection("filetype").Value;
                    objEsignedDoc.transactionid = transactionid;
                    objEsignedDoc.docname = _config.GetSection("docname").Value;
                    objEsignedDoc.designation = _config.GetSection("designation").Value;
                    objEsignedDoc.status = _config.GetSection("status").Value;
                    objEsignedDoc.llx = _config.GetSection("llx").Value;
                    objEsignedDoc.lly = _config.GetSection("lly").Value;
                    objEsignedDoc.positionX = _config.GetSection("positionX").Value;
                    objEsignedDoc.positionY = _config.GetSection("positionY").Value;
                    //objEsignedDoc.mode = _config.GetSection("mode").Value;
                    objInputJson.File = pdfBase64;
                    string objVM = JsonConvert.SerializeObject(new
                    {
                        inputJson = objInputJson,
                        filetype = objEsignedDoc.filetype,
                        transactionid = objEsignedDoc.transactionid,
                        docname = objEsignedDoc.docname,
                        designation = objEsignedDoc.designation,                 
                        status = objEsignedDoc.status,
                        llx = objEsignedDoc.llx,
                        lly = objEsignedDoc.lly,
                        positionX = objEsignedDoc.positionX,
                        positionY = objEsignedDoc.positionY,
                        //mode = objEsignedDoc.mode,
                    });

                    streamWriter.Write(objVM);
                    streamWriter.Flush();
                    streamWriter.Close();

                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        ResponseModel objj = new ResponseModel();
                        objj = JsonConvert.DeserializeObject<ResponseModel>(result);

                        if (objj.Status == 1)
                        {
                            #region convert Base64 to Pdf Format
                            Byte[] bytes = Convert.FromBase64String(objj.Document);
                            System.IO.File.WriteAllBytes(PdfPath, bytes);
                            //PdfToHtml(PdfPath);
                            #endregion
                            return "1";
                        }
                        else
                        {
                            return "0";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // return ex.Message;
                if (ex.Message.Contains("404"))
                {
                    EsignPdf(pdfBase64, transactionid, PdfPath);
                    return "1";
                }
                else
                    return "0";
            }
        }

        //[HttpPost("[action]")]
        //public  void PdfToHtmlAsMemoryStream([FromBody] CommanClassPDF objCommanClassPDF)
        //{
        //    // We are using files only for demonstration.
        //    // The whole conversion process will be done in memory.
        //    string pdfFile = objCommanClassPDF.INVOICE; //@"d:\Zoo.pdf";
        //    try
        //    {
        //        PdfFocus f = new PdfFocus();

        //        using (FileStream pdfStream = new FileStream(pdfFile, FileMode.Open))
        //        {
        //            f.OpenPdf(pdfStream);
        //            if (f.PageCount > 0)
        //            {
        //                // Let's force the component to store images inside HTML document
        //                // using base-64 encoding.
        //                // Thus the component will not use HDD.
        //                f.HtmlOptions.IncludeImageInHtml = true;
        //                f.HtmlOptions.InlineCSS = true;

        //                string html = f.ToHtml();
        //                // Here we have the HTML result as string object.
        //            }
        //        }
        //    }
        //    catch(Exception ex)
        //    {

        //    }
        //}

        [HttpPost("[action]")]
        public string PdfToHtml(string PdfPath)// ([FromBody] CommanClassPDF objCommanClassPDF)
        {

          

            var sb = new StringBuilder();
            try
            {
                //string pathToPdf = PdfPath;
                //string pathToHtml = System.IO.Path.ChangeExtension(pathToPdf, ".htm");
                //SautinSoft.PdfFocus f = new SautinSoft.PdfFocus();

                //f.HtmlOptions.IncludeImageInHtml = true;
                //f.HtmlOptions.Title = "Simple text";
                //f.OpenPdf(pathToPdf);

                //if (f.PageCount > 0)
                //{
                //    int result = f.ToHtml(pathToHtml);

                //    //Show HTML document in browser 
                //    if (result == 0)
                //    {
                //        System.Diagnostics.Process.Start(pathToHtml);
                //    }
                //}

                using (PdfReader reader = new PdfReader(PdfPath))// (objCommanClassPDF.INVOICE))
                {
                    string prevPage = "";
                    for (int page = 1; page <= reader.NumberOfPages; page++)
                    {
                        ITextExtractionStrategy its = new SimpleTextExtractionStrategy();
                        var s = PdfTextExtractor.GetTextFromPage(reader, page, its);
                        if (prevPage != s) sb.Append(s);
                        prevPage = s;
                    }
                    reader.Close();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            //string result = sb.ToString();
            return sb.ToString();
        }

        [HttpPost("[action]")]
        public string Esignotp([FromBody] CommanClassPDF objCommanClassPDF)
        {
            //objCommanClassPDF.Aadhar_ID = "688506502454";
            objCommanClassPDF.departmentname = "test";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(_config.GetSection("ESign_SendOTP").Value + _config.GetSection("ESign_ClientId").Value);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            //httpWebRequest.Accept = "application/json; charset=utf-8";
            try
            {
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string objVM = JsonConvert.SerializeObject(new
                    {
                        aadharid = objCommanClassPDF.Aadhar_ID,
                        departmentname = objCommanClassPDF.departmentname,
                    });

                    streamWriter.Write(objVM);
                    streamWriter.Flush();
                    streamWriter.Close();

                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        ResponseModel objj = new ResponseModel();
                        objj = JsonConvert.DeserializeObject<ResponseModel>(result);
                        var res = JsonConvert.SerializeObject(objj);
                        return res;
                        //if (objj.Status == 1)
                        //{
                        //    return "1";
                        //}
                        //else
                        //    return "0";
                        // pass.Text = result.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [HttpPost("[action]")]
        public string EsignOTPVerifyAndesignpdf([FromBody] CommanClassPDF objCommanClassPDF)
        {

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(_config.GetSection("ESign_authOTP").Value + _config.GetSection("ESign_ClientId").Value);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            //httpWebRequest.Accept = "application/json; charset=utf-8";
            try
            {
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string objVM = JsonConvert.SerializeObject(new
                    {
                        otp = objCommanClassPDF.otp,
                        transactionid = objCommanClassPDF.transactionid,
                    });

                    streamWriter.Write(objVM);
                    streamWriter.Flush();
                    streamWriter.Close();
                    string Pdffilename = "";
                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        ResponseModel objj = new ResponseModel();
                        objj = JsonConvert.DeserializeObject<ResponseModel>(result);

                        if (objj.Status == 1)
                        {
                            Guid obj;
                            obj = Guid.NewGuid();
                            Pdffilename = obj.ToString() + "_" + objCommanClassPDF.INVOICE + ".pdf";
                            eInvoiceFile objInvoiceFile = new eInvoiceFile();
                            objInvoiceFile = InvoiceDetailByInvoiceNo(objCommanClassPDF.INVOICE, Pdffilename); //101065
                            string PdfPath = _config.GetSection("AppSettings").GetSection("InvoicePath").Value + "\\" + Pdffilename;
                            #region ConvertHTML to PDF Format
                            ConvertHtmlToPDF(objInvoiceFile.INVOICE, PdfPath);
                            #endregion
                            #region Convert Pdf To Base64
                            PdfReader reader = new PdfReader(PdfPath);
                            string text = string.Empty;
                            for (int page = 1; page <= reader.NumberOfPages; page++)
                            {
                                text += PdfTextExtractor.GetTextFromPage(reader, page);
                            }
                            byte[] pdfBytes = System.IO.File.ReadAllBytes(PdfPath);
                            string pdfBase64 = Convert.ToBase64String(pdfBytes);
                            reader.Close();
                            #endregion
                            string pdfcreateres = EsignPdf(pdfBase64, objCommanClassPDF.transactionid, PdfPath);
                            return JsonConvert.SerializeObject(new { status = Convert.ToInt32(pdfcreateres), filename = Pdffilename });
                        }
                        else
                            return JsonConvert.SerializeObject(new { status = 2, filename = Pdffilename });

                    }
                }
            }
            catch (Exception ex)
            {
                //return ex.Message;
                if (ex.Message.Contains("not found as file or resource"))
                {
                    return EsignOTPVerifyAndesignpdf(objCommanClassPDF);
                }
                else
                    return "0";
            }
        }

        [HttpPost("[action]")]
        public string Esignpdf([FromBody] CommanClassPDF objCommanClassPDF)
        {
            try
            {
                Guid obj;
                obj = Guid.NewGuid();
                string Pdffilename = obj.ToString() + "_" + objCommanClassPDF.INVOICE + ".pdf";
                eInvoiceFile objInvoiceFile = new eInvoiceFile();
                objInvoiceFile = InvoiceDetailByInvoiceNo(objCommanClassPDF.INVOICE, Pdffilename); //101065
                string PdfPath = _config.GetSection("AppSettings").GetSection("InvoicePath").Value + "\\" + Pdffilename;
                #region ConvertHTML to PDF Format
                ConvertHtmlToPDF(objInvoiceFile.INVOICE, PdfPath);
                #endregion
                #region Convert Pdf To Base64
                PdfReader reader = new PdfReader(PdfPath);
                string text = string.Empty;
                for (int page = 1; page <= reader.NumberOfPages; page++)
                {
                    text += PdfTextExtractor.GetTextFromPage(reader, page);
                }
                byte[] pdfBytes = System.IO.File.ReadAllBytes(PdfPath);
                string pdfBase64 = Convert.ToBase64String(pdfBytes);
                reader.Close();
                #endregion
                string pdfcreateres = EsignPdf(pdfBase64, objCommanClassPDF.transactionid, PdfPath);
                return JsonConvert.SerializeObject(new { status = Convert.ToInt32(pdfcreateres), filename = Pdffilename });
            }
            catch (Exception ex)
            {
                //return ex.Message;
                if (ex.Message.Contains("not found as file or resource"))
                {
                    return Esignpdf(objCommanClassPDF);
                }
                else
                    return "0";
            }
        }
    }
}