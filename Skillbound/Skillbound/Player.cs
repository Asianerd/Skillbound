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
            jumpValue.Max = 2;
        }

        public override void Move()
        {
            if (onGround)
            {
                jumpValue.AffectValue(1f);
            }

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
            /*if (target.Length() > 0)
            {
                target.Normalize();
                target *= speed;
            }*/
            target *= speed;

            velocity += target;

            if (Input.inputs[Keys.Space].active && (jumpValue.I >= 1))
            {
                velocity.Y = -20;
                jumpValue.AffectValue(-1d);
            }

            if (Input.inputs[Keys.S].isPressed)
            {
                velocity.Y += 2;
            }
        }

        public override void Update()
        {
            base.Update();

            if (Input.inputs[Keys.Q].active)
            {
                Map.tiles.Add(new Map_objects.Tile(Map_objects.Tile.TileType.Default, new Rectangle(
                    rect.X, rect.Bottom + 10, rect.Width, 40
                    )));
            }
        }
    }
}
