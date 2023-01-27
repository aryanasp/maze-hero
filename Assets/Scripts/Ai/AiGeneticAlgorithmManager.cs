using System.Collections.Generic;
using System.Linq;
using Character.Ai.GeneticAlgorithm;
using Character.Factory;
using Game;
using Game.GameAnalyzer;
using UnityEngine;
using Zenject;
using Logger = Log.Logger;

namespace Ai
{
    public class AiGeneticAlgorithmManager : MonoBehaviour
    {
        [Inject] private GameTimeModel _gameTimeModel;
        [Inject] private CharacterFactory _characterFactory;
        [Inject] private GeneticAlgorithmConfig _geneticAlgorithmConfig;
        [Inject] private AiGeneticAlgorithmModel _geneticAlgorithmModel;
        [Inject] private GameAnalyzerModel _gameAnalyzerModel;
        [Inject] private CurrentRoundStatModel _currentRoundStatModel;
        private int _agentIndex = 0;

        public void Start()
        {
            //TODO: Generate Agents
            AddInitPopulation();
            _gameTimeModel.IsStartingRoundAgain = false;
            _gameTimeModel.OnRoundChanged += UpdateAgents;
        }

        private void AddInitPopulation()
        {
            var newPopulation = GenerateRandomNewPopulation(_geneticAlgorithmConfig.initialPopulation);
            _geneticAlgorithmModel.Agents = newPopulation;
        }

        private List<IGeneticAlgorithmAgent> GenerateRandomNewPopulation(int count)
        {
            var newPopulation = new List<IGeneticAlgorithmAgent>();
            for (int i = 0; i < count; i++)
            {
                var agentGameObject = AdjustTransform(_characterFactory.Create());
                var geneticAlgorithmAgent = agentGameObject.GetComponent<IGeneticAlgorithmAgent>();
                geneticAlgorithmAgent.FillRandom();
                newPopulation.Add(geneticAlgorithmAgent);
            }
            return newPopulation;
        }

        private GameObject AdjustTransform(GameObject agentGameObject)
        {
            agentGameObject.transform.parent = transform;
            agentGameObject.transform.localPosition = Vector3.zero;
            agentGameObject.name = $"Agent_{_agentIndex}";
            _agentIndex++;
            return agentGameObject;
        }

        private void UpdateAgents()
        {
            SortByFitnessFunction(); // We need to sort them before pausing and reset game
            _currentRoundStatModel.MaxScore = _geneticAlgorithmModel.Agents.Last().GetScore();
            _gameAnalyzerModel.TriggerStartAnalyze = true;
            _gameTimeModel.IsStartingRoundAgain = true;
            Selection();
            var newGeneration = CrossOver();
            MutateNewGeneration(newGeneration);
            RemoveOldGeneration();
            newGeneration.ForEach(_geneticAlgorithmModel.Agents.Add);
            _geneticAlgorithmModel.Agents.ForEach(agent => agent.ResetRound());
            _gameTimeModel.IsStartingRoundAgain = false;
        }

        private void RemoveOldGeneration()
        {
            _geneticAlgorithmModel.Agents.ForEach(item => Destroy(item.GetGameObject()));
            _geneticAlgorithmModel.Agents.Clear();
        }

        private void MutateNewGeneration(List<IGeneticAlgorithmAgent> newGeneration)
        {
            foreach (var agent in newGeneration)
            {
                if (UnityEngine.Random.Range(0f, 1f) <= _geneticAlgorithmConfig.chanceToMutateNewPerson)
                {
                    agent.Mutate(_geneticAlgorithmConfig.geneMutationChance);
                }
            }
            var newRandomPopulation = GenerateRandomNewPopulation(newGeneration.Count);
            newRandomPopulation.ForEach(newGeneration.Add);
        }

        private List<IGeneticAlgorithmAgent> CrossOver()
        {
            var newGeneration = new List<IGeneticAlgorithmAgent>();
            for (int i = 0; i < _geneticAlgorithmModel.Agents.Count; i += 2)
            {
                var agent1 = AdjustTransform(_characterFactory.Create()).GetComponent<IGeneticAlgorithmAgent>();
                var agent2 = AdjustTransform(_characterFactory.Create()).GetComponent<IGeneticAlgorithmAgent>();
                agent1.Combine(_geneticAlgorithmModel.Agents[i], _geneticAlgorithmModel.Agents[i + 1]);
                agent2.Combine(_geneticAlgorithmModel.Agents[i + 1], _geneticAlgorithmModel.Agents[i]);
                newGeneration.Add(agent1);
                newGeneration.Add(agent2);
            }

            return newGeneration;
        }

        private void Selection()
        {
            var count = _geneticAlgorithmModel.Agents.Count;
            for (int i = 0; i < count / 2; i++)
            {
                // _agentIndex--;
                Destroy(_geneticAlgorithmModel.Agents[0].GetGameObject());
                _geneticAlgorithmModel.Agents.Remove(_geneticAlgorithmModel.Agents[0]);
            }
        }

        private void SortByFitnessFunction()
        {
            _geneticAlgorithmModel.Agents.Sort();
        }
    }
}