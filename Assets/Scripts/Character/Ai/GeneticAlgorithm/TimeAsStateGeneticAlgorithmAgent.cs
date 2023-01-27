using System.Collections;
using System.Collections.Generic;
using Ai;
using Game;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Character.Ai.GeneticAlgorithm
{
    public class TimeAsStateGeneticAlgorithmAgent : MonoBehaviour, IGeneticAlgorithmAgent
    {
        [Inject] private ScoreModel _scoreModel;
        [Inject] private GeneticAlgorithmConfig _geneticAlgorithmConfig;
        [Inject] private GameConfig _gameConfig;
        [Inject] private MoveModel _characterMoveModel;
        [Inject] private MoveConfig _moveConfig;
        [SerializeField] private Transform rigidBodyTransform;
        
        private List<MoveModel> _dna;
        private int _eachRoundTotalEpochs;
        private int _currentRoundEpochIndex;
        private Coroutine _aiProcess;

        public void Awake()
        {
            _eachRoundTotalEpochs = (int)(_gameConfig.roundDuration / _geneticAlgorithmConfig.updateEpochTime) + 1;
            _dna = new List<MoveModel>();
            for (int i = 0; i < _eachRoundTotalEpochs; i++)
            {
                _dna.Add(new MoveModel());
            }
            _aiProcess = StartCoroutine(ProcessUpdateMoveModel());
        }

        private IEnumerator ProcessUpdateMoveModel()
        {
            _currentRoundEpochIndex = 0;
            var timeToStay = new WaitForSeconds(_geneticAlgorithmConfig.updateEpochTime);
            while (true)
            {
                UpdateMoveModel();
                yield return timeToStay;
                _currentRoundEpochIndex++;
            }
        }

        private void OnDestroy()
        {
            StopCoroutine(_aiProcess);
        }

        private void UpdateMoveModel()
        {
            _characterMoveModel.CurrentMoveSpeed = _dna[_currentRoundEpochIndex].CurrentMoveSpeed;
            _characterMoveModel.CurrentDirection = _dna[_currentRoundEpochIndex].CurrentDirection;
            _characterMoveModel.IsMoving = _dna[_currentRoundEpochIndex].IsMoving;
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
            _currentRoundEpochIndex = 0;
            ResetPos();
        }

        private void FillGeneRandom(MoveModel gene)
        {
            gene.CurrentDirection = (MoveDirection)Random.Range(0, 4);
            gene.CurrentMoveSpeed = Random.Range(0f, 1f) * _moveConfig.moveSpeed;
            gene.IsMoving = Random.Range(0f, 1f) >= 0.5f;
        }

        private void Combine(TimeAsStateGeneticAlgorithmAgent agentParent1, TimeAsStateGeneticAlgorithmAgent agentParent2)
        {
            var halfCount = _eachRoundTotalEpochs / 2;
            for (int i = 0; i < halfCount; i++)
            {
                _dna[i] = agentParent1._dna[i];
            }
            for (int i = halfCount; i < _eachRoundTotalEpochs; i++)
            {
                _dna[i] = agentParent2._dna[i];
            }
        }

        private void ResetPos()
        {
            _currentRoundEpochIndex = 0;
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