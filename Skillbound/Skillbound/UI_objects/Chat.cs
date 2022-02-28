using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Skillbound.UI_objects
{
    class Chat
    {
        public static Chat Instance;
        public static Texture2D darkOverlay;

        public static void Initialize()
        {
            Instance = new Chat();

            darkOverlay = Main.Instance.Content.Load<Texture2D>("UI/Chat/darkOverlay");
        }

        public List<string> messages = new List<string>();
        public bool chatActive = false;

        public void Update()
        {
            if(Input.inputs[Keys.Escape].active)
            {
                chatActive = !chatActive;
            }

            if(chatActive && UI.useControls)
            {
                UI.useControls = false;
            }

            Debug.WriteLine(chatActive.ToString());
        }

        public void Draw()
        {
            if(!chatActive)
            {
                return;
            }
            Debug.WriteLine("Window drawn");

            Main.spriteBatch.Draw(darkOverlay, new Rectangle(0, 0, (int)Main.screenSize.X, (int)Main.screenSize.Y), Color.White);
        }
    }
}
