using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Character.Ai.GeneticAlgorithm
{
    public interface IGeneticAlgorithmAgent : IComparable<IGeneticAlgorithmAgent>
    {
        void FillRandom();
        void ResetRound();
        GameObject GetGameObject();
        int GetScore();
        void Mutate(float geneMutationChance);
        void Combine(IGeneticAlgorithmAgent agentParent1, IGeneticAlgorithmAgent agentParent2);
    }
}