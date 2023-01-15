using System;
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

        [SerializeField] private Transform rigidBodyTransform;
        
        private List<MoveModel> _dna;
        public void Awake()
        {
            _dna = new List<MoveModel>();
            for (int i = 0; i < _gameConfig.roundDuration + 1; i++)
            {
                _dna.Add(new MoveModel());
            }
            _gameModel.OnRoundTimePassedUpdate += UpdateMoveModel;
        }

        private void OnDestroy()
        {
            _gameModel.OnRoundTimePassedUpdate -= UpdateMoveModel;
        }

        private void UpdateMoveModel()
        {
            _characterMoveModel.CurrentMoveSpeed = _dna[_gameModel.CurrentRoundEpochPassed].CurrentMoveSpeed;
            _characterMoveModel.CurrentDirection = _dna[_gameModel.CurrentRoundEpochPassed].CurrentDirection;
            _characterMoveModel.IsMoving = _dna[_gameModel.CurrentRoundEpochPassed].IsMoving;
        }

        public int GetScore()
        {
            return _scoreModel.Score;
        }

        public void FillRandom()
        {
            foreach (var gene in _dna)
            {
                FillGeneRandom(gene);
            }
        }

        private void FillGeneRandom(MoveModel gene)
        {
            gene.CurrentDirection = (MoveDirection)UnityEngine.Random.Range(0, 4);
            gene.CurrentMoveSpeed = UnityEngine.Random.Range(0f, 1f) * _moveConfig.moveSpeed;
            gene.IsMoving = UnityEngine.Random.Range(0f, 1f) >= 0.5f;
        }

        public void Combine(GeneticAlgorithmAgent agentParent1, GeneticAlgorithmAgent agentParent2)
        {
            var halfCount = _gameConfig.roundDuration / 2;
            for (int i = 0; i < halfCount; i++)
            {
                _dna[i] = agentParent1._dna[i];
            }
            for (int i = halfCount; i < _gameConfig.roundDuration; i++)
            {
                _dna[i] = agentParent2._dna[i];
            }
        }

        public int CompareTo(GeneticAlgorithmAgent other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return GetScore().CompareTo(other.GetScore());
        }

        public void ResetPos()
        {
            transform.localPosition = Vector3.zero;
            rigidBodyTransform.localPosition = new Vector3(0.216f, 0.363f);
        }

        public void Mutate(float geneMutationChance)
        {
            foreach (var gene in _dna)
            {
                if (UnityEngine.Random.Range(0f, 1f) <= geneMutationChance)
                {
                    FillGeneRandom(gene);
                }
            }
        }
    }
}