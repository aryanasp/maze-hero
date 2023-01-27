using System;
using System.Collections.Generic;

namespace Game
{
    [Serializable]
    public struct RoundStat
    {
        public int appleEaten;
        public int maxScore;
        public double percentageOfHigherThanAveragePeople;
        public double maxScorePerAverage;
    }
    
    public class GameStatModel
    {
        public readonly Dictionary<int, RoundStat> RoundsStat;
        public Action OnGameFinish = () => { };
        
        public GameStatModel()
        {
            RoundsStat = new Dictionary<int, RoundStat>();
        }

        public void RecordRoundStat(int roundNumber, RoundStat roundStat)
        {
            RoundsStat.Add(roundNumber, roundStat);
        }

        public void FinishGame()
        {
            OnGameFinish.Invoke();
            ClearLastGameStat();
        }
        
        private void ClearLastGameStat()
        {
            RoundsStat.Clear();
        }
    }
}