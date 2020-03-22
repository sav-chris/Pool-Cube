using System;
using System.Collections.Generic;
using System.Text;

namespace PoolCube
{
    /// <summary>
    /// Specifies what mode the camera is in.
    /// 
    /// Simple - allows the camera to move freely within the pool cube.
    /// RotationalAim - the camera rotates about the target ball.
    /// Observe - moves back to a fixed distance from the centre, to allow the camera to observe
    /// the entire pool cube.
    /// </summary>
    enum CameraMode
    {
        Simple,
        RotationalAim,
        Observe
    }
}
