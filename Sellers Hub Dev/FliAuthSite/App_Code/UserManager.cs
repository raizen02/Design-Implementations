//**************************************************************
//Programmer Name		: John Alexander M.Baltazar
//Date Created			: 2014.02.27
//Program Name           : UserManager
//Program Description    : Data access Class for User functions
//Remarks                : DEV - 2013.02.05 | PROD - 2013.07.08
//**************************************************************
//Updates Information    : John Alexander M. Baltazar | JO# JYYXXXXX
//Date Updated           : 2015.04.22
//Changes                : Added Web User Properties
//Remarks                : DEV - 2015.04.22 | PROD - n/a
//**************************************************************
//Updates Information    : John Alexander M. Baltazar | JO# J1501697
//Date Updated           : 2015.06.26
//Changes                : added email as validation on Account Recovery
//Remarks                : DEV - 2015.06.26 | PROD - n/a
//**************************************************************
//Updates Information    : John Alexander M. Baltazar | JO# JYYXXXXX
//Date Updated           : 2015.08.19
//Changes                : added function IsUserActive(username) and IsEmailActive(email)
//Remarks                : DEV - 2015.08.19 | PROD - n/a
//**************************************************************

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Web;

public class UserManager
{
    public UserManager()
	{
		//
		// TODO: Add constructor logic here
		//
        _priseError = new SqlError(4444, "");
	}

    private SqlConnection SqlConn;
    private SqlTransaction SqlTran;
    private SqlError _priseError;

    public SqlError LastError
    {
        get
        {
            return _priseError;            
        }
    }

    public bool OpenDbConnection()
    { 
        bool success = true;

        try
        {
            SqlConn = SqlHelper.CreateAndOpenConnection();           
        }
        catch
        {
            success = false;
            SqlConn = null;
            _priseError = new SqlError(1, "Cannot establish database connection");
        }

        return success;
    }

