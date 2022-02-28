using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Skillbound.UI_objects;

namespace Skillbound
{
    class UI
    {
        public static UI Instance = null;
        public static bool useControls = true; // if buttons pressed control ui or the player
        /* eg.
         * useControls is false when chat is opened; because buttons pressed are used to type letters
         * useControls is true when no ui window needs button input; like when the player is playing the game
         */

        public static void Initialize()
        {
            if(Instance == null)
            {
                Instance = new UI();
            }
        }

        public UI()
        {
            Chat.Initialize();
        }

        public void Update()
        {
            useControls = true;
            Chat.Instance.Update();
        }

        public void Draw()
        {
            Chat.Instance.Draw();
        }
    }
}
