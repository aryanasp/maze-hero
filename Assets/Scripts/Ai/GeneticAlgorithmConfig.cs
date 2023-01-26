using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Ai
{
    public enum InitialPopulationHeuristic
    {
        Random = 0,
        AStar = 1 
    }
    public enum GeneticAlgorithmType
    {
        TimeAsState = 0,
        GridAsState = 1
    }
    [CreateAssetMenu(fileName = "GeneticAlgorithmConfig", menuName = "Game/Ai/GeneticAlgorithmConfig", order = 0)]
    public class GeneticAlgorithmConfig : ScriptableObject
    {
        [Header("Start Population")]
        public int initialPopulation = 2;
        public InitialPopulationHeuristic initialHeuristic;
        [Space]
        [Header("Algorithm Details")]
        [MinValue(0.02d)]
        public float updateEpochTime;
        public GeneticAlgorithmType geneticAlgorithmType;
        [Header("Mutation Details")]
        [FormerlySerializedAs("mutationChance")] public float chanceToMutateNewPerson;
        public float geneMutationChance;
    }
}