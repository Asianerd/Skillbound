using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Skillbound.Map_objects;

namespace Skillbound
{
    class Map
    {
        public static List<Tile> tiles;

        public static void Initialize(Dictionary<Tile.TileType, List<Texture2D>> final)
        {
            Tile.Initialize(final);
            tiles = new List<Tile>()
            {
                new Tile(
                    new Rectangle(-100, 64, 500, 500)
                    )
            };

            Main.DrawEvent += DrawTiles;
            Main.UpdateEvent += UpdateTiles;
        }

        public static void UpdateTiles()
        {
            foreach(Tile x in tiles)
            {
                x.Update();
            }
        }

        public static void DrawTiles()
        {
            foreach(Tile x in tiles)
            {
                x.Draw();
            }
        }

        public static bool CollideTiles(Point point)
        {
            foreach(Tile x in tiles)
            {
                if(x.rect.Contains(point))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool CollideTiles(Rectangle rectangle)
        {
            foreach (Tile x in tiles)
            {
                if (x.rect.Intersects(rectangle))
                {
                    return true;
                }
            }
            return false;
        }

        public static Tile GetCollidedTile(Rectangle rect)
        {
            foreach(Tile x in tiles)
            {
                if (x.rect.Intersects(rect))
                {
                    return x;
                }
            }
            return null;
        }
    }
}
