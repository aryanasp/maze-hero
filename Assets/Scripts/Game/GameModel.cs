using System;
using UnityEngine;

namespace Game
{
    public class GameModel
    {
        private bool _isStarted;
        private int _currentRound;
        private int _currentRoundTimePassed;

        public Action OnGameStarted = () => { };
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
            set => _currentRoundTimePassed = value;
        }

        public bool IsStarted
        {
            get => _isStarted;
            set
            {
                if (_isStarted == value)
                {
                    return;
                }
                _isStarted = value;
                OnGameStarted.Invoke();
            }
        }
    }
}