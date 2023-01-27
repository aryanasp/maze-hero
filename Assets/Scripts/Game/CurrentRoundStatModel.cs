using System;

namespace Game
{
    public class CurrentRoundStatModel
    {
        private int _applesEaten;
        public Action OnAppleEat = () => { };

        public int ApplesEaten
        {
            get => _applesEaten;
            set
            {
                if (_applesEaten == value)
                {
                    return;
                }
                _applesEaten = value;
                OnAppleEat.Invoke();
            }
        }
        private int _maxScore;
        public Action OnMaxScoreChange = () => {};

        public int MaxScore
        {
            get => _maxScore;
            set
            {
                if (_maxScore == value)
                {
                    return;
                }
                _maxScore = value;
                OnMaxScoreChange.Invoke();
            }
        }

        public void CheckForMaxScore(int maxScore)
        {
            if (maxScore > MaxScore)
            {
                MaxScore = maxScore;
            }
        }
    }
}