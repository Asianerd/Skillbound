using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Skillbound
{
    class Player : Entity
    {
        public static Player Instance;

        public static void Initialize(Texture2D _sprite)
        {
            Instance = new Player(new Rectangle(0, 0, 64, 64), _sprite);
        }

        public Player(Rectangle _rect, Texture2D _sprite) : base(_sprite, _rect)
        {

        }

        public override void Move()
        {
            Vector2 target = Vector2.Zero;
            Dictionary<Keys, Vector2> directionalVectors = new Dictionary<Keys, Vector2>()
            {
                { Keys.A, new Vector2(-1, 0) },
                { Keys.D, new Vector2(1, 0) }
            };
            foreach (Keys x in new Keys[] { Keys.A, Keys.D })
            {
                if (Input.inputs[x].isPressed)
                {
                    target += directionalVectors[x];
                }
            }
            if (target.Length() > 0)
            {
                target.Normalize();
                target *= speed;
            }

            velocity += target;

            if (Input.inputs[Keys.Space].active)
            {
                velocity.Y = -20;
            }
        }
    }
}
