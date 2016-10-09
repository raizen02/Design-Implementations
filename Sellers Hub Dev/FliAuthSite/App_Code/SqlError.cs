/// <summary>
/// Summary description for SqlError
/// </summary>
public class SqlError
{
    private int _priintErrorNumber;
    private string _pristrErrorMessage;

    public static int SuccessErrorNumber
    {
        get
        {
            return 8888;
        }
    }

    public SqlError()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public SqlError(int ErrorNumber, string Message)
    {
        _priintErrorNumber = ErrorNumber;
        _pristrErrorMessage = Message;
    }

    public int ErrorNumber
    {
        get
        {
            return _priintErrorNumber;
        }
        set
        {
            _priintErrorNumber = value;
        }
    }

    public string ErrorMessage
    {
        get
        {
            return _pristrErrorMessage;
        }
        set
        {
            _pristrErrorMessage = value;
        }
    }

}
