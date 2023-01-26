using System.Collections.Generic;
using Character.Ai.GeneticAlgorithm;
using Character.Factory;
using Game;
using UnityEditor;
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
            var newPopulation = GenerateRandomNewPopulation(_geneticAlgorithmConfig.initialPopulation);
            agents = newPopulation;
        }

        private List<GeneticAlgorithmAgent> GenerateRandomNewPopulation(int count)
        {
            var newPopulation = new List<GeneticAlgorithmAgent>();
            for (int i = 0; i < count; i++)
            {
                var agentGameObject = AdjustTransform(_characterFactory.Create());
                var geneticAlgorithmAgent = agentGameObject.GetComponent<GeneticAlgorithmAgent>();
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
            _gameModel.IsPausing = true;
            Selection();
            var newGeneration = CrossOver();
            MutateNewGeneration(newGeneration);
            RemoveOldGeneration();
            newGeneration.ForEach(agents.Add);
            agents.ForEach(agent => agent.ResetPos());
            _gameModel.IsPausing = false;
        }

        private void RemoveOldGeneration()
        {
            agents.ForEach(item => Destroy(item.gameObject));
            agents.Clear();
        }

        private void PrintScores()
        {
            string scoresPrint = "";
            agents.ForEach(agent =>
            {
                scoresPrint += agent.GetScore() + ", ";
            });
            Logger.Log(scoresPrint, true);
        }

        private void MutateNewGeneration(List<GeneticAlgorithmAgent> newGeneration)
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
                // _agentIndex--;
                Destroy(agents[0].gameObject);
                agents.Remove(agents[0]);
            }
        }

        private void SortByFitnessFunction()
        {
            agents.Sort();
        }
    }
}