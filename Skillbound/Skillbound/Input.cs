using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Skillbound
{
    class Input
    {
        public static KeyboardState keyboardState;
        public static MouseState mouseState;
        public static Dictionary<Keys, Input> inputs;
        public static Dictionary<MouseButton, MouseInput> mouseInputs;

        public static void Initialize(List<Keys> keys)
        {
            inputs = new Dictionary<Keys, Input>();
            foreach(Keys x in keys)
            {
                inputs.Add(x, new Input(x));
            }

            mouseInputs = new Dictionary<MouseButton, MouseInput>();
            foreach(var x in Enum.GetValues(typeof(MouseButton)).Cast<MouseButton>())
            {
                mouseInputs.Add(x, new MouseInput(x));
            }

            Main.UpdateEvent += MouseInput.MouseUpdate;
            Main.UpdateEvent += StaticUpdate;
        }

        public static void StaticUpdate()
        {
            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();
            foreach(Input x in inputs.Values)
            {
                x.Update();
            }
            foreach(MouseInput x in mouseInputs.Values)
            {
                x.Update();
            }
        }

        public bool wasPressed = false;
        public bool isPressed = true;
        public bool active = false;

        public Keys key;

        public Input(Keys _key)
        {
            key = _key;
        }

        public Input()
        {

        }

        public virtual void Update()
        {
            wasPressed = isPressed;
            isPressed = keyboardState.IsKeyDown(key);
            active = isPressed && !wasPressed;
        }

        public enum MouseButton
        {
            Right,
            Left,
            Middle
        }
    }
}
