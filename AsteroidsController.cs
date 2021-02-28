using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AsteroidsGame
{
    public class AsteroidsController
    {
        public List<AsteroidSprite> Asteroids { get; set; }

        private Random generator = new Random(100);

        private Rectangle boundary;

        private double spawnTime;

        private double timeToSpawn;

        private Texture2D image;

        private double timeDecrease;

        private int maxSpeed = 800;
        private int acceleration = 20;

        private int speed;

        public AsteroidsController(Rectangle boundary, Texture2D image)
        {
            this.image = image;
            this.boundary = boundary;

            Asteroids = new List<AsteroidSprite>();
            Reset();
        }

        public void Update(GameTime gameTime)
        {
            timeToSpawn -= gameTime.ElapsedGameTime.TotalSeconds;

            if(timeToSpawn <= 0)
            {
                int y = generator.Next((int)boundary.Height);
                
                AsteroidSprite asteroid = 
                    new AsteroidSprite((int)boundary.Width + image.Width/2, y);

                asteroid.Image = image;
                asteroid.Boundary = boundary;

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
                if(asteroid.Position.X < -asteroid.Width)
                {
                    asteroid.IsAlive = false;
                }
            }

            Asteroids.RemoveAll(a => a.IsAlive == false);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (AsteroidSprite asteroid in Asteroids)
            {
                spriteBatch.Draw(asteroid.Image, 
                    asteroid.Position, Color.White);
            }

        }

        public bool CollidesWith(PlayerSprite player)
        {
            foreach(AsteroidSprite asteroid in Asteroids)
            {
                if(player.BoundingBox.Intersects(asteroid.BoundingBox))
                {
                    return true;
                }

            }
            return false;
        }

        public void Reset()
        {
            spawnTime = 2.0D;
            timeToSpawn = spawnTime;
            timeDecrease = 0.2D;
            speed = 200;
        }
    }
}
