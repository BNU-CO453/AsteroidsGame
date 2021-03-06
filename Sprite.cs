﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AsteroidsGame
{
    /// <summary>
    /// This is the base class for all 2D Sprites
    /// A Sprite is a 2D object which has at least
    /// one image, a postion and a speed of movement.
    /// </summary>
    /// <Author>
    /// Derek Peacock
    /// </Author>
    public class Sprite
    {
        // Structures

        public Rectangle Boundary;

        public Vector2 StartPosition;

        public Vector2 Position;

        // Properties
        public int MaxSpeed { get; set; }

        public int Speed { get; set; }

        public Texture2D Image { get; set; }

        public bool IsVisible { get; set; }

        public bool IsAlive { get; set; }

        public int Width 
        {
            get { return Image.Width; }
        }

        public int Height 
        {
            get { return Image.Height; }
        }

        public Rectangle BoundingBox 
        {
            get 
            {
                return new Rectangle(
                    (int)Position.X, (int)Position.Y, Width, Height);
            }
        }
        // Variables

        protected float deltaTime;
       
        /// <summary>
        /// Constructor sets the starting position of
        /// the Sprite and its starting speed
        /// </summary>
        public Sprite(int x, int y)
        {
            MaxSpeed = 1000;
            Speed = 300;

            IsVisible = true;
            IsAlive = true;

            Position = new Vector2(x, y);
            StartPosition = Position;
        }

        public Vector2 GetCenterPosition()
        {
            return new Vector2(Position.X - Image.Width / 2,
                Position.Y - Image.Height / 2);
        }

        public void ResetPosition()
        {
            Position = StartPosition;
        }

        public virtual void Update(GameTime gameTime)
        {
            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

    }
}
