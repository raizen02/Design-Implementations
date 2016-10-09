using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Security.Cryptography;
using System.IO;
using System.Text;

/// <summary>
/// Summary description for Crypto
/// </summary>
public class Crypto
{
    public Crypto()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    //Byte vector required for Rijndael.  This is randomly generated and recommended you change it on a per-application basis.
    //It is 16 bytes.
    private static byte[] bytIV = new Byte[] { 68, 10, 27, 67, 19, 81, 97, 34, 43, 30, 66, 97, 24, 53, 43, 14};

    //Character to pad keys with to make them at least intMinKeySize.
    private const char chrKeyFill = 'X';

    //String to display on error for functions that return strings. {0} is Exception.Message.
    private const string strTextErrorString = "#ERROR - {0}";

    //Min size in bytes of randomly generated salt.
    private const int intMinSalt = 4;

    //Max size in bytes of randomly generated salt.
    private const int intMaxSalt = 8;

    //Size in bytes of Hash result.  MD5 returns a 128 bit hash.
    private const int intHashSize = 16;

    //Size in bytes of the key length.  Rijndael takes either a 128, 192, or 256 bit key.  
    //If it is under this, pad with chrKeyFill. If it is over this, truncate to the length.
    private const int intKeySize = 32;

    //null char
    private static char vbNullChar = Convert.ToChar(0);

    //Encrypt a String with Rijndael symmetric encryption.
    public static string Encrypt(string strPlainText, string strKey)
    {
        try
        {
            byte[] bytPlainText;
            byte[] bytKey;
            byte[] bytEncoded;
          
            strPlainText = strPlainText.Replace(vbNullChar.ToString(), String.Empty);

            bytPlainText = Encoding.UTF8.GetBytes(strPlainText);
            bytKey = ConvertKeyToBytes(strKey);

            MemoryStream objMemoryStream = new MemoryStream();
            RijndaelManaged objRijndaelManaged = new RijndaelManaged();

            CryptoStream objCryptoStream = new CryptoStream(objMemoryStream,
                objRijndaelManaged.CreateEncryptor(bytKey, bytIV),
                CryptoStreamMode.Write);

            objCryptoStream.Write(bytPlainText, 0, bytPlainText.Length);
            objCryptoStream.FlushFinalBlock();

            bytEncoded = objMemoryStream.ToArray();
            objMemoryStream.Close();
            objCryptoStream.Close();

            return Convert.ToBase64String(bytEncoded);
        }
        catch (Exception ex)
        {
            return String.Format(strTextErrorString, ex.Message);
        }
    }

    //Decrypt a String with Rijndael symmetric encryption.
    public static string Decrypt(string strCryptText, string strKey)
    {
        try
        {
            byte[] bytCryptText;
            byte[] bytKey;
           
            bytCryptText = Convert.FromBase64String(strCryptText);
            bytKey = ConvertKeyToBytes(strKey);

            byte[] bytTemp = new byte[bytCryptText.Length];

            MemoryStream objMemoryStream = new MemoryStream(bytCryptText);
            RijndaelManaged objRijndaelManaged = new RijndaelManaged();
            
            CryptoStream objCryptoStream = new CryptoStream(objMemoryStream,
                objRijndaelManaged.CreateDecryptor(bytKey, bytIV),
                CryptoStreamMode.Read);

             objCryptoStream.Read(bytTemp, 0, bytTemp.Length);

            objMemoryStream.Close();
            objCryptoStream.Close();

            return Encoding.UTF8.GetString(bytTemp).Replace(vbNullChar.ToString(), String.Empty);
        }
        catch (Exception ex)
        {
            return String.Format(strTextErrorString, ex.Message);
        }
    }

    //A function to convert a string into a 32 byte key. 
    private static byte[] ConvertKeyToBytes(string strKey) 
    {
        try
        { 
            int intLength = strKey.Length;

            if (intLength < intKeySize)
            {
                strKey = strKey.PadRight(intKeySize, chrKeyFill);
            }
            else
            {
                strKey = strKey.Substring(0, intKeySize);
            }

            return Encoding.UTF8.GetBytes(strKey);  
        }
        catch
        {
            return null;
        }
    }

}
