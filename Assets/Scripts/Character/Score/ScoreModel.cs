using System;
using System.Collections.Generic;

namespace Character
{
    public class ScoreModel : IComparable<ScoreModel>
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

        public int CompareTo(ScoreModel other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return _score.CompareTo(other._score);
        }
    }
}