﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.42000.
// 
#pragma warning disable 1591

namespace FliAuthLib.FliAuthService {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="AuthenticationServiceSoap", Namespace="https://kiosk.filinvest.com.ph/SSO-DEV/services")]
    public partial class AuthenticationService : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback AuthenticateOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetUserByTokenOperationCompleted;
        
        private System.Threading.SendOrPostCallback IsUserLoggedInOperationCompleted;
        
        private System.Threading.SendOrPostCallback IsValidRequestOperationCompleted;
        
        private System.Threading.SendOrPostCallback ValidateTokenAndRequestOperationCompleted;
        
        private System.Threading.SendOrPostCallback AuthenticateAppOperationCompleted;
        
        private System.Threading.SendOrPostCallback AuthenticateAppSessionOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public AuthenticationService() {
            this.Url = "https://kiosk.filinvest.com.ph/SSO-DEV/AuthenticationService.asmx";
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event AuthenticateCompletedEventHandler AuthenticateCompleted;
        
        /// <remarks/>
        public event GetUserByTokenCompletedEventHandler GetUserByTokenCompleted;
        
        /// <remarks/>
        public event IsUserLoggedInCompletedEventHandler IsUserLoggedInCompleted;
        
        /// <remarks/>
        public event IsValidRequestCompletedEventHandler IsValidRequestCompleted;
        
        /// <remarks/>
        public event ValidateTokenAndRequestCompletedEventHandler ValidateTokenAndRequestCompleted;
        
        /// <remarks/>
        public event AuthenticateAppCompletedEventHandler AuthenticateAppCompleted;
        
        /// <remarks/>
        public event AuthenticateAppSessionCompletedEventHandler AuthenticateAppSessionCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("https://kiosk.filinvest.com.ph/SSO-DEV/services/Authenticate", RequestNamespace="https://kiosk.filinvest.com.ph/SSO-DEV/services", ResponseNamespace="https://kiosk.filinvest.com.ph/SSO-DEV/services", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public WebUser Authenticate(string _parstrUserName, string _parstrPassword, string _parstrComputerName, string _parstrIpAddr, out ServiceError _parseError) {
            object[] results = this.Invoke("Authenticate", new object[] {
                        _parstrUserName,
                        _parstrPassword,
                        _parstrComputerName,
                        _parstrIpAddr});
            _parseError = ((ServiceError)(results[1]));
            return ((WebUser)(results[0]));
        }
        
        /// <remarks/>
        public void AuthenticateAsync(string _parstrUserName, string _parstrPassword, string _parstrComputerName, string _parstrIpAddr) {
            this.AuthenticateAsync(_parstrUserName, _parstrPassword, _parstrComputerName, _parstrIpAddr, null);
        }
        
        /// <remarks/>
        public void AuthenticateAsync(string _parstrUserName, string _parstrPassword, string _parstrComputerName, string _parstrIpAddr, object userState) {
            if ((this.AuthenticateOperationCompleted == null)) {
                this.AuthenticateOperationCompleted = new System.Threading.SendOrPostCallback(this.OnAuthenticateOperationCompleted);
            }
            this.InvokeAsync("Authenticate", new object[] {
                        _parstrUserName,
                        _parstrPassword,
                        _parstrComputerName,
                        _parstrIpAddr}, this.AuthenticateOperationCompleted, userState);
        }
        
        private void OnAuthenticateOperationCompleted(object arg) {
            if ((this.AuthenticateCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.AuthenticateCompleted(this, new AuthenticateCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("https://kiosk.filinvest.com.ph/SSO-DEV/services/GetUserByToken", RequestNamespace="https://kiosk.filinvest.com.ph/SSO-DEV/services", ResponseNamespace="https://kiosk.filinvest.com.ph/SSO-DEV/services", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public WebUser GetUserByToken(string _parstrToken) {
            object[] results = this.Invoke("GetUserByToken", new object[] {
                        _parstrToken});
            return ((WebUser)(results[0]));
        }
        
        /// <remarks/>
        public void GetUserByTokenAsync(string _parstrToken) {
            this.GetUserByTokenAsync(_parstrToken, null);
        }
        
        /// <remarks/>
        public void GetUserByTokenAsync(string _parstrToken, object userState) {
            if ((this.GetUserByTokenOperationCompleted == null)) {
                this.GetUserByTokenOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetUserByTokenOperationCompleted);
            }
            this.InvokeAsync("GetUserByToken", new object[] {
                        _parstrToken}, this.GetUserByTokenOperationCompleted, userState);
        }
        
        private void OnGetUserByTokenOperationCompleted(object arg) {
            if ((this.GetUserByTokenCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetUserByTokenCompleted(this, new GetUserByTokenCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("https://kiosk.filinvest.com.ph/SSO-DEV/services/IsUserLoggedIn", RequestNamespace="https://kiosk.filinvest.com.ph/SSO-DEV/services", ResponseNamespace="https://kiosk.filinvest.com.ph/SSO-DEV/services", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool IsUserLoggedIn(string _parstrToken) {
            object[] results = this.Invoke("IsUserLoggedIn", new object[] {
                        _parstrToken});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void IsUserLoggedInAsync(string _parstrToken) {
            this.IsUserLoggedInAsync(_parstrToken, null);
        }
        
        /// <remarks/>
        public void IsUserLoggedInAsync(string _parstrToken, object userState) {
            if ((this.IsUserLoggedInOperationCompleted == null)) {
                this.IsUserLoggedInOperationCompleted = new System.Threading.SendOrPostCallback(this.OnIsUserLoggedInOperationCompleted);
            }
            this.InvokeAsync("IsUserLoggedIn", new object[] {
                        _parstrToken}, this.IsUserLoggedInOperationCompleted, userState);
        }
        
        private void OnIsUserLoggedInOperationCompleted(object arg) {
            if ((this.IsUserLoggedInCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.IsUserLoggedInCompleted(this, new IsUserLoggedInCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("https://kiosk.filinvest.com.ph/SSO-DEV/services/IsValidRequest", RequestNamespace="https://kiosk.filinvest.com.ph/SSO-DEV/services", ResponseNamespace="https://kiosk.filinvest.com.ph/SSO-DEV/services", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool IsValidRequest(string _parstrRedirectId) {
            object[] results = this.Invoke("IsValidRequest", new object[] {
                        _parstrRedirectId});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void IsValidRequestAsync(string _parstrRedirectId) {
            this.IsValidRequestAsync(_parstrRedirectId, null);
        }
        
        /// <remarks/>
        public void IsValidRequestAsync(string _parstrRedirectId, object userState) {
            if ((this.IsValidRequestOperationCompleted == null)) {
                this.IsValidRequestOperationCompleted = new System.Threading.SendOrPostCallback(this.OnIsValidRequestOperationCompleted);
            }
            this.InvokeAsync("IsValidRequest", new object[] {
                        _parstrRedirectId}, this.IsValidRequestOperationCompleted, userState);
        }
        
        private void OnIsValidRequestOperationCompleted(object arg) {
            if ((this.IsValidRequestCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.IsValidRequestCompleted(this, new IsValidRequestCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("https://kiosk.filinvest.com.ph/SSO-DEV/services/ValidateTokenAndRequest", RequestNamespace="https://kiosk.filinvest.com.ph/SSO-DEV/services", ResponseNamespace="https://kiosk.filinvest.com.ph/SSO-DEV/services", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public UserStatus ValidateTokenAndRequest(string _parstrToken, string _parstrRequestId) {
            object[] results = this.Invoke("ValidateTokenAndRequest", new object[] {
                        _parstrToken,
                        _parstrRequestId});
            return ((UserStatus)(results[0]));
        }
        
        /// <remarks/>
        public void ValidateTokenAndRequestAsync(string _parstrToken, string _parstrRequestId) {
            this.ValidateTokenAndRequestAsync(_parstrToken, _parstrRequestId, null);
        }
        
        /// <remarks/>
        public void ValidateTokenAndRequestAsync(string _parstrToken, string _parstrRequestId, object userState) {
            if ((this.ValidateTokenAndRequestOperationCompleted == null)) {
                this.ValidateTokenAndRequestOperationCompleted = new System.Threading.SendOrPostCallback(this.OnValidateTokenAndRequestOperationCompleted);
            }
            this.InvokeAsync("ValidateTokenAndRequest", new object[] {
                        _parstrToken,
                        _parstrRequestId}, this.ValidateTokenAndRequestOperationCompleted, userState);
        }
        
        private void OnValidateTokenAndRequestOperationCompleted(object arg) {
            if ((this.ValidateTokenAndRequestCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ValidateTokenAndRequestCompleted(this, new ValidateTokenAndRequestCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("https://kiosk.filinvest.com.ph/SSO-DEV/services/AuthenticateApp", RequestNamespace="https://kiosk.filinvest.com.ph/SSO-DEV/services", ResponseNamespace="https://kiosk.filinvest.com.ph/SSO-DEV/services", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public WebUserLogInfo AuthenticateApp(string _parstrUserName, string _parstrPassword, bool _parblnIsRemember) {
            object[] results = this.Invoke("AuthenticateApp", new object[] {
                        _parstrUserName,
                        _parstrPassword,
                        _parblnIsRemember});
            return ((WebUserLogInfo)(results[0]));
        }
        
        /// <remarks/>
        public void AuthenticateAppAsync(string _parstrUserName, string _parstrPassword, bool _parblnIsRemember) {
            this.AuthenticateAppAsync(_parstrUserName, _parstrPassword, _parblnIsRemember, null);
        }
        
        /// <remarks/>
        public void AuthenticateAppAsync(string _parstrUserName, string _parstrPassword, bool _parblnIsRemember, object userState) {
            if ((this.AuthenticateAppOperationCompleted == null)) {
                this.AuthenticateAppOperationCompleted = new System.Threading.SendOrPostCallback(this.OnAuthenticateAppOperationCompleted);
            }
            this.InvokeAsync("AuthenticateApp", new object[] {
                        _parstrUserName,
                        _parstrPassword,
                        _parblnIsRemember}, this.AuthenticateAppOperationCompleted, userState);
        }
        
        private void OnAuthenticateAppOperationCompleted(object arg) {
            if ((this.AuthenticateAppCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.AuthenticateAppCompleted(this, new AuthenticateAppCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("https://kiosk.filinvest.com.ph/SSO-DEV/services/AuthenticateAppSession", RequestNamespace="https://kiosk.filinvest.com.ph/SSO-DEV/services", ResponseNamespace="https://kiosk.filinvest.com.ph/SSO-DEV/services", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public WebUserLogInfo AuthenticateAppSession(string _parstrSessionId, bool _parstrIsEncoded) {
            object[] results = this.Invoke("AuthenticateAppSession", new object[] {
                        _parstrSessionId,
                        _parstrIsEncoded});
            return ((WebUserLogInfo)(results[0]));
        }
        
        /// <remarks/>
        public void AuthenticateAppSessionAsync(string _parstrSessionId, bool _parstrIsEncoded) {
            this.AuthenticateAppSessionAsync(_parstrSessionId, _parstrIsEncoded, null);
        }
        
        /// <remarks/>
        public void AuthenticateAppSessionAsync(string _parstrSessionId, bool _parstrIsEncoded, object userState) {
            if ((this.AuthenticateAppSessionOperationCompleted == null)) {
                this.AuthenticateAppSessionOperationCompleted = new System.Threading.SendOrPostCallback(this.OnAuthenticateAppSessionOperationCompleted);
            }
            this.InvokeAsync("AuthenticateAppSession", new object[] {
                        _parstrSessionId,
                        _parstrIsEncoded}, this.AuthenticateAppSessionOperationCompleted, userState);
        }
        
        private void OnAuthenticateAppSessionOperationCompleted(object arg) {
            if ((this.AuthenticateAppSessionCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.AuthenticateAppSessionCompleted(this, new AuthenticateAppSessionCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(WebUserLogInfo))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://kiosk.filinvest.com.ph/SSO-DEV/services")]
    public partial class WebUser {
        
        private string sessionIdField;
        
        private string tokenField;
        
        private string usernameField;
        
        private string sellerCodeField;
        
        private string userLevelField;
        
        private bool loggedFromAppField;
        
        private System.DateTime expiresOnField;
        
        private string sellerAllocationField;
        
        private string sellerSalesChannelField;
        
        private string sellerClassificationField;
        
        private string sellerLevelField;
        
        private string sellerPositionField;
        
        /// <remarks/>
        public string SessionId {
            get {
                return this.sessionIdField;
            }
            set {
                this.sessionIdField = value;
            }
        }
        
        /// <remarks/>
        public string Token {
            get {
                return this.tokenField;
            }
            set {
                this.tokenField = value;
            }
        }
        
        /// <remarks/>
        public string Username {
            get {
                return this.usernameField;
            }
            set {
                this.usernameField = value;
            }
        }
        
        /// <remarks/>
        public string SellerCode {
            get {
                return this.sellerCodeField;
            }
            set {
                this.sellerCodeField = value;
            }
        }
        
        /// <remarks/>
        public string UserLevel {
            get {
                return this.userLevelField;
            }
            set {
                this.userLevelField = value;
            }
        }
        
        /// <remarks/>
        public bool LoggedFromApp {
            get {
                return this.loggedFromAppField;
            }
            set {
                this.loggedFromAppField = value;
            }
        }
        
        /// <remarks/>
        public System.DateTime ExpiresOn {
            get {
                return this.expiresOnField;
            }
            set {
                this.expiresOnField = value;
            }
        }
        
        /// <remarks/>
        public string SellerAllocation {
            get {
                return this.sellerAllocationField;
            }
            set {
                this.sellerAllocationField = value;
            }
        }
        
        /// <remarks/>
        public string SellerSalesChannel {
            get {
                return this.sellerSalesChannelField;
            }
            set {
                this.sellerSalesChannelField = value;
            }
        }
        
        /// <remarks/>
        public string SellerClassification {
            get {
                return this.sellerClassificationField;
            }
            set {
                this.sellerClassificationField = value;
            }
        }
        
        /// <remarks/>
        public string SellerLevel {
            get {
                return this.sellerLevelField;
            }
            set {
                this.sellerLevelField = value;
            }
        }
        
        /// <remarks/>
        public string SellerPosition {
            get {
                return this.sellerPositionField;
            }
            set {
                this.sellerPositionField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://kiosk.filinvest.com.ph/SSO-DEV/services")]
    public partial class UserStatus {
        
        private bool userLoggedInField;
        
        private bool requestIdValidField;
        
        /// <remarks/>
        public bool UserLoggedIn {
            get {
                return this.userLoggedInField;
            }
            set {
                this.userLoggedInField = value;
            }
        }
        
        /// <remarks/>
        public bool RequestIdValid {
            get {
                return this.requestIdValidField;
            }
            set {
                this.requestIdValidField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://kiosk.filinvest.com.ph/SSO-DEV/services")]
    public partial class ServiceError {
        
        private int errorNumberField;
        
        private string errorMessageField;
        
        /// <remarks/>
        public int ErrorNumber {
            get {
                return this.errorNumberField;
            }
            set {
                this.errorNumberField = value;
            }
        }
        
        /// <remarks/>
        public string ErrorMessage {
            get {
                return this.errorMessageField;
            }
            set {
                this.errorMessageField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://kiosk.filinvest.com.ph/SSO-DEV/services")]
    public partial class WebUserLogInfo : WebUser {
        
        private int errorNumberField;
        
        private string errorMessageField;
        
        /// <remarks/>
        public int ErrorNumber {
            get {
                return this.errorNumberField;
            }
            set {
                this.errorNumberField = value;
            }
        }
        
        /// <remarks/>
        public string ErrorMessage {
            get {
                return this.errorMessageField;
            }
            set {
                this.errorMessageField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    public delegate void AuthenticateCompletedEventHandler(object sender, AuthenticateCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class AuthenticateCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal AuthenticateCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public WebUser Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((WebUser)(this.results[0]));
            }
        }
        
        /// <remarks/>
        public ServiceError _parseError {
            get {
                this.RaiseExceptionIfNecessary();
                return ((ServiceError)(this.results[1]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    public delegate void GetUserByTokenCompletedEventHandler(object sender, GetUserByTokenCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetUserByTokenCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetUserByTokenCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public WebUser Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((WebUser)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    public delegate void IsUserLoggedInCompletedEventHandler(object sender, IsUserLoggedInCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class IsUserLoggedInCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal IsUserLoggedInCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public bool Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    public delegate void IsValidRequestCompletedEventHandler(object sender, IsValidRequestCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class IsValidRequestCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal IsValidRequestCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public bool Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    public delegate void ValidateTokenAndRequestCompletedEventHandler(object sender, ValidateTokenAndRequestCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ValidateTokenAndRequestCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ValidateTokenAndRequestCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public UserStatus Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((UserStatus)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    public delegate void AuthenticateAppCompletedEventHandler(object sender, AuthenticateAppCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class AuthenticateAppCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal AuthenticateAppCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public WebUserLogInfo Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((WebUserLogInfo)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    public delegate void AuthenticateAppSessionCompletedEventHandler(object sender, AuthenticateAppSessionCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class AuthenticateAppSessionCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal AuthenticateAppSessionCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public WebUserLogInfo Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((WebUserLogInfo)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591