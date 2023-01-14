using System;
using System.Collections.Generic;
using Character;
using Character.Ai.GeneticAlgorithm;
using Character.Factory;
using Game;
using UnityEngine;
using Zenject;

namespace Ai
{
    public class AiGeneticAlgorithmManager : MonoBehaviour
    {
        [Inject] private GameModel _gameModel;
        [Inject] private CharacterFactory _characterFactory;
        [Inject] private GeneticAlgorithmConfig _geneticAlgorithmConfig;

        public List<GeneticAlgorithmAgent> agents;

        public void Start()
        {
            //TODO: Generate Agents
            AddInitPopulation();
            _gameModel.IsPausing = false;
            _gameModel.OnRoundChanged += UpdateAgents;
        }

        private void AddInitPopulation()
        {
            for (int i = 0; i < _geneticAlgorithmConfig.initialPopulation; i++)
            {
                var agentGameObject = AdjustTransform(_characterFactory.Create());
                var geneticAlgorithmAgent = agentGameObject.GetComponent<GeneticAlgorithmAgent>();
                geneticAlgorithmAgent.FillRandom();
                agents.Add(geneticAlgorithmAgent);
            }
        }

        private GameObject AdjustTransform(GameObject agentGameObject)
        {
            agentGameObject.transform.parent = transform;
            agentGameObject.transform.localPosition = Vector3.zero;
            return agentGameObject;
        }

        private void UpdateAgents()
        {
            SortByFitnessFunction(); // We need to sort them before pausing and reset game
            _gameModel.IsPausing = true;
            Selection();
            var newGeneration = CrossOver();
            MutateNewGeneration(newGeneration);
            newGeneration.ForEach(item => agents.Add(item));
            _gameModel.IsPausing = false;
        }

        private void MutateNewGeneration(List<GeneticAlgorithmAgent> newGeneration)
        {
            foreach (var agent in newGeneration)
            {
                if (UnityEngine.Random.Range(0f, 1f) <= _geneticAlgorithmConfig.mutationChance)
                {
                    agent.FillRandom();
                }
            }
        }

        private List<GeneticAlgorithmAgent> CrossOver()
        {
            var newGeneration = new List<GeneticAlgorithmAgent>();
            for (int i = 0; i < agents.Count; i += 2)
            {
                var agent1 = AdjustTransform(_characterFactory.Create()).GetComponent<GeneticAlgorithmAgent>();
                var agent2 = AdjustTransform(_characterFactory.Create()).GetComponent<GeneticAlgorithmAgent>();
                agent1.Combine(agents[i], agents[i + 1]);
                agent2.Combine(agents[i + 1], agents[i]);
                newGeneration.Add(agent1);
                newGeneration.Add(agent2);
            }

            return newGeneration;
        }

        private void Selection()
        {
            var count = agents.Count;
            for (int i = 0; i < count / 2; i++)
            {
                agents.Remove(agents[0]);
            }
        }

        private void SortByFitnessFunction()
        {
            agents.Sort();
        }
    }
}