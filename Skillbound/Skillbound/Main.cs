using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Skillbound.Map_objects;
using Skillbound.Skills;

namespace Skillbound
{
    public class Main : Game
    {
        public static GraphicsDeviceManager Graphics;
        public static SpriteBatch spriteBatch;
        public static Vector2 screenSize = new Vector2(600, 1080);

        public delegate void GameEvents();
        public static GameEvents UpdateEvent;
        public static GameEvents DrawEvent;

        public Main()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            Graphics.PreferredBackBufferWidth = (int)screenSize.X;
            Graphics.PreferredBackBufferHeight = (int)screenSize.Y;
            //Graphics.IsFullScreen = true;
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            Dictionary<Tile.TileType, List<Texture2D>> final = new Dictionary<Tile.TileType, List<Texture2D>>();
            foreach (var _t in Enum.GetValues(typeof(Tile.TileType)).Cast<Tile.TileType>())
            {
                List<Texture2D> _textures = new List<Texture2D>();
                for (int i = 1; i <= 9; i++) // 1-9
                {
                    _textures.Add(Content.Load<Texture2D>($"Map/{_t.ToString().ToLower()}/{i}"));
                }
                final.Add(_t, _textures);
            }
            Map.Initialize(final);

            Input.Initialize(new List<Keys>()
            {
                Keys.W,
                Keys.A,
                Keys.S,
                Keys.D,
                Keys.Q,
                Keys.E,
                Keys.R,
                Keys.T,
                Keys.F,
                Keys.G,
                Keys.Z,
                Keys.X,
                Keys.C,
                Keys.V,
                Keys.Space,
                Keys.LeftShift
            });

            Base_skill.Initialize();

            Entity.Initialize();
            Player.Initialize(Content.Load<Texture2D>("Player/body"));
            Camera.Initialize(); // Must be init after player

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if(UpdateEvent != null)
            {
                UpdateEvent();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            Matrix cameraOffset = Matrix.CreateTranslation(new Vector3(Camera.Instance.offset, 0f));
            spriteBatch.Begin(
                samplerState: SamplerState.PointClamp,
                transformMatrix:cameraOffset
                );
            if (DrawEvent != null)
            {
                DrawEvent();
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
