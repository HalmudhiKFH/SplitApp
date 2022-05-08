using System;
using System.IO;
using System.Net;
using System.ServiceModel;
using System.Xml;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var binding = new BasicHttpsBinding();
            var epa = new EndpointAddress("https://localhost:44307/WebService1.asmx?wsdl");
            var client = new TestWs.WebService1SoapClient(binding, epa);
            //result = client.Allorders(3, "Cash", 1);


           

            //creating object of program class to access methods    
            //Program obj = new Program();
            //obj.InvokeService(null, "Cash", 5);

            //Give the options to the user
            Console.WriteLine("Please choose a service\n1-All Orders\n2-Create new order\n3-Login\n4-Create new user");
            int choice = Convert.ToInt32(Console.ReadLine());
            

            switch (choice) {

                case 1:
                    int? OrderID = null;
                    Console.WriteLine("Please Enter OrderID:");
                    //Reading input values from console
                    //OrderID = Convert.ToInt32(Console.ReadLine());


                    // If its not blank then assign it to OrderID
                    if (int.TryParse(Console.ReadLine(), out int input))
                    {
                        OrderID = input;
                    }
                    

                    Console.WriteLine("Please Enter paymentMethod(string) :");
                    string paymentMethod = Console.ReadLine();

                    Console.WriteLine("Please Enter scheduleID:");
                    int scheduleID = Convert.ToInt32(Console.ReadLine());

                    var result = client.Allorders(OrderID, paymentMethod, scheduleID);


                    if (result.resultCode == 200) {
                        Console.WriteLine("Result:-\n" + result.Data.ToString());
                    }else{
                        Console.WriteLine("No Result found");
                    }


                    break;

                case 2:


                    break;

        }

        }


        public HttpWebRequest CreateSOAPWebRequest()
        {
            //Making Web Request    
            HttpWebRequest Req = (HttpWebRequest)WebRequest.Create(@"https://localhost:44307/WebService1.asmx");
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

        public void InvokeService(int? OrderID, string paymentMethod, int scheduleID)
        {
            //Calling CreateSOAPWebRequest method    
            HttpWebRequest request = CreateSOAPWebRequest();

            XmlDocument SOAPReqBody = new XmlDocument();
            //SOAP Body Request    
            SOAPReqBody.LoadXml(@"<?xml version=""1.0""?> <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/""><soap:Body> <Allorders xmlns = ""http://tempuri.org/"" > <OrderID> " + OrderID + @" </OrderID> <paymentMethod>" + paymentMethod + @"</paymentMethod> <scheduleID> " + scheduleID + @" </scheduleID> </Allorders></soap:Body></soap:Envelope>");


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
