using System;
using System.Collections.Generic;
using System.Web;


public class Config
{
    public class ConnectionStrings
    {
        public const string FliUserAuth = "data source=172.20.5.79;Database=PropertyPortal-SIM2;user id=usr-propertyportal;password=Mvr&portal2;Min Pool Size=20;Max Pool Size=4096;Connection Lifetime=120;";
    }

    public class CookieSettings
    { 
        public const int CookieTimeoutInMinutes = 20;
        public const bool SlidingExpiration = true;
    }

    public class Crypto
    {
        public const string Key = "AjRtrvr4P4GZpQaGMWQmbaJ6YeFu-DEV"; //32 chars
    }

    public class PreKey
    {
        public const string AccountSessionId = "fL!5355!0N"; 
    }

    public class MyCaptchaKey
    {
        //PROD Key
        public const string PublicKey = "6LdyveUSAAAAAKVFftkBBTRrFwrbOvXkQi-d5ATy";
        public const string PrivateKey = "6LdyveUSAAAAAOc9Ehi91havl1g1NKebFyS8hDii";

        //DEV Key
        //public const string PublicKey = "6LcV180SAAAAAHdl3D5nnwjDqwHR873Y0ywIkWJm";
        //public const string PrivateKey = "6LcV180SAAAAALCnbMGVqVkO7wjSyyYg6zezPE_u";
    }
}
