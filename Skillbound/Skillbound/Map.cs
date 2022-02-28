using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Skillbound.Map_objects;

using System.IO;
using Newtonsoft.Json;

namespace Skillbound
{
    class Map
    {
        public delegate void MapEvents();
        public static MapEvents OnMapChange;
        public static int unitSize = 256;

        public static Type type;
        public static List<Tile> tiles;
        public static List<Vector2> spawnPoints;

        public static void Initialize(Dictionary<Tile.TileType, List<Texture2D>> final)
        {
            Tile.Initialize(final);

            OnMapChange += UpdateMap;
            ProgressLevel(Type.Default);

            Main.DrawEvent += DrawTiles;
            Main.UpdateEvent += UpdateTiles;
        }

        public static void UpdateMap()
        {
            // read the specific map(.json) as a string
            Dictionary<string, Dictionary<string, Dictionary<string, string>>> mapData = 
                JsonConvert.DeserializeObject<Dictionary<string,Dictionary<string,Dictionary<string, string>>>>(
                File.ReadAllText($"./map_data/{type}.json")
            );

            tiles = new List<Tile>();
            Dictionary<string, Dictionary<string, string>> tileData = mapData["tiles"];
            foreach(Dictionary<string, string> x in tileData.Values)
            {
                Tile.TileType t = (Tile.TileType)Enum.Parse(typeof(Tile.TileType), x["type"]);
                tiles.Add(new Tile(new Rectangle(
                    (int)(float.Parse(x["x"]) * unitSize),
                    (int)(float.Parse(x["y"]) * unitSize),
                    (int)(float.Parse(x["w"]) * unitSize),
                    (int)(float.Parse(x["h"]) * unitSize)
                    ),
                    t
                    ));
            }

            spawnPoints = new List<Vector2>();
            Dictionary<string, Dictionary<string, string>> spawnpointData = mapData["spawns"];
            foreach (Dictionary<string, string> x in spawnpointData.Values)
            {
                spawnPoints.Add(new Vector2(
                    (int)(float.Parse(x["x"]) * unitSize),
                    (int)(float.Parse(x["y"]) * unitSize)
                    ));
            }
        }

        public static void ProgressLevel()
        {
            type = (Type)((int)type + 1);
            if (OnMapChange != null)
            {
                OnMapChange();
            }
        }

        public static void ProgressLevel(Type t)
        {
            type = t;
            if (OnMapChange != null)
            {
                OnMapChange();
            }
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

        public enum Type
        {
            Default,
            Tutorial
        }
    }
}
