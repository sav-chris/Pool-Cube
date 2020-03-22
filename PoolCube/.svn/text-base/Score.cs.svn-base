using System;
using System.Collections.Generic;
using System.Text;

namespace PoolCube
{
    class Score
    {
        int numBallsSunk;
        int shotsTaken;
        int cueBallsSunk;
        List<int> ballsSunk;

        /// <summary>
        /// Constructs a new Score.
        /// </summary>
        public Score()
        {
            numBallsSunk = 0;
            shotsTaken = 0;
            cueBallsSunk = 0;
            ballsSunk = new List<int>();
        }

        /// <summary>
        /// Specifies that a ball with a specified number has been sunk.
        /// </summary>
        /// <param name="ball">The number of the ball sunk.</param>
        public void BallSunk(int ball)
        {
            numBallsSunk++;
            ballsSunk.Add(ball);
        }

        /// <summary>
        /// Specifies that a shot has just been taken.
        /// </summary>
        public void ShotTaken()
        {
            shotsTaken++;
        }

        /// <summary>
        /// Specifies that the cue ball has been sunk.
        /// </summary>
        public void CueBallSunk()
        {
            cueBallsSunk++;
        }

        /// <summary>
        /// Determines whether the game has ended according to this scoring system.
        /// </summary>
        /// <returns>True if the game has ended, false otherwise.</returns>
        public bool GameOver()
        {
            int layers = GameVariables.ballLayers;
            int numberOfBalls = (layers * (layers+1) * (layers+2)) / 6;
            return numBallsSunk >= numberOfBalls;
        }

        public int NumBallsSunk
        {
            get
            {
                return numBallsSunk;
            }
        }

        public int ShotsTaken
        {
            get
            {
                return shotsTaken;
            }
        }

        public int CueBallsSunk
        {
            get
            {
                return cueBallsSunk;
            }
        }
    }
}
