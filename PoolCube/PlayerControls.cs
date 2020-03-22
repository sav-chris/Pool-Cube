using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace PoolCube
{
    class PlayerControls
    {
        GameMode mode;

        /// <summary>
        /// Constructs a new PlayerControls.
        /// </summary>
        public PlayerControls()
        {
            mode = GameMode.Aim;
        }

        public GameMode Mode
        {
            get
            {
                return mode;
            }
            set
            {
                mode = value;
            }
        }

        /// <summary>
        /// Determines whether the key used to exit the game is being pressed.
        /// </summary>
        /// <returns>True if the key used to exit the game is being pressed; false otherwise.</returns>
        public bool Exit()
        {
            return GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
                || Keyboard.GetState().IsKeyDown(Keys.Escape);
        }

        /// <summary>
        /// Determines whether the key used to enter debug mode is being pressed.
        /// </summary>
        /// <returns>True if key used to enter debug mode is being pressed; false otherwise.</returns>
        public bool SwitchToDebug()
        {
            return Keyboard.GetState().IsKeyDown(Keys.F12);
        }

        /// <summary>
        /// Determines whether the key used to switch perspective is being pressed.
        /// </summary>
        /// <returns>True if key used to switch perspective is being pressed; false otherwise.</returns>
        public bool SwitchPerspective()
        {
            return (Keyboard.GetState().IsKeyDown(Keys.F2) || GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.Y));
        }

        public bool Reset()
        {
            return Keyboard.GetState().IsKeyDown(Keys.R);
        }

        //-------------------------------------------------------------------------------------------------------------------------
        // Aiming controls
        //-------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Determines whether the key used to increase the adjustment speed is being pressed.
        /// </summary>
        /// <returns>True if key used to increase the adjustment speed is being pressed; false otherwise.</returns>
        public bool FastAdjustment()
        {
            return mode == GameMode.Aim && (Keyboard.GetState().IsKeyDown(Keys.LeftControl) || GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.LeftTrigger));
        }

        /// <summary>
        /// Determines whether the key used to aim left is being pressed.
        /// </summary>
        /// <returns>True if key used to aim left is being pressed; false otherwise.</returns>
        public bool AimLeft()
        {
            return mode == GameMode.Aim && (Keyboard.GetState().IsKeyDown(Keys.Left) || GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X < 0);
        }

        /// <summary>
        /// Determines whether the key used to aim left is being pressed.
        /// </summary>
        /// <returns>True if key used to aim left is being pressed; false otherwise.</returns>
        public bool AimRight()
        {
            return mode == GameMode.Aim && (Keyboard.GetState().IsKeyDown(Keys.Right) || GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X > 0);
        }

        /// <summary>
        /// Determines whether the key used to aim right is being pressed.
        /// </summary>
        /// <returns>True if key used to aim right is being pressed; false otherwise.</returns>
        public bool AimUp()
        {
            return mode == GameMode.Aim && (Keyboard.GetState().IsKeyDown(Keys.Up) || GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y > 0);
        }

        /// <summary>
        /// Determines whether the key used to aim down is being pressed.
        /// </summary>
        /// <returns>True if key used to aim down is being pressed; false otherwise.</returns>
        public bool AimDown()
        {
            return mode == GameMode.Aim && (Keyboard.GetState().IsKeyDown(Keys.Down) || GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y < 0);
        }

        /// <summary>
        /// Determines whether the key used to shoot is being pressed.
        /// </summary>
        /// <returns>True if key used to shoot is being pressed; false otherwise.</returns>
        public bool Shoot()
        {
            return mode == GameMode.Aim && (Keyboard.GetState().IsKeyDown(Keys.Space) || GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.A));
        }

        /// <summary>
        /// Determines whether the key used to decrease the adjustment speed is being pressed.
        /// </summary>
        /// <returns>True if key used to decrease the adjustment speed is being pressed; false otherwise.</returns>
        public bool SlowDown()
        {
            return mode == GameMode.Aim && (Keyboard.GetState().IsKeyDown(Keys.LeftAlt) || GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.RightTrigger));
        }

        /// <summary>
        /// Determines whether the key used to zoom in is being pressed.
        /// </summary>
        /// <returns>True if key used to zoom in is being pressed; false otherwise.</returns>
        public bool ZoomIn()
        {
            return mode == GameMode.Aim && (Keyboard.GetState().IsKeyDown(Keys.Add)) || (Keyboard.GetState().IsKeyDown(Keys.OemPlus));

        }

        /// <summary>
        /// Determines whether the key used to zoom out is being pressed.
        /// </summary>
        /// <returns>True if key used to zoom out is being pressed; false otherwise.</returns>
        public bool ZoomOut()
        {
            return mode == GameMode.Aim && (Keyboard.GetState().IsKeyDown(Keys.Subtract)) || (Keyboard.GetState().IsKeyDown(Keys.OemMinus));
        }

        //-------------------------------------------------------------------------------------------------------------------------
        // Debug controls
        //-------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Determines whether the key used to move left in debug mode is being pressed.
        /// </summary>
        /// <returns>True if key used to move left is being pressed; false otherwise.</returns>
        public bool DebugMoveLeft()
        {
            return mode == GameMode.Debug && (Keyboard.GetState().IsKeyDown(Keys.A));
        }

        /// <summary>
        /// Determines whether the key used to move right in debug mode is being pressed.
        /// </summary>
        /// <returns>True if key used to move right is being pressed; false otherwise.</returns>
        public bool DebugMoveRight()
        {
            return mode == GameMode.Debug && (Keyboard.GetState().IsKeyDown(Keys.D));
        }

        /// <summary>
        /// Determines whether the key used to move forward in debug mode is being pressed.
        /// </summary>
        /// <returns>True if key used to move forward is being pressed; false otherwise.</returns>
        public bool DebugMoveForward()
        {
            return mode == GameMode.Debug && (Keyboard.GetState().IsKeyDown(Keys.W));
        }

        /// <summary>
        /// Determines whether the key used to move back in debug mode is being pressed.
        /// </summary>
        /// <returns>True if key used to move back is being pressed; false otherwise.</returns>
        public bool DebugMoveBack()
        {
            return mode == GameMode.Debug && (Keyboard.GetState().IsKeyDown(Keys.S));
        }

        /// <summary>
        /// Determines whether the key used to move up in debug mode is being pressed.
        /// </summary>
        /// <returns>True if key used to move up is being pressed; false otherwise.</returns>
        public bool DebugMoveUp()
        {
            return mode == GameMode.Debug && (Keyboard.GetState().IsKeyDown(Keys.LeftShift));
        }

        /// <summary>
        /// Determines whether the key used to move down in debug mode is being pressed.
        /// </summary>
        /// <returns>True if key used to move down is being pressed; false otherwise.</returns>
        public bool DebugMoveDown()
        {
            return mode == GameMode.Debug && (Keyboard.GetState().IsKeyDown(Keys.LeftControl));
        }

        /// <summary>
        /// Determines whether the key used to look left in debug mode is being pressed.
        /// </summary>
        /// <returns>True if key used to look left is being pressed; false otherwise.</returns>
        public bool DebugLookLeft()
        {
            return mode == GameMode.Debug && (Keyboard.GetState().IsKeyDown(Keys.Left));
        }

        /// <summary>
        /// Determines whether the key used to look right in debug mode is being pressed.
        /// </summary>
        /// <returns>True if key used to look right is being pressed; false otherwise.</returns>
        public bool DebugLookRight()
        {
            return mode == GameMode.Debug && (Keyboard.GetState().IsKeyDown(Keys.Right));
        }

        /// <summary>
        /// Determines whether the key used to look up in debug mode is being pressed.
        /// </summary>
        /// <returns>True if key used to look up is being pressed; false otherwise.</returns>
        public bool DebugLookUp()
        {
            return mode == GameMode.Debug && (Keyboard.GetState().IsKeyDown(Keys.Up));
        }

        /// <summary>
        /// Determines whether the key used to look down in debug mode is being pressed.
        /// </summary>
        /// <returns>True if key used to look down is being pressed; false otherwise.</returns>
        public bool DebugLookDown()
        {
            return mode == GameMode.Debug && (Keyboard.GetState().IsKeyDown(Keys.Down));
        }

        /// <summary>
        /// Determines whether the key used to switch to simple camera in debug mode is being pressed.
        /// </summary>
        /// <returns>True if key used to switch to simple camera is being pressed; false otherwise.</returns>
        public bool DebugSwitchToSimpleCamera()
        {
            return mode == GameMode.Debug && (Keyboard.GetState().IsKeyDown(Keys.NumPad1));
        }

        /// <summary>
        /// Determines whether the key used to switch to the aiming camera in debug mode is being pressed.
        /// </summary>
        /// <returns>True if key used to switch to the aiming camera is being pressed; false otherwise.</returns>
        public bool DebugSwitchToAimCamera()
        {
            return mode == GameMode.Debug && (Keyboard.GetState().IsKeyDown(Keys.NumPad2));
        }

        /// <summary>
        /// Determines whether the key used to switch the observe camera in debug mode is being pressed.
        /// </summary>
        /// <returns>True if key used to switch to the observe camera is being pressed; false otherwise.</returns>
        public bool DebugSwitchToObserveCamera()
        {
            return mode == GameMode.Debug && (Keyboard.GetState().IsKeyDown(Keys.NumPad3));
        }
    }
}
