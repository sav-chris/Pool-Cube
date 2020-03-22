using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using PhysicsEngine;


namespace PoolCube
{
    class Balls
    {
        List<Ball> numberedBalls;
        Ball cueBall;
        Model[] ballModels;
        Hull ballHull;
        Matrix[] ballTransforms;
        int layers;
        PhysicsEngine.Environment physics;

        /// <summary>
        /// Constructs all of the necessary balls for the game.
        /// </summary>
        /// <param name="layers">The number of layers of numbered balls to construct.</param>
        /// <param name="ballModels">The ball models to use in constructing the balls.
        /// The model in the nth position is the model for the nth numbered ball.
        /// The model in position 0 is the model for the cue ball.</param>
        /// <param name="ballHull">The hull to be used for each ball.</param>
        /// <param name="ballTransforms">The ball transforms.</param>
        /// <param name="physics">The physics environment to which the balls belong.</param>
        public Balls(int layers, Model[] ballModels, Hull ballHull, Matrix[] ballTransforms, PhysicsEngine.Environment physics)
        {
            this.physics = physics;
            this.layers = layers;
            this.ballModels = ballModels;
            this.ballTransforms = ballTransforms;
            this.ballHull = ballHull;

            SetUpBalls();
        }

        public Ball CueBall
        {
            get
            {
                return cueBall;
            }
        }

        /// <summary>
        /// Generates a tetrahedron of numbered balls.
        /// </summary>
        /// <param name="initialPosition">The number of layers balls in the tetrahedron.</param>
        private void GenerateNumberedBalls(Vector3 initialPosition)
        {
            int number = 0;
            for (int xLayer = 0; xLayer < layers; ++xLayer)
            {
                float xPosition = initialPosition.X + (GameVariables.ballSeparation.X * xLayer);
                for (int yLayer = 0; yLayer <= xLayer; ++yLayer)
                {
                    float yPosition = initialPosition.Y + GameVariables.ballSeparation.Y * (yLayer - 0.5f * xLayer);
                    for (int zLayer = 0; zLayer <= yLayer; ++zLayer)
                    {
                        float zPosition = initialPosition.Z + GameVariables.ballSeparation.Z * (zLayer - 0.5f * yLayer);
                        GenerateNumberedBall(++number, new Vector3(xPosition, yPosition, zPosition));
                    }
                }
            }
        }

        /// <summary>
        /// Generates a numbered ball with a given number and initial position.
        /// </summary>
        /// <param name="number">The ball's number.</param>
        /// <param name="position">The initial position of the ball.</param>
        private void GenerateNumberedBall(int number, Vector3 position)
        {
            numberedBalls.Add(new Ball(position, physics, ballHull, number, ballModels[number]));
        }

        /// <summary>
        /// Generates the cue ball.
        /// </summary>
        private void GenerateCueBall()
        {
            cueBall = new Ball(GameVariables.cueBallPosition, physics, ballHull, ballModels[0]);
            
        }

        /// <summary>
        /// Draws the balls to the screen.
        /// </summary>
        /// <param name="camera">The camera to use.</param>
        /// <param name="aspectRatio">The aspect ratio to use.</param>
        public void Draw(Camera camera, float aspectRatio)
        {
            if (!cueBall.IsOutOfCube())
            {
                cueBall.Draw(camera, aspectRatio, ballModels[0], ballTransforms);
            }

            foreach (Ball ball in numberedBalls)
            {
                ball.Draw(camera, aspectRatio, ball.BallModel, ballTransforms);
            }
        }

        /// <summary>
        /// Determines whether all of the balls have slowed to a sufficiently low speed to be considered stationary.
        /// </summary>
        /// <returns></returns>
        public bool IsStopped()
        {
            bool allStopped = cueBall.isStopped();

            foreach (Ball ball in numberedBalls)
            {
                allStopped &= ball.isStopped();
            }

            return allStopped;
        }

        /// <summary>
        /// Stops all of the balls from moving or rotating.
        /// </summary>
        public void Stop()
        {
            cueBall.Stop();

            foreach (Ball ball in numberedBalls)
            {
                ball.Stop();
            }
        }

        /// <summary>
        /// Finds any balls that have left the pool cube.
        /// </summary>
        /// <returns>All balls that have left the pool cube.</returns>
        public Ball[] BallsOutOfCube()
        {
            
            return Array.FindAll<Ball>(numberedBalls.ToArray(), delegate(Ball ball)
            {
                return ball.IsOutOfCube();
            });
        }

        /// <summary>
        /// Removes a ball from the physics engine.
        /// </summary>
        /// <param name="ball">The ball to be removed.</param>
        public void Remove(Ball ball)
        {
            physics.Remove(ball.PhysicsReference);

            if (numberedBalls.Contains(ball))
            {
                numberedBalls.Remove(ball);
            }
        }

        private void SetUpBalls()
        {
            int numberOfBalls = layers * (layers+1) * (layers+2) / 6;
            numberedBalls = new List<Ball>(numberOfBalls);
            GenerateNumberedBalls(GameVariables.initialBallPosition);
            GenerateCueBall();
        }

        public void Reset()
        {
            foreach (Ball ball in numberedBalls)
            {
                physics.Remove(ball.PhysicsReference);
            }
            physics.Remove(cueBall.PhysicsReference);

            SetUpBalls();
        }
    }
}
