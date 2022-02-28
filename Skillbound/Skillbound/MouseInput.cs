using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace Skillbound
{
    class MouseInput:Input
    {
        public static Dictionary<MouseButton, bool> activeButtons = new Dictionary<MouseButton, bool>();

        public static void MouseUpdate()
        {
            activeButtons[MouseButton.Left] = mouseState.LeftButton == ButtonState.Pressed;
            activeButtons[MouseButton.Right] = mouseState.RightButton == ButtonState.Pressed;
            activeButtons[MouseButton.Middle] = mouseState.MiddleButton == ButtonState.Pressed;
        }

        public MouseButton button;

        public MouseInput(MouseButton b):base()
        {
            button = b;
        }

        public override void Update()
        {
            wasPressed = isPressed;
            isPressed = activeButtons[button];
            active = isPressed && !wasPressed;
        }
    }
}
