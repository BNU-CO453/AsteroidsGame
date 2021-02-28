using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AsteroidsGame
{
    public class PlayerSprite : Sprite
    {
        public PlayerSprite(int x, int y) : base(x, y) { }

        /// <summary>
        /// delta time is the time elapsed in second (usualy 1/60 second)
        /// 0.01667 seconds.  So to move 1 pixel at 60 fps the speed should
        /// be set to 60.
        /// </summary>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            KeyboardState keyState = Keyboard.GetState();


            if(keyState.IsKeyDown(Keys.Right) && 
               Position.X < Boundary.X + Boundary.Width - Width)
            {
                Position.X += Speed * deltaTime;
            }
            
            if (keyState.IsKeyDown(Keys.Left) && Position.X > 0)
            {
                Position.X -= Speed * deltaTime;
            }
            
            if (keyState.IsKeyDown(Keys.Up) && Position.Y > 0)
            {
                Position.Y -= Speed * deltaTime;
            }
            
            if (keyState.IsKeyDown(Keys.Down) &&
                Position.Y < (Boundary.Y + Boundary.Height - Height))
            {
                Position.Y += Speed * deltaTime;
            }

        }
    }
}
