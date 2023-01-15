using UnityEngine;

namespace Ai
{
    [CreateAssetMenu(fileName = "GeneticAlgorithmConfig", menuName = "Game/Ai/GeneticAlgorithmConfig", order = 0)]
    public class GeneticAlgorithmConfig : ScriptableObject
    {
        public int initialPopulation;
        public float mutationChance;
        public float geneMutationChance;
    }
}