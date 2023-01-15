using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Character.Ui
{
    public class CharacterScoreView : MonoBehaviour
    {
        [Inject] private ScoreModel _scoreModel;
        [SerializeField] private TextMeshProUGUI text;
        
        private void Start()
        {
            _scoreModel.OnScoreChange += UpdateScore;
        }

        private void UpdateScore()
        {
            text.text = $"Score: {_scoreModel.Score}";
        }

        private void OnDestroy()
        {
            _scoreModel.OnScoreChange -= UpdateScore;
        }
    }
}