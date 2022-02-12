using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Skillbound
{
    class Camera
    {
        public static Camera Instance;

        public static void Initialize()
        {
            Instance = new Camera();

            Main.UpdateEvent += Instance.Update;
        }

        public Vector2 position;
        public Vector2 target;
        public float amount = 0.2f;

        public Vector2 offset = Vector2.Zero;

        public Camera()
        {
            target = Player.Instance.rect.Location.ToVector2();
            position = target;
        }

        public void Update()
        {
            target = Player.Instance.rect.Location.ToVector2() + (Player.Instance.rect.Size.ToVector2() / 2f);
            position = Vector2.Lerp(position, target, amount);
            offset = (Main.screenSize / 2f) - position;
        }
    }
}
