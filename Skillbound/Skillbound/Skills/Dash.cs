using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Skillbound.Skills
{
    class Dash:Base_skill
    {
        GameValue dashValue = new GameValue(0, 5, 1);
        bool dashActivated = false;
        Vector2 increment;

        public Dash():base("Dash", 50f, new GameValue(0, 60, 1))
        {

        }

        public override void Execute()
        {
            if(dashActivated)
            {
                return;
            }
            base.Execute();
            dashActivated = true;
            dashValue.AffectValue(0f);

            increment = new Vector2(Player.Instance.velocity.X, 0) * 5f;
        }

        public override void Update()
        {
            base.Update();
            if(dashActivated)
            {
                dashValue.Regenerate();
                if(dashValue.Percent() == 1f)
                {
                    dashActivated = false;
                }

                Rectangle newRect = new Rectangle(Player.Instance.rect.Location + increment.ToPoint(), Player.Instance.rect.Size);
                if(Map.CollideTiles(newRect))
                {
                    for (int i = 0; i <= 10; i++)
                    {
                        Point newPoint = new Point(
                                (int)GameValue.Lerp(
                                    Player.Instance.rect.X,
                                    newRect.X,
                                    i / 10),
                                (int)GameValue.Lerp(
                                    Player.Instance.rect.Y,
                                    newRect.Y,
                                    i / 10)
                                );
                        if (!Map.CollideTiles(new Rectangle(
                            newPoint,
                            newRect.Size
                            )))
                        {
                            Player.Instance.rect.Location = newPoint;
                        }
                    }
                }
                else
                {
                    Player.Instance.rect.Location = newRect.Location;
                }
            }
        }
    }
}
