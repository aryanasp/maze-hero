using System;

namespace Game
{
    public class GameTimeModel
    {
        private bool _isStartingRoundAgain = true;
        private int _currentRound;
        private int _currentRoundTimePassed;

        public Action OnGameStartingRoundAgainStateChange = () => { };
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

        public bool IsStartingRoundAgain
        {
            get => _isStartingRoundAgain;
            set
            {
                if (_isStartingRoundAgain == value)
                {
                    return;
                }
                _isStartingRoundAgain = value;
                OnGameStartingRoundAgainStateChange.Invoke();
            }
        }
    }
}