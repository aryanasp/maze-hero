using Ai;
using Character;
using Character.Factory;
using Design;
using Game;
using Map;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class CoreInstaller : MonoInstaller
    {
        [SerializeField] private GameConfig gameConfig;
        [SerializeField] private MazeConfig mazeConfig;
        [SerializeField] private GeneticAlgorithmConfig geneticAlgorithmConfig;
        [SerializeField] private GameObject characterPrefab;
        public override void InstallBindings()
        {
            Container.Bind<GameConfig>().FromInstance(gameConfig)
                .AsSingle();
            Container.Bind<GameModel>()
                .AsSingle();
            Container.Bind<MazeConfig>().FromInstance(mazeConfig)
                .AsSingle();
            Container.Bind<DesignModel>()
                .AsSingle();
            Container.Bind<MapModel>()
                .AsSingle();
            Container.BindFactory<GameObject, CharacterFactory>()
                .FromSubContainerResolve()
                .ByNewContextPrefab(characterPrefab).AsSingle();
            Container.Bind<GeneticAlgorithmConfig>().FromInstance(geneticAlgorithmConfig)
                .AsSingle();
        }
    }
}
