using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Skillbound
{
    class Entity
    {
        public static List<Entity> collection;

        public static void Initialize()
        {
            collection = new List<Entity>();

            Main.UpdateEvent += StaticUpdate;
            Main.DrawEvent += StaticDraw;
        }

        public static void StaticUpdate()
        {
            foreach(Entity x in collection)
            {
                x.Update();
            }
        }

        public static void StaticDraw()
        {
            foreach(Entity x in collection)
            {
                x.Draw();
            }
        }

        public Rectangle rect;
        public Texture2D sprite;
        public float speed = 10f;
        public Vector2 velocity = Vector2.Zero;
        public bool onGround;
        public GameValue jumpValue;

        public Entity(Texture2D _sprite, Rectangle _rect)
        {
            rect = _rect;
            sprite = _sprite;
            jumpValue = new GameValue(0, 1, 1);

            collection.Add(this);
        }

        public virtual void Update()
        {
            PhysicsChecks();
            Move();
            ApplyVelocity();
            ApplyFriction();
        }

        public virtual void Draw()
        {
            Main.spriteBatch.Draw(sprite, rect, Color.White);
        }

        public virtual void PhysicsChecks()
        {
            onGround = Map.CollideTiles(new Rectangle(rect.X, rect.Bottom, rect.Width, 5));
        }

        public virtual void Move()
        {
            if(onGround)
            {
                velocity.Y = 0;
            }
        }

        public virtual void ApplyVelocity()
        {
            velocity.Y = Math.Clamp(velocity.Y + 1, -30, 30);

            Point targetPoint = new Point((int)velocity.X, (int)velocity.Y);
            Rectangle newRect = new Rectangle(targetPoint + rect.Location, rect.Size);

            if (!Map.CollideTiles(newRect))
            {
                rect = newRect;
                return;
            }

            // All the code below runs if there was something in the way when moving
            for (int i = 0; i <= 10; i++)
            {
                Rectangle withoutY = new Rectangle(new Point(
                    (int)Lerp(rect.X, newRect.X, (i / 10f)),
                    rect.Y), newRect.Size);
                if (!Map.CollideTiles(withoutY))
                {
                    rect = withoutY;
                }
            }
            if((int)velocity.X > 0)
            {
                // Right
                // Crap-ton of checks, i know
                Rectangle blockCheck = new Rectangle(rect.Right, rect.Y, 5, rect.Height);
                if (Map.CollideTiles(blockCheck))
                {
                    // if theres blockage on the right
                    Map_objects.Tile collision = Map.GetCollidedTile(blockCheck);
                    if (collision != null) // just gotta make sure
                    {
                        Rectangle heightCheck = new Rectangle(newRect.X, rect.Y, rect.Width, 5);
                        if (!Map.CollideTiles(heightCheck))
                        {
                            Rectangle spaceCheck = new Rectangle(newRect.X, collision.rect.Y - rect.Height, rect.Width, rect.Height);
                            if (!Map.CollideTiles(spaceCheck))
                            {
                                rect = spaceCheck;
                            }
                        }
                    }
                }
            }
            else if ((int)velocity.X < 0)
            {
                // Left
                Rectangle blockCheck = new Rectangle(rect.X - 5, rect.Y, 5, rect.Height);
                if (Map.CollideTiles(blockCheck))
                {
                    Map_objects.Tile collision = Map.GetCollidedTile(blockCheck);
                    if (collision != null)
                    {
                        Rectangle heightCheck = new Rectangle(newRect.X, rect.Y, rect.Width, 5);
                        if (!Map.CollideTiles(heightCheck))
                        {
                            Rectangle spaceCheck = new Rectangle(newRect.X, collision.rect.Y - rect.Height, rect.Width, rect.Height);
                            if (!Map.CollideTiles(spaceCheck))
                            {
                                rect = spaceCheck;
                            }
                        }
                    }
                }
            }

            for (int i = 0; i <= 10; i++)
            {
                Rectangle withoutX = new Rectangle(new Point(
                    rect.X,
                    (int)Lerp(rect.Y, newRect.Y, (i / 10f))
                    ), newRect.Size);
                if (!Map.CollideTiles(withoutX))
                {
                    rect = withoutX;
                }
            }
        }

        public virtual void ApplyFriction()
        {
            velocity.X *= 0.5f;
        }

        public static float Lerp(float start, float end, float amount)
        {
            return start + ((end - start) * amount);
        }
    }
}
