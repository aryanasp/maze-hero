using System;
using UnityEngine;
using Zenject;
using Logger = Log.Logger;

namespace Character
{
    public class CharacterApple : MonoBehaviour
    {
        [Inject] private ScoreModel _scoreModel;

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
                com.Consume();
            }
        }
    }
}