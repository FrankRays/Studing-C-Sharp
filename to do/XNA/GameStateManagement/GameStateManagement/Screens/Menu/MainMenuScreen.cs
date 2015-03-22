using Microsoft.Xna.Framework;

namespace GameStateManagement
{
    class MainMenuScreen : MenuScreen
    {
        public MainMenuScreen() : base("Main Menu")
        {
            // Create our menu entries.
            MenuEntry playGameMenuEntry = new MenuEntry("Play Game");
            MenuEntry optionsMenuEntry = new MenuEntry("Options");
            MenuEntry exitMenuEntry = new MenuEntry("Exit");

            // Hook up menu event handlers.
            playGameMenuEntry.Selected += PlayGameMenuEntrySelected;
            optionsMenuEntry.Selected += OptionsMenuEntrySelected;
            exitMenuEntry.Selected += OnCancel;

            MenuEntries.Add(playGameMenuEntry);
            MenuEntries.Add(optionsMenuEntry);
            MenuEntries.Add(exitMenuEntry);

            this.Initialize();
        }

        void PlayGameMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            //LoadingScreen.Load(ScreenManager, true, e.PlayerIndex,
            //                   new GameplayScreen());
            ScreenManager.AddScreen(new GamePlayScreen());
        }

        void OptionsMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            //ScreenManager.AddScreen(new OptionsMenuScreen(), e.PlayerIndex);
        }

        void OnCancel(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.Game.Exit();
            //const string message = "Are you sure you want to exit this sample?";

            //MessageBoxScreen confirmExitMessageBox = new MessageBoxScreen(message);

            //confirmExitMessageBox.Accepted += ConfirmExitMessageBoxAccepted;

            //ScreenManager.AddScreen(confirmExitMessageBox);
        }
        
        void ConfirmExitMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.Game.Exit();
        }


    }
}
