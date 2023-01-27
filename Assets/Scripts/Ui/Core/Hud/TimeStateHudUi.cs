using System;
using Game;
using TMPro;
using UnityEngine;
using Zenject;

namespace Ui.Core.Hud
{
    public class TimeStateHudUi : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI round;
        [SerializeField] private TextMeshProUGUI duration;

        [Inject] private GameConfig _gameConfig;
        [Inject] private GameTimeModel _gameTimeModel;
        
        private void Start()
        {
            UpdateRoundTimePassed();
            UpdateRound();
            _gameTimeModel.OnRoundTimePassedUpdate += UpdateRoundTimePassed;
            _gameTimeModel.OnRoundChanged += UpdateRound;
        }

        private void UpdateRoundTimePassed()
        {
            duration.text = $"Round Time Passed: {_gameTimeModel.CurrentRoundTimePassed} / {_gameConfig.roundDuration}";
        }

        private void UpdateRound()
        {
            round.text = $"Current Round: {_gameTimeModel.CurrentRound + 1} / {_gameConfig.roundCount}";
        }

        private void OnDestroy()
        {
            _gameTimeModel.OnRoundTimePassedUpdate -= UpdateRoundTimePassed;
            _gameTimeModel.OnRoundChanged -= UpdateRound;
        }
    }
}