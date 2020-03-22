using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using PhysicsEngine;

namespace PoolCube
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        PhysicsEngine.Environment physics;
        Camera camera;
        float aspectRatio;
        PlayerControls controls;
        GameMode gameMode;
        Score score;
        float shotPower = 0.0f;
        bool shooting = false;
        bool switchPerspectiveLastUpdate = false;
        bool resetLastUpdate = false;

        // Text components
        SpriteFont lucidaConsole;

        Texture2D whitePixel;

        Room room;
        Balls balls;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            this.graphics.PreferredBackBufferWidth = GameVariables.screenWidth;
            this.graphics.PreferredBackBufferHeight = GameVariables.screenHeight;
            Content.RootDirectory = "Content";
            this.graphics.IsFullScreen = GameVariables.fullScreen;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // Set up camera
            GameVariables.screenWidth = graphics.GraphicsDevice.Viewport.Width;
            GameVariables.screenHeight = graphics.GraphicsDevice.Viewport.Height;
            camera = new Camera();
            aspectRatio = (float)graphics.GraphicsDevice.Viewport.Width / (float)graphics.GraphicsDevice.Viewport.Height;
            camera.SetUpViewAndProjectionMatrices(aspectRatio);

            gameMode = GameVariables.initialGameMode;
            controls = new PlayerControls();
            score = new Score();
            
            // Set up physics engine
            physics = new PhysicsEngine.Environment();
            PhysicsEngine.PhysicsParameters pp = new PhysicsEngine.PhysicsParameters();
            pp.Gravity = GameVariables.gravity;
            pp.WindSpeed = GameVariables.windSpeed;
            physics.PhysicsParameters = pp;

            // Set up physical objects (walls and balls)
            room = new Room(camera, Content, physics);
            int numberOfBalls = GameVariables.ballLayers * (GameVariables.ballLayers + 1) * (GameVariables.ballLayers + 2) / 6;
            Model[] ballModels = new Model[numberOfBalls + 1]; // Add 1 for the cue ball
            ballModels[0] = Content.Load<Model>("Models\\ball_white");
            for (int i = 1; i <= numberOfBalls; i++)
            {
                if (i < 10)
                {
                    ballModels[i] = Content.Load<Model>("Models\\ball_0" + i.ToString());
                }
                else if (i <= 20)
                {
                    ballModels[i] = Content.Load<Model>("Models\\ball_" + i.ToString());
                }
                else
                {
                    ballModels[i] = Content.Load<Model>("Models\\ball_extra");
                }
            }
            Matrix[] ballTransforms = CommonFunctions.SetupEffectDefaults(ballModels[0], camera);
            ConvexSegment ballSegment
                = PhysicsEngine.CommonFunctions.LoadConvexHull(new System.IO.StreamReader(@"..\..\..\Content/Hulls/Ball20.hull"));//@"..\..\..\Content/Hulls/UnitCube.hull"));
            Hull ballHull = new Hull(new ConvexHull[] { new ConvexHull(ballSegment, Matrix.CreateScale(GameVariables.ballScale)) });
            balls = new Balls(GameVariables.ballLayers, ballModels, ballHull, ballTransforms, physics);
            camera.TargetBall = balls.CueBall;

            camera.SetUpViewAndProjectionMatrices(aspectRatio);
            
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            lucidaConsole = Content.Load<SpriteFont>("Fonts/Lucida Console");
            whitePixel = Content.Load<Texture2D>("Textures/WhitePixel");
        }
/// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            physics.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

            //-------------------------------------------------------------------------------------
            // Permanent controls (controls that are available regardless of game mode
            //-------------------------------------------------------------------------------------

            if (controls.Exit())
            {
                this.Exit();
            }
            if (controls.SwitchToDebug())
            {
                gameMode = GameMode.Debug;
            }
            if (controls.Reset() && !resetLastUpdate)
            {
                gameMode = GameVariables.initialGameMode;
                controls = new PlayerControls();
                camera.Reset();
                balls.Reset();
                camera.TargetBall = balls.CueBall;
                gameMode = GameMode.Aim;
                score = new Score();
                resetLastUpdate = true;
            }
            else if (!controls.Reset() && resetLastUpdate)
            {
                resetLastUpdate = false;
            }

            if (controls.SwitchPerspective() && !switchPerspectiveLastUpdate)
            {
                GameVariables.orthographicProj = !GameVariables.orthographicProj;
                switchPerspectiveLastUpdate = true;
            }
            else if (!controls.SwitchPerspective() && switchPerspectiveLastUpdate)
            {
                switchPerspectiveLastUpdate = false;
            }

            controls.Mode = gameMode;

            //-------------------------------------------------------------------------------------
            // Aiming and shooting
            //-------------------------------------------------------------------------------------

            if (gameMode == GameMode.Aim)
            {
                camera.Mode = CameraMode.RotationalAim;

                if (controls.FastAdjustment())
                {
                    GameVariables.angleAdjustment = GameVariables.fastAngleAdjustment;
                }
                else
                {
                    GameVariables.angleAdjustment = GameVariables.slowAngleAdjustment;
                }

                if (balls.CueBall.IsOutOfCube())
                {
                    balls.CueBall.Position = GameVariables.cueBallPosition;
                    score.CueBallSunk();
                }

                if (controls.AimUp() && !shooting)
                {
                    if (controls.SlowDown())
                    {
                        camera.LookUp((float)gameTime.ElapsedGameTime.TotalSeconds);
                    }
                    else
                    {
                        camera.LookUp(GameVariables.speedMultipier * (float)gameTime.ElapsedGameTime.TotalSeconds);
                    }
                }

                if (controls.AimDown() && !shooting)
                {
                    if (controls.SlowDown())
                    {
                        camera.LookDown((float)gameTime.ElapsedGameTime.TotalSeconds);
                    }
                    else
                    {
                        camera.LookDown(GameVariables.speedMultipier * (float)gameTime.ElapsedGameTime.TotalSeconds);
                    }
                }

                if (controls.AimLeft() && !shooting)
                {
                    if (controls.SlowDown())
                    {
                        camera.LookLeft((float)gameTime.ElapsedGameTime.TotalSeconds);
                    }
                    else
                    {
                        camera.LookLeft(GameVariables.speedMultipier * (float)gameTime.ElapsedGameTime.TotalSeconds);
                    }
                }

                if (controls.AimRight() && !shooting)
                {
                    if (controls.SlowDown())
                    {
                        camera.LookRight((float)gameTime.ElapsedGameTime.TotalSeconds);
                    }
                    else
                    {
                        camera.LookRight(GameVariables.speedMultipier * (float)gameTime.ElapsedGameTime.TotalSeconds);
                    }
                }

                if (controls.ZoomIn() && !shooting)
                {
                    if (controls.SlowDown())
                    {
                        camera.MoveForward((float)gameTime.ElapsedGameTime.TotalSeconds);
                    }
                    else
                    {
                        camera.MoveForward(GameVariables.speedMultipier * (float)gameTime.ElapsedGameTime.TotalSeconds);
                    }
                }

                if (controls.ZoomOut() && !shooting)
                {
                    if (controls.SlowDown())
                    {
                        camera.MoveBackward((float)gameTime.ElapsedGameTime.TotalSeconds);
                    }
                    else
                    {
                        camera.MoveBackward(GameVariables.speedMultipier * (float)gameTime.ElapsedGameTime.TotalSeconds);
                    }
                }

                if (controls.Shoot())
                {
                    if (!shooting)
                    {
                        shooting = true;
                    }
                    shotPower += GameVariables.shotPowerIncreasePerSecond * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (shotPower > 1.0f)
                    {
                        shotPower = 1.0f;
                    }
                }
                else
                {
                    if (shooting)
                    {
                        Vector3 direction = camera.CameraFocusOn - camera.CameraPosition;
                        direction.Normalize();

                        Vector3 velocity = direction * GameVariables.defaultBallSpeed * shotPower;

                        balls.CueBall.Velocity = velocity;

                        gameMode = GameMode.Observe;
                        camera.Mode = CameraMode.Observe;
                        shooting = false;
                        shotPower = 0.0f;
                        score.ShotTaken();
                    }
                }
            }

            //-------------------------------------------------------------------------------------
            // Simulation
            //-------------------------------------------------------------------------------------

            else if (gameMode == GameMode.Observe)
            {
                camera.Mode = CameraMode.Observe;

                // Check for sunk balls
                Ball[] ballsOut = balls.BallsOutOfCube();
                foreach (Ball ball in ballsOut)
                {
                    score.BallSunk(ball.Number);
                    balls.Remove(ball);
                }
                if (balls.CueBall.IsOutOfCube())
                {
                    balls.CueBall.Stop();
                }

                // Check whether balls are slow enough to stop simulation
                if (balls.IsStopped()) 
                {
                    balls.Stop();
                    gameMode = GameMode.Aim;
                    camera.Mode = CameraMode.RotationalAim;
                }
            }

            //-------------------------------------------------------------------------------------
            // Debug mode
            //-------------------------------------------------------------------------------------

            else if (gameMode == GameMode.Debug)
            {
                if (controls.DebugSwitchToSimpleCamera())
                {
                    camera.Mode = CameraMode.Simple;
                }
                else if (controls.DebugSwitchToAimCamera())
                {
                    camera.Mode = CameraMode.RotationalAim;
                }
                else if (controls.DebugSwitchToObserveCamera())
                {
                    camera.Mode = CameraMode.Observe;
                }

                if (controls.DebugMoveForward())
                {
                    camera.MoveForward((float)gameTime.ElapsedGameTime.TotalSeconds);
                }

                if (controls.DebugMoveBack())
                {
                    camera.MoveBackward((float)gameTime.ElapsedGameTime.TotalSeconds);
                }

                if (controls.DebugMoveLeft())
                {
                    camera.MoveLeft((float)gameTime.ElapsedGameTime.TotalSeconds);
                }

                if (controls.DebugMoveRight())
                {
                    camera.MoveRight((float)gameTime.ElapsedGameTime.TotalSeconds);
                }

                if (controls.DebugMoveUp())
                {
                    camera.MoveUp((float)gameTime.ElapsedGameTime.TotalSeconds);
                }

                if (controls.DebugMoveDown())
                {
                    camera.MoveDown((float)gameTime.ElapsedGameTime.TotalSeconds);
                }

                if (controls.DebugLookUp())
                {
                    camera.LookUp((float)gameTime.ElapsedGameTime.TotalSeconds);
                }

                if (controls.DebugLookDown())
                {
                    camera.LookDown((float)gameTime.ElapsedGameTime.TotalSeconds);
                }

                if (controls.DebugLookLeft())
                {
                    camera.LookLeft((float)gameTime.ElapsedGameTime.TotalSeconds);
                }

                if (controls.DebugLookRight())
                {
                    camera.LookRight((float)gameTime.ElapsedGameTime.TotalSeconds);
                }
            }

            camera.UpdateCamera();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(GameVariables.backgroundColour);

            room.Draw(camera, aspectRatio);
            balls.Draw(camera, aspectRatio);

            
            
            

            

            

            spriteBatch.Begin(SpriteBlendMode.AlphaBlend,
                              SpriteSortMode.Immediate, SaveStateMode.SaveState);

            // ------------------------------------------------------------------------------------
            // Draw game statistics
            // ------------------------------------------------------------------------------------

            // Balls sunk
            String sunkString = "Balls Sunk: " + score.NumBallsSunk;
            Vector2 sunkStringSize = lucidaConsole.MeasureString(sunkString);
            spriteBatch.DrawString(lucidaConsole, sunkString,
                                   Vector2.UnitX * GameVariables.screenWidth / 2 +
                                   Vector2.UnitY * GameVariables.textLineSpacing -
                                   sunkStringSize / 2, GameVariables.textColour);

            // Shots taken
            String shotsTakenString = "Shots Taken: " + score.ShotsTaken;
            Vector2 shotsTakenStringSize = lucidaConsole.MeasureString(shotsTakenString);
            spriteBatch.DrawString(lucidaConsole, shotsTakenString,
                                   Vector2.UnitX * GameVariables.screenWidth / 2 +
                                   Vector2.UnitY * 2 * GameVariables.textLineSpacing -
                                   shotsTakenStringSize / 2, GameVariables.textColour);

            // Cue balls sunk
            String cueBallsSunkString = "Cue Balls Sunk: " + score.CueBallsSunk;
            Vector2 cueBallsSunkStringSize = lucidaConsole.MeasureString(cueBallsSunkString);
            spriteBatch.DrawString(lucidaConsole, cueBallsSunkString,
                                   Vector2.UnitX * GameVariables.screenWidth / 2 +
                                   Vector2.UnitY * 3 * GameVariables.textLineSpacing -
                                   cueBallsSunkStringSize / 2, GameVariables.textColour);


            if (gameMode == GameMode.Aim)
            {
                // --------------------------------------------------------------------------------
                // Draw aiming instructions
                // --------------------------------------------------------------------------------

                // Aim faster
                String speedIncreaseString = "Aim Faster - Left Control";
                Vector2 speedIncreaseStringSize = lucidaConsole.MeasureString(speedIncreaseString);
                spriteBatch.DrawString(lucidaConsole, speedIncreaseString,
                                       new Vector2(GameVariables.screenWidth / 2 - speedIncreaseStringSize.X / 2,
                                                   GameVariables.screenHeight - GameVariables.meterDistanceFromBottom - GameVariables.meterHeight - 3 * GameVariables.textLineSpacing),
                                       GameVariables.meterColour);

                // Aim slower
                String speedDecreaseString = "Aim Slower - Left Alt";
                Vector2 speedDecreaseStringSize = lucidaConsole.MeasureString(speedDecreaseString);
                spriteBatch.DrawString(lucidaConsole, speedDecreaseString,
                                       new Vector2(GameVariables.screenWidth / 2 - speedDecreaseStringSize.X / 2,
                                                   GameVariables.screenHeight - GameVariables.meterDistanceFromBottom - GameVariables.meterHeight - 2 * GameVariables.textLineSpacing),
                                       GameVariables.meterColour);

                // --------------------------------------------------------------------------------
                // Draw shot power meter
                // --------------------------------------------------------------------------------
                
                // Shot power text
                String shotPowerString = "Shot Power";
                Vector2 shotPowerStringSize = lucidaConsole.MeasureString(shotPowerString);
                spriteBatch.DrawString(lucidaConsole, shotPowerString,
                                       new Vector2(GameVariables.screenWidth / 2 - shotPowerStringSize.X / 2,
                                                   GameVariables.screenHeight - GameVariables.meterDistanceFromBottom - GameVariables.meterHeight - GameVariables.textLineSpacing),
                                       GameVariables.meterColour);

                // Power meter
                spriteBatch.Draw(whitePixel, new Rectangle(GameVariables.screenWidth / 2 - GameVariables.meterMaxWidth / 2,
                                                           GameVariables.screenHeight - GameVariables.meterDistanceFromBottom - GameVariables.meterHeight,
                                                           (int)(shotPower * GameVariables.meterMaxWidth),
                                                           GameVariables.meterHeight),
                                 GameVariables.meterColour);

                // Power meter left boundary
                spriteBatch.Draw(whitePixel, new Rectangle(GameVariables.screenWidth / 2 - GameVariables.meterMaxWidth / 2 - GameVariables.meterEndWidth / 2,
                                                           GameVariables.screenHeight - GameVariables.meterDistanceFromBottom - GameVariables.meterHeight - (GameVariables.meterEndHeight - GameVariables.meterHeight) / 2,
                                                           GameVariables.meterEndWidth,
                                                           GameVariables.meterEndHeight),
                                 GameVariables.meterColour);
                // 0% label on left boundary
                String percent0 = "0%";
                Vector2 percent0StringSize = lucidaConsole.MeasureString(percent0);
                spriteBatch.DrawString(lucidaConsole, percent0,
                                       new Vector2(GameVariables.screenWidth / 2 - GameVariables.meterMaxWidth / 2 - percent0StringSize.X / 2,
                                                   GameVariables.screenHeight - GameVariables.meterDistanceFromBottom - GameVariables.meterHeight + GameVariables.textLineSpacing),
                                       Color.Black);

                // Power meter right boundary
                spriteBatch.Draw(whitePixel, new Rectangle(GameVariables.screenWidth / 2 + GameVariables.meterMaxWidth / 2 - GameVariables.meterEndWidth / 2,
                                                           GameVariables.screenHeight - GameVariables.meterDistanceFromBottom - GameVariables.meterHeight - (GameVariables.meterEndHeight - GameVariables.meterHeight) / 2,
                                                           GameVariables.meterEndWidth,
                                                           GameVariables.meterEndHeight),
                                 GameVariables.meterColour);

                // 100% label on right boundary
                String percent100 = "100%";
                Vector2 percent100StringSize = lucidaConsole.MeasureString(percent100);
                spriteBatch.DrawString(lucidaConsole, percent100,
                                       new Vector2(GameVariables.screenWidth / 2 + GameVariables.meterMaxWidth / 2 - percent100StringSize.X / 2,
                                                   GameVariables.screenHeight - GameVariables.meterDistanceFromBottom - GameVariables.meterHeight + GameVariables.textLineSpacing),
                                       Color.Black);
            }

            // ------------------------------------------------------------------------------------
            // Draw game over text
            // ------------------------------------------------------------------------------------

            if (score.GameOver())
            {
                String restartMessage = "Congratulations, you've sunk all of the balls!\nPress R to restart";
                Vector2 restartMessageSize = lucidaConsole.MeasureString(restartMessage);
                spriteBatch.DrawString(lucidaConsole, restartMessage,
                                       new Vector2(GameVariables.screenWidth / 2 - restartMessageSize.X / 2,
                                                   GameVariables.screenHeight / 2 - restartMessageSize.Y / 2),
                                       Color.Red);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
