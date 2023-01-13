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
            Logger.Log($"[CharacterApple]: Score {_scoreModel.Score}", true);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Apple"))
            {
                _scoreModel.Score += 1;
            }
        }
    }
}