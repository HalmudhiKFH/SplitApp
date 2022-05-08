using Newtonsoft.Json;
using splitAppAsmxServices.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace splitAppAsmxServices
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {
        [WebMethod]
        public ResponseModel<string> create_new_user (string username, string password, DateTime createdDate, string userEmail)
        {
            ResponseModel<string> response = new ResponseModel<string>();

            if ((username != null && password != null && userEmail != null) )
            {
                using (SqlConnection conn = new SqlConnection(@"Data Source=LAPTOP-8959\HAMADALMU;Initial Catalog=SplitAppDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("createNewUser", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@pass", password);
                    cmd.Parameters.AddWithValue("@createdDate", createdDate);
                    cmd.Parameters.AddWithValue("@userEmail", userEmail);


                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);


                    try
                    {
                        response.Data = JsonConvert.SerializeObject(dt);
                        response.resultCode = 200;
                        response.message = "New user has been created";
                    }
                    catch
                    {
                        response.message = "Could not create new user";
                        response.resultCode = 500;
                    }


                }
            }
            return response;
        }

        [WebMethod]
        public ResponseModel<string> Login (string email, string password)
        {
            ResponseModel<string> response = new ResponseModel<string>();

            if (email != null)
            {
                using (SqlConnection conn = new SqlConnection(@"Data Source=LAPTOP-8959\HAMADALMU;Initial Catalog=SplitAppDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("userLogIn", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@userEmail", email);
                    cmd.Parameters.AddWithValue("@userPassword", password);


                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);


                    if (dt.Rows.Count > 0)
                    {
                        response.Data = JsonConvert.SerializeObject(dt);
                        response.resultCode = 200;
                    }
                    else
                    {
                        response.message = "User Not Found!";
                        response.resultCode = 500;
                    }


                }
            }
            return response;
        }
        [WebMethod]
        public ResponseModel<string> Allorders (int? OrderID, string paymentMethod, int? scheduleID)
        {
            ResponseModel<string> response = new ResponseModel<string>();


                using (SqlConnection conn = new SqlConnection(@"Data Source=LAPTOP-8959\HAMADALMU;Initial Catalog=SplitAppDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("ordersFilter", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    if(OrderID != null)
                        cmd.Parameters.AddWithValue("@orderID", OrderID);
                    else
                        cmd.Parameters.AddWithValue("@orderID", DBNull.Value);

                    cmd.Parameters.AddWithValue("@paymentMethod", paymentMethod);
                    cmd.Parameters.AddWithValue("@scheduleID", scheduleID);



                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);


                    if (dt.Rows.Count > 0)
                    {
                        response.Data = JsonConvert.SerializeObject(dt);
                        response.resultCode = 200;
                    }
                    else
                    {
                        response.message = "No Orders Found!";
                        response.resultCode = 500;
                    }


                }
           
            return response;
        }

        [WebMethod]
        public ResponseModel<string> Create_New_Order(int userID, int orderNumber, DateTime orderDate, decimal price, int type)
        {
            ResponseModel<string> response = new ResponseModel<string>();

                using (SqlConnection conn = new SqlConnection(@"Data Source=LAPTOP-8959\HAMADALMU;Initial Catalog=SplitAppDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("create_new_order", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@passedUserID", userID);
                    cmd.Parameters.AddWithValue("@odernumber", orderNumber);
                    cmd.Parameters.AddWithValue("@orderDate", orderDate);
                    cmd.Parameters.AddWithValue("@price", price);
                    cmd.Parameters.AddWithValue("@type", type);


                try
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    response.Data = JsonConvert.SerializeObject(dt);
                    response.resultCode = 200;
                }
                catch
                {
                    response.message = "Could not create new order";
                    response.resultCode = 500;
                }
                }
            
            return response;
        }

        /*
         * Not done yet with the EditUserInfo service
         */
        [WebMethod]
        public ResponseModel<string> EditUserInfo(int? passedID, string email, string password)
        {
            ResponseModel<string> response = new ResponseModel<string>();

            if (email != null)
            {
                using (SqlConnection conn = new SqlConnection(@"Data Source=LAPTOP-8959\HAMADALMU;Initial Catalog=SplitAppDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("userLogIn", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@userEmail", email);
                    cmd.Parameters.AddWithValue("@userPassword", password);


                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);


                    if (dt.Rows.Count > 0)
                    {
                        response.Data = JsonConvert.SerializeObject(dt);
                        response.resultCode = 200;
                    }
                    else
                    {
                        response.message = "User Not Found!";
                        response.resultCode = 500;
                    }


                }
            }
            return response;
        }

    }
}
    
