using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace AsteroidsGame
{
    /// <summary>
    /// This is a simplified version of Asteroids
    /// based on the Udemy Course MonoGame: Introduction
    /// to C# Game Programming 2021
    /// Author: Kyle Schaub
    /// Refactored by: Derek Peacock
    /// </summary>
    public class SpaceGame : Game
    {
        public const int GameAreaWidth = 1280;
        public const int GameAreaHeight = 720;

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private Texture2D shipTexture2D;
        private Texture2D asteroidTexture2D;
        private Texture2D backgroundTexture2D;

        private SpriteFont gameFont;
        private SpriteFont timeFont;

        private PlayerSprite playerSprite;

        private Rectangle gameArea;

        private AsteroidsController asteroidsController;

        private bool inGame;

        private float totalTime;

        /// <summary>
        /// Construct a SpaceGame and set the root directory
        /// </summary>
        public SpaceGame()
        {
            graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            totalTime = 0f;
        }


        /// <summary>
        /// Set up the screen size, and store the screens
        /// width and heigth in a Vector2 called gameArea
        /// </summary>
        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = GameAreaWidth;
            graphics.PreferredBackBufferHeight = GameAreaHeight;
            graphics.ApplyChanges();

            gameArea = new Rectangle(0, 0, GameAreaWidth, GameAreaHeight);
            inGame = false;

            base.Initialize();
        }

        /// <summary>
        /// Load all the images needed for the background 
        /// the spaceship and the asteroids
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            shipTexture2D = Content.Load<Texture2D>("ship");
            
            playerSprite = new PlayerSprite(GameAreaWidth/2, GameAreaHeight/2);
            playerSprite.Image = shipTexture2D;
            playerSprite.Boundary = gameArea;

            asteroidTexture2D = Content.Load<Texture2D>("asteroid");

            asteroidsController =
                new AsteroidsController(gameArea, asteroidTexture2D);

            backgroundTexture2D = Content.Load<Texture2D>("space");

            gameFont = Content.Load<SpriteFont>("spaceFont");
            timeFont = Content.Load<SpriteFont>("timerFont");
        }

        /// <summary>
        /// Update the player sprite and all the asteroids.
        /// End the game if escape is pressed, and start
        /// the game with the enter key
        /// </summary>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || 
                Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }
            else if(Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                inGame = true;
                totalTime = 0;
                asteroidsController.Reset();
            }

            if (inGame)
            {
                totalTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

                playerSprite.Update(gameTime);
                asteroidsController.Update(gameTime);
                
                if(asteroidsController.CollidesWith(playerSprite))
                {
                    inGame = false;
                    asteroidsController.Asteroids.Clear();
                    playerSprite.ResetPosition();
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// Draw the background, the player sprite and then
        /// all the asteroids
        /// </summary>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            spriteBatch.Draw(backgroundTexture2D, new Vector2(0, 0), Color.White);
            spriteBatch.Draw(playerSprite.Image, playerSprite.Position, Color.White);

            asteroidsController.Draw(spriteBatch);

            if(inGame == false)
            {
                DrawStartScreen(spriteBatch);
            }

            spriteBatch.DrawString(timeFont, $"Time: {totalTime:0.00}", 
                new Vector2(3,3), Color.White);
            
            spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Draw a Start Game Message to the centre of the Screen
        /// </summary>
        private void DrawStartScreen(SpriteBatch spriteBatch)
        {
            string startMessage = "Press Enter to start!";
            Vector2 sizeofText = gameFont.MeasureString(startMessage);

            Vector2 textPosition = 
                new Vector2((GameAreaWidth / 2 - sizeofText.X / 2), 200);

            spriteBatch.DrawString(gameFont, startMessage, 
                textPosition, Color.White);
        }
    }
}