    public void BeginTransaction()
    {
        try
        {
            SqlTran = SqlConn.BeginTransaction();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void CommitTran()
    {
        if (SqlTran != null)
        {
            SqlTran.Commit();
            SqlTran = null;
        }
    }

    public void RollbackTran()
    {
        if (SqlTran != null)
        {
            SqlTran.Rollback();
            SqlTran = null;
        }
    }

    public void CloseConnection()
    {
        SqlHelper.CloseConnection(SqlConn);
        SqlConn = null;
    }

    private SqlCommand CreateLoginCommand(int _parintMode, string _parstrUsername, string _parstrPassword, string _parstrApplicationCode,
        string _pastrComputerName, string _parstrIPAddress, string _parstrOldPassword, string _parstrSessionId)
    {
        SqlCommand SqlCmd = new SqlCommand();
        SqlCmd.CommandText = "sp_ssmLogin";
        SqlCmd.CommandTimeout = 0;
        SqlCmd.CommandType = CommandType.StoredProcedure;
        SqlCmd.Connection = SqlConn;
        SqlCmd.Transaction = SqlTran;

        SqlCmd.Parameters.AddWithValue("@pintMode", SqlHelper.ConvertToNull(_parintMode));
        SqlCmd.Parameters.AddWithValue("@pstrUserName", SqlHelper.ConvertToNull(_parstrUsername));
        SqlCmd.Parameters.AddWithValue("@pstrPassword", SqlHelper.ConvertToNull(_parstrPassword));
        SqlCmd.Parameters.AddWithValue("@pstrApplicationCode", SqlHelper.ConvertToNull(_parstrApplicationCode));
        SqlCmd.Parameters.AddWithValue("@pstrComputerName", SqlHelper.ConvertToNull(_pastrComputerName));
        SqlCmd.Parameters.AddWithValue("@pstrIPAddress", SqlHelper.ConvertToNull(_parstrIPAddress));
        SqlCmd.Parameters.AddWithValue("@pstrOldPassword", SqlHelper.ConvertToNull(_parstrOldPassword));
        SqlCmd.Parameters.AddWithValue("@pstrSessionId", SqlHelper.ConvertToNull(_parstrSessionId));

        SqlCmd.Parameters.Add("@pintErrorNumber", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
        SqlCmd.Parameters.Add("@pstrErrorMessage", SqlDbType.NVarChar, 200).Direction = ParameterDirection.Output;

        return SqlCmd;
    }

    private SqlCommand CreateUserCommand(int _parintMode, string _parstrConfimationId, string _parstrUsername, string _parstrPassword,
       string _parstrEmail, string _parstrSellerCode, string _parstrTinNo, DateTime _pardteBirthday, string _parstrOldPassword, string _parstrSysUsername)
    {
        SqlCommand SqlCmd = new SqlCommand();
        SqlCmd.CommandText = "sp_ssmUsers";
        SqlCmd.CommandTimeout = 0;
        SqlCmd.CommandType = CommandType.StoredProcedure;
        SqlCmd.Connection = SqlConn;
        SqlCmd.Transaction = SqlTran;

        SqlCmd.Parameters.AddWithValue("@pintMode", SqlHelper.ConvertToNull(_parintMode));
        SqlCmd.Parameters.AddWithValue("@pstrConfimationId", SqlHelper.ConvertToNull(_parstrConfimationId));
        SqlCmd.Parameters.AddWithValue("@pstrUserName", SqlHelper.ConvertToNull(_parstrUsername));
        SqlCmd.Parameters.AddWithValue("@pstrPassword", SqlHelper.ConvertToNull(_parstrPassword));
        SqlCmd.Parameters.AddWithValue("@pstrEmail", SqlHelper.ConvertToNull(_parstrEmail));
        SqlCmd.Parameters.AddWithValue("@pstrSellerCode", SqlHelper.ConvertToNull(_parstrSellerCode));
        SqlCmd.Parameters.AddWithValue("@pstrTinNo", SqlHelper.ConvertToNull(_parstrTinNo));
        SqlCmd.Parameters.AddWithValue("@pdteBirthday", SqlHelper.ConvertToNull(_pardteBirthday));
        SqlCmd.Parameters.AddWithValue("@pstrOldPassword", SqlHelper.ConvertToNull(_parstrOldPassword));
        SqlCmd.Parameters.AddWithValue("@pstrSysUsername", SqlHelper.ConvertToNull(_parstrSysUsername));

        SqlCmd.Parameters.Add("@pintErrorNumber", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
        SqlCmd.Parameters.Add("@pstrErrorMessage", SqlDbType.NVarChar, 200).Direction = ParameterDirection.Output;

        return SqlCmd;
    }

    public WebUser AuthenticateUser(string _parstrUsername, string _parstrPassword, string _parstrApplicationCode,
        string _pastrComputerName, string _parstrIPAddress)
    {
        using (SqlCommand _dimcmdSqlCmd = CreateLoginCommand(1, _parstrUsername, _parstrPassword, _parstrApplicationCode, _pastrComputerName, _parstrIPAddress, null, null))
        {
            using (DataSet _dimdsUserInfo = SqlHelper.ExecuteDataSet(_dimcmdSqlCmd))
            {
                if (Convert.ToInt32(_dimcmdSqlCmd.Parameters["@pintErrorNumber"].Value) != SqlError.SuccessErrorNumber)
                {
                    _priseError = new SqlError();
                    _priseError.ErrorNumber = Convert.ToInt32(_dimcmdSqlCmd.Parameters["@pintErrorNumber"].Value);
                    _priseError.ErrorMessage = _dimcmdSqlCmd.Parameters["@pstrErrorMessage"].Value.ToString();
                    return null;
                }

                if (_dimdsUserInfo == null)
                {
                    return null;
                }

                if (_dimdsUserInfo.Tables.Count == 0)
                {
                    return null;
                }

                if (_dimdsUserInfo.Tables[0].Rows.Count == 0)
                {
                    return null;
                }

                DataRow _dimdrRow = _dimdsUserInfo.Tables[0].Rows[0];

                WebUser _dimwuUser = new WebUser();
                _dimwuUser.Username = _dimdrRow["Username"].ToString();
                _dimwuUser.SellerCode = _dimdrRow["SellerCode"].ToString();
                _dimwuUser.UserLevel = _dimdrRow["UserLevel"].ToString();
                //_dimwuUser.SellerAllocation = _dimdrRow["SellerAllocation"].ToString();
                //_dimwuUser.SellerSalesChannel = _dimdrRow["SellerSalesChannel"].ToString();
                //_dimwuUser.SellerClassification = _dimdrRow["SellerClassification"].ToString();
                //_dimwuUser.SellerLevel = _dimdrRow["SellerLevel"].ToString();
                //_dimwuUser.SellerPosition = _dimdrRow["SellerPosition"].ToString();
                _dimwuUser.Token = Util.GetGuidHash();

                return _dimwuUser;
            }
        }
    }

    public bool UsernameExists(string _parstrUsername)
    {
        bool _dimblnExists = false;

        using (SqlCommand _dimscCmd = CreateUserCommand(4, null, _parstrUsername, null, null, null, null, SqlHelper.NULL_DATE, null, null))
        {
            _dimscCmd.ExecuteNonQuery();

            if (WithError(_dimscCmd))
            {
                _priseError = new SqlError();
                _priseError.ErrorNumber = Convert.ToInt32(_dimscCmd.Parameters["@pintErrorNumber"].Value);
                _priseError.ErrorMessage = _dimscCmd.Parameters["@pstrErrorMessage"].Value.ToString();
                _dimblnExists = true;
            }
        }

        return _dimblnExists;
    }

    public bool ConfirmationIdExist(string _parstrConfirmationId)
    {
        bool _dimblnExist = false;

        using (SqlCommand _dimscCmd = CreateUserCommand(6, _parstrConfirmationId, null, null, null, null, null, SqlHelper.NULL_DATE, null, null))
        {
            _dimscCmd.ExecuteNonQuery();

            if (Convert.ToInt32(_dimscCmd.Parameters["@pintErrorNumber"].Value) != SqlError.SuccessErrorNumber)
            {
                _priseError = new SqlError();
                _priseError.ErrorNumber = Convert.ToInt32(_dimscCmd.Parameters["@pintErrorNumber"].Value);
                _priseError.ErrorMessage = _dimscCmd.Parameters["@pstrErrorMessage"].Value.ToString();
                _dimblnExist = true;
            }
        }

        return _dimblnExist;
    }

    public bool ValidConfirmationId(string _parstrConfirmationId)
    {
        bool _dimblnValid = true;

        using (SqlCommand _dimscCmd = CreateUserCommand(8, _parstrConfirmationId, null, null, null, null, null, SqlHelper.NULL_DATE, null, null))
        {
            _dimscCmd.ExecuteNonQuery();

            if (WithError(_dimscCmd))
            {
                _priseError = new SqlError();
                _priseError.ErrorNumber = Convert.ToInt32(_dimscCmd.Parameters["@pintErrorNumber"].Value);
                _priseError.ErrorMessage = _dimscCmd.Parameters["@pstrErrorMessage"].Value.ToString();
                _dimblnValid = false;
            }
        }

        return _dimblnValid;
    }

    public bool EmailExists(string _parstrEmailAddr)
    {
        bool _dimblnExist = false;

        using (SqlCommand _dimscCmd = CreateUserCommand(7, null, null, null, _parstrEmailAddr, null, null, SqlHelper.NULL_DATE, null, null))
        {
            _dimscCmd.ExecuteNonQuery();

            if (WithError(_dimscCmd))
            {
                _priseError = new SqlError();
                _priseError.ErrorNumber = Convert.ToInt32(_dimscCmd.Parameters["@pintErrorNumber"].Value);
                _priseError.ErrorMessage = _dimscCmd.Parameters["@pstrErrorMessage"].Value.ToString();
                _dimblnExist = true;
            }
        }

        return _dimblnExist;
    }

    public bool ValidSeller(string _parstrSellerCode, string _parstrTinNo, DateTime _pardteBirthday)
    {
        bool _dimblnValid = true;

        using (SqlCommand _dimscCmd = CreateUserCommand(5, null, null, null, null, _parstrSellerCode, _parstrTinNo, _pardteBirthday, null, null))
        {
            _dimscCmd.ExecuteNonQuery();

            if (WithError(_dimscCmd))
            {
                _priseError = new SqlError();
                _priseError.ErrorNumber = Convert.ToInt32(_dimscCmd.Parameters["@pintErrorNumber"].Value);
                _priseError.ErrorMessage = _dimscCmd.Parameters["@pstrErrorMessage"].Value.ToString();
                _dimblnValid = false;
            }
        }

        return _dimblnValid;
    }

    public bool AddUserForConfirmation(string _parstrUsername, string _parstrPassword,
       string _parstrEmail, string _parstrSellerCode, string _parstrTinNo, DateTime _pardteBirthday, string _parstrSysUsername)
    {
        bool _dimblnSucess = true;

        string _dimstrConfirmationId;

        getConfimationId:
        _dimstrConfirmationId = Util.GetGuidHash();
        if (ConfirmationIdExist(_dimstrConfirmationId) == true)
        {
            goto getConfimationId;
        }

        using (SqlCommand _dimscCmd = CreateUserCommand(2, _dimstrConfirmationId, _parstrUsername, _parstrPassword, _parstrEmail, _parstrSellerCode, _parstrTinNo, _pardteBirthday, null, _parstrSysUsername))
        {
            _dimscCmd.ExecuteNonQuery();

            if (WithError(_dimscCmd))
            {
                _priseError = ExtractErrorInfo(_dimscCmd);
                _dimblnSucess = false;
            }
        }

        return _dimblnSucess;
    }

    public bool ConfirmUser(string _parstrConfirmationId, string _parstrSysUsername)
    {
        bool _dimblnSuccess = true;

        using (SqlCommand _dimscCmd = CreateUserCommand(3, _parstrConfirmationId, null, null, null, null, null, SqlHelper.NULL_DATE, null, _parstrSysUsername))
        {
            _dimscCmd.ExecuteNonQuery();

            if (WithError(_dimscCmd))
            {
                _priseError = ExtractErrorInfo(_dimscCmd);
                _dimblnSuccess = false;
            }
        }

        return _dimblnSuccess;
    }

    public bool ChangePassword(string _parstrUsername, string _parstrPassword, string _parstrOldPassword)
    {
        bool _dimblnSuccess = true;

        using (SqlCommand _dimscCmd = CreateUserCommand(9, null, _parstrUsername, _parstrPassword, null, null, null, SqlHelper.NULL_DATE, _parstrOldPassword, _parstrUsername))
        {
            _dimscCmd.ExecuteNonQuery();

            if (WithError(_dimscCmd))
            {
                _priseError = ExtractErrorInfo(_dimscCmd);
                _dimblnSuccess = false;
            }
        }

        return _dimblnSuccess;
    }

    public bool RecoverAccount(string _parstrUsername, string _parstrEmailAddr, string _parstrPassword, string _parSellerCode)
    {
        bool _dimblnSuccess = true;

        using (SqlCommand _dimscCmd = CreateUserCommand(10, null, _parstrUsername, _parstrPassword, _parstrEmailAddr, _parSellerCode, null, SqlHelper.NULL_DATE, null, _parstrUsername))
        {
            _dimscCmd.ExecuteNonQuery();

            if (WithError(_dimscCmd))
            {
                _priseError = ExtractErrorInfo(_dimscCmd);
                _dimblnSuccess = false;
            }
        }

        return _dimblnSuccess;
    }

    public bool AddSessionId(string _parstrUsername, string _parstrSessionId)
    {
        bool _dimblnSuccess = true;
        using (SqlCommand _dimcmdSqlCmd = CreateLoginCommand(4, _parstrUsername, null, null, null, null, null, _parstrSessionId))
        {
            _dimcmdSqlCmd.ExecuteNonQuery();

            if (WithError(_dimcmdSqlCmd))
            {
                _priseError = ExtractErrorInfo(_dimcmdSqlCmd);
                _dimblnSuccess = false;
            }
        }

        return _dimblnSuccess;
    }

    public bool RemoveSessionId(string _parstrSessionId)
    {
        bool _dimblnSuccess = true;

        using (SqlCommand _dimcmdSqlCmd = CreateLoginCommand(6, null, null, null, null, null, null, _parstrSessionId))
        {
            _dimcmdSqlCmd.ExecuteNonQuery();

            if (WithError(_dimcmdSqlCmd))
            {
                _priseError = ExtractErrorInfo(_dimcmdSqlCmd);
                _dimblnSuccess = false;
            }
        }

        return _dimblnSuccess;
    }

    public WebUser AuthenticateSession(string _parstrSessionId, string _parstrApplicationCode,
        string _pastrComputerName, string _parstrIPAddress)
    {
        using (SqlCommand _dimcmdSqlCmd = CreateLoginCommand(5, null, null, _parstrApplicationCode, _pastrComputerName, _parstrIPAddress, null, _parstrSessionId))
        {
            using (DataSet _dimdsUserInfo = SqlHelper.ExecuteDataSet(_dimcmdSqlCmd))
            {
                if (WithError(_dimcmdSqlCmd))
                {
                    _priseError = ExtractErrorInfo(_dimcmdSqlCmd);
                    return null;
                }

                if (_dimdsUserInfo == null)
                {
                    return null;
                }

                if (_dimdsUserInfo.Tables.Count == 0)
                {
                    return null;
                }

                if (_dimdsUserInfo.Tables[0].Rows.Count == 0)
                {
                    return null;
                }

                DataRow _dimdrRow = _dimdsUserInfo.Tables[0].Rows[0];

                WebUser _dimwuUser = new WebUser();
                _dimwuUser.Username = _dimdrRow["Username"].ToString();
                _dimwuUser.SellerCode = _dimdrRow["SellerCode"].ToString();
                _dimwuUser.UserLevel = _dimdrRow["UserLevel"].ToString();
                _dimwuUser.Token = Util.GetGuidHash();

                return _dimwuUser;
            }
        }
    }

    private bool WithError(SqlCommand _parscCmd)
    {
        return Convert.ToInt32(_parscCmd.Parameters["@pintErrorNumber"].Value) != SqlError.SuccessErrorNumber;
    }

    private SqlError ExtractErrorInfo(SqlCommand _parscCmd)
    {
        SqlError _dimSqlError = new SqlError();
        _dimSqlError.ErrorNumber = Convert.ToInt32(_parscCmd.Parameters["@pintErrorNumber"].Value);
        _dimSqlError.ErrorMessage = _parscCmd.Parameters["@pstrErrorMessage"].Value.ToString();
        return _dimSqlError;
    }

    public bool IsUserActive(string _parstrUsername)
    {
        bool _dimblnIsActive = false;

        using (SqlCommand _dimscCmd = CreateUserCommand(12, null, _parstrUsername, null, null, null, null, SqlHelper.NULL_DATE, null, null))
        {
            _dimblnIsActive = Convert.ToBoolean( _dimscCmd.ExecuteScalar());
        }

        return _dimblnIsActive;
    }

    public bool IsEmailActive(string _parstrEmailAddr)
    {
        bool _dimblnIsActive = false;

        using (SqlCommand _dimscCmd = CreateUserCommand(13, null, null, null, _parstrEmailAddr, null, null, SqlHelper.NULL_DATE, null, null))
        {
            _dimblnIsActive = Convert.ToBoolean(_dimscCmd.ExecuteScalar());
        }

        return _dimblnIsActive;
    }
}
