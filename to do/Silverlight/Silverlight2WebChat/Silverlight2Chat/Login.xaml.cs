using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Silverlight2Chat
{
    public partial class Login : UserControl
    {
        bool _isLoginButtonClicked = false;

        public Login()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            _isLoginButtonClicked = true;
            ValidateUserName();
            ValidatePassword();

            if (!String.IsNullOrEmpty(TxtUserName.Text) && !String.IsNullOrEmpty(PbxPassword.Password))
            { 
                // validate user based on the username and password
                ValidateUser();
            }
        }

        private void TxtUserName_LostFocus(object sender, RoutedEventArgs e)
        {
            if(_isLoginButtonClicked)
                ValidateUserName();
        }

        private void PbxPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            if(_isLoginButtonClicked)
                ValidatePassword();
        }

        private void ValidateUserName()
        {
            if (String.IsNullOrEmpty(TxtUserName.Text))
                TxtbUserNameRequired.Visibility = Visibility.Visible;
            else
                TxtbUserNameRequired.Visibility = Visibility.Collapsed;
        }

        private void ValidatePassword()
        {
            if (String.IsNullOrEmpty(PbxPassword.Password))
                TxtbPasswordRequired.Visibility = Visibility.Visible;
            else
                TxtbPasswordRequired.Visibility = Visibility.Collapsed;
        }

        private void ValidateUser()
        {
            LinqChatReference.LinqChatServiceClient proxy = new LinqChatReference.LinqChatServiceClient();
            proxy.UserExistCompleted += new EventHandler<Silverlight2Chat.LinqChatReference.UserExistCompletedEventArgs>(proxy_UserExistCompleted);
            proxy.UserExistAsync(TxtUserName.Text, PbxPassword.Password);  
        }

        void proxy_UserExistCompleted(object sender, Silverlight2Chat.LinqChatReference.UserExistCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                int userID = e.Result;

                if (userID != -1)
                {
                    // go to the chatroom page
                    App app = (App)Application.Current;
                    app.UserID = userID;
                    app.UserName = TxtUserName.Text;
                    app.RedirectTo(new Chatroom());
                }
                else
                {
                    TxtbNotfound.Visibility = Visibility.Visible; 
                }
            }
        }

        private void TxtUserName_MouseEnter(object sender, MouseEventArgs e)
        {
            HideUserNamePasswordError();   
        }

        private void PbxPassword_MouseEnter(object sender, MouseEventArgs e)
        {
            HideUserNamePasswordError();
        }

        private void HideUserNamePasswordError()
        {
            if (TxtbNotfound.Visibility == Visibility.Visible)
                TxtbNotfound.Visibility = Visibility.Collapsed;
        }
    }
}
