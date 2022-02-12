using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Skillbound
{
    class Input
    {
        public static KeyboardState keyboardState;
        public static Dictionary<Keys, Input> inputs;

        public static void Initialize(List<Keys> keys)
        {
            inputs = new Dictionary<Keys, Input>();
            foreach(Keys x in keys)
            {
                inputs.Add(x, new Input(x));
            }

            Main.UpdateEvent += StaticUpdate;
        }

        public static void StaticUpdate()
        {
            keyboardState = Keyboard.GetState();
            foreach(Input x in inputs.Values)
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

        public void Update()
        {
            wasPressed = isPressed;
            isPressed = keyboardState.IsKeyDown(key);
            active = isPressed && !wasPressed;
        }
    }
}
