using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Skillbound.Skills;

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
            rect.Location = Map.spawnPoints[0].ToPoint();
            jumpValue.Max = 2;
        }

        public override void Move()
        {
            base.Move();

            if (onGround)
            {
                jumpValue.AffectValue(1f);
            }

            if(!UI.useControls)
            {
                return;
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

            ExecuteSkills();
        }

        public void ExecuteSkills()
        {
            if(!UI.useControls)
            {
                return;
            }

            if (Input.inputs[Keys.Q].active)
            {
                Map.tiles.Add(new Map_objects.Tile(new Rectangle(
                    rect.X - rect.Width, rect.Bottom, rect.Width * 3, 40
                    )));
                /*for(int i = 0; i <= 100; i++)
                {
                    Map.tiles.Add(new Map_objects.Tile(new Rectangle(
                        rect.Right + (i * 40),
                        rect.Y - ((i-1) * 40),
                        300,
                        40
                        )));
                }*/
            }

            if (Input.mouseInputs[Input.MouseButton.Left].active)
            {
                Point pos = Camera.ScreenToWorld(Input.mouseState.Position);
                velocity.Y = 0;
                rect.Location = pos;
            }

            if (Input.inputs[Keys.LeftShift].active)
            {
                Base_skill.skills[Base_skill.Skill.Dash].Execute();
            }
        }
    }
}
