﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.17626
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This code was auto-generated by Microsoft.Silverlight.ServiceReference, version 5.0.61118.0
// 
namespace Silverlight2Chat.LinqChatReference {
    using System.Runtime.Serialization;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="MessageContract", Namespace="http://schemas.datacontract.org/2004/07/Silverlight2Chat.Web")]
    public partial class MessageContract : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string ColorField;
        
        private int MessageIDField;
        
        private string TextField;
        
        private string UserNameField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Color {
            get {
                return this.ColorField;
            }
            set {
                if ((object.ReferenceEquals(this.ColorField, value) != true)) {
                    this.ColorField = value;
                    this.RaisePropertyChanged("Color");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int MessageID {
            get {
                return this.MessageIDField;
            }
            set {
                if ((this.MessageIDField.Equals(value) != true)) {
                    this.MessageIDField = value;
                    this.RaisePropertyChanged("MessageID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Text {
            get {
                return this.TextField;
            }
            set {
                if ((object.ReferenceEquals(this.TextField, value) != true)) {
                    this.TextField = value;
                    this.RaisePropertyChanged("Text");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UserName {
            get {
                return this.UserNameField;
            }
            set {
                if ((object.ReferenceEquals(this.UserNameField, value) != true)) {
                    this.UserNameField = value;
                    this.RaisePropertyChanged("UserName");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="UserContract", Namespace="http://schemas.datacontract.org/2004/07/Silverlight2Chat.Web")]
    public partial class UserContract : object, System.ComponentModel.INotifyPropertyChanged {
        
        private int UserIDField;
        
        private string UserNameField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int UserID {
            get {
                return this.UserIDField;
            }
            set {
                if ((this.UserIDField.Equals(value) != true)) {
                    this.UserIDField = value;
                    this.RaisePropertyChanged("UserID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UserName {
            get {
                return this.UserNameField;
            }
            set {
                if ((object.ReferenceEquals(this.UserNameField, value) != true)) {
                    this.UserNameField = value;
                    this.RaisePropertyChanged("UserName");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="LinqChatReference.ILinqChatService")]
    public interface ILinqChatService {
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://tempuri.org/ILinqChatService/UserExist", ReplyAction="http://tempuri.org/ILinqChatService/UserExistResponse")]
        System.IAsyncResult BeginUserExist(string username, string password, System.AsyncCallback callback, object asyncState);
        
        int EndUserExist(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://tempuri.org/ILinqChatService/GetMessages", ReplyAction="http://tempuri.org/ILinqChatService/GetMessagesResponse")]
        System.IAsyncResult BeginGetMessages(int messageID, int roomID, System.DateTime timeUserJoined, System.AsyncCallback callback, object asyncState);
        
        System.Collections.ObjectModel.ObservableCollection<Silverlight2Chat.LinqChatReference.MessageContract> EndGetMessages(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://tempuri.org/ILinqChatService/InsertMessage", ReplyAction="http://tempuri.org/ILinqChatService/InsertMessageResponse")]
        System.IAsyncResult BeginInsertMessage(int roomID, int userID, System.Nullable<int> toUserID, string messageText, string color, System.AsyncCallback callback, object asyncState);
        
        void EndInsertMessage(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://tempuri.org/ILinqChatService/GetUsers", ReplyAction="http://tempuri.org/ILinqChatService/GetUsersResponse")]
        System.IAsyncResult BeginGetUsers(int roomID, int userID, System.AsyncCallback callback, object asyncState);
        
        System.Collections.ObjectModel.ObservableCollection<Silverlight2Chat.LinqChatReference.UserContract> EndGetUsers(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://tempuri.org/ILinqChatService/LogOutUser", ReplyAction="http://tempuri.org/ILinqChatService/LogOutUserResponse")]
        System.IAsyncResult BeginLogOutUser(int userID, int roomID, string username, System.AsyncCallback callback, object asyncState);
        
        void EndLogOutUser(System.IAsyncResult result);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ILinqChatServiceChannel : Silverlight2Chat.LinqChatReference.ILinqChatService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class UserExistCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public UserExistCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public int Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class GetMessagesCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public GetMessagesCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public System.Collections.ObjectModel.ObservableCollection<Silverlight2Chat.LinqChatReference.MessageContract> Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((System.Collections.ObjectModel.ObservableCollection<Silverlight2Chat.LinqChatReference.MessageContract>)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class GetUsersCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public GetUsersCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public System.Collections.ObjectModel.ObservableCollection<Silverlight2Chat.LinqChatReference.UserContract> Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((System.Collections.ObjectModel.ObservableCollection<Silverlight2Chat.LinqChatReference.UserContract>)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class LinqChatServiceClient : System.ServiceModel.ClientBase<Silverlight2Chat.LinqChatReference.ILinqChatService>, Silverlight2Chat.LinqChatReference.ILinqChatService {
        
        private BeginOperationDelegate onBeginUserExistDelegate;
        
        private EndOperationDelegate onEndUserExistDelegate;
        
        private System.Threading.SendOrPostCallback onUserExistCompletedDelegate;
        
        private BeginOperationDelegate onBeginGetMessagesDelegate;
        
        private EndOperationDelegate onEndGetMessagesDelegate;
        
        private System.Threading.SendOrPostCallback onGetMessagesCompletedDelegate;
        
        private BeginOperationDelegate onBeginInsertMessageDelegate;
        
        private EndOperationDelegate onEndInsertMessageDelegate;
        
        private System.Threading.SendOrPostCallback onInsertMessageCompletedDelegate;
        
        private BeginOperationDelegate onBeginGetUsersDelegate;
        
        private EndOperationDelegate onEndGetUsersDelegate;
        
        private System.Threading.SendOrPostCallback onGetUsersCompletedDelegate;
        
        private BeginOperationDelegate onBeginLogOutUserDelegate;
        
        private EndOperationDelegate onEndLogOutUserDelegate;
        
        private System.Threading.SendOrPostCallback onLogOutUserCompletedDelegate;
        
        private BeginOperationDelegate onBeginOpenDelegate;
        
        private EndOperationDelegate onEndOpenDelegate;
        
        private System.Threading.SendOrPostCallback onOpenCompletedDelegate;
        
        private BeginOperationDelegate onBeginCloseDelegate;
        
        private EndOperationDelegate onEndCloseDelegate;
        
        private System.Threading.SendOrPostCallback onCloseCompletedDelegate;
        
        public LinqChatServiceClient() {
        }
        
        public LinqChatServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public LinqChatServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public LinqChatServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public LinqChatServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
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
                    throw new System.InvalidOperationException("Не удалось установить CookieContainer. Убедитесь, что привязка содержит HttpCooki" +
                            "eContainerBindingElement.");
                }
            }
        }
        
        public event System.EventHandler<UserExistCompletedEventArgs> UserExistCompleted;
        
        public event System.EventHandler<GetMessagesCompletedEventArgs> GetMessagesCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> InsertMessageCompleted;
        
        public event System.EventHandler<GetUsersCompletedEventArgs> GetUsersCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> LogOutUserCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> OpenCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> CloseCompleted;
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult Silverlight2Chat.LinqChatReference.ILinqChatService.BeginUserExist(string username, string password, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginUserExist(username, password, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        int Silverlight2Chat.LinqChatReference.ILinqChatService.EndUserExist(System.IAsyncResult result) {
            return base.Channel.EndUserExist(result);
        }
        
        private System.IAsyncResult OnBeginUserExist(object[] inValues, System.AsyncCallback callback, object asyncState) {
            string username = ((string)(inValues[0]));
            string password = ((string)(inValues[1]));
            return ((Silverlight2Chat.LinqChatReference.ILinqChatService)(this)).BeginUserExist(username, password, callback, asyncState);
        }
        
        private object[] OnEndUserExist(System.IAsyncResult result) {
            int retVal = ((Silverlight2Chat.LinqChatReference.ILinqChatService)(this)).EndUserExist(result);
            return new object[] {
                    retVal};
        }
        
        private void OnUserExistCompleted(object state) {
            if ((this.UserExistCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.UserExistCompleted(this, new UserExistCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void UserExistAsync(string username, string password) {
            this.UserExistAsync(username, password, null);
        }
        
        public void UserExistAsync(string username, string password, object userState) {
            if ((this.onBeginUserExistDelegate == null)) {
                this.onBeginUserExistDelegate = new BeginOperationDelegate(this.OnBeginUserExist);
            }
            if ((this.onEndUserExistDelegate == null)) {
                this.onEndUserExistDelegate = new EndOperationDelegate(this.OnEndUserExist);
            }
            if ((this.onUserExistCompletedDelegate == null)) {
                this.onUserExistCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnUserExistCompleted);
            }
            base.InvokeAsync(this.onBeginUserExistDelegate, new object[] {
                        username,
                        password}, this.onEndUserExistDelegate, this.onUserExistCompletedDelegate, userState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult Silverlight2Chat.LinqChatReference.ILinqChatService.BeginGetMessages(int messageID, int roomID, System.DateTime timeUserJoined, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginGetMessages(messageID, roomID, timeUserJoined, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Collections.ObjectModel.ObservableCollection<Silverlight2Chat.LinqChatReference.MessageContract> Silverlight2Chat.LinqChatReference.ILinqChatService.EndGetMessages(System.IAsyncResult result) {
            return base.Channel.EndGetMessages(result);
        }
        
        private System.IAsyncResult OnBeginGetMessages(object[] inValues, System.AsyncCallback callback, object asyncState) {
            int messageID = ((int)(inValues[0]));
            int roomID = ((int)(inValues[1]));
            System.DateTime timeUserJoined = ((System.DateTime)(inValues[2]));
            return ((Silverlight2Chat.LinqChatReference.ILinqChatService)(this)).BeginGetMessages(messageID, roomID, timeUserJoined, callback, asyncState);
        }
        
        private object[] OnEndGetMessages(System.IAsyncResult result) {
            System.Collections.ObjectModel.ObservableCollection<Silverlight2Chat.LinqChatReference.MessageContract> retVal = ((Silverlight2Chat.LinqChatReference.ILinqChatService)(this)).EndGetMessages(result);
            return new object[] {
                    retVal};
        }
        
        private void OnGetMessagesCompleted(object state) {
            if ((this.GetMessagesCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.GetMessagesCompleted(this, new GetMessagesCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void GetMessagesAsync(int messageID, int roomID, System.DateTime timeUserJoined) {
            this.GetMessagesAsync(messageID, roomID, timeUserJoined, null);
        }
        
        public void GetMessagesAsync(int messageID, int roomID, System.DateTime timeUserJoined, object userState) {
            if ((this.onBeginGetMessagesDelegate == null)) {
                this.onBeginGetMessagesDelegate = new BeginOperationDelegate(this.OnBeginGetMessages);
            }
            if ((this.onEndGetMessagesDelegate == null)) {
                this.onEndGetMessagesDelegate = new EndOperationDelegate(this.OnEndGetMessages);
            }
            if ((this.onGetMessagesCompletedDelegate == null)) {
                this.onGetMessagesCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnGetMessagesCompleted);
            }
            base.InvokeAsync(this.onBeginGetMessagesDelegate, new object[] {
                        messageID,
                        roomID,
                        timeUserJoined}, this.onEndGetMessagesDelegate, this.onGetMessagesCompletedDelegate, userState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult Silverlight2Chat.LinqChatReference.ILinqChatService.BeginInsertMessage(int roomID, int userID, System.Nullable<int> toUserID, string messageText, string color, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginInsertMessage(roomID, userID, toUserID, messageText, color, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        void Silverlight2Chat.LinqChatReference.ILinqChatService.EndInsertMessage(System.IAsyncResult result) {
            base.Channel.EndInsertMessage(result);
        }
        
        private System.IAsyncResult OnBeginInsertMessage(object[] inValues, System.AsyncCallback callback, object asyncState) {
            int roomID = ((int)(inValues[0]));
            int userID = ((int)(inValues[1]));
            System.Nullable<int> toUserID = ((System.Nullable<int>)(inValues[2]));
            string messageText = ((string)(inValues[3]));
            string color = ((string)(inValues[4]));
            return ((Silverlight2Chat.LinqChatReference.ILinqChatService)(this)).BeginInsertMessage(roomID, userID, toUserID, messageText, color, callback, asyncState);
        }
        
        private object[] OnEndInsertMessage(System.IAsyncResult result) {
            ((Silverlight2Chat.LinqChatReference.ILinqChatService)(this)).EndInsertMessage(result);
            return null;
        }
        
        private void OnInsertMessageCompleted(object state) {
            if ((this.InsertMessageCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.InsertMessageCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void InsertMessageAsync(int roomID, int userID, System.Nullable<int> toUserID, string messageText, string color) {
            this.InsertMessageAsync(roomID, userID, toUserID, messageText, color, null);
        }
        
        public void InsertMessageAsync(int roomID, int userID, System.Nullable<int> toUserID, string messageText, string color, object userState) {
            if ((this.onBeginInsertMessageDelegate == null)) {
                this.onBeginInsertMessageDelegate = new BeginOperationDelegate(this.OnBeginInsertMessage);
            }
            if ((this.onEndInsertMessageDelegate == null)) {
                this.onEndInsertMessageDelegate = new EndOperationDelegate(this.OnEndInsertMessage);
            }
            if ((this.onInsertMessageCompletedDelegate == null)) {
                this.onInsertMessageCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnInsertMessageCompleted);
            }
            base.InvokeAsync(this.onBeginInsertMessageDelegate, new object[] {
                        roomID,
                        userID,
                        toUserID,
                        messageText,
                        color}, this.onEndInsertMessageDelegate, this.onInsertMessageCompletedDelegate, userState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult Silverlight2Chat.LinqChatReference.ILinqChatService.BeginGetUsers(int roomID, int userID, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginGetUsers(roomID, userID, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Collections.ObjectModel.ObservableCollection<Silverlight2Chat.LinqChatReference.UserContract> Silverlight2Chat.LinqChatReference.ILinqChatService.EndGetUsers(System.IAsyncResult result) {
            return base.Channel.EndGetUsers(result);
        }
        
        private System.IAsyncResult OnBeginGetUsers(object[] inValues, System.AsyncCallback callback, object asyncState) {
            int roomID = ((int)(inValues[0]));
            int userID = ((int)(inValues[1]));
            return ((Silverlight2Chat.LinqChatReference.ILinqChatService)(this)).BeginGetUsers(roomID, userID, callback, asyncState);
        }
        
        private object[] OnEndGetUsers(System.IAsyncResult result) {
            System.Collections.ObjectModel.ObservableCollection<Silverlight2Chat.LinqChatReference.UserContract> retVal = ((Silverlight2Chat.LinqChatReference.ILinqChatService)(this)).EndGetUsers(result);
            return new object[] {
                    retVal};
        }
        
        private void OnGetUsersCompleted(object state) {
            if ((this.GetUsersCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.GetUsersCompleted(this, new GetUsersCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void GetUsersAsync(int roomID, int userID) {
            this.GetUsersAsync(roomID, userID, null);
        }
        
        public void GetUsersAsync(int roomID, int userID, object userState) {
            if ((this.onBeginGetUsersDelegate == null)) {
                this.onBeginGetUsersDelegate = new BeginOperationDelegate(this.OnBeginGetUsers);
            }
            if ((this.onEndGetUsersDelegate == null)) {
                this.onEndGetUsersDelegate = new EndOperationDelegate(this.OnEndGetUsers);
            }
            if ((this.onGetUsersCompletedDelegate == null)) {
                this.onGetUsersCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnGetUsersCompleted);
            }
            base.InvokeAsync(this.onBeginGetUsersDelegate, new object[] {
                        roomID,
                        userID}, this.onEndGetUsersDelegate, this.onGetUsersCompletedDelegate, userState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult Silverlight2Chat.LinqChatReference.ILinqChatService.BeginLogOutUser(int userID, int roomID, string username, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginLogOutUser(userID, roomID, username, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        void Silverlight2Chat.LinqChatReference.ILinqChatService.EndLogOutUser(System.IAsyncResult result) {
            base.Channel.EndLogOutUser(result);
        }
        
        private System.IAsyncResult OnBeginLogOutUser(object[] inValues, System.AsyncCallback callback, object asyncState) {
            int userID = ((int)(inValues[0]));
            int roomID = ((int)(inValues[1]));
            string username = ((string)(inValues[2]));
            return ((Silverlight2Chat.LinqChatReference.ILinqChatService)(this)).BeginLogOutUser(userID, roomID, username, callback, asyncState);
        }
        
        private object[] OnEndLogOutUser(System.IAsyncResult result) {
            ((Silverlight2Chat.LinqChatReference.ILinqChatService)(this)).EndLogOutUser(result);
            return null;
        }
        
        private void OnLogOutUserCompleted(object state) {
            if ((this.LogOutUserCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.LogOutUserCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void LogOutUserAsync(int userID, int roomID, string username) {
            this.LogOutUserAsync(userID, roomID, username, null);
        }
        
        public void LogOutUserAsync(int userID, int roomID, string username, object userState) {
            if ((this.onBeginLogOutUserDelegate == null)) {
                this.onBeginLogOutUserDelegate = new BeginOperationDelegate(this.OnBeginLogOutUser);
            }
            if ((this.onEndLogOutUserDelegate == null)) {
                this.onEndLogOutUserDelegate = new EndOperationDelegate(this.OnEndLogOutUser);
            }
            if ((this.onLogOutUserCompletedDelegate == null)) {
                this.onLogOutUserCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnLogOutUserCompleted);
            }
            base.InvokeAsync(this.onBeginLogOutUserDelegate, new object[] {
                        userID,
                        roomID,
                        username}, this.onEndLogOutUserDelegate, this.onLogOutUserCompletedDelegate, userState);
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
        
        protected override Silverlight2Chat.LinqChatReference.ILinqChatService CreateChannel() {
            return new LinqChatServiceClientChannel(this);
        }
        
        private class LinqChatServiceClientChannel : ChannelBase<Silverlight2Chat.LinqChatReference.ILinqChatService>, Silverlight2Chat.LinqChatReference.ILinqChatService {
            
            public LinqChatServiceClientChannel(System.ServiceModel.ClientBase<Silverlight2Chat.LinqChatReference.ILinqChatService> client) : 
                    base(client) {
            }
            
            public System.IAsyncResult BeginUserExist(string username, string password, System.AsyncCallback callback, object asyncState) {
                object[] _args = new object[2];
                _args[0] = username;
                _args[1] = password;
                System.IAsyncResult _result = base.BeginInvoke("UserExist", _args, callback, asyncState);
                return _result;
            }
            
            public int EndUserExist(System.IAsyncResult result) {
                object[] _args = new object[0];
                int _result = ((int)(base.EndInvoke("UserExist", _args, result)));
                return _result;
            }
            
            public System.IAsyncResult BeginGetMessages(int messageID, int roomID, System.DateTime timeUserJoined, System.AsyncCallback callback, object asyncState) {
                object[] _args = new object[3];
                _args[0] = messageID;
                _args[1] = roomID;
                _args[2] = timeUserJoined;
                System.IAsyncResult _result = base.BeginInvoke("GetMessages", _args, callback, asyncState);
                return _result;
            }
            
            public System.Collections.ObjectModel.ObservableCollection<Silverlight2Chat.LinqChatReference.MessageContract> EndGetMessages(System.IAsyncResult result) {
                object[] _args = new object[0];
                System.Collections.ObjectModel.ObservableCollection<Silverlight2Chat.LinqChatReference.MessageContract> _result = ((System.Collections.ObjectModel.ObservableCollection<Silverlight2Chat.LinqChatReference.MessageContract>)(base.EndInvoke("GetMessages", _args, result)));
                return _result;
            }
            
            public System.IAsyncResult BeginInsertMessage(int roomID, int userID, System.Nullable<int> toUserID, string messageText, string color, System.AsyncCallback callback, object asyncState) {
                object[] _args = new object[5];
                _args[0] = roomID;
                _args[1] = userID;
                _args[2] = toUserID;
                _args[3] = messageText;
                _args[4] = color;
                System.IAsyncResult _result = base.BeginInvoke("InsertMessage", _args, callback, asyncState);
                return _result;
            }
            
            public void EndInsertMessage(System.IAsyncResult result) {
                object[] _args = new object[0];
                base.EndInvoke("InsertMessage", _args, result);
            }
            
            public System.IAsyncResult BeginGetUsers(int roomID, int userID, System.AsyncCallback callback, object asyncState) {
                object[] _args = new object[2];
                _args[0] = roomID;
                _args[1] = userID;
                System.IAsyncResult _result = base.BeginInvoke("GetUsers", _args, callback, asyncState);
                return _result;
            }
            
            public System.Collections.ObjectModel.ObservableCollection<Silverlight2Chat.LinqChatReference.UserContract> EndGetUsers(System.IAsyncResult result) {
                object[] _args = new object[0];
                System.Collections.ObjectModel.ObservableCollection<Silverlight2Chat.LinqChatReference.UserContract> _result = ((System.Collections.ObjectModel.ObservableCollection<Silverlight2Chat.LinqChatReference.UserContract>)(base.EndInvoke("GetUsers", _args, result)));
                return _result;
            }
            
            public System.IAsyncResult BeginLogOutUser(int userID, int roomID, string username, System.AsyncCallback callback, object asyncState) {
                object[] _args = new object[3];
                _args[0] = userID;
                _args[1] = roomID;
                _args[2] = username;
                System.IAsyncResult _result = base.BeginInvoke("LogOutUser", _args, callback, asyncState);
                return _result;
            }
            
            public void EndLogOutUser(System.IAsyncResult result) {
                object[] _args = new object[0];
                base.EndInvoke("LogOutUser", _args, result);
            }
        }
    }
}