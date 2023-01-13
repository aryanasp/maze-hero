using UnityEngine;

namespace Ai.GeneticAlgorithm.TimeAsState
{
    [CreateAssetMenu(fileName = "AiTimeBaseConfig", menuName = "Game/Character/Ai/Ai TimeBase Config", order = 0)]
    public class AiGaTimeAsStateConfig : AiGaConfigBase
    {
        public override AiType GetAiType()
        {
            return AiType.TimeAsState;
        }
    }
}