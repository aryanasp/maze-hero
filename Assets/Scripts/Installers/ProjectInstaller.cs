using Ai;
using Game;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private GameConfig gameConfig;
        [SerializeField] private GeneticAlgorithmConfig geneticAlgorithmConfig;
        public override void InstallBindings()
        {
            Container.Bind<CursorVisible>().AsSingle();
            Container.Bind<GameConfig>().FromInstance(gameConfig).AsSingle();
            Container.Bind<GeneticAlgorithmConfig>().FromInstance(geneticAlgorithmConfig).AsSingle();
            Container.Bind<GameStatModel>().AsSingle();
            Container.Bind<GameReportManager>().AsSingle().NonLazy();
        }
    }
}