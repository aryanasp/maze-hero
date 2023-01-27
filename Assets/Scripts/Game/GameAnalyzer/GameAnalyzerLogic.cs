using System;
using System.Linq;
using Ai;
using Game.GameAnalyzer;
using Log;
using Map;
using Zenject;

namespace Game.GameAnalyzer
{
    public class GameAnalyzerLogic : IDisposable
    {
        private GameConfig _gameConfig;
        private GameStatModel _gameStatModel;
        private GameTimeModel _gameTimeModel;
        private CurrentRoundStatModel _currentRoundStatModel;
        private GeneticAlgorithmConfig _geneticAlgorithmConfig;
        private AiGeneticAlgorithmModel _geneticAlgorithmModel;
        private GameAnalyzerModel _gameAnalyzerModel;
        private MazeConfig _mazeConfig;
        private int _totalApples;
        [Inject]
        public GameAnalyzerLogic(GameConfig gameConfig, GameStatModel gameStatModel,
            GameTimeModel gameTimeModel, CurrentRoundStatModel currentRoundStatModel,
            GeneticAlgorithmConfig geneticAlgorithmConfig, AiGeneticAlgorithmModel geneticAlgorithmModel,
            GameAnalyzerModel gameAnalyzerModel, MazeConfig mazeConfig)
        {
            _gameConfig = gameConfig;
            _gameStatModel = gameStatModel;
            _gameTimeModel = gameTimeModel;
            _currentRoundStatModel = currentRoundStatModel;
            _geneticAlgorithmConfig = geneticAlgorithmConfig;
            _geneticAlgorithmModel = geneticAlgorithmModel;
            _gameAnalyzerModel = gameAnalyzerModel;
            _mazeConfig = mazeConfig;
            _gameAnalyzerModel.TriggerAnalyzeGame += OnAnalyze;
            foreach (var mapObject in _mazeConfig.mapObjects)
            {
                if (mapObject.blockType == BlockType.Apple)
                {
                    _totalApples++;
                }
            }
        }
        
        public void Dispose()
        {
            _gameAnalyzerModel.TriggerAnalyzeGame -= OnAnalyze;
        }

        private void OnAnalyze()
        {
            PrintScores();
            _gameStatModel.RecordRoundStat(_gameTimeModel.CurrentRound, AnalyzeGame());
            _currentRoundStatModel.Reset();
        }

        private RoundStat AnalyzeGame()
        {
            int sumScores = _geneticAlgorithmModel.Agents.Sum(geneticAlgorithmAgent => geneticAlgorithmAgent.GetScore());
            var averageScores = sumScores * 1d / _geneticAlgorithmModel.Agents.Count;
            var peopleHigherThanAverage =
                _geneticAlgorithmModel.Agents.FindAll(agent => agent.GetScore() >= averageScores);
            var roundStat = new RoundStat
            {
                appleEaten = _currentRoundStatModel.ApplesEaten,
                maxScoreToApplesTotalScoreFragment = _currentRoundStatModel.MaxScore * 1d / _totalApples,
                peopleCountHigherThanAverage = peopleHigherThanAverage.Count,
                percentageOfHigherThanAveragePeople =
                    peopleHigherThanAverage.Count * 1d / _geneticAlgorithmModel.Agents.Count,
                maxScoreToAverageFragment = averageScores == 0?99999d : _currentRoundStatModel.MaxScore / averageScores
            };
            return roundStat;
        }

        private void PrintScores()
        {
            string scoresPrint = "";
            _geneticAlgorithmModel.Agents.ForEach(agent => { scoresPrint += agent.GetScore() + ", "; });
            Logger.Log(scoresPrint, true);
        }
    }
}