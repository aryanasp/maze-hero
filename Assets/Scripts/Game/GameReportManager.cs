using System;
using Zenject;

namespace Game
{
    public class GameReportManager : IDisposable
    {
        private readonly GameStatModel _gameStatModel;
        private readonly GameConfig _gameConfig;

        public void Dispose()
        {
            _gameStatModel.OnGameFinish -= DoReport;
        }

        [Inject]
        public GameReportManager(GameStatModel gameStatModel, GameConfig gameConfig)
        {
            _gameStatModel = gameStatModel;
            _gameConfig = gameConfig;
            _gameStatModel.OnGameFinish += DoReport;
        }
        private void DoReport()
        {
            if (!_gameConfig.doReport)
            {
                return;
            }
            //TODO: Generate a CSV file by indexing round number and inputs in game config and genetic algorithm config
            foreach (var keyValue in _gameStatModel.RoundsStat)
            {
                
            }
            throw new System.NotImplementedException();
        }

    }
}