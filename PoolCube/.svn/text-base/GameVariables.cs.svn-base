using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using PhysicsEngine;
using Microsoft.Xna.Framework.Graphics;

namespace PoolCube
{
    class GameVariables
    {
        // Game Variables
        public static int numBalls = 10;
        public static GameMode initialGameMode = GameMode.Aim;

        // Camera Adjustment Variables
        public static float angleAdjustment     = 0.1f; // Total radians per second angle adjustment
        public const  float perspective         = 40.0f;
        public static float movementAmount      = 20; // Total movement per second
        public const  float cameraMaxDistance   = 6000f;
        public const  float observationRadius   = 500f;//1000f;
        public static float slowAngleAdjustment = 0.05f;
        public static float fastAngleAdjustment = 0.3f;
        public static bool  orthographicProj    = false;
        public static float minZoomDistance     = 100.0f;
        public static float maxZoomDistance     = observationRadius;

        // Physics Variables
        public static Vector3          gravity          = Vector3.Zero;
        public static Vector3          windSpeed        = Vector3.Zero;
        public static float            elasticityFactor = 1.0f; 
        public static ElasticCollision elasticCollision = new ElasticCollision(elasticityFactor);

        // Room Variables
        public const  float wallHalfWidth      = 78.7402f;//236.2205f;
        public const  float wallHalfThickness  = 9.8425f;//8.1240f;
        public const  float wallMass           = 1.0f;
        public static float wallBoundingRadius = 2.0f * (float)Math.Sqrt(2.0f) * wallHalfWidth;

        // Ball Variables
        public static int     ballLayers             = 4;
        public static Vector3 initialBallOrientation = Vector3.Zero;
        public const  float   ballMass               = 1.0f;
        public static float   ballAirResistance      = 0.3f;//0.5f;
        public static float   ballElasticity         = 1.0f;
        public static Vector3 initialBallPosition    = Vector3.Zero;
        public static float   ballScale              = 2.0f;
        public static float ballRadius             = 3.0f * ballScale;
        public static Vector3 ballSeparation
            = 1.01f * ballRadius * new Vector3((float)(Math.Sqrt(8)/Math.Sqrt(3)), (float)(Math.Sqrt(3)), 2); //ballRadius * new Vector3((float)(Math.Sqrt(3)/Math.Sqrt(2)), (float)(Math.Sqrt(3)/2), 1);  // ballScale*1.01f
        public static Vector3 cueBallPosition        = -75.0f * Vector3.UnitX;
        public static float   ballStopSpeed          = 4.0f;
        public static float   airResistanceMinSpeed  = 15.0f;

        // Shooting Variables
        public static float defaultBallSpeed = 150.0f;
        public static float shotPowerIncreasePerSecond = 1.0f;
        public static float speedMultipier = 14.0f;
        public static float cameraDistanceFromBall = 300.0f;

        // Screen Variables
        public static int screenWidth = 1680;
        public static int screenHeight = 1050;
        public static int textLineSpacing = 25;
        public static Color textColour = Color.Red;
        public static int meterMaxWidth = 300;
        public static Color meterColour = Color.Black;
        public static int meterHeight = 20;
        public static int meterDistanceFromBottom = 30;
        public static int meterEndWidth = 4;
        public static int meterEndHeight = 26;
        public static Color backgroundColour = Color.LightSkyBlue;
        public static bool fullScreen = true;
    }
}
