using UnityEngine;

namespace Ai.GeneticAlgorithm
{
    public abstract class AiGaConfigBase : ScriptableObject
    {
        public abstract AiType GetAiType();
        
    }
}