using System;
using System.Configuration;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Cti.Seller.Data.Data_Repositories
{
    public static class DaoHelperMSSQL
    {

        private static SqlConnection _conn;
        private static string _connstring = "";
        public static string ConnString
        {
            get { return _connstring; }
            set { if (String.IsNullOrEmpty(_connstring)) _connstring = value; }
        }
        static DaoHelperMSSQL()
        {
            ConnString = ConfigurationManager.ConnectionStrings["Seller"].ConnectionString;
            _conn = new SqlConnection(ConnString);
        }

        private static void OpenConnection()
        {
            if (string.IsNullOrEmpty(ConnString))
            {
                ConnString = ConfigurationManager.ConnectionStrings["Seller"].ConnectionString; ;
            }
            _conn = new SqlConnection(ConnString);

            if (_conn != null && _conn.State == ConnectionState.Closed)
                _conn.Open();
        }
        public static DataTable GetData(string Sql)
        {
            DataTable dt = new DataTable();
            try
            {

                SqlDataAdapter daItemQty = new SqlDataAdapter(Sql, _conn);
                daItemQty.Fill(dt);
                return dt;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _conn.Close();
            }
        }
        public static DataTable GetData(string Sql, SqlParameter[] parameters)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter daItemQty = new SqlDataAdapter(Sql, _conn);
                daItemQty.SelectCommand.Parameters.AddRange(parameters);
                daItemQty.Fill(dt);
                return dt;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _conn.Close();
            }
        }
        public static int GetDataCount(string Sql, SqlParameter[] Parameters)
        {
            int result;

            try
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = System.Data.CommandType.Text;
                OpenConnection();
                cmd.Connection = _conn;

                if (Parameters != null)
                {
                    cmd.Parameters.AddRange(Parameters);
                }
                cmd.CommandText = Sql;
                result = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception)
            {
                throw;
            }
            finally { _conn.Close(); }

            return result;
        }
        public static string GetSingleData(string Sql, SqlParameter[] Parameters)
        {
            string result;

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                if (Parameters != null)
                {
                    cmd.Parameters.AddRange(Parameters);
                }
                cmd.CommandText = Sql;
                OpenConnection();
                cmd.Connection = _conn;

                result = Convert.ToString(cmd.ExecuteScalar());
            }
            catch (Exception)
            {
                throw;
            }
            finally { _conn.Close(); }

            return result;
        }
        public static void SaveData(string Sql, SqlParameter[] Parameters)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                if (Parameters != null)
                {
                    cmd.Parameters.AddRange(Parameters);
                }
                cmd.CommandText = Sql;

                OpenConnection();
                cmd.Connection = _conn;

                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally { _conn.Close(); }
        }
        public static void SaveData(string Sql, SqlParameter[] Parameters, SqlConnection connection)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                if (Parameters != null)
                {
                    cmd.Parameters.AddRange(Parameters);
                }
                cmd.CommandText = Sql;
                OpenConnection();
                cmd.Connection = _conn;

                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally { _conn.Close(); }
        }
        public static void SaveData(string[] Sql, SqlParameter[] Parameters, bool WithinTransaction)
        {

            SqlCommand cmd = new SqlCommand();
            OpenConnection();
            cmd.Connection = _conn;
            cmd.CommandType = System.Data.CommandType.Text;
            if (Parameters != null)
            {
                cmd.Parameters.AddRange(Parameters);
            }
            SqlTransaction trans = cmd.Connection.BeginTransaction();
            try
            {
                cmd.Transaction = trans;
                foreach (string item in Sql)
                {
                    cmd.CommandText = item;
                    cmd.ExecuteNonQuery();
                }
                trans.Commit();
            }
            catch (Exception)
            {
                trans.Rollback();
                throw;
            }
            finally
            {
                _conn.Close();
            }
        }

    }
}



