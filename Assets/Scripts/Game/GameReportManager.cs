using System;
using System.Globalization;
using System.IO;
using System.Text;
using Ai;
using UnityEngine;
using Zenject;

namespace Game
{
    public class GameReportManager : IDisposable
    {
        private readonly GameStatModel _gameStatModel;
        private readonly GameConfig _gameConfig;
        private readonly GeneticAlgorithmConfig _geneticAlgorithmConfig;

        public void Dispose()
        {
            _gameStatModel.OnGameFinish -= DoReport;
        }

        [Inject]
        public GameReportManager(GameStatModel gameStatModel, GameConfig gameConfig,
            GeneticAlgorithmConfig geneticAlgorithmConfig)
        {
            _gameStatModel = gameStatModel;
            _gameConfig = gameConfig;
            _geneticAlgorithmConfig = geneticAlgorithmConfig;
            _gameStatModel.OnGameFinish += DoReport;
        }

        private void DoReport()
        {
            if (!_gameConfig.doReport)
            {
                return;
            }

            var reportName =
                $"__Report_" +
                $"_RoundCount_{_gameConfig.roundCount}_" +
                $"_RoundDuration_{_gameConfig.roundDuration}_" +
                $"_InitialPopulation_{_geneticAlgorithmConfig.initialPopulation}_" +
                $"_Heuristic_{_geneticAlgorithmConfig.initialHeuristic}_" +
                $"_AlgorithmType_{_geneticAlgorithmConfig.geneticAlgorithmType}_" +
                $"_PersonMutate_{_geneticAlgorithmConfig.chanceToMutateNewPerson}_" +
                $"_GeneMutate_{_geneticAlgorithmConfig.geneMutationChance}_" +
                $"_UpdateEpochTime_{_geneticAlgorithmConfig.updateEpochTime}_";
            var stringBuilder = new StringBuilder();
            //TODO: Generate a CSV file by indexing round number and inputs in game config and genetic algorithm config
            using var writer = new StreamWriter($"{Application.persistentDataPath}/{reportName}.csv");
            stringBuilder.AppendLine(
                "Round Number, Apples Eaten, Max Score Fragment, Higher Than Average People Fragment, Max Score To Average Fragment");
            foreach (var keyValue in _gameStatModel.RoundsStat)
            {
                stringBuilder.AppendLine(
                    $"{keyValue.Key}, {keyValue.Value.appleEaten}," +
                    $" {(keyValue.Value.maxScoreToApplesTotalScoreFragment * 100f).ToString(CultureInfo.InvariantCulture)}," +
                    $" {(keyValue.Value.percentageOfHigherThanAveragePeople * 100f).ToString(CultureInfo.InvariantCulture)}," +
                    $" {(keyValue.Value.maxScoreToAverageFragment * 100f).ToString(CultureInfo.InvariantCulture)}");
            }

            writer.Write(stringBuilder.ToString());
        }
    }
}