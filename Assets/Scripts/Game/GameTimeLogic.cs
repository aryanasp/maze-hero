﻿using UnityEngine;
using Zenject;

namespace Game
{
    public class GameTimeLogic : MonoBehaviour
    {
        [Inject] private GameConfig _gameConfig;
        [Inject] private GameModel _gameModel;

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
                return;
            }
            if (ShouldNewRoundStart())
            {
                UpdateRound();
            }
            UpdateRoundTimePassed();
        }

        private void EndSession()
        {
            Application.Quit();
        }

        private bool CheckIfNeedEndSession()
        {
            return _gameModel.CurrentRound > _gameConfig.roundCount;
        }

        private bool CheckGameIsPaused()
        {
            return _gameModel.IsPausing;
        }

        private void UpdateRound()
        {
            _timePassedFromLastGameEpochUpdate = 0f;
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
            _timePassedFromLastGameEpochUpdate += Time.fixedDeltaTime;
            if (_timePassedFromLastGameEpochUpdate < 1) return;
            _gameModel.CurrentRoundTimePassed += 1;
            _timePassedFromLastGameEpochUpdate = 0f;
        }
    }
}