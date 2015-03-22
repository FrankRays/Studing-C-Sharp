using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameStateManagement
{
    public class InputState
    {
        public MouseState CurrentMouseState;
        public MouseState LastMouseState;

        public KeyboardState CurrentKeyboardStates;
        public KeyboardState LastKeyboardStates;

        public BoundingBox MouseBb;

        public InputState()
        {
            CurrentMouseState = new MouseState();
            CurrentKeyboardStates = new KeyboardState();
            
            LastMouseState = new MouseState();
            LastKeyboardStates = new KeyboardState();

            MouseBb = new BoundingBox();
        }

        public void Update()
        {
            LastMouseState = CurrentMouseState;
            LastKeyboardStates = CurrentKeyboardStates;
            
            CurrentMouseState = Mouse.GetState();
            CurrentKeyboardStates = Keyboard.GetState();

            MouseBb.Min = new Vector3(CurrentMouseState.X, CurrentMouseState.Y, 0);
            MouseBb.Max = MouseBb.Min + new Vector3(1, 1, 0);
        }

        public bool IsNewKeyPress(Keys key)
        {
            return (CurrentKeyboardStates.IsKeyDown(key) &&
                    LastKeyboardStates.IsKeyUp(key));
        }

        public bool IsMenuSelected()
        {
            return IsNewKeyPress(Keys.Space) ||
                   IsNewKeyPress(Keys.Enter);
        }

        public bool IsMenuCanceled()
        {
            return IsNewKeyPress(Keys.Escape);
        }
        
        public bool IsMenuUp()
        {
            return IsNewKeyPress(Keys.Up);
        }

        public bool IsMenuDown()
        {
            return IsNewKeyPress(Keys.Down);
        }
                
        public bool IsPauseGame()
        {
            return IsNewKeyPress(Keys.Escape);
        }


        public bool MouseIntersects(BoundingBox bb)
        {
            return MouseBb.Intersects(bb);
        }

        public bool MousePressed()
        {
            return CurrentMouseState.LeftButton == ButtonState.Pressed;
        }
    }
}
