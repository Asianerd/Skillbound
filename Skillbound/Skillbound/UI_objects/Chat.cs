using System;
using System.Linq;
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
        public static List<Texture2D> chatboxSlices = new List<Texture2D>();

        public static Dictionary<Keys, char> keysToCheck;
        public static int maxMessageAmount = 30; // amount of messages possible before the earliest ones get deleted

        public static SpriteFont font;
        public static float generalFontHeight;

        public static void Initialize()
        {
            Instance = new Chat();

            darkOverlay = Main.Instance.Content.Load<Texture2D>("UI/Chat/darkOverlay");
            font = Main.Instance.Content.Load<SpriteFont>("UI/Chat/chatFont");
            generalFontHeight = font.MeasureString("A").Y;

            for (int i = 1; i <= 9; i++)
            {
                chatboxSlices.Add(Main.Instance.Content.Load<Texture2D>($"UI/Chat/chat_9slice/{i}"));
            }

            keysToCheck = new Dictionary<Keys, char>()
            {
                { Keys.A, 'a' },
                { Keys.B, 'b' },
                { Keys.C, 'c' },
                { Keys.D, 'd' },
                { Keys.E, 'e' },
                { Keys.F, 'f' },
                { Keys.G, 'g' },
                { Keys.H, 'h' },
                { Keys.I, 'i' },
                { Keys.J, 'j' },
                { Keys.K, 'k' },
                { Keys.L, 'l' },
                { Keys.M, 'm' },
                { Keys.N, 'n' },
                { Keys.O, 'o' },
                { Keys.P, 'p' },
                { Keys.Q, 'q' },
                { Keys.R, 'r' },
                { Keys.S, 's' },
                { Keys.T, 't' },
                { Keys.U, 'u' },
                { Keys.V, 'v' },
                { Keys.W, 'w' },
                { Keys.X, 'x' },
                { Keys.Y, 'y' },
                { Keys.Z, 'z' },
                { Keys.Space, ' ' },
                { Keys.D1, '1' },
                { Keys.D2, '2' },
                { Keys.D3, '3' },
                { Keys.D4, '4' },
                { Keys.D5, '5' },
                { Keys.D6, '6' },
                { Keys.D7, '7' },
                { Keys.D8, '8' },
                { Keys.D9, '9' },
                { Keys.D0, '0' },
                { Keys.OemMinus, '-' },
                { Keys.OemPlus, '=' },
                { Keys.OemOpenBrackets, '[' },
                { Keys.OemCloseBrackets, ']' },
                { Keys.OemSemicolon, ';' },
                { Keys.OemQuotes, '\'' },
                { Keys.OemPeriod, '.' },
                { Keys.OemQuestion, '/' }
            };
        }

        public List<string> messages = new List<string>();
        public string currentString = "";
        public bool chatActive = false;
        public GameValue messageVisibility = new GameValue(0, 300, 1);

        public Chat()
        {
            messages.Add("welcome to skillbound!");
        }

        public void Update()
        {
            if(Input.inputs[Keys.Enter].active)
            {
                chatActive = true;
            }

            if(Input.inputs[Keys.Escape].active)
            {
                chatActive = false;
            }

            if(chatActive && UI.useControls)
            {
                UI.useControls = false;
            }

            if(!chatActive)
            {
                messageVisibility.Regenerate(-1);
                return;
            }
            else
            {
                messageVisibility.AffectValue(1f);
            }
            // Everything beyond this point runs when chat is active

            foreach (var x in keysToCheck)
            {
                if (Input.inputs[x.Key].active)
                {
                    //currentString += Input.inputs[Keys.LeftShift].isPressed ? (char)(x.Key.ToString().ToUpper()[0]) : x.Key;  // why doesnt this work????
                    currentString += x.Value;
                }
            }
            if (currentString.Length > 0)
            {
                if (Input.inputs[Keys.Back].active)
                {
                    currentString = currentString.Remove(currentString.Length - 1);
                }
                if (Input.inputs[Keys.Enter].active)
                {
                    messages.Add(currentString);
                    currentString = "";

                    if (messages.Count >= maxMessageAmount)
                    {
                        messages.RemoveRange(0, messages.Count - maxMessageAmount);
                    }
                }
            }
        }

        public void Draw()
        {
            List<string> iteratedList = new List<string>(messages);
            iteratedList.Reverse();
            if (!chatActive)
            {
                // equation for opacity = (log(i*3)/5) + 1
                float messageOpacity = (MathF.Log((float)messageVisibility.Percent() * 3f) / 5f) + 1f;
                foreach (var x in iteratedList.Select((value, index) => new { index, value }))
                {
                    Main.spriteBatch.DrawString(font, x.value, new Vector2(150, Main.screenSize.Y - (generalFontHeight * (x.index + 5))), new Color(Color.Black, messageOpacity));
                }
                return;
            }

            Main.spriteBatch.Draw(darkOverlay, new Rectangle(0, 0, (int)Main.screenSize.X, (int)Main.screenSize.Y), Color.White);
            foreach (var x in iteratedList.Select((value, index) => new { index, value }))
            {
                Main.spriteBatch.DrawString(font, x.value, new Vector2(150, Main.screenSize.Y - (generalFontHeight * (x.index + 5))), Color.White);
            }
            // drawing chat box
            Rectangle rect = new Rectangle(
                80,
                (int)(Main.screenSize.Y - generalFontHeight - generalFontHeight - 20),
                (int)(Main.screenSize.X - 160),
                (int)((generalFontHeight * 2)));
            Color color = Color.White;
            int pixel = 8;
            Main.spriteBatch.Draw(chatboxSlices[0], new Rectangle(rect.X, rect.Y, pixel, pixel), color);
            Main.spriteBatch.Draw(chatboxSlices[1], new Rectangle(rect.X + pixel, rect.Y, rect.Width - pixel - pixel, pixel), color);
            Main.spriteBatch.Draw(chatboxSlices[2], new Rectangle(rect.Right - pixel, rect.Y, pixel, pixel), color);

            Main.spriteBatch.Draw(chatboxSlices[3], new Rectangle(rect.X, rect.Y + pixel, pixel, rect.Height - pixel - pixel), color);
            Main.spriteBatch.Draw(chatboxSlices[4], new Rectangle(rect.X + pixel, rect.Y + pixel, rect.Width - pixel - pixel, rect.Height - pixel - pixel), color);
            Main.spriteBatch.Draw(chatboxSlices[5], new Rectangle(rect.Right - pixel, rect.Y + pixel, pixel, rect.Height - pixel - pixel), color);

            Main.spriteBatch.Draw(chatboxSlices[6], new Rectangle(rect.X, rect.Bottom - pixel, pixel, pixel), color);
            Main.spriteBatch.Draw(chatboxSlices[7], new Rectangle(rect.X + pixel, rect.Bottom - pixel, rect.Width - pixel - pixel, pixel), color);
            Main.spriteBatch.Draw(chatboxSlices[8], new Rectangle(rect.Right - pixel, rect.Bottom - pixel, pixel, pixel), color);
            //
            Main.spriteBatch.DrawString(font, currentString, new Vector2(100, Main.screenSize.Y - generalFontHeight - generalFontHeight), Color.White);
        }
    }
}
