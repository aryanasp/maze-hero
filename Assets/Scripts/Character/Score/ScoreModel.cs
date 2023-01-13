using System;

namespace Character
{
    public class ScoreModel
    {
        private int _score;

        public Action OnScoreChange = () => { };


        public int Score
        {
            get => _score;
            set
            {
                if (_score.Equals(value))
                {
                    return;
                }
                _score = value;
                OnScoreChange.Invoke();
            }
        }
    }
}