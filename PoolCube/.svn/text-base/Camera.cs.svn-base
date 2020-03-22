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
    class Camera
    {
        private CameraMode mode;
        private TargetCamera targetCamera = new TargetCamera();
        private SimpleCamera simpleCamera = new SimpleCamera();
        private ObserveCamera observeCamera = new ObserveCamera();

        Vector3 cameraPosition;
        Vector3 cameraFocusOn;
        Vector3 cameraUp;
        Matrix viewMatrix;
        Matrix projectionMatrix;
        float viewElevation;
        float viewAngle;

        /// <summary>
        /// Constructs a new default camera.
        /// </summary>
        public Camera()
        {
            SetUpCamera();
        }

        /// <summary>
        /// Sets up view and projection matrices.
        /// </summary>
        /// <param name="aspectRatio">The aspect ratio to use.</param>
        public void SetUpViewAndProjectionMatrices(float aspectRatio)
        {
            // Simple camera
            simpleCamera.ViewMatrix = Matrix.CreateLookAt(simpleCamera.CameraPosition, simpleCamera.CameraFocusOn, simpleCamera.CameraUp);
            simpleCamera.ProjectionMatrix = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.ToRadians(GameVariables.perspective),
                aspectRatio,
                1.0f,
                GameVariables.cameraMaxDistance);

            // Target camera
            targetCamera.ViewMatrix = Matrix.CreateLookAt(targetCamera.CameraPosition, targetCamera.CameraFocusOn, targetCamera.CameraUp);
            targetCamera.ProjectionMatrix = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.ToRadians(GameVariables.perspective),
                aspectRatio,
                1.0f,
                GameVariables.cameraMaxDistance);

            // Observe camera
            observeCamera.ViewMatrix = Matrix.CreateLookAt(observeCamera.CameraPosition, observeCamera.CameraFocusOn, observeCamera.CameraUp);
            observeCamera.ProjectionMatrix = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.ToRadians(GameVariables.perspective),
                aspectRatio,
                1.0f,
                GameVariables.cameraMaxDistance);
        }

        /// <summary>
        /// Specifies the mode of the camera.
        /// 
        /// Simple - Normal camera mode which can look left/right and up/down, and move in any direction.
        /// RotationalAim - The camera mode used for aiming. Rotates about the target ball.
        /// Observe - Fixed camera which zooms out to a specified distance. Used for observing the simulation.
        /// </summary>
        public CameraMode Mode
        {
            get
            {
                return mode;
            }
            set
            {
                // If changing to observation mode, we need to get the position/direction of the current camera
                if (value == CameraMode.Observe)
                {
                    if (mode == CameraMode.Simple)
                    {
                        observeCamera.UpdateCamera(simpleCamera.CameraPosition, simpleCamera.CameraFocusOn, simpleCamera.CameraUp);
                    }
                    else if (mode == CameraMode.RotationalAim)
                    {
                        observeCamera.UpdateCamera(targetCamera.CameraPosition, targetCamera.CameraFocusOn, targetCamera.CameraUp);
                    }
                }
                mode = value;
            }
        }

        /// <summary>
        /// Specifies which ball the camera should rotate about when in RotationalAim mode.
        /// </summary>
        public Ball TargetBall
        {
            get
            {
                return targetCamera.TargetBall;
            }
            set
            {
                targetCamera.TargetBall = value;
            }
        }

        /// <summary>
        /// The position of the camera.
        /// </summary>
        public Vector3 CameraPosition
        {
            get
            {
                if (mode == CameraMode.RotationalAim)
                {
                    return targetCamera.CameraPosition;
                }
                else if (mode == CameraMode.Simple)
                {
                    return simpleCamera.CameraPosition;
                }
                else if (mode == CameraMode.Observe)
                {
                    return observeCamera.CameraPosition;
                }
                else
                {
                    return Vector3.Zero;
                }
            }
            set
            {
                if (mode == CameraMode.RotationalAim)
                {
                    targetCamera.CameraPosition = value;
                }
                else if (mode == CameraMode.Simple)
                {
                    simpleCamera.CameraPosition = value;
                }
            }
        }

        /// <summary>
        /// The position toward which the camera should aim.
        /// </summary>
        public Vector3 CameraFocusOn
        {
            get
            {
                if (mode == CameraMode.RotationalAim)
                {
                    return targetCamera.CameraFocusOn;
                }
                else if (mode == CameraMode.Simple)
                {
                    return simpleCamera.CameraFocusOn;
                }
                else if (mode == CameraMode.Observe)
                {
                    return observeCamera.CameraFocusOn;
                }
                else
                {
                    return Vector3.Zero;
                }
            }
            set
            {
                if (mode == CameraMode.Simple)
                {
                    simpleCamera.CameraFocusOn = value;
                }
            }
        }

        /// <summary>
        /// The upward direction of the camera.
        /// </summary>
        public Vector3 CameraUp
        {
            get
            {
                if (mode == CameraMode.RotationalAim)
                {
                    return targetCamera.CameraUp;
                }
                else if (mode == CameraMode.Simple)
                {
                    return simpleCamera.CameraUp;
                }
                else if (mode == CameraMode.Observe)
                {
                    return observeCamera.CameraUp;
                }
                else
                {
                    return Vector3.Zero;
                }
            }
            set
            {
                if (mode == CameraMode.RotationalAim)
                {
                    targetCamera.CameraUp = value;
                }
                else if (mode == CameraMode.Simple)
                {
                    simpleCamera.CameraUp = value;
                }
            }
        }
        
        public Matrix ViewMatrix
        {
            get
            {
                if (mode == CameraMode.RotationalAim)
                {
                    return targetCamera.ViewMatrix;
                }
                else if (mode == CameraMode.Simple)
                {
                    return simpleCamera.ViewMatrix;
                }
                else if (mode == CameraMode.Observe)
                {
                    return observeCamera.ViewMatrix;
                }
                else
                {
                    return Matrix.Identity;
                }
            }
            set
            {
                if (mode == CameraMode.RotationalAim)
                {
                    targetCamera.ViewMatrix = value;
                }
                else if (mode == CameraMode.Simple)
                {
                    simpleCamera.ViewMatrix = value;
                }
            }
        }

        public Matrix ProjectionMatrix
        {
            get
            {
                if (mode == CameraMode.RotationalAim)
                {
                    return targetCamera.ProjectionMatrix;
                }
                else if (mode == CameraMode.Simple)
                {
                    return simpleCamera.ProjectionMatrix;
                }
                else if (mode == CameraMode.Observe)
                {
                    return observeCamera.ProjectionMatrix;
                }
                else
                {
                    return Matrix.Identity;
                }
            }
            set
            {
                if (mode == CameraMode.RotationalAim)
                {
                    targetCamera.ProjectionMatrix = value;
                }
                else if (mode == CameraMode.Simple)
                {
                    simpleCamera.ProjectionMatrix = value;
                }
            }
        }


        public float ViewAngle
        {
            get
            {
                if (mode == CameraMode.RotationalAim)
                {
                    return 0.0f;
                }
                else if (mode == CameraMode.Simple)
                {
                    return simpleCamera.ViewAngle;
                }
                else
                {
                    return 0.0f;
                }
            }
            set
            {
                if (mode == CameraMode.RotationalAim)
                {
                    //targetCamera.ViewAngle = value;
                }
                else if (mode == CameraMode.Simple)
                {
                    simpleCamera.ViewAngle = value;
                }
            }
        }

        /// <summary>
        /// Looks up. For a simple camera, the camera is pointed further up.
        /// For the target camera, the camera rotates upward around the target ball.
        /// </summary>
        /// <param name="elapsedTime">The time since elapsed the last update.</param>
        public void LookUp(float elapsedTime)
        {
            if (mode == CameraMode.RotationalAim)
            {
                targetCamera.LookUp(elapsedTime);
            }
            else if (mode == CameraMode.Simple)
            {
                simpleCamera.LookUp(elapsedTime);
            }
        }

        /// <summary>
        /// Looks up. For a simple camera, the camera is pointed further down.
        /// For the target camera, the camera rotates downward around the target ball.
        /// </summary>
        /// <param name="elapsedTime">The time since elapsed the last update.</param>
        public void LookDown(float elapsedTime)
        {
            if (mode == CameraMode.RotationalAim)
            {
                targetCamera.LookDown(elapsedTime);
            }
            else if (mode == CameraMode.Simple)
            {
                simpleCamera.LookDown(elapsedTime);
            }
        }

        /// <summary>
        /// Looks up. For a simple camera, the camera is rotated to the left.
        /// For the target camera, the camera rotates left around the target ball.
        /// </summary>
        /// <param name="elapsedTime">The time since elapsed the last update.</param>
        public void LookLeft(float elapsedTime)
        {
            if (mode == CameraMode.RotationalAim)
            {
                targetCamera.LookLeft(elapsedTime);
            }
            else if (mode == CameraMode.Simple)
            {
                simpleCamera.LookLeft(elapsedTime);
            }
        }

        /// <summary>
        /// Looks up. For a simple camera, the camera is rotated to the right.
        /// For the target camera, the camera rotates upward around the target ball.
        /// </summary>
        /// <param name="elapsedTime">The time since elapsed the last update.</param>
        public void LookRight(float elapsedTime)
        {
            if (mode == CameraMode.RotationalAim)
            {
                targetCamera.LookRight(elapsedTime);
            }
            else if (mode == CameraMode.Simple)
            {
                simpleCamera.LookRight(elapsedTime);
            }
        }

        /// <summary>
        /// Moves the camera forward.
        /// </summary>
        /// <param name="elapsedTime">The time since elapsed the last update.</param>
        public void MoveForward(float elapsedTime)
        {
            if (mode == CameraMode.RotationalAim)
            {
                targetCamera.MoveForward(elapsedTime);
            }
            else if (mode == CameraMode.Simple)
            {
                simpleCamera.MoveForward(elapsedTime);
            }
        }

        /// <summary>
        /// Moves the camera backward.
        /// </summary>
        /// <param name="elapsedTime">The time since elapsed the last update.</param>
        public void MoveBackward(float elapsedTime)
        {
            if (mode == CameraMode.RotationalAim)
            {
                targetCamera.MoveBackward(elapsedTime);
            }
            else if (mode == CameraMode.Simple)
            {
                simpleCamera.MoveBackward(elapsedTime);
            }
        }

        /// <summary>
        /// Moves the camera to the left. Only allowed in simple mode.
        /// </summary>
        /// <param name="elapsedTime">The time since elapsed the last update.</param>
        public void MoveLeft(float elapsedTime)
        {
            if (mode == CameraMode.Simple)
            {
                simpleCamera.MoveLeft(elapsedTime);
            }
        }

        /// <summary>
        /// Moves the camera to the right. Only allowed in simple mode.
        /// </summary>
        /// <param name="elapsedTime">The time since elapsed the last update.</param>
        public void MoveRight(float elapsedTime)
        {
            if (mode == CameraMode.Simple)
            {
                simpleCamera.MoveRight(elapsedTime);
            }
        }

        /// <summary>
        /// Moves the camera up. Only allowed in simple mode.
        /// </summary>
        /// <param name="elapsedTime">The time since elapsed the last update.</param>
        public void MoveUp(float elapsedTime)
        {
            if (mode == CameraMode.Simple)
            {
                simpleCamera.MoveUp(elapsedTime);
            }
        }

        /// <summary>
        /// Moves the camera down. Only allowed in simple mode.
        /// </summary>
        /// <param name="elapsedTime">The time since elapsed the last update.</param>
        public void MoveDown(float elapsedTime)
        {
            if (mode == CameraMode.Simple)
            {
                simpleCamera.MoveDown(elapsedTime);
            }
        }

        /// <summary>
        /// Updates the camera.
        /// </summary>
        public void UpdateCamera()
        {
            if (mode == CameraMode.RotationalAim)
            {
                targetCamera.UpdateCamera();
            }
            else if (mode == CameraMode.Simple)
            {
                simpleCamera.UpdateCamera();
            }
        }

        public void Reset()
        {
            SetUpCamera();
            targetCamera.Reset();
        }

        private void SetUpCamera()
        {
            cameraPosition = new Vector3(0.0f, 50.0f, 50.0f);
            cameraFocusOn = new Vector3(0.0f, 50.0f, 50.0f);
            cameraUp = Vector3.Up;

            mode = CameraMode.Simple;
        }


    }
}
