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
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows.Threading;

namespace Silverlight2Chat
{
    public partial class Chatroom : UserControl
    {
        DispatcherTimer timer;
        private bool _isTimerStarted;
        private bool _isWithBackground = false;
        private int _lastMessageId = 0;
        private int _roomId = 1;
        private int _userID;
        private DateTime _timeUserJoined;

        public Chatroom()
        {
            InitializeComponent();
            
            App app = (App)Application.Current;

            if (String.IsNullOrEmpty(app.UserName))
            {
                app.RedirectTo(new Login());
            }
            else
            {
                _userID = app.UserID;
                _timeUserJoined = DateTime.Now;
                TxtbLoggedInUser.Text = app.UserName;
            }
        }

        private void LayoutRoot_Loaded(object sender, RoutedEventArgs e)
        {            
            TxtMessage.Focus();
            InsertNewlyJoinedMessage();
            GetUsers();
            SetTimer();
        }

        private void GetUsers()
        {
            LinqChatReference.LinqChatServiceClient proxy = new LinqChatReference.LinqChatServiceClient();
            proxy.GetUsersCompleted += new EventHandler<Silverlight2Chat.LinqChatReference.GetUsersCompletedEventArgs>(proxy_GetUsersCompleted);
            proxy.GetUsersAsync(_roomId, _userID);
        }

        void proxy_GetUsersCompleted(object sender, Silverlight2Chat.LinqChatReference.GetUsersCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                ObservableCollection<LinqChatReference.UserContract> users = e.Result;
                ItmcUserList.ItemsSource = users;
            }
        }

        private void InsertNewlyJoinedMessage()
        {
            string message = "joined the room.";
            LinqChatReference.LinqChatServiceClient proxy = new LinqChatReference.LinqChatServiceClient();
            proxy.InsertMessageAsync(_roomId, _userID, null, message, "Gray");
        }

        private void SetTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 3, 0);
            timer.Tick += new EventHandler(TimerTick);
            timer.Start();

            _isTimerStarted = true;
        }

        private void InsertMessage()
        {      
            LinqChatReference.LinqChatServiceClient proxy = new LinqChatReference.LinqChatServiceClient();
            proxy.InsertMessageAsync(_roomId, _userID, null, TxtMessage.Text, CbxFontColor.SelectionBoxItem.ToString());
        }

        private void GetMessages()
        {
            LinqChatReference.LinqChatServiceClient proxy = new LinqChatReference.LinqChatServiceClient();
            proxy.GetMessagesCompleted += new EventHandler<Silverlight2Chat.LinqChatReference.GetMessagesCompletedEventArgs>(proxy_GetMessagesCompleted);
            proxy.GetMessagesAsync(_lastMessageId, _roomId, _timeUserJoined);
        }

        void proxy_GetMessagesCompleted(object sender, Silverlight2Chat.LinqChatReference.GetMessagesCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                ObservableCollection<LinqChatReference.MessageContract> messages = e.Result;

                foreach (var message in messages)
                {
                    // add a horizontal stack panel
                    StackPanel sp = new StackPanel();
                    sp.Orientation = Orientation.Horizontal;
                    sp.HorizontalAlignment = HorizontalAlignment.Left;
                    sp.Width = SpnlMessages.ActualWidth;

                    // put an alternating background
                    if (!_isWithBackground)
                        sp.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(100, 235, 235, 235));

                    // add a TextBlock to hold the user's name to the stack panel
                    TextBlock name = new TextBlock();
                    name.Text = message.UserName + ": ";
                    name.FontSize = 12.0;
                    name.FontWeight = FontWeights.Bold;
                    name.Padding = new Thickness(4, 8, 0, 8);

                    if (message.Color == "Gray")
                        name.Foreground = new SolidColorBrush(Colors.Gray);
                    else
                        name.Foreground = new SolidColorBrush(Colors.Black);

                    sp.Children.Add(name);

                    // add a TextBox to hold the user's message to the stack panel
                    TextBox text = new TextBox();
                    text.BorderBrush = new SolidColorBrush(Colors.Transparent);
                    text.FontSize = 12.0;
                    text.Text = message.Text.Trim();
                    text.VerticalAlignment = VerticalAlignment.Top;
                    text.Width = SpnlMessages.ActualWidth - name.ActualWidth;
                    text.TextWrapping = TextWrapping.Wrap;
                    text.Margin = new Thickness(0, 4, 4, 0);
                    text.IsReadOnly = true;
                    
                    // change text color based on the user's chosen color
                    if(message.Color == "Red")
                        text.Foreground = new SolidColorBrush(Colors.Red);
                    else if (message.Color == "Blue")
                        text.Foreground = new SolidColorBrush(Colors.Blue);
                    else if (message.Color == "Gray")
                        text.Foreground = new SolidColorBrush(Colors.Gray);
                    else
                        text.Foreground = new SolidColorBrush(Colors.Black);

                    // put an alternating background
                    if (!_isWithBackground)
                    {
                        text.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(100, 235, 235, 235));
                        _isWithBackground = true;
                    }
                    else
                    {
                        _isWithBackground = false;
                    }

                    sp.Children.Add(text);

                    // add the horizontal stack panel to the base stack panel
                    SpnlMessages.Children.Add(sp);

                    // remember the last message id
                    _lastMessageId = message.MessageID;
                }

                SetScrollBarToBottom();
                TxtMessage.Text = String.Empty;
                TxtMessage.Focus();
            }
        }

        private void SetScrollBarToBottom()
        {
            // set the scroll bar to the bottom
            SvwrMessages.UpdateLayout();
            SvwrMessages.ScrollToVerticalOffset(double.MaxValue);
        }

        private void TxtMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SendMessage();
                timer.Start();
                _isTimerStarted = true;
            }
            else
            {
                if (_isTimerStarted)
                {
                    timer.Stop();
                    _isTimerStarted = false;
                }
            }
        }

        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            SendMessage();
        }

        private void SendMessage()
        {
            if(!String.IsNullOrEmpty(TxtMessage.Text))
            {
                InsertMessage();
                GetMessages();
                GetUsers();
            }
        }

        void TimerTick(object sender, EventArgs e)
        {
            GetMessages();
            GetUsers();
        }

        private void BtnLogOut_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            
            LinqChatReference.LinqChatServiceClient proxy = new LinqChatReference.LinqChatServiceClient();
            proxy.LogOutUserAsync(_userID, _roomId, TxtbLoggedInUser.Text);  
 
            // redirect to the login page
            App app = (App)Application.Current;
            app.RedirectTo(new Login());
        }
    }
}
