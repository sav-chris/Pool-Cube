using System;
using System.Collections.Generic;
using System.Text;
using PhysicsEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PoolCube
{
    class Ball
    {
        DefaultEntity physicsReference;
        int number;
        Model ballModel;
        
        class BallEntity : DefaultEntity
        {
            /// <summary>
            /// Constructs a new BallEntity.
            /// </summary>
            /// <param name="position">The position of the ball.</param>
            /// <param name="orientation">The orientation of the ball.</param>
            /// <param name="mass">The mass of the ball.</param>
            /// <param name="boundingRadius">The bounding radius of the ball.</param>
            /// <param name="hull">The ball's hull.</param>
            /// <param name="collisionHandler">The ball's collision handler.</param>
            /// <param name="environment">The environment to which the ball belongs.</param>
            /// <param name="coefficientOfAirResistance">The air resistance coefficient of the ball.</param>
            public BallEntity(Vector3 position, Vector3 orientation, float mass, float boundingRadius, Hull hull, CollisionHandler collisionHandler, PhysicsEngine.Environment environment, float coefficientOfAirResistance)
                : base(position, orientation, mass, boundingRadius, hull, collisionHandler, environment, coefficientOfAirResistance)
            {
            }
            
            /// <summary>
            /// Calculates the air resistance of the ball.
            /// </summary>
            /// <returns>The air resistance of the ball.</returns>
            public new Vector3 AirResistance()
            {
                if ((Velocity + GameVariables.windSpeed).LengthSquared() < GameVariables.airResistanceMinSpeed * GameVariables.airResistanceMinSpeed
                    && (Velocity + GameVariables.windSpeed).LengthSquared() != 0.0f)
                {
                    float airResisCoeff = GameVariables.ballAirResistance * GameVariables.airResistanceMinSpeed;
                    return -airResisCoeff * Vector3.Normalize(Velocity + GameVariables.windSpeed);
                }
                else
                {
                    return base.AirResistance();
                }
            }

            /// <summary>
            /// Calculates the total force acting on the ball.
            /// </summary>
            /// <returns>The total force acting on the ball.</returns>
            public override Vector3 Force()
            {
                return (AirResistance() + base.Gravity());
            }
        }

        /// <summary>
        /// Constructs a new ball.
        /// </summary>
        /// <param name="initialPosition">The initial position of the ball.</param>
        /// <param name="initialOrientation">The initial orientation of the ball.</param>
        /// <param name="mass">The mass of the ball.</param>
        /// <param name="ballRadius">The radius of the ball.</param>
        /// <param name="ballHull">The ball's hull.</param>
        /// <param name="collisionHandler">The ball's collision handler.</param>
        /// <param name="physics">The physics engine environment to which the ball belongs.</param>
        /// <param name="airResistance">The air resistance of the ball.</param>
        /// <param name="number">The ball's number.</param>
        /// <param name="ballModel">The ball's model.</param>
        public Ball(Vector3 initialPosition, Vector3 initialOrientation, float mass, float ballRadius, Hull ballHull,
                    CollisionHandler collisionHandler, PhysicsEngine.Environment physics, float airResistance, int number, Model ballModel)
        {
            physicsReference = new BallEntity(initialPosition, initialOrientation, mass, ballRadius, ballHull, 
                                                 collisionHandler, physics, airResistance);
            physics.Add(physicsReference);

            this.number = number;

            this.ballModel = ballModel;
            
        }

        /// <summary>
        /// Constructs a new ball with default orientation, mass, radius, elasticity, and air resistance.
        /// </summary>
        /// <param name="initialPosition">The initial position of the ball.</param>
        /// <param name="physics">The physics engine environment to which the ball belongs.</param>
        /// <param name="ballHull">The ball's hull.</param>
        /// <param name="number">The ball's number.</param>
        /// <param name="ballModel">The ball's model.</param>
        public Ball(Vector3 initialPosition, PhysicsEngine.Environment physics, Hull ballHull, int number, Model ballModel)
            : this(initialPosition, GameVariables.initialBallOrientation, GameVariables.ballMass, GameVariables.ballRadius,
            ballHull, new ElasticCollision(GameVariables.ballElasticity), physics, GameVariables.ballAirResistance, number, ballModel)
        {
        }

        /// <summary>
        /// Constructs a new cue ball with default orientation, mass, radius, elasticity, and air resistance.
        /// </summary>
        /// <param name="initialPosition">The initial position of the ball.</param>
        /// <param name="physics">The physics engine environment to which the ball belongs.</param>
        /// <param name="ballHull">The ball's hull.</param>
        /// <param name="ballModel">The ball's model.</param>
        public Ball(Vector3 initialPosition, PhysicsEngine.Environment physics, Hull ballHull, Model ballModel)
            : this(initialPosition, GameVariables.initialBallOrientation, GameVariables.ballMass, GameVariables.ballRadius,
    ballHull, new ElasticCollision(GameVariables.ballElasticity), physics, GameVariables.ballAirResistance, 0, ballModel)
        {
        }

        public Matrix Transform
        {
            get
            {
                return physicsReference.Transform;
            }
        }

        public Vector3 Position
        {
            get
            {
                return physicsReference.Position;
            }
            set
            {
                physicsReference.Position = value;
            }
        }

        /// <summary>
        /// Draws the ball to the screen.
        /// </summary>
        /// <param name="camera">The camera to use.</param>
        /// <param name="aspectRatio">The aspect ratio to use.</param>
        /// <param name="ballModel">The ball model to draw.</param>
        /// <param name="ballTransforms">The ball's transform matrices.</param>
        public void Draw(Camera camera, float aspectRatio, Model ballModel, Matrix[] ballTransforms)
        {
            CommonFunctions.DrawModel(ballModel, Matrix.CreateScale(GameVariables.ballScale) * physicsReference.Transform, ballTransforms, camera, aspectRatio);
        }

        public Vector3 Velocity
        {
            get
            {
                return physicsReference.Velocity;
            }
            set
            {
                physicsReference.Velocity = value;
            }
        }

        public Vector3 AngularVelocity
        {
            get
            {
                return physicsReference.AngularVelocity;
            }
            set
            {
                physicsReference.AngularVelocity = value;
            }
        }

        public int Number
        {
            get
            {
                return number;
            }
        }

        public PhysicsEngine.Entity PhysicsReference
        {
            get
            {
                return physicsReference;
            }
        }

        public Model BallModel
        {
            get
            {
                return ballModel;
            }
        }

        /// <summary>
        /// Determines whether the ball has slowed to a sufficiently low speed to be considered stationary.
        /// </summary>
        /// <returns>True if the ball has sufficiently slowed; false otherwise.</returns>
        public bool isStopped()
        {
            float speed = Velocity.Length();
            return speed < GameVariables.ballStopSpeed;

        }

        /// <summary>
        /// Stops the ball from moving and rotating.
        /// </summary>
        public void Stop()
        {
            Velocity = Vector3.Zero;
            AngularVelocity = Vector3.Zero;
        }

        /// <summary>
        /// Determines whether the ball has left the pool cube.
        /// </summary>
        /// <returns>True if the ball has left the cube; false otherwise.</returns>
        public bool IsOutOfCube()
        {
            return Math.Abs(physicsReference.Position.X) > GameVariables.wallHalfWidth
                || Math.Abs(physicsReference.Position.Y) > GameVariables.wallHalfWidth
                || Math.Abs(physicsReference.Position.Z) > GameVariables.wallHalfWidth;
        }
    }
}
