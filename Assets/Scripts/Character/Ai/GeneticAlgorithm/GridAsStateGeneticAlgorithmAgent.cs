using System.Collections;
using System.Collections.Generic;
using Ai;
using Game;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Character.Ai.GeneticAlgorithm
{
    public class GridAsStateGeneticAlgorithmAgent : MonoBehaviour, IGeneticAlgorithmAgent
    {
        [Inject] private ScoreModel _scoreModel;
        [Inject] private MoveModel _characterMoveModel;
        [Inject] private MoveConfig _moveConfig;
        [SerializeField] private Transform rigidBodyTransform;

        private List<MoveModel> _dna;

        public void Awake()
        {
            _dna = new List<MoveModel>();
            //TODO: Fill DNA with Genes
        }

        private void OnDestroy()
        {
        }

        private void UpdateMoveModel()
        {
            //TODO: Update Genes
            // _characterMoveModel.CurrentMoveSpeed = _dna[_currentRoundEpochIndex].CurrentMoveSpeed;
            // _characterMoveModel.CurrentDirection = _dna[_currentRoundEpochIndex].CurrentDirection;
            // _characterMoveModel.IsMoving = _dna[_currentRoundEpochIndex].IsMoving;
        }

        public GameObject GetGameObject()
        {
            return gameObject;
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

        public void ResetRound()
        {
            ResetPos();
        }

        private void FillGeneRandom(MoveModel gene)
        {
            gene.CurrentDirection = (MoveDirection)Random.Range(0, 4);
            gene.CurrentMoveSpeed = Random.Range(0f, 1f) * _moveConfig.moveSpeed;
            gene.IsMoving = Random.Range(0f, 1f) >= 0.5f;
        }

        private void Combine(TimeAsStateGeneticAlgorithmAgent agentParent1,
            TimeAsStateGeneticAlgorithmAgent agentParent2)
        {
            //TODO: Combine DNAs
            // var halfCount = _eachRoundTotalEpochs / 2;
            // for (int i = 0; i < halfCount; i++)
            // {
            // _dna[i] = agentParent1._dna[i];
            // }
            // for (int i = halfCount; i < _eachRoundTotalEpochs; i++)
            // {
            // _dna[i] = agentParent2._dna[i];
            // }
        }

        private void ResetPos()
        {
            transform.localPosition = Vector3.zero;
            rigidBodyTransform.localPosition = new Vector3(0.216f, 0.363f);
        }

        public void Mutate(float geneMutationChance)
        {
            foreach (var gene in _dna)
            {
                if (Random.Range(0f, 1f) <= geneMutationChance)
                {
                    FillGeneRandom(gene);
                }
            }
        }

        public void Combine(IGeneticAlgorithmAgent agentParent1, IGeneticAlgorithmAgent agentParent2)
        {
            var castedAgentParent1 = (TimeAsStateGeneticAlgorithmAgent)agentParent1;
            var castedAgentParent2 = (TimeAsStateGeneticAlgorithmAgent)agentParent2;
            Combine(castedAgentParent1, castedAgentParent2);
        }

        public int CompareTo(IGeneticAlgorithmAgent other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return GetScore().CompareTo(other.GetScore());
        }
    }
}