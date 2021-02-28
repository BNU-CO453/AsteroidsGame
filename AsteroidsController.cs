using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AsteroidsGame
{
    public class AsteroidsController
    {
        public List<AsteroidSprite> Asteroids { get; set; }

        private Random generator = new Random(100);

        private double spawnTime = 2.0D;

        private double timeToSpawn;

        private Vector2 gameArea;

        private Texture2D image;

        private double timeDecrease;

        private int maxSpeed = 800;
        private int speed = 200;
        private int acceleration = 20;

        public AsteroidsController(Vector2 gameArea, Texture2D image)
        {
            this.gameArea = gameArea;
            this.image = image;

            Asteroids = new List<AsteroidSprite>();

            timeToSpawn = spawnTime;
            timeDecrease = 0.2D;
        }

        public void Update(GameTime gameTime)
        {
            timeToSpawn -= gameTime.ElapsedGameTime.TotalSeconds;

            if(timeToSpawn <= 0)
            {
                int y = generator.Next((int)gameArea.Y);
                
                AsteroidSprite asteroid = 
                    new AsteroidSprite((int)gameArea.X + image.Width/2, y);

                asteroid.Image = image;
                if(speed < maxSpeed)
                {
                    speed += acceleration;
                }

                asteroid.Speed = speed;

                Asteroids.Add(asteroid);

                if(spawnTime > 0.5)
                    spawnTime -= timeDecrease;

                timeToSpawn = spawnTime;

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
