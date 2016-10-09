using System;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Collections.Generic;
using System.Text;
using FliAuthLib.FliAuthService;

namespace FliAuthLib
{
    public class AuthService
    {
        private AuthenticationService _priwsService;

        public AuthService()
        {
            _priwsService = new AuthenticationService();
        }

        public WebUser Authenticate(string _parstrUsername, string _parstrPassword, string _parstrComputerName, string _parstrIpAddr, out ServiceError _parseError)
        {
            return _priwsService.Authenticate(_parstrUsername, _parstrPassword, _parstrComputerName, _parstrIpAddr, out _parseError);
        }

        public UserStatus ValidateTokenAndRequest(string _parstrToken, string _parstrRequestId)
        {
            return _priwsService.ValidateTokenAndRequest(_parstrToken, _parstrRequestId);
        }

        public WebUser GetUserByToken(string _parstrToken)
        {
            return _priwsService.GetUserByToken(_parstrToken);
        }
    }
}