using System.Collections.Generic;
using Character.Ai.GeneticAlgorithm;
using Character.Factory;
using Game;
using UnityEngine;
using Zenject;
using Logger = Log.Logger;

namespace Ai
{
    public class AiGeneticAlgorithmManager : MonoBehaviour
    {
        [Inject] private GameModel _gameModel;
        [Inject] private CharacterFactory _characterFactory;
        [Inject] private GeneticAlgorithmConfig _geneticAlgorithmConfig;
        private int _agentIndex = 0;
        private List<IGeneticAlgorithmAgent> _agents;

        public void Start()
        {
            //TODO: Generate Agents
            AddInitPopulation();
            _gameModel.IsStartingRoundAgain = false;
            _gameModel.OnRoundChanged += UpdateAgents;
        }

        private void AddInitPopulation()
        {
            var newPopulation = GenerateRandomNewPopulation(_geneticAlgorithmConfig.initialPopulation);
            _agents = newPopulation;
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
            PrintScores();
            _gameModel.IsStartingRoundAgain = true;
            Selection();
            var newGeneration = CrossOver();
            MutateNewGeneration(newGeneration);
            RemoveOldGeneration();
            newGeneration.ForEach(_agents.Add);
            _agents.ForEach(agent => agent.ResetRound());
            _gameModel.IsStartingRoundAgain = false;
        }

        private void RemoveOldGeneration()
        {
            _agents.ForEach(item => Destroy(item.GetGameObject()));
            _agents.Clear();
        }

        private void PrintScores()
        {
            string scoresPrint = "";
            _agents.ForEach(agent =>
            {
                scoresPrint += agent.GetScore() + ", ";
            });
            Logger.Log(scoresPrint, true);
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
            for (int i = 0; i < _agents.Count; i += 2)
            {
                var agent1 = AdjustTransform(_characterFactory.Create()).GetComponent<IGeneticAlgorithmAgent>();
                var agent2 = AdjustTransform(_characterFactory.Create()).GetComponent<IGeneticAlgorithmAgent>();
                agent1.Combine(_agents[i], _agents[i + 1]);
                agent2.Combine(_agents[i + 1], _agents[i]);
                newGeneration.Add(agent1);
                newGeneration.Add(agent2);
            }

            return newGeneration;
        }

        private void Selection()
        {
            var count = _agents.Count;
            for (int i = 0; i < count / 2; i++)
            {
                // _agentIndex--;
                Destroy(_agents[0].GetGameObject());
                _agents.Remove(_agents[0]);
            }
        }

        private void SortByFitnessFunction()
        {
            _agents.Sort();
        }
    }
}