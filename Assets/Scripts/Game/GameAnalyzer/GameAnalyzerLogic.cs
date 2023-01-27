using System;
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
        private MapModel _mapModel;

        [Inject]
        public GameAnalyzerLogic(GameConfig gameConfig, GameStatModel gameStatModel,
            GameTimeModel gameTimeModel, CurrentRoundStatModel currentRoundStatModel,
            GeneticAlgorithmConfig geneticAlgorithmConfig, AiGeneticAlgorithmModel geneticAlgorithmModel,
            GameAnalyzerModel gameAnalyzerModel, MapModel mapModel)
        {
            _gameConfig = gameConfig;
            _gameStatModel = gameStatModel;
            _gameTimeModel = gameTimeModel;
            _currentRoundStatModel = currentRoundStatModel;
            _geneticAlgorithmConfig = geneticAlgorithmConfig;
            _geneticAlgorithmModel = geneticAlgorithmModel;
            _gameAnalyzerModel = gameAnalyzerModel;
            _mapModel = mapModel;
            _gameAnalyzerModel.TriggerAnalyzeGame += OnAnalyze;
        }
        
        public void Dispose()
        {
            _gameAnalyzerModel.TriggerAnalyzeGame -= OnAnalyze;
        }

        private void OnAnalyze()
        {
            PrintScores();
            _gameStatModel.RecordRoundStat(_gameTimeModel.CurrentRound, AnalyzeGame());
        }

        private RoundStat AnalyzeGame()
        {
            int sumScores = 0;
            foreach (var geneticAlgorithmAgent in _geneticAlgorithmModel.Agents)
            {
                sumScores += geneticAlgorithmAgent.GetScore();
            }
            var averageScores = sumScores * 1d / _geneticAlgorithmModel.Agents.Count;
            var peopleHigherThanAverage =
                _geneticAlgorithmModel.Agents.FindAll(agent => agent.GetScore() > averageScores);
            var roundStat = new RoundStat
            {
                appleEaten = _currentRoundStatModel.ApplesEaten,
                maxScore = _currentRoundStatModel.MaxScore,
                percentageOfHigherThanAveragePeople =
                    peopleHigherThanAverage.Count * 1d / _geneticAlgorithmConfig.initialPopulation
            };
            if (averageScores == 0)
            {
                roundStat.maxScorePerAverage = 99999d;
            }
            else
            {
                roundStat.maxScorePerAverage = _currentRoundStatModel.MaxScore / averageScores;
            }
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