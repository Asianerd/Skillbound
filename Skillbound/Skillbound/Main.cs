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
        public static Main Instance;

        public static GraphicsDeviceManager Graphics;
        public static SpriteBatch spriteBatch;
        public static Vector2 screenSize = new Vector2(1920, 1080);

        public delegate void GameEvents();
        public static GameEvents UpdateEvent;
        public static GameEvents DrawEvent;

        public Main()
        {
            Instance = this;

            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            Graphics.PreferredBackBufferWidth = (int)screenSize.X;
            Graphics.PreferredBackBufferHeight = (int)screenSize.Y;
            Graphics.IsFullScreen = screenSize == new Vector2(1920, 1080);
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
                Keys.A,
                Keys.B,
                Keys.C,
                Keys.D,
                Keys.E,
                Keys.F,
                Keys.G,
                Keys.H,
                Keys.I,
                Keys.J,
                Keys.K,
                Keys.L,
                Keys.M,
                Keys.N,
                Keys.O,
                Keys.P,
                Keys.Q,
                Keys.R,
                Keys.S,
                Keys.T,
                Keys.U,
                Keys.V,
                Keys.W,
                Keys.X,
                Keys.Y,
                Keys.Z,
                Keys.Space,
                Keys.LeftShift,
                Keys.Enter,
                Keys.Escape
            }); // Dont check all the keys, only the ones we need

            UI.Initialize();

            Entity.Initialize();
            Player.Initialize(Content.Load<Texture2D>("Player/body"));
            Camera.Initialize(); // Must be init after player

            Base_skill.Initialize();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.F4))
                Exit();

            UI.Instance.Update();
            if (UpdateEvent != null)
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
                transformMatrix: cameraOffset
                );
            if (DrawEvent != null)
            {
                DrawEvent();
            }
            spriteBatch.End();
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            UI.Instance.Draw();
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
