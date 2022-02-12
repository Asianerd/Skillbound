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


        // public Vector2 position;
        public Rectangle rect;
        public Texture2D sprite;
        public float speed = 10f;

        public Entity(Texture2D _sprite, Rectangle _rect)
        {
            rect = _rect;
            sprite = _sprite;

            collection.Add(this);
        }

        public virtual void Update()
        {

        }

        public virtual void Draw()
        {
            Main.spriteBatch.Draw(sprite, rect, Color.White);
        }
    }
}
