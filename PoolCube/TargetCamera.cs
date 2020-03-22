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

namespace PoolCube
{
    class TargetCamera
    {
        Vector3 cameraPosition;
        Vector3 cameraFocusOn;
        private float cameraDistance = GameVariables.cameraDistanceFromBall;
        Vector3 cameraUp;
        Matrix viewMatrix;
        Matrix projectionMatrix;
        Matrix rotation;
        float viewElevation;
        float viewAngle;
        Ball targetBall;

        public TargetCamera()
        {
            //Camera/View information
            SetUpCamera();
        }

        public Ball TargetBall
        {
            get
            {
                return targetBall;
            }
            set
            {
                targetBall = value;
            }
        }


        public Vector3 CameraPosition
        {
            get
            {
                return cameraPosition;
            }
            set
            {
                cameraPosition = value;
            }
        }

        public Vector3 CameraFocusOn
        {
            get
            {
                if (targetBall != null)
                {
                    return targetBall.Position;
                }
                else
                {
                    return Vector3.Zero;
                }
            }
        }

        public Vector3 CameraUp
        {
            get
            {
                return cameraUp;
            }
            set
            {
                cameraUp = value;
            }
        }

        public Matrix ViewMatrix
        {
            get
            {
                return viewMatrix;
            }
            set
            {
                viewMatrix = value;
            }
        }

        public Matrix ProjectionMatrix
        {
            get
            {
                return projectionMatrix;
            }
            set
            {
                projectionMatrix = value;
            }
        }

        /// <summary>
        /// Rotates the camera upward around the target ball.
        /// </summary>
        /// <param name="elapsedTime">The time since elapsed the last update.</param>
        public void LookUp(float elapsedTime)
        {
            Vector3 cameraRight = Vector3.Transform(new Vector3(0.0f, 0.0f, 1.0f), rotation);
            rotation *= Matrix.CreateFromAxisAngle(cameraRight, -GameVariables.angleAdjustment * elapsedTime);
        }

        /// <summary>
        /// Rotates the camera downward around the target ball.
        /// </summary>
        /// <param name="elapsedTime">The time since elapsed the last update.</param>
        public void LookDown(float elapsedTime)
        {
            Vector3 cameraRight = Vector3.Transform(new Vector3(0.0f, 0.0f, 1.0f), rotation);
            rotation *= Matrix.CreateFromAxisAngle(cameraRight, GameVariables.angleAdjustment * elapsedTime);
        }

        /// <summary>
        /// Rotates the camera to the left around the target ball.
        /// </summary>
        /// <param name="elapsedTime">The time since elapsed the last update.</param>
        public void LookLeft(float elapsedTime)
        {
            cameraUp = Vector3.Transform(new Vector3(0.0f, 1.0f, 0.0f), rotation);
            rotation *= Matrix.CreateFromAxisAngle(cameraUp, -GameVariables.angleAdjustment * elapsedTime);
        }

        /// <summary>
        /// Rotates the camera to the right around the target ball.
        /// </summary>
        /// <param name="elapsedTime">The time since elapsed the last update.</param>
        public void LookRight(float elapsedTime)
        {
            cameraUp = Vector3.Transform(new Vector3(0.0f, 1.0f, 0.0f), rotation);
            rotation *= Matrix.CreateFromAxisAngle(cameraUp, GameVariables.angleAdjustment * elapsedTime);
        }

        /// <summary>
        /// Moves the camera toward the target ball.
        /// </summary>
        /// <param name="elapsedTime">The time since elapsed the last update.</param>
        public void MoveForward(float elapsedTime)
        {
            cameraDistance -= GameVariables.movementAmount * elapsedTime;
            cameraDistance = MathHelper.Max(MathHelper.Min(cameraDistance, GameVariables.maxZoomDistance), GameVariables.minZoomDistance);
            
        }

        /// <summary>
        /// Moves the camera away from the target ball.
        /// </summary>
        /// <param name="elapsedTime">The time since elapsed the last update.</param>
        public void MoveBackward(float elapsedTime)
        {
            cameraDistance += GameVariables.movementAmount * elapsedTime;
            cameraDistance = MathHelper.Max(MathHelper.Min(cameraDistance, GameVariables.maxZoomDistance), GameVariables.minZoomDistance);
        }

        /// <summary>
        /// Updates the camera.
        /// </summary>
        public void UpdateCamera()
        {
            Vector3 offset = cameraDistance * Vector3.Transform(new Vector3(-1.0f, 0.0f, 0.0f), rotation);
            cameraFocusOn = targetBall.Position;

            cameraPosition = cameraFocusOn + offset;
            cameraUp = Vector3.Transform(new Vector3(0.0f, 1.0f, 0.0f), rotation);
        }

        public void Reset()
        {
            SetUpCamera();
        }

        private void SetUpCamera()
        {
            cameraPosition = new Vector3(-50.0f, 0.0f, 0.0f);
            cameraFocusOn = new Vector3(0.0f, 0.0f, 0.0f);
            cameraUp = Vector3.Up;
            rotation = Matrix.Identity;

            viewAngle = -MathHelper.PiOver2;
        }
    }
}
