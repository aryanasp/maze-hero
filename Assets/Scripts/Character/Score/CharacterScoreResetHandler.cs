using Game;
using UnityEngine;
using Zenject;

namespace Character
{
    public class CharacterScoreResetHandler : MonoBehaviour
    {
        [Inject] private ScoreModel _scoreModel;
        [Inject] private GameModel _gameModel;
        
        private void Start()
        {
            _gameModel.OnGameStartingRoundAgainStateChange += ResetScore;
        }

        private void OnDestroy()
        {
            _gameModel.OnGameStartingRoundAgainStateChange -= ResetScore;
        }

        private void ResetScore()
        {
            _scoreModel.Score = 0;
        }
    }
}