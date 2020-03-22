using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PoolCube
{
    static class CommonFunctions
    {
        static Random randomGenerator = new Random(System.DateTime.Now.Millisecond);
        public static Matrix[] SetupEffectDefaults(Model myModel, Camera camera)
        {
            Matrix[] absoluteTransforms = new Matrix[myModel.Bones.Count];
            myModel.CopyAbsoluteBoneTransformsTo(absoluteTransforms);

            foreach (ModelMesh mesh in myModel.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.DiffuseColor = new Vector3(0.75f, 0.75f, 0.75f);
                    effect.Projection = camera.ProjectionMatrix;
                    effect.View = camera.ViewMatrix;
                }
            }
            return absoluteTransforms;
        }

        public static void DrawModel(Model model, Matrix modelTransform, Matrix[] absoluteBoneTransforms, Camera camera, float aspectRatio)
        {
            //Draw the model, a model can have multiple meshes, so loop
            foreach (ModelMesh mesh in model.Meshes)
            {
                //This is where the mesh orientation is set
                foreach (BasicEffect effect in mesh.Effects)
                {
                    //Microsoft.Xna.Framework.Graphics.
                    //effect.Texture =
                    effect.World = absoluteBoneTransforms[mesh.ParentBone.Index] * modelTransform;
                    effect.View = Matrix.CreateLookAt(camera.CameraPosition, camera.CameraFocusOn, camera.CameraUp); 
                    if (GameVariables.orthographicProj)
                    {
                        float width  = 0.25f * (float)GameVariables.screenWidth  * GameVariables.cameraDistanceFromBall / GameVariables.wallBoundingRadius;
                        float height = 0.25f * (float)GameVariables.screenHeight * GameVariables.cameraDistanceFromBall / GameVariables.wallBoundingRadius;
                        effect.Projection = Matrix.CreateOrthographic(width, height, 1.0f, GameVariables.cameraMaxDistance);
                    }
                    else
                    {
                        effect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(GameVariables.perspective),
                            aspectRatio, 1.0f, GameVariables.cameraMaxDistance);
                    }
                    effect.DiffuseColor = Vector3.One; // Gets rid of a weird texture tint problem
                }
                //Draw the mesh, will use the effects set above.
                mesh.Draw();
            }
        }

        //public static void DrawModel(Model model, Matrix modelTransform, Matrix[] absoluteBoneTransforms, Camera camera, float aspectRatio, Color tint)
        //{
        //    //Draw the model, a model can have multiple meshes, so loop
        //    foreach (ModelMesh mesh in model.Meshes)
        //    {
        //        //This is where the mesh orientation is set
        //        foreach (BasicEffect effect in mesh.Effects)
        //        {
        //            //Microsoft.Xna.Framework.Graphics.
        //            //Matrix[] explosionTransforms = CommonFunctions.SetupEffectDefaults(model, camera);
        //            effect.DiffuseColor = new Vector3(tint.R, tint.G, tint.B);//effect.AmbientLightColor = new Vector3(tint.R, tint.G, tint.B);//effect.DiffuseColor = new Vector3(tint.R, tint.G, tint.B);
        //            effect.World = absoluteBoneTransforms[mesh.ParentBone.Index] * modelTransform;
        //            effect.View = Matrix.CreateLookAt(camera.CameraPosition, camera.CameraFocusOn, Vector3.Up);
        //            effect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(GameVariables.perspective),
        //                aspectRatio, 1.0f, GameVariables.cameraMaxDistance);
        //        }
        //        //Draw the mesh, will use the effects set above.
        //        mesh.Draw();
        //    }
        //}

        public static float GenerateRandom(int lowerBound, int upperBound)
        {
            //Random r = new Random(System.DateTime.Now.Millisecond);
            return (float)(upperBound - lowerBound) * (float)randomGenerator.NextDouble() + lowerBound;
        }

        /// <summary>
        /// Generate a random number between -1 and 1.
        /// </summary>
        /// <returns>Returns a random number between -1 and 1.</returns>
        public static float GetRandom()
        {
            return (float)((randomGenerator.NextDouble() * 2) - 1);
        }
    }
}
