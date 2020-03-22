using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using PhysicsEngine;

namespace PoolCube
{
    class Room
    {
        Model wallModel;
        Matrix[] wallGraphicsTransforms;
        ConvexHull[] wallHull;
        StaticEntity leftWall, rightWall, bottomWall, topWall, backWall, frontWall;

        /// <summary>
        /// Constructs a new room.
        /// </summary>
        /// <param name="camera">The camera to be used to draw the room.</param>
        /// <param name="Content">The content manager to load models from.</param>
        /// <param name="physics">The physics environment to which the room belogns.</param>
        public Room(Camera camera, ContentManager Content, PhysicsEngine.Environment physics)
        {
            wallModel = Content.Load<Model>("Models\\Wall");
            wallGraphicsTransforms = CommonFunctions.SetupEffectDefaults(wallModel, camera);

            ConvexSegment wallSegment
                = PhysicsEngine.CommonFunctions.LoadConvexHull(new System.IO.StreamReader(@"..\..\..\Content/Hulls/Wall.hull"));
            wallHull = new ConvexHull[] { new ConvexHull(wallSegment, Matrix.Identity) };

            // Create wall entities
            leftWall = CreateWallEntity(-Vector3.UnitX, -MathHelper.PiOver2 * Vector3.UnitZ);
            rightWall = CreateWallEntity(Vector3.UnitX, +MathHelper.PiOver2 * Vector3.UnitZ);
            bottomWall = CreateWallEntity(-Vector3.UnitY, 0.0f * Vector3.UnitX);
            topWall = CreateWallEntity(Vector3.UnitY, MathHelper.Pi * Vector3.UnitX);
            backWall = CreateWallEntity(-Vector3.UnitZ, +MathHelper.PiOver2 * Vector3.UnitX);
            frontWall = CreateWallEntity(Vector3.UnitZ, -MathHelper.PiOver2 * Vector3.UnitX);

            physics.Add(new Entity[] { leftWall, rightWall, bottomWall, topWall, backWall, frontWall });
        }

        /// <summary>
        /// Creates a wall entity.
        /// </summary>
        /// <param name="UnitPosition">The direction in which the wall should be placed.</param>
        /// <param name="Orientation">The orientation of the wall.</param>
        /// <returns>A new wall entity.</returns>
        private StaticEntity CreateWallEntity(Vector3 UnitPosition, Vector3 Orientation)
        {
            return new StaticEntity(
                UnitPosition * (GameVariables.wallHalfWidth + GameVariables.wallHalfThickness), Orientation, 
                GameVariables.wallBoundingRadius, new Hull(wallHull));
        }

        /// <summary>
        /// Draws the walls to the screen.
        /// </summary>
        /// <param name="camera">The camera to use.</param>
        /// <param name="aspectRatio">The aspect ratio to use.</param>
        public void Draw(Camera camera, float aspectRatio)
        {
            if (!(camera.CameraPosition.X < -GameVariables.wallHalfWidth))
            {
                CommonFunctions.DrawModel(wallModel, leftWall.Transform, wallGraphicsTransforms, camera, aspectRatio);
            }
            if (!(camera.CameraPosition.X > +GameVariables.wallHalfWidth))
            {
                CommonFunctions.DrawModel(wallModel, rightWall.Transform, wallGraphicsTransforms, camera, aspectRatio);
            }
            if (!(camera.CameraPosition.Y < -GameVariables.wallHalfWidth))
            {
                CommonFunctions.DrawModel(wallModel, bottomWall.Transform, wallGraphicsTransforms, camera, aspectRatio);
            }
            if (!(camera.CameraPosition.Y > +GameVariables.wallHalfWidth))
            {
                CommonFunctions.DrawModel(wallModel, topWall.Transform, wallGraphicsTransforms, camera, aspectRatio);
            }
            if (!(camera.CameraPosition.Z < -GameVariables.wallHalfWidth))
            {
                CommonFunctions.DrawModel(wallModel, backWall.Transform, wallGraphicsTransforms, camera, aspectRatio);
            }
            if (!(camera.CameraPosition.Z > +GameVariables.wallHalfWidth))
            {
                CommonFunctions.DrawModel(wallModel, frontWall.Transform, wallGraphicsTransforms, camera, aspectRatio);
            }
        }
    }
}
