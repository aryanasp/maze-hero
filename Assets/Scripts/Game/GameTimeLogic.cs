using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Game
{
    public class GameTimeLogic : MonoBehaviour
    {
        [Inject] private GameConfig _gameConfig;
        [Inject] private GameTimeModel _gameTimeModel;
        [Inject] private GameStatModel _gameStatModel;
        
        private float _timePassedFromLastGameEpochUpdate;

        private void FixedUpdate()
        {
            if (CheckGameIsPaused())
            {
                return;
            }
            if (ShouldNewRoundStart())
            {
                UpdateRound();
            }
            UpdateRoundTimePassed();
            if (CheckIfGameFinished())
            {
                GoToMenuScene();
                _gameStatModel.FinishGame();
            }
        }

        private void GoToMenuScene()
        {
            SceneManager.LoadScene("Meta");
        }

        private bool CheckGameIsPaused()
        {
            return _gameTimeModel.IsStartingRoundAgain;
        }

        private void UpdateRound()
        {
            _timePassedFromLastGameEpochUpdate = 0f;
            _gameTimeModel.CurrentRoundTimePassed = 0;
            _gameTimeModel.CurrentRound += 1;
        }

        private bool ShouldNewRoundStart()
        {
            return _gameTimeModel.CurrentRoundTimePassed >= _gameConfig.roundDuration;
        }

        private bool CheckIfGameFinished()
        {
            return _gameTimeModel.CurrentRound >= _gameConfig.roundCount;
        }

        private void UpdateRoundTimePassed()
        {
            _timePassedFromLastGameEpochUpdate += Time.fixedDeltaTime;
            if (_timePassedFromLastGameEpochUpdate < 1) return;
            _gameTimeModel.CurrentRoundTimePassed += 1;
            _timePassedFromLastGameEpochUpdate = 0f;
        }
    }
}