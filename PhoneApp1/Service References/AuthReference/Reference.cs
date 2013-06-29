﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18046
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This code was auto-generated by Microsoft.Silverlight.Phone.ServiceReference, version 3.7.0.0
// 
namespace SpeechApp.AuthReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://asp.net/ApplicationServices/v200", ConfigurationName="AuthReference.AuthenticationService")]
    public interface AuthenticationService {
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://asp.net/ApplicationServices/v200/AuthenticationService/ValidateUser", ReplyAction="http://asp.net/ApplicationServices/v200/AuthenticationService/ValidateUserRespons" +
            "e")]
        System.IAsyncResult BeginValidateUser(string username, string password, string customCredential, System.AsyncCallback callback, object asyncState);
        
        bool EndValidateUser(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://asp.net/ApplicationServices/v200/AuthenticationService/Login", ReplyAction="http://asp.net/ApplicationServices/v200/AuthenticationService/LoginResponse")]
        System.IAsyncResult BeginLogin(string username, string password, string customCredential, bool isPersistent, System.AsyncCallback callback, object asyncState);
        
        bool EndLogin(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://asp.net/ApplicationServices/v200/AuthenticationService/IsLoggedIn", ReplyAction="http://asp.net/ApplicationServices/v200/AuthenticationService/IsLoggedInResponse")]
        System.IAsyncResult BeginIsLoggedIn(System.AsyncCallback callback, object asyncState);
        
        bool EndIsLoggedIn(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://asp.net/ApplicationServices/v200/AuthenticationService/Logout", ReplyAction="http://asp.net/ApplicationServices/v200/AuthenticationService/LogoutResponse")]
        System.IAsyncResult BeginLogout(System.AsyncCallback callback, object asyncState);
        
        void EndLogout(System.IAsyncResult result);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface AuthenticationServiceChannel : SpeechApp.AuthReference.AuthenticationService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ValidateUserCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public ValidateUserCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public bool Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class LoginCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public LoginCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public bool Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class IsLoggedInCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public IsLoggedInCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public bool Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class AuthenticationServiceClient : System.ServiceModel.ClientBase<SpeechApp.AuthReference.AuthenticationService>, SpeechApp.AuthReference.AuthenticationService {
        
        private BeginOperationDelegate onBeginValidateUserDelegate;
        
        private EndOperationDelegate onEndValidateUserDelegate;
        
        private System.Threading.SendOrPostCallback onValidateUserCompletedDelegate;
        
        private BeginOperationDelegate onBeginLoginDelegate;
        
        private EndOperationDelegate onEndLoginDelegate;
        
        private System.Threading.SendOrPostCallback onLoginCompletedDelegate;
        
        private BeginOperationDelegate onBeginIsLoggedInDelegate;
        
        private EndOperationDelegate onEndIsLoggedInDelegate;
        
        private System.Threading.SendOrPostCallback onIsLoggedInCompletedDelegate;
        
        private BeginOperationDelegate onBeginLogoutDelegate;
        
        private EndOperationDelegate onEndLogoutDelegate;
        
        private System.Threading.SendOrPostCallback onLogoutCompletedDelegate;
        
        private BeginOperationDelegate onBeginOpenDelegate;
        
        private EndOperationDelegate onEndOpenDelegate;
        
        private System.Threading.SendOrPostCallback onOpenCompletedDelegate;
        
        private BeginOperationDelegate onBeginCloseDelegate;
        
        private EndOperationDelegate onEndCloseDelegate;
        
        private System.Threading.SendOrPostCallback onCloseCompletedDelegate;
        
        public AuthenticationServiceClient() {
        }
        
        public AuthenticationServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public AuthenticationServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AuthenticationServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AuthenticationServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Net.CookieContainer CookieContainer {
            get {
                System.ServiceModel.Channels.IHttpCookieContainerManager httpCookieContainerManager = this.InnerChannel.GetProperty<System.ServiceModel.Channels.IHttpCookieContainerManager>();
                if ((httpCookieContainerManager != null)) {
                    return httpCookieContainerManager.CookieContainer;
                }
                else {
                    return null;
                }
            }
            set {
                System.ServiceModel.Channels.IHttpCookieContainerManager httpCookieContainerManager = this.InnerChannel.GetProperty<System.ServiceModel.Channels.IHttpCookieContainerManager>();
                if ((httpCookieContainerManager != null)) {
                    httpCookieContainerManager.CookieContainer = value;
                }
                else {
                    throw new System.InvalidOperationException("Unable to set the CookieContainer. Please make sure the binding contains an HttpC" +
                            "ookieContainerBindingElement.");
                }
            }
        }
        
        public event System.EventHandler<ValidateUserCompletedEventArgs> ValidateUserCompleted;
        
        public event System.EventHandler<LoginCompletedEventArgs> LoginCompleted;
        
        public event System.EventHandler<IsLoggedInCompletedEventArgs> IsLoggedInCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> LogoutCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> OpenCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> CloseCompleted;
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult SpeechApp.AuthReference.AuthenticationService.BeginValidateUser(string username, string password, string customCredential, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginValidateUser(username, password, customCredential, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        bool SpeechApp.AuthReference.AuthenticationService.EndValidateUser(System.IAsyncResult result) {
            return base.Channel.EndValidateUser(result);
        }
        
        private System.IAsyncResult OnBeginValidateUser(object[] inValues, System.AsyncCallback callback, object asyncState) {
            string username = ((string)(inValues[0]));
            string password = ((string)(inValues[1]));
            string customCredential = ((string)(inValues[2]));
            return ((SpeechApp.AuthReference.AuthenticationService)(this)).BeginValidateUser(username, password, customCredential, callback, asyncState);
        }
        
        private object[] OnEndValidateUser(System.IAsyncResult result) {
            bool retVal = ((SpeechApp.AuthReference.AuthenticationService)(this)).EndValidateUser(result);
            return new object[] {
                    retVal};
        }
        
        private void OnValidateUserCompleted(object state) {
            if ((this.ValidateUserCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.ValidateUserCompleted(this, new ValidateUserCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void ValidateUserAsync(string username, string password, string customCredential) {
            this.ValidateUserAsync(username, password, customCredential, null);
        }
        
        public void ValidateUserAsync(string username, string password, string customCredential, object userState) {
            if ((this.onBeginValidateUserDelegate == null)) {
                this.onBeginValidateUserDelegate = new BeginOperationDelegate(this.OnBeginValidateUser);
            }
            if ((this.onEndValidateUserDelegate == null)) {
                this.onEndValidateUserDelegate = new EndOperationDelegate(this.OnEndValidateUser);
            }
            if ((this.onValidateUserCompletedDelegate == null)) {
                this.onValidateUserCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnValidateUserCompleted);
            }
            base.InvokeAsync(this.onBeginValidateUserDelegate, new object[] {
                        username,
                        password,
                        customCredential}, this.onEndValidateUserDelegate, this.onValidateUserCompletedDelegate, userState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult SpeechApp.AuthReference.AuthenticationService.BeginLogin(string username, string password, string customCredential, bool isPersistent, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginLogin(username, password, customCredential, isPersistent, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        bool SpeechApp.AuthReference.AuthenticationService.EndLogin(System.IAsyncResult result) {
            return base.Channel.EndLogin(result);
        }
        
        private System.IAsyncResult OnBeginLogin(object[] inValues, System.AsyncCallback callback, object asyncState) {
            string username = ((string)(inValues[0]));
            string password = ((string)(inValues[1]));
            string customCredential = ((string)(inValues[2]));
            bool isPersistent = ((bool)(inValues[3]));
            return ((SpeechApp.AuthReference.AuthenticationService)(this)).BeginLogin(username, password, customCredential, isPersistent, callback, asyncState);
        }
        
        private object[] OnEndLogin(System.IAsyncResult result) {
            bool retVal = ((SpeechApp.AuthReference.AuthenticationService)(this)).EndLogin(result);
            return new object[] {
                    retVal};
        }
        
        private void OnLoginCompleted(object state) {
            if ((this.LoginCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.LoginCompleted(this, new LoginCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void LoginAsync(string username, string password, string customCredential, bool isPersistent) {
            this.LoginAsync(username, password, customCredential, isPersistent, null);
        }
        
        public void LoginAsync(string username, string password, string customCredential, bool isPersistent, object userState) {
            if ((this.onBeginLoginDelegate == null)) {
                this.onBeginLoginDelegate = new BeginOperationDelegate(this.OnBeginLogin);
            }
            if ((this.onEndLoginDelegate == null)) {
                this.onEndLoginDelegate = new EndOperationDelegate(this.OnEndLogin);
            }
            if ((this.onLoginCompletedDelegate == null)) {
                this.onLoginCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnLoginCompleted);
            }
            base.InvokeAsync(this.onBeginLoginDelegate, new object[] {
                        username,
                        password,
                        customCredential,
                        isPersistent}, this.onEndLoginDelegate, this.onLoginCompletedDelegate, userState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult SpeechApp.AuthReference.AuthenticationService.BeginIsLoggedIn(System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginIsLoggedIn(callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        bool SpeechApp.AuthReference.AuthenticationService.EndIsLoggedIn(System.IAsyncResult result) {
            return base.Channel.EndIsLoggedIn(result);
        }
        
        private System.IAsyncResult OnBeginIsLoggedIn(object[] inValues, System.AsyncCallback callback, object asyncState) {
            return ((SpeechApp.AuthReference.AuthenticationService)(this)).BeginIsLoggedIn(callback, asyncState);
        }
        
        private object[] OnEndIsLoggedIn(System.IAsyncResult result) {
            bool retVal = ((SpeechApp.AuthReference.AuthenticationService)(this)).EndIsLoggedIn(result);
            return new object[] {
                    retVal};
        }
        
        private void OnIsLoggedInCompleted(object state) {
            if ((this.IsLoggedInCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.IsLoggedInCompleted(this, new IsLoggedInCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void IsLoggedInAsync() {
            this.IsLoggedInAsync(null);
        }
        
        public void IsLoggedInAsync(object userState) {
            if ((this.onBeginIsLoggedInDelegate == null)) {
                this.onBeginIsLoggedInDelegate = new BeginOperationDelegate(this.OnBeginIsLoggedIn);
            }
            if ((this.onEndIsLoggedInDelegate == null)) {
                this.onEndIsLoggedInDelegate = new EndOperationDelegate(this.OnEndIsLoggedIn);
            }
            if ((this.onIsLoggedInCompletedDelegate == null)) {
                this.onIsLoggedInCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnIsLoggedInCompleted);
            }
            base.InvokeAsync(this.onBeginIsLoggedInDelegate, null, this.onEndIsLoggedInDelegate, this.onIsLoggedInCompletedDelegate, userState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult SpeechApp.AuthReference.AuthenticationService.BeginLogout(System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginLogout(callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        void SpeechApp.AuthReference.AuthenticationService.EndLogout(System.IAsyncResult result) {
            base.Channel.EndLogout(result);
        }
        
        private System.IAsyncResult OnBeginLogout(object[] inValues, System.AsyncCallback callback, object asyncState) {
            return ((SpeechApp.AuthReference.AuthenticationService)(this)).BeginLogout(callback, asyncState);
        }
        
        private object[] OnEndLogout(System.IAsyncResult result) {
            ((SpeechApp.AuthReference.AuthenticationService)(this)).EndLogout(result);
            return null;
        }
        
        private void OnLogoutCompleted(object state) {
            if ((this.LogoutCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.LogoutCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void LogoutAsync() {
            this.LogoutAsync(null);
        }
        
        public void LogoutAsync(object userState) {
            if ((this.onBeginLogoutDelegate == null)) {
                this.onBeginLogoutDelegate = new BeginOperationDelegate(this.OnBeginLogout);
            }
            if ((this.onEndLogoutDelegate == null)) {
                this.onEndLogoutDelegate = new EndOperationDelegate(this.OnEndLogout);
            }
            if ((this.onLogoutCompletedDelegate == null)) {
                this.onLogoutCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnLogoutCompleted);
            }
            base.InvokeAsync(this.onBeginLogoutDelegate, null, this.onEndLogoutDelegate, this.onLogoutCompletedDelegate, userState);
        }
        
        private System.IAsyncResult OnBeginOpen(object[] inValues, System.AsyncCallback callback, object asyncState) {
            return ((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(callback, asyncState);
        }
        
        private object[] OnEndOpen(System.IAsyncResult result) {
            ((System.ServiceModel.ICommunicationObject)(this)).EndOpen(result);
            return null;
        }
        
        private void OnOpenCompleted(object state) {
            if ((this.OpenCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.OpenCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void OpenAsync() {
            this.OpenAsync(null);
        }
        
        public void OpenAsync(object userState) {
            if ((this.onBeginOpenDelegate == null)) {
                this.onBeginOpenDelegate = new BeginOperationDelegate(this.OnBeginOpen);
            }
            if ((this.onEndOpenDelegate == null)) {
                this.onEndOpenDelegate = new EndOperationDelegate(this.OnEndOpen);
            }
            if ((this.onOpenCompletedDelegate == null)) {
                this.onOpenCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnOpenCompleted);
            }
            base.InvokeAsync(this.onBeginOpenDelegate, null, this.onEndOpenDelegate, this.onOpenCompletedDelegate, userState);
        }
        
        private System.IAsyncResult OnBeginClose(object[] inValues, System.AsyncCallback callback, object asyncState) {
            return ((System.ServiceModel.ICommunicationObject)(this)).BeginClose(callback, asyncState);
        }
        
        private object[] OnEndClose(System.IAsyncResult result) {
            ((System.ServiceModel.ICommunicationObject)(this)).EndClose(result);
            return null;
        }
        
        private void OnCloseCompleted(object state) {
            if ((this.CloseCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.CloseCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void CloseAsync() {
            this.CloseAsync(null);
        }
        
        public void CloseAsync(object userState) {
            if ((this.onBeginCloseDelegate == null)) {
                this.onBeginCloseDelegate = new BeginOperationDelegate(this.OnBeginClose);
            }
            if ((this.onEndCloseDelegate == null)) {
                this.onEndCloseDelegate = new EndOperationDelegate(this.OnEndClose);
            }
            if ((this.onCloseCompletedDelegate == null)) {
                this.onCloseCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnCloseCompleted);
            }
            base.InvokeAsync(this.onBeginCloseDelegate, null, this.onEndCloseDelegate, this.onCloseCompletedDelegate, userState);
        }
        
        protected override SpeechApp.AuthReference.AuthenticationService CreateChannel() {
            return new AuthenticationServiceClientChannel(this);
        }
        
        private class AuthenticationServiceClientChannel : ChannelBase<SpeechApp.AuthReference.AuthenticationService>, SpeechApp.AuthReference.AuthenticationService {
            
            public AuthenticationServiceClientChannel(System.ServiceModel.ClientBase<SpeechApp.AuthReference.AuthenticationService> client) : 
                    base(client) {
            }
            
            public System.IAsyncResult BeginValidateUser(string username, string password, string customCredential, System.AsyncCallback callback, object asyncState) {
                object[] _args = new object[3];
                _args[0] = username;
                _args[1] = password;
                _args[2] = customCredential;
                System.IAsyncResult _result = base.BeginInvoke("ValidateUser", _args, callback, asyncState);
                return _result;
            }
            
            public bool EndValidateUser(System.IAsyncResult result) {
                object[] _args = new object[0];
                bool _result = ((bool)(base.EndInvoke("ValidateUser", _args, result)));
                return _result;
            }
            
            public System.IAsyncResult BeginLogin(string username, string password, string customCredential, bool isPersistent, System.AsyncCallback callback, object asyncState) {
                object[] _args = new object[4];
                _args[0] = username;
                _args[1] = password;
                _args[2] = customCredential;
                _args[3] = isPersistent;
                System.IAsyncResult _result = base.BeginInvoke("Login", _args, callback, asyncState);
                return _result;
            }
            
            public bool EndLogin(System.IAsyncResult result) {
                object[] _args = new object[0];
                bool _result = ((bool)(base.EndInvoke("Login", _args, result)));
                return _result;
            }
            
            public System.IAsyncResult BeginIsLoggedIn(System.AsyncCallback callback, object asyncState) {
                object[] _args = new object[0];
                System.IAsyncResult _result = base.BeginInvoke("IsLoggedIn", _args, callback, asyncState);
                return _result;
            }
            
            public bool EndIsLoggedIn(System.IAsyncResult result) {
                object[] _args = new object[0];
                bool _result = ((bool)(base.EndInvoke("IsLoggedIn", _args, result)));
                return _result;
            }
            
            public System.IAsyncResult BeginLogout(System.AsyncCallback callback, object asyncState) {
                object[] _args = new object[0];
                System.IAsyncResult _result = base.BeginInvoke("Logout", _args, callback, asyncState);
                return _result;
            }
            
            public void EndLogout(System.IAsyncResult result) {
                object[] _args = new object[0];
                base.EndInvoke("Logout", _args, result);
            }
        }
    }
}
