using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Character.Ai.GeneticAlgorithm
{
    public class GeneticAlgorithmAgent : MonoBehaviour, IComparable<GeneticAlgorithmAgent>
    {
        [Inject] private ScoreModel _scoreModel;

        [Inject] private GameConfig _gameConfig;
        [Inject] private GameModel _gameModel;
        [Inject] private MoveModel _characterMoveModel;
        [Inject] private MoveConfig _moveConfig;
        
        public List<MoveModel> Dna;
        public void Awake()
        {
            Dna = new List<MoveModel>();
            for (int i = 0; i < _gameConfig.roundDuration; i++)
            {
                Dna.Add(new MoveModel());
            }
            _gameModel.OnRoundTimePassedUpdate += UpdateMoveModel;
        }

        private void OnDestroy()
        {
            _gameModel.OnRoundTimePassedUpdate -= UpdateMoveModel;
        }

        private void UpdateMoveModel()
        {
            _characterMoveModel.CurrentMoveSpeed = Dna[_gameModel.CurrentRoundTimePassed].CurrentMoveSpeed;
            _characterMoveModel.CurrentDirection = Dna[_gameModel.CurrentRoundTimePassed].CurrentDirection;
            _characterMoveModel.IsMoving = Dna[_gameModel.CurrentRoundTimePassed].IsMoving;
        }

        public int GetScore()
        {
            return _scoreModel.Score;
        }

        public void FillRandom()
        {
            foreach (var moveModel in Dna)
            {
                moveModel.CurrentDirection = (MoveDirection) UnityEngine.Random.Range(0, 4);
                moveModel.CurrentMoveSpeed = UnityEngine.Random.Range(0f, 1f) * _moveConfig.moveSpeed;
                moveModel.IsMoving = UnityEngine.Random.Range(0f, 1f) >= 0.5f;
            }
        }

        public void Combine(GeneticAlgorithmAgent agentParent1, GeneticAlgorithmAgent agentParent2)
        {
            var halfCount = _gameConfig.roundDuration / 2;
            for (int i = 0; i < halfCount; i++)
            {
                Dna[i] = agentParent1.Dna[i];
            }
            for (int i = halfCount; i < _gameConfig.roundDuration; i++)
            {
                Dna[i] = agentParent2.Dna[i];
            }
        }

        public int CompareTo(GeneticAlgorithmAgent other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return GetScore().CompareTo(other.GetScore());
        }
    }
}