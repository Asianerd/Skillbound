using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Skillbound.Map_objects
{
    class Tile
    {
        public static Dictionary<TileType, List<Texture2D>> tileSprites; // List of 9-slice sprites according to the TileType

        public static void Initialize(Dictionary<TileType, List<Texture2D>> _tileSprites)
        {
            tileSprites = _tileSprites;
        }

        public enum TileType
        {
            Default
        }

        public static int pixel = 16;

        public TileType type;
        public Rectangle rect;
        List<Texture2D> slices;

        public Tile(TileType _type, Rectangle _rect)
        {
            type = _type;
            rect = _rect;
            slices = tileSprites[type];
        }

        public void Draw()
        {
            Main.spriteBatch.Draw(slices[0], new Rectangle(rect.X, rect.Y, pixel, pixel), Color.White);
            Main.spriteBatch.Draw(slices[1], new Rectangle(rect.X + pixel, rect.Y, rect.Width - pixel - pixel, pixel), Color.White);
            Main.spriteBatch.Draw(slices[2], new Rectangle(rect.Right - pixel, rect.Y, pixel, pixel), Color.White);

            Main.spriteBatch.Draw(slices[3], new Rectangle(rect.X, rect.Y + pixel, pixel, rect.Height - pixel - pixel), Color.White);
            Main.spriteBatch.Draw(slices[4], new Rectangle(rect.X + pixel, rect.Y + pixel, rect.Width - pixel - pixel, rect.Height - pixel - pixel), Color.White);
            Main.spriteBatch.Draw(slices[5], new Rectangle(rect.Right - pixel, rect.Y + pixel, pixel, rect.Height - pixel - pixel), Color.White);

            Main.spriteBatch.Draw(slices[6], new Rectangle(rect.X, rect.Bottom - pixel, pixel, pixel), Color.White);
            Main.spriteBatch.Draw(slices[7], new Rectangle(rect.X + pixel, rect.Bottom - pixel, rect.Width - pixel - pixel, pixel), Color.White);
            Main.spriteBatch.Draw(slices[8], new Rectangle(rect.Right - pixel, rect.Bottom - pixel, pixel, pixel), Color.White);
        }
    }
}
