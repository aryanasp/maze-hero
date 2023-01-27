using System;
using Zenject;

namespace Game
{
    public class GameReportManager : IDisposable
    {
        private readonly GameStatModel _gameStatModel;
        public void Dispose()
        {
            _gameStatModel.OnGameFinish -= DoReport;
        }

        [Inject]
        public GameReportManager(GameStatModel gameStatModel)
        {
            _gameStatModel = gameStatModel;
            _gameStatModel.OnGameFinish += DoReport;
        }
        private void DoReport(bool doReport)
        {
            if (!doReport) return;
            //TODO: Generate a CSV file by indexing round number and inputs in game config and genetic algorithm config
            foreach (var keyValue in _gameStatModel.RoundsStat)
            {
                
            }
            throw new System.NotImplementedException();
        }

    }
}