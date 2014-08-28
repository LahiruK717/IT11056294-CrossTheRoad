#region File Description
//-----------------------------------------------------------------------------
// IT11056294_Cross_the_RoadGame.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System.Collections;

#endregion

namespace Cross_the_Road
{
    /// <summary>
    /// Default Project Template
    /// </summary>
    public class Game1 : Game
    {

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D vehicleRed;
        Texture2D vehicleYellow;
        Texture2D boy;
        Texture2D background;
        Texture2D blood;
        static ArrayList vehicles1;
        static ArrayList vehicles2;
        static ArrayList vehicles3;
        static ArrayList vehicles4;
        static ArrayList allCars;
        Dexter dexter;
        Rectangle screenBounds;
        float timer = 10;  
        const float TIMER = 10;
        float splashTimer = 10;
        const float SPLASHTIMER = 10;
        int vehicleSpeed = 20;
        int splashSpeed = 10;
        Vector2 splashPosition = new Vector2();
        int lives = 3;
        int score = 0;
        SpriteFont font;
        SpriteFont fontStatus;
        KeyboardState keyboardState;
        int chance = 1;
        bool won = false;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.IsFullScreen = false;
            //Set screen size
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            //Set screen bounds
            screenBounds = new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be use to draw textures
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Load content
            vehicleYellow = Content.Load<Texture2D>("yellowcar");
            vehicleRed = Content.Load<Texture2D>("redcar");
            boy = Content.Load<Texture2D>("dexter");
            background = Content.Load<Texture2D>("roadmap");
            blood = Content.Load<Texture2D>("blood");
            font = Content.Load<SpriteFont>("SpriteFont1");
            fontStatus = Content.Load<SpriteFont>("SpriteFont2");

            LoadObjects();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        private void LoadObjects()
        {
            dexter = new Dexter(boy, screenBounds);
            vehicles1 = new ArrayList();
            vehicles2 = new ArrayList();
            vehicles3 = new ArrayList();
            vehicles4 = new ArrayList();
            allCars = new ArrayList { vehicles1, vehicles2, vehicles3, vehicles4 };
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //If player has lost
            if (lives == 0 || won)
            {
                keyboardState = Keyboard.GetState();
                //If space is pressed, restart the game
                if (keyboardState.IsKeyDown(Keys.Enter))
                {
                    lives = 3;
                    won = false;
                    LoadObjects();
                }
            }
            else//If game is still going
            {
                //Update game objects
                dexter.Update();
                updateDetails(allCars);

                //Periodically generate a water drop
                float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
                timer -= elapsed * vehicleSpeed;
                if (timer < 0)
                {
                    //Timer expired, execute action
                    Random r = new Random();
                    chance = r.Next(4);
                    switch (chance)
                    {
                        case 0: AddVehicle(vehicles1, 0);
                            break;
                        case 1: AddVehicle(vehicles2, 1);
                            break;
                        case 2: AddVehicle(vehicles3, 2);
                            break;
                        case 3: AddVehicle(vehicles4, 3);
                            break;
                    }

                    timer = TIMER;   //Reset Timer
                }

                foreach (ArrayList cars in allCars)
                {
                    foreach (Vehicle v in cars)
                        v.Update(dexter);
                }
            }
            base.Update(gameTime);
        }

        private void updateDetails(ArrayList allCars)
        {
            foreach (ArrayList cars in allCars)
            {
                foreach(Vehicle v in cars)
                {
                    if (v.IsHit())
                    {
                        lives--;
                        v.setHit(false);
                        splashPosition = dexter.Position;
                        dexter.setInStartPosition();
                    }
                    if (v.IsOutsideScreen())
                    {
                        cars.Remove(v);
                        break;
                    }
                }
            }
        }

        private void AddVehicle(ArrayList vehicles, int trackNumber)
        {
            Random random = new Random();
            int yValue;
            int roadWidth = graphics.PreferredBackBufferHeight / 6;
            switch (trackNumber)
            {
                case 0:
                    yValue = roadWidth-15;
                    break;
                case 1:
                    yValue = roadWidth+101;
                    break;
                case 2:
                    yValue = roadWidth + 210;
                    break;
                default:
                    yValue = roadWidth + 325;
                    break;
            }
            if(trackNumber==0 || trackNumber==2)
                vehicles.Add(new Vehicle(vehicleYellow, screenBounds, yValue,trackNumber,5));
            else
                vehicles.Add(new Vehicle(vehicleRed, screenBounds, yValue, trackNumber,-5));
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            // Clear the backbuffer
            graphics.GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin();

            //if player has lost draw score screen
            if (lives <= 0)
            {
                graphics.GraphicsDevice.Clear(Color.Black);
                spriteBatch.DrawString(fontStatus, "Game Over!", new Vector2((graphics.PreferredBackBufferWidth - 150) / 2, (graphics.PreferredBackBufferHeight / 2 - 75)), Color.Green);
                spriteBatch.DrawString(fontStatus, "Score " + dexter.Score, new Vector2((graphics.PreferredBackBufferWidth -80) / 2, (graphics.PreferredBackBufferHeight / 2)), Color.Green);
                spriteBatch.DrawString(font, "Press 'Enter' to Continue...", new Vector2((graphics.PreferredBackBufferWidth - 250) / 2, (graphics.PreferredBackBufferHeight / 2) + 100), Color.Green);
            }
            else//if game is still going
            {
                //draw background
                spriteBatch.Draw(background, new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.White);
                //draw score
                spriteBatch.DrawString(font, "Score: " + dexter.Score , new Vector2(10, 10), Color.White);
                //Show lives
                spriteBatch.DrawString(font, "Lives: " + lives, new Vector2(10, 30), Color.White);
                //Draw game objects
                dexter.Draw(spriteBatch);

                foreach (ArrayList cars in allCars)
                {
                    foreach (Vehicle v in cars)
                        v.Draw(spriteBatch);
                }
                
                //Draw splash where drop fell
                if (splashPosition.X != 0 && splashPosition.Y != 0)
                {
                    spriteBatch.Draw(blood, new Rectangle((int)splashPosition.X, (int)splashPosition.Y, 50, 50), Color.White);
                    float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
                    splashTimer -= elapsed * splashSpeed;
                    if (splashTimer < 0)
                    {
                        //Timer expired, execute action
                        splashPosition = new Vector2();
                        splashTimer = SPLASHTIMER;   //Reset Timer
                    }
                }
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
