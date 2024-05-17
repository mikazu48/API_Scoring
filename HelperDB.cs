using API_Scoring.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

namespace API_Scoring
{
    public class HelperDB
    {
        //static string connvalue = "Data Source=.;Initial Catalog=smk_score;User ID=sa;Password=sa12345";
        public static string connvalue = "";
        public static string user_id = "";


        static SqlConnection sqlConn;
        public static DataTable GetDataDB(string Query)
        {
            sqlConn = new SqlConnection(connvalue);
            sqlConn.Open();
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand(Query,sqlConn);
            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            adap.Fill(dt);
            sqlConn.Close();

            return dt;
        }
        public static DataRow GetSingleData(string Query)
        {
            sqlConn = new SqlConnection(connvalue);
            sqlConn.Open();
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand(Query, sqlConn);
            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            adap.Fill(dt);
            sqlConn.Close();
            if (dt.Rows.Count == 0)
                return null;

            return dt.Rows[0];
        }
        public static void ExecuteDB(string Query)
        {
            sqlConn = new SqlConnection(connvalue);
            sqlConn.Open();
            SqlCommand cmd = new SqlCommand(Query, sqlConn);
            cmd.ExecuteNonQuery();
            sqlConn.Close();
        }
        public static string CheckValidToken(string Token)
        {
            DataRow dr = GetSingleData("select * from access_token where token = '" + Token + "'");
            if(dr == null)
            {
                return "Token not found, please get token again.";
            }

            DateTime TokenExpired = DateTime.Parse(dr["expired_at"].ToString());
            if(TokenExpired < DateTime.Now)
            {
                return "Token has been expired, please hit again.";
            }
            return "";
        }
        public static string ComputeHash(string input)
        {
            // Bycrypt
            SHA256CryptoServiceProvider algorithm = new SHA256CryptoServiceProvider();
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);

            Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);

            // SHA256
            return BitConverter.ToString(hashedBytes).Replace("-", "");
        }
    }
}
