using UnityEngine;
using Zenject;

namespace Game
{
    public class GameTimeLogic : MonoBehaviour
    {
        [Inject] private GameConfig _gameConfig;
        [Inject] private GameModel _gameModel;

        private float _timePassedFromLastGameTimeUpdate;
        
        private void FixedUpdate()
        {
            if (CheckIfGameFinished())
            {
                return;
            }
            if (ShouldNewRoundStart())
            {
                UpdateRound();
            }
            UpdateRoundTimePassed();
        }

        private void UpdateRound()
        {
            _timePassedFromLastGameTimeUpdate = 0f;
            _gameModel.CurrentRoundTimePassed = 0;
            _gameModel.CurrentRound += 1;
        }

        private bool ShouldNewRoundStart()
        {
            return _gameModel.CurrentRoundTimePassed >= _gameConfig.roundDuration;
        }

        private bool CheckIfGameFinished()
        {
            return _gameModel.CurrentRound >= _gameConfig.roundCount ;
        }

        private void UpdateRoundTimePassed()
        {
            _timePassedFromLastGameTimeUpdate += Time.fixedDeltaTime;
            if (_timePassedFromLastGameTimeUpdate < 1f) return;
            _gameModel.CurrentRoundTimePassed += 1;
            _timePassedFromLastGameTimeUpdate = 0f;
        }
    }
}