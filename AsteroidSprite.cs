using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AsteroidsGame
{
    /// <summary>
    /// This sprite will just move across the screen from
    /// right to left at a fixed speed
    /// 
    /// </summary>
    public class AsteroidSprite : Sprite
    {
        
        
        public AsteroidSprite(int x, int y) : base(x, y)
        {
            MaxSpeed = 800;
            Speed = 220;
        }

        /// <summary>
        /// delta time is the time elapsed in second (usualy 1/60 second)
        /// 0.01667 seconds.  So to move 1 pixel at 60 fps the speed should
        /// be set to 60.
        /// </summary>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Position.X -= Speed * deltaTime;
        }
    }
}
