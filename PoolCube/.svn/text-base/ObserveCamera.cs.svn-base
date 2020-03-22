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
    class ObserveCamera
    {
        Vector3 cameraPosition;
        Vector3 cameraFocusOn;
        Vector3 cameraUp;
        Matrix viewMatrix;
        Matrix projectionMatrix;

        /// <summary>
        /// Constructs a new ObserveCamera.
        /// </summary>
        public ObserveCamera()
        {
            cameraPosition = Vector3.Zero;
            cameraFocusOn = Vector3.One;
            cameraUp = Vector3.Up;
        }

        /// <summary>
        /// Updates the camera. Gets the current direction of the camera, and points in that
        /// direction toward the centre of the pool cube from a set distance.
        /// </summary>
        /// <param name="currentPosition"></param>
        /// <param name="currentFocusOn"></param>
        /// <param name="cameraUp"></param>
        public void UpdateCamera(Vector3 currentPosition, Vector3 currentFocusOn, Vector3 cameraUp)
        {
            Vector3 cameraDirection = currentFocusOn - currentPosition; // F - C
            cameraDirection.Normalize();

            cameraPosition = -GameVariables.observationRadius * cameraDirection;
            this.cameraUp = cameraUp;
        }

        public Vector3 CameraPosition
        {
            get
            {
                return cameraPosition;
            }
        }

        public Vector3 CameraFocusOn
        {
            get
            {
                return cameraFocusOn;
            }
        }

        public Vector3 CameraUp
        {
            get
            {
                return cameraUp;
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
    }
}
