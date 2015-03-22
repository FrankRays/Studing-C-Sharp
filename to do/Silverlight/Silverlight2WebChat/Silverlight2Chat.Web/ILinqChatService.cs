using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Activation;

namespace Silverlight2Chat.Web
{
    // NOTE: If you change the interface name "ILinqChatService" here, you must also update the reference to "ILinqChatService" in Web.config.
    [ServiceContract]
    public interface ILinqChatService
    {
        [OperationContract]
        int UserExist(string username, string password);

        [OperationContract]
        List<MessageContract> GetMessages(int messageID, int roomID, DateTime timeUserJoined);

        [OperationContract]
        void InsertMessage(int roomID, int userID, int? toUserID, string messageText, string color);

        [OperationContract]
        List<UserContract> GetUsers(int roomID, int userID);

        [OperationContract]
        void LogOutUser(int userID, int roomID, string username);
    }

    [DataContract]
    public class MessageContract
    {
        [DataMember]
        public int MessageID;
        
        [DataMember]
        public string Text;
        
        [DataMember]
        public string UserName;

        [DataMember]
        public string Color;
    }

    [DataContract]
    public class UserContract
    {
        [DataMember]
        public int UserID;

        [DataMember]
        public string UserName;
    }
}
