using System;

namespace Game.GameAnalyzer
{
    public class GameAnalyzerModel
    {
        public Action TriggerAnalyzeGame = () => { };

        public bool TriggerStartAnalyze
        {
            set
            {
                if (value)
                {
                    TriggerAnalyzeGame.Invoke();
                }
            }
        }
    }
}