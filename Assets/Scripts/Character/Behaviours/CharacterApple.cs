using System;
using Game;
using UnityEngine;
using Zenject;
using Logger = Log.Logger;

namespace Character
{
    public class CharacterApple : MonoBehaviour
    {
        [Inject] private ScoreModel _scoreModel;
        [Inject] private CurrentRoundStatModel _currentRoundStatModel;
        
        private void Update()
        {
            Logger.Log($"[CharacterApple]: Score {_scoreModel.Score}");
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Apple"))
            {
                var com = col.gameObject.GetComponent<EatableByCharacter>();
                if (com.IsConsumed())
                {
                    return;
                }
                _scoreModel.Score += 1;
                _currentRoundStatModel.ApplesEaten += 1;
                _currentRoundStatModel.CheckForMaxScore(_scoreModel.Score);
                com.Consume();
            }
        }
    }
}