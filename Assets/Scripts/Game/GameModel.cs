using System;
using UnityEngine;

namespace Game
{
    public class GameModel
    {
        private bool _isPausing = true;
        private int _currentRound;
        private int _currentRoundTimePassed;

        public Action OnGamePausedChanged = () => { };
        public Action OnRoundChanged = () => { };
        public Action OnRoundTimePassedUpdate = () => { };

        public int CurrentRound
        {
            get => _currentRound;
            set
            {
                if (_currentRound == value) return;
                _currentRound = value;
                OnRoundChanged.Invoke();
            }
        }

        public int CurrentRoundTimePassed
        {
            get => _currentRoundTimePassed;
            set
            {
                if (_currentRoundTimePassed == value)
                {
                    return;
                }

                _currentRoundTimePassed = value;
                OnRoundTimePassedUpdate.Invoke();
            }
        }

        public bool IsPausing
        {
            get => _isPausing;
            set
            {
                if (_isPausing == value)
                {
                    return;
                }
                _isPausing = value;
                OnGamePausedChanged.Invoke();
            }
        }
    }
}