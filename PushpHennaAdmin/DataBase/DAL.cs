using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PushpHennaAdmin.DataBase
{
    public class DAL
    {
        protected static DAL _objDAL;
        private DAL()
        {
        }
        public static DAL GetObject()
        {

            if (_objDAL == null)
            {
                _objDAL = new DAL();
            }

            return _objDAL;
        }

        /// <summary>
        ///  User For manage Get Request from api and return Json string from  Stored Procedure
        /// </summary>
        /// <param name="conn">Database connection string </param>
        /// <param name="spName">Stored Procedure Name</param>
        /// <param name="spParams">Parameter name</param>
        /// <returns>Json String</returns>
        public string GetJson(string conn, string spName, SqlParameter[] spParams = null)
        {
            string ResultSet = string.Empty;
            using (SqlConnection sqlcon = new SqlConnection(conn))
            {
                try
                {
                    sqlcon.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = sqlcon;
                    cmd.CommandText = spName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (spParams != null)
                        cmd.Parameters.AddRange(spParams);

                    //SqlDataReader sdr = cmd.ExecuteReader();
                    //sdr.Read();
                    //if (sdr.HasRows)
                    //    ResultSet = sdr[0].ToString();

                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adp.Fill(ds);
                    DataTable dt = new DataTable();
                    dt = ds.Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            ResultSet = ResultSet + dt.Rows[i][0].ToString();
                        }
                    }

                    ds.Dispose();
                    dt.Dispose();
                    if (string.IsNullOrEmpty(ResultSet))
                    {
                        ResultSet = "[]";
                    }
                    //if (sqlcon.State == System.Data.ConnectionState.Open) sqlcon.Close();                
                    return ResultSet;
                }
                catch (Exception ex)
                {
                    sqlcon.Close();
                    throw ex;
                }
            }
        }

        /// <summary>
        ///  User For manage Post Request from api and return Json string from  Stored Procedure
        /// </summary>
        /// <param name="conn">Database connection string </param>
        /// <param name="spName">Stored Procedure Name</param>
        /// <param name="spParams">Parameter List</param>
        /// <returns>Json String</returns>
        public string PostWithResultCode(string conn, string spName, SqlParameter[] spParams)
        {
            using (SqlConnection sqlcon = new SqlConnection(conn))
            {
                try
                {
                    sqlcon.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = sqlcon;
                    cmd.CommandText = spName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(spParams);
                    //cmd.Parameters["@RESULTCODE"].Direction = ParameterDirection.Output;
                    cmd.Parameters["@RESULTMSG"].Direction = ParameterDirection.Output;
                    cmd.Parameters["@RESULTMSG"].Size = 4000;
                    cmd.ExecuteNonQuery();
                    return Convert.ToString(cmd.Parameters["@RESULTMSG"].Value);  //(cmd.Parameters["@RESULTCODE"].Value + "|" + cmd.Parameters["@RESULTMSG"].Value);
                }
                catch (Exception ex)
                {
                    if (sqlcon.State == System.Data.ConnectionState.Open) sqlcon.Close();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// User For manage Get Request from api and return data set
        /// </summary>
        /// <param name="conn">Database connection string </param>
        /// <param name="spName">Stored Procedure Name</param>
        /// <param name="spParams">Parameter name</param>
        /// <returns>Data Set</returns>
        public DataSet Get(string conn, string spName, SqlParameter[] spParams = null)
        {
            using (SqlConnection sqlcon = new SqlConnection(conn))
            {
                try
                {
                    sqlcon.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = sqlcon;
                    cmd.CommandText = spName;
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (spParams != null)
                        cmd.Parameters.AddRange(spParams);

                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adp.Fill(ds);

                    return ds;
                }
                catch (Exception ex)
                {
                    if (sqlcon.State == System.Data.ConnectionState.Open) sqlcon.Close();
                    throw ex;
                }
            }
        }

        public bool Post(string conn, string spName, SqlParameter[] spParams)
        {
            using (SqlConnection sqlcon = new SqlConnection(conn))
            {
                try
                {
                    sqlcon.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = sqlcon;
                    cmd.CommandText = spName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(spParams);
                    cmd.Parameters["@RESULT"].Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();

                    return Convert.ToBoolean(cmd.Parameters["@RESULT"].Value);
                }
                catch (Exception ex)
                {
                    if (sqlcon.State == System.Data.ConnectionState.Open) sqlcon.Close();
                    throw ex;

                }
            }
        }

        #region Run Script In DB
        public string ExecuteDbScript(string Connection, string Script, int ScriptType, int ReturnType)
        {
            string ResultResponse = string.Empty;
            using (SqlConnection sqlcon = new SqlConnection(Connection))
            {
                try
                {
                    sqlcon.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd = new SqlCommand(Script, sqlcon);

                    IEnumerable<string> commandStrings = Regex.Split(Script, @"^\s*GO\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase);
                    if (ScriptType == 1)
                    {
                        int result = cmd.ExecuteNonQuery();
                        //ResultResponse = "Script run successfully.";
                        if (result == -1)
                            ResultResponse = "Script run successfully.";
                        else
                            ResultResponse = "Script is not correct.";
                    }
                    else if (ScriptType == 2)
                    {
                        SqlDataAdapter adp = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        adp.Fill(ds);
                        DataTable dt = new DataTable();
                        dt = ds.Tables[0];
                        if (ReturnType == 1)
                            ResultResponse = JsonConvert.SerializeObject(dt);
                        else
                            ResultResponse = ConvertDataTableToString(dt);
                    }

                }
                catch (Exception ex)
                {
                    ResultResponse = ex.Message;
                    if (sqlcon.State == System.Data.ConnectionState.Open) sqlcon.Close();
                }

                return ResultResponse;
            }
        }

        public static string ConvertDataTableToString(DataTable dt)
        {
            StringBuilder stringBuilder = new StringBuilder();
            dt.Rows.Cast<DataRow>().ToList().ForEach(dataRow =>
            {
                dt.Columns.Cast<DataColumn>().ToList().ForEach(column =>
                {
                    stringBuilder.AppendFormat("{0}{1} ", "", dataRow[column]);
                });
                stringBuilder.Append(Environment.NewLine);
            });
            return stringBuilder.ToString();
        }

        #endregion

    }
}
