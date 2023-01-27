using Game;
using UnityEngine;
using Zenject;

namespace Character
{
    public class CharacterScoreResetHandler : MonoBehaviour
    {
        [Inject] private ScoreModel _scoreModel;
        [Inject] private GameTimeModel _gameTimeModel;
        
        private void Start()
        {
            _gameTimeModel.OnGameStartingRoundAgainStateChange += ResetScore;
        }

        private void OnDestroy()
        {
            _gameTimeModel.OnGameStartingRoundAgainStateChange -= ResetScore;
        }

        private void ResetScore()
        {
            _scoreModel.Score = 0;
        }
    }
}