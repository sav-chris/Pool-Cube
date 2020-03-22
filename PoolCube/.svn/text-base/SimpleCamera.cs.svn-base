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
    class SimpleCamera
    {
        Vector3 cameraPosition;
        Vector3 cameraFocusOn;
        Vector3 cameraUp;
        Matrix viewMatrix;
        Matrix projectionMatrix;
        float viewElevation;
        float viewAngle;

        /// <summary>
        /// Constructs a new SimpleCamera.
        /// </summary>
        public SimpleCamera()
        {
            //Camera/View information
            cameraPosition = new Vector3(0.0f, 50.0f, 50.0f);
            cameraFocusOn = new Vector3(0.0f, 50.0f, 50.0f);
            cameraUp = Vector3.Up;
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
                return cameraFocusOn;
            }
            set
            {
                cameraFocusOn = value;
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

        public float ViewElevation
        {
            get
            {
                return viewElevation;
            }
            set
            {
                viewElevation = value;
            }
        }

        public float ViewAngle
        {
            get
            {
                return viewAngle;
            }
            set
            {
                viewAngle = value;
            }
        }

        /// <summary>
        /// Points the camera further downward.
        /// </summary>
        /// <param name="elapsedTime">The time since elapsed the last update.</param>
        public void LookDown(float elapsedTime)
        {
            if (viewElevation > (-1) * MathHelper.PiOver2)
            {
                viewElevation = viewElevation - GameVariables.angleAdjustment * elapsedTime;
            }
        }

        /// <summary>
        /// Points the camera further upward.
        /// </summary>
        /// <param name="elapsedTime">The time since elapsed the last update.</param>
        public void LookUp(float elapsedTime)
        {
            if (viewElevation < MathHelper.PiOver2)
            {
                viewElevation = viewElevation + GameVariables.angleAdjustment * elapsedTime;
            }
        }

        /// <summary>
        /// Rotates the camera to the left.
        /// </summary>
        /// <param name="elapsedTime">The time since elapsed the last update.</param>
        public void LookLeft(float elapsedTime)
        {
            viewAngle = viewAngle - GameVariables.angleAdjustment * elapsedTime;
            if (viewAngle < ((-1) * MathHelper.Pi))
            {
                viewAngle += MathHelper.TwoPi;
            }
        }

        /// <summary>
        /// Rotates the camera to the right.
        /// </summary>
        /// <param name="elapsedTime">The time since elapsed the last update.</param>
        public void LookRight(float elapsedTime)
        {
            viewAngle = viewAngle + GameVariables.angleAdjustment * elapsedTime;
            if (viewAngle > MathHelper.Pi)
            {
                viewAngle -= MathHelper.TwoPi;
            }
        }

        /// <summary>
        /// Moves the camera forward.
        /// </summary>
        /// <param name="elapsedTime">The time since elapsed the last update.</param>
        public void MoveForward(float elapsedTime)
        {
            Vector3 moveDirection = new Vector3(cameraFocusOn.X - cameraPosition.X, 0, cameraFocusOn.Z - cameraPosition.Z);
            moveDirection.Normalize();
            cameraPosition += moveDirection * GameVariables.movementAmount * elapsedTime;
            cameraFocusOn += moveDirection * GameVariables.movementAmount * elapsedTime;
        }

        /// <summary>
        /// Moves the camera backward.
        /// </summary>
        /// <param name="elapsedTime">The time since elapsed the last update.</param>
        public void MoveBackward(float elapsedTime)
        {
            Vector3 moveDirection = new Vector3(cameraFocusOn.X - cameraPosition.X, 0, cameraFocusOn.Z - cameraPosition.Z);
            moveDirection.Normalize();
            cameraPosition -= moveDirection * GameVariables.movementAmount * elapsedTime;
            cameraFocusOn -= moveDirection * GameVariables.movementAmount * elapsedTime;
        }

        /// <summary>
        /// Moves the camera to the left.
        /// </summary>
        /// <param name="elapsedTime">The time since elapsed the last update.</param>
        public void MoveLeft(float elapsedTime)
        {
            Vector3 moveDirection = new Vector3(cameraFocusOn.X - cameraPosition.X, 0, cameraFocusOn.Z - cameraPosition.Z);
            moveDirection = Vector3.Cross(moveDirection, Vector3.Up);
            moveDirection.Normalize();
            cameraPosition -= moveDirection * GameVariables.movementAmount * elapsedTime;
            cameraFocusOn -= moveDirection * GameVariables.movementAmount * elapsedTime;
        }

        /// <summary>
        /// Moves the camera to the right.
        /// </summary>
        /// <param name="elapsedTime">The time since elapsed the last update.</param>
        public void MoveRight(float elapsedTime)
        {
            Vector3 moveDirection = new Vector3(cameraFocusOn.X - cameraPosition.X, 0, cameraFocusOn.Z - cameraPosition.Z);
            moveDirection = Vector3.Cross(moveDirection, Vector3.Up);
            moveDirection.Normalize();
            cameraPosition += moveDirection * GameVariables.movementAmount * elapsedTime;
            cameraFocusOn += moveDirection * GameVariables.movementAmount * elapsedTime;
        }

        /// <summary>
        /// Moves the camera upward.
        /// </summary>
        /// <param name="elapsedTime">The time since elapsed the last update.</param>
        public void MoveUp(float elapsedTime)
        {
            cameraPosition.Y += GameVariables.movementAmount * elapsedTime;
        }

        /// <summary>
        /// Moves the camera downward.
        /// </summary>
        /// <param name="elapsedTime">The time since elapsed the last update.</param>
        public void MoveDown(float elapsedTime)
        {
            cameraPosition.Y -= GameVariables.movementAmount * elapsedTime;
        }

        /// <summary>
        /// Updates the camera.
        /// </summary>
        public void UpdateCamera()
        {
            // Horizontal viewpoint:
            float xOffset = (float)Math.Cos((double)viewAngle);
            float zOffset = (float)Math.Sin((double)viewAngle);

            // Vertical viewpoint:
            float yOffset = (float)Math.Sin((double)viewElevation);

            // Aim camera
            cameraFocusOn.X = cameraPosition.X + xOffset;
            cameraFocusOn.Z = cameraPosition.Z + zOffset;
            cameraFocusOn.Y = cameraPosition.Y + yOffset;
        }


    }
}
