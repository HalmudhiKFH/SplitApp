using System;
using System.IO;
using System.Net;
using System.Xml;

namespace SplitAppCallingWebservicesApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //creating object of program class to access methods    
            Program obj = new Program();
            Console.WriteLine("Please Enter OrderID:");
            //Reading input values from console    
            int orderID = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Please Enter paymentMethod(string) :");
            string paymentMethod = Console.ReadLine();

            Console.WriteLine("Please Enter scheduleID:");
            int scheduleID = Convert.ToInt32(Console.ReadLine());


            //Calling InvokeService method    
            obj.InvokeService(orderID, paymentMethod, scheduleID);
        }

        public HttpWebRequest CreateSOAPWebRequest()
        {
            //Making Web Request    
            HttpWebRequest Req = (HttpWebRequest)WebRequest.Create(@"http://localhost/WebService1.asmx");
            //SOAPAction    
            Req.Headers.Add(@"SOAPAction:http://tempuri.org/Allorders");
            //Content_type    
            Req.ContentType = "text/xml; charset=utf-8\"";
            Req.Accept = "text/xml";
            //HTTP method    
            Req.Method = "POST";
            //return HttpWebRequest    
            return Req;
        }

        public void InvokeService(int OrderID, string paymentMethod, int scheduleID)
        {
            //Calling CreateSOAPWebRequest method    
            HttpWebRequest request = CreateSOAPWebRequest();

            XmlDocument SOAPReqBody = new XmlDocument();
            //SOAP Body Request    
            SOAPReqBody.LoadXml(@"<?xml version=""1.0"" encoding=""utf-8""?> < soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"" >< soap:Body > < Allorders xmlns = ""http://tempuri.org/"" > < OrderID > " + OrderID + @" </ OrderID > < paymentMethod > " + paymentMethod + @" </ paymentMethod > < scheduleID > " + scheduleID + @" </ scheduleID > </ Allorders ></ soap:Body ></ soap:Envelope > ");


            using (Stream stream = request.GetRequestStream())
            {
                SOAPReqBody.Save(stream);
            }
            //Geting response from request    
            using (WebResponse Serviceres = request.GetResponse())
            {
                using (StreamReader rd = new StreamReader(Serviceres.GetResponseStream()))
                {
                    //reading stream    
                    var ServiceResult = rd.ReadToEnd();
                    //writting stream result on console    
                    Console.WriteLine(ServiceResult);
                    Console.ReadLine();
                }
            }
        }

    }
}
