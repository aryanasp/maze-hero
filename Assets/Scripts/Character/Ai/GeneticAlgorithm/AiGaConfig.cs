using Ai.GeneticAlgorithm.TimeAsState;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Ai.GeneticAlgorithm
{
    public enum AiType
    {
        TimeAsState = 10,
        TileContentAsState = 20,
    }
    [CreateAssetMenu(fileName = "AiConfig", menuName = "Game/Character/Ai/Ai Config", order = 0)]
    public class AiGaConfig : ScriptableObjectInstaller<AiGaConfig>
    {
        public bool isActive;
        public AiType aiType;
        
        [SerializeField]
        [ShowInInlineEditors]
        [ShowIf("@aiType == AiType.TimeAsState")]
        private AiGaTimeAsStateConfig aiGaTimeAsStateConfig;
        
        public override void InstallBindings()
        {
            Container.BindInstance(this).AsSingle();
            Container.BindInstance(aiGaTimeAsStateConfig).AsSingle();
        }
    }
}