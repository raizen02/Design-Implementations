using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Web;


public class SqlHelper
{
    public static DateTime NULL_DATE = Convert.ToDateTime("12/31/9999");

    public static SqlConnection CreateAndOpenConnection()
    {
        SqlConnection _dimscSqlConn = new SqlConnection(Config.ConnectionStrings.FliUserAuth);

        try
        {
            _dimscSqlConn.Open();
        }
        catch (SqlException ex)
        {
            _dimscSqlConn = null;
            throw ex;
        }

        return _dimscSqlConn;
    }

    public static void CloseConnection(SqlConnection _parconSqlConn)
    {
        if (_parconSqlConn.State == ConnectionState.Open)
        {
            _parconSqlConn.Close();
        }
    }

    public static DataSet ExecuteDataSet(SqlCommand _parcmdSelectCmd)
    {
        bool _dimblnConnectionCreated = false;
        using (SqlDataAdapter daDataAdapter = new SqlDataAdapter(_parcmdSelectCmd))
        {
            if (daDataAdapter.SelectCommand.Connection == null)
            {
                daDataAdapter.SelectCommand.Connection = new SqlConnection(Config.ConnectionStrings.FliUserAuth);
                _dimblnConnectionCreated = true;
            }

            DataSet dsInfo = new DataSet();
            daDataAdapter.Fill(dsInfo);

            if (_dimblnConnectionCreated)
            {
                CloseConnection(daDataAdapter.SelectCommand.Connection);
            }

            return dsInfo;
        }
    }

    public static DataSet ExecuteDataSet(SqlCommand _parcmdSelectCmd, string _parstrConnectionString)
    {
        _parcmdSelectCmd.Connection =  new SqlConnection(_parstrConnectionString);    
        return ExecuteDataSet(_parcmdSelectCmd);
    }

    public static object ConvertToNull(string val)
    {
        if (string.IsNullOrEmpty(val))
        {
            return DBNull.Value;
        }

        return val;
    }

    public static object ConvertToNull(int val)
    {
        if (val == -1)
        {
            return DBNull.Value;
        }

        return val;
    }

    public static object ConvertToNull(decimal val)
    {
        if (val == -1)
        {
            return DBNull.Value;
        }

        return val;
    }

    public static object ConvertToNull(DateTime val)
    {       
        if (val == Convert.ToDateTime("12:00:00 AM"))
        {
            return DBNull.Value;
        }

        if (val == Convert.ToDateTime("12/31/9999"))
        {
            return DBNull.Value;
        }

        return val;
    }

    public static object ConvertToNull(object val)
    {
        if (val == null)
        {
            return DBNull.Value;
        }

        return val;
    }
}
