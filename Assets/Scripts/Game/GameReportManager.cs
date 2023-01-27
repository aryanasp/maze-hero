using System;
using System.Globalization;
using System.IO;
using System.Text;
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
            var stringBuilder = new StringBuilder();
            //TODO: Generate a CSV file by indexing round number and inputs in game config and genetic algorithm config
            using var writer = new StreamWriter("Assets/Resources/Report.csv");
            stringBuilder.AppendLine(
                "Round Number, Apples Eaten, Max Score Fragment, Higher Than Average People Fragment, Max Score To Average Fragment");
            foreach (var keyValue in _gameStatModel.RoundsStat)
            {
                stringBuilder.AppendLine($"{keyValue.Key}, {keyValue.Value.appleEaten}, {keyValue.Value.maxScoreToApplesTotalScoreFragment * 100d}, {keyValue.Value.percentageOfHigherThanAveragePeople * 100d}, {keyValue.Value.maxScoreToAverageFragment * 100d}");
            }
            writer.Write(stringBuilder.ToString());
        }
    }
}