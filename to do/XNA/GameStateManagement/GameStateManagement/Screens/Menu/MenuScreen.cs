using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameStateManagement
{
    abstract class MenuScreen : GameScreen
    {
        List<MenuEntry> menuEntries = new List<MenuEntry>();
        List<BoundingBox> bbox = new List<BoundingBox>();

        int s,selectedEntry = 0;
        string menuTitle;

        protected IList<MenuEntry> MenuEntries
        {
            get { return menuEntries; }
        }
        protected IList<BoundingBox> Bbox
        {
            get { return bbox; }
        }


        public MenuScreen(string menuTitle)
        {
            this.menuTitle = menuTitle;
            TransitionOnTime = TimeSpan.FromSeconds(0.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }

        public void Initialize()
        {
            for (int i = 0; i < menuEntries.Count; i++)
            {
                menuEntries[i].position = new Vector2(500, 150 + 50 * i);
                bbox.Add(new BoundingBox(new Vector3(500, 150 + 50 * i, 0), new Vector3(600, 190 + 50 * i, 0)));
            }
        }

        public override void HandleInput(InputState input)
        {
            if (input.IsMenuUp())
            {
                selectedEntry--;

                if (selectedEntry < 0)
                    selectedEntry = menuEntries.Count - 1;
            }

            // Move to the next menu entry?
            if (input.IsMenuDown())
            {
                selectedEntry++;

                if (selectedEntry >= menuEntries.Count)
                    selectedEntry = 0;
            }

            for (int i = 0; i < Bbox.Count; i++)
                if (input.MouseIntersects(Bbox[i]))
                    selectedEntry = i;

            if (input.IsMenuSelected())
            {
                OnSelectEntry(selectedEntry);
            }

            if (input.MousePressed())
            {
                s = selectedEntry;
                selectedEntry = -1;
                for (int i = 0; i < Bbox.Count; i++)
                    if (input.MouseIntersects(Bbox[i]))
                        selectedEntry = i;
                if (selectedEntry == -1)
                    selectedEntry = s;
                else
                    OnSelectEntry(selectedEntry);
            }


            else if (input.IsMenuCanceled())
            {
                OnCancel();
            }


            
        }

        protected virtual void OnSelectEntry(int entryIndex)
        {
            menuEntries[selectedEntry].OnSelectEntry();
        }

        protected virtual void OnCancel()
        {
            ExitScreen();
        }

        protected void OnCancel(object sender)
        {
            OnCancel();
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

            // Update each nested MenuEntry object.
            for (int i = 0; i < menuEntries.Count; i++)
            {
                bool isSelected = IsActive && (i == selectedEntry);

                menuEntries[i].Update(this, isSelected, gameTime);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            SpriteFont font = ScreenManager.Font;

            // Draw each menu entry in turn.
            for (int i = 0; i < menuEntries.Count; i++)
            {
                bool isSelected = IsActive && (i == selectedEntry);

                menuEntries[i].Draw(this, isSelected, gameTime);
            }

            // Draw the menu title.
            float transitionOffset = (float)Math.Pow(TransitionPosition, 2);
            Vector2 titlePosition = new Vector2(426, 80);
            Vector2 titleOrigin = font.MeasureString(menuTitle) / 2;
            Color titleColor = new Color(192, 192, 192, TransitionAlpha);
            float titleScale = 1.25f;

            titlePosition.Y -= transitionOffset * 100;

            spriteBatch.DrawString(font, menuTitle, titlePosition, titleColor, 0, titleOrigin, titleScale, SpriteEffects.None, 0);
            base.Draw(gameTime);
        }
    }
}
