using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AsteroidsGame
{
    public class AsteroidsController
    {
        public const double SpawnTime = 2.0D;

        public List<AsteroidSprite> Asteroids { get; set; }

        private Random generator = new Random(100);

        private double timeToSpawn = SpawnTime;

        private Vector2 gameArea;

        private Texture2D image;

        public AsteroidsController(Vector2 gameArea, Texture2D image)
        {
            this.gameArea = gameArea;
            this.image = image;

            Asteroids = new List<AsteroidSprite>();
        }

        public void Update(GameTime gameTime)
        {
            timeToSpawn -= gameTime.ElapsedGameTime.TotalSeconds;

            if(timeToSpawn <= 0)
            {
                int y = generator.Next((int)gameArea.Y);
                
                AsteroidSprite asteroid = 
                    new AsteroidSprite((int)gameArea.X, y);

                asteroid.Image = image;

                Asteroids.Add(asteroid);
                timeToSpawn = SpawnTime;
            }
            
            foreach(AsteroidSprite asteroid in Asteroids)
            {
                asteroid.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (AsteroidSprite asteroid in Asteroids)
            {
                spriteBatch.Draw(asteroid.Image, 
                    asteroid.Position, Color.White);
            }

        }
    }
}
