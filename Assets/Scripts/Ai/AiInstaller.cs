using Character.Ai.GeneticAlgorithm;
using UnityEngine;
using Zenject;

namespace Ai
{
    public class AiInstaller : MonoInstaller<AiInstaller>
    {
        [SerializeField] private TimeAsStateGeneticAlgorithmAgent agent;
        public override void InstallBindings()
        {
            Container.BindInstance(agent).AsSingle();
        }
    }
}