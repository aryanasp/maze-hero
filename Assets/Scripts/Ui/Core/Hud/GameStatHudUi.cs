using System;
using System.Linq;
using Ai;
using Game;
using Map;
using TMPro;
using UnityEngine;
using Zenject;

namespace Ui.Core.Hud
{
    public class GameStatHudUi : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI eatenApple;
        [SerializeField] private TextMeshProUGUI maxScore;
        [SerializeField] private TextMeshProUGUI peopleMoreThanAverage;

        [Inject] private CurrentRoundStatModel _currentRoundStatModel;
        [Inject] private GameStatModel _gameStatModel;
        [Inject] private GameTimeModel _gameTimeModel;
        [Inject] private AiGeneticAlgorithmModel _geneticAlgorithmModel;
        [Inject] private MapModel _mapModel;
        private int _totalApples;
        
        private void Start()
        {
            foreach (var _ in _mapModel.Tiles.Values.Where(tiles => tiles.HasBlock).Where(tiles => tiles.BlockType == BlockType.Apple))
            {
                _totalApples++;
            }
            _currentRoundStatModel.OnAppleEat += UpdateAppleEat;
            _currentRoundStatModel.OnMaxScoreChange += UpdateMaxScore;
            _gameTimeModel.OnGameStartingRoundAgainStateChange += UpdateAverage;
        }

        private void OnDestroy()
        {
            _currentRoundStatModel.OnAppleEat -= UpdateAppleEat;
            _currentRoundStatModel.OnMaxScoreChange -= UpdateMaxScore;
            _gameTimeModel.OnGameStartingRoundAgainStateChange -= UpdateAverage;
        }

        private void UpdateAppleEat()
        {
            eatenApple.text = $": {_currentRoundStatModel.ApplesEaten} / {_totalApples}";
        }

        private void UpdateMaxScore()
        {
            maxScore.text = $"Current Round Max Score: {_currentRoundStatModel.MaxScore}";
        }

        private void UpdateAverage()
        {
            peopleMoreThanAverage.text =
                $"Last Round People > Avg: {CalculatePeopleHigherThanAverage()} / {_geneticAlgorithmModel.Agents.Count}";

            int CalculatePeopleHigherThanAverage()
            {
                return ((int)_gameStatModel.RoundsStat.Last().Value.percentageOfHigherThanAveragePeople *
                        _geneticAlgorithmModel.Agents.Count);
            }
        }
    }
}