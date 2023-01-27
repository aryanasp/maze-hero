using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Game
{
    public class GameTimeLogic : MonoBehaviour
    {
        [Inject] private GameConfig _gameConfig;
        [Inject] private GameTimeModel _gameTimeModel;

        private float _timePassedFromLastGameEpochUpdate;
        
        private void FixedUpdate()
        {
            if (CheckIfNeedEndSession())
            {
                EndSession();
            }
            if (CheckGameIsPaused())
            {
                return;
            }
            if (CheckIfGameFinished())
            {
                GoToMenuScene();
                return;
            }
            if (ShouldNewRoundStart())
            {
                UpdateRound();
            }
            UpdateRoundTimePassed();
        }

        private void GoToMenuScene()
        {
            SceneManager.LoadScene("Meta");
        }

        private void EndSession()
        {
            Application.Quit();
        }

        private bool CheckIfNeedEndSession()
        {
            return _gameTimeModel.CurrentRound > _gameConfig.roundCount;
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
            return _gameTimeModel.CurrentRound >= _gameConfig.roundCount ;
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