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
        public Action<bool> OnGameFinish = (doReport) => { };
        
        public GameStatModel()
        {
            RoundsStat = new Dictionary<int, RoundStat>();
        }

        public void RecordRoundStat(int roundNumber, RoundStat roundStat)
        {
            RoundsStat.Add(roundNumber, roundStat);
        }

        public void FinishGame(bool doReport)
        {
            OnGameFinish.Invoke(doReport);
            ClearLastGameStat();
        }
        
        private void ClearLastGameStat()
        {
            RoundsStat.Clear();
        }
    }
}