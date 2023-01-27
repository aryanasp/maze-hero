using System;
using Ai;
using Character.Factory;
using Design;
using Game;
using Game.GameAnalyzer;
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
        public override void InstallBindings()
        {
            Container.Bind<GameConfig>().FromInstance(gameConfig)
                .AsSingle();
            Container.Bind<GameTimeModel>()
                .AsSingle();
            Container.Bind<MazeConfig>().FromInstance(mazeConfig)
                .AsSingle();
            Container.Bind<DesignModel>()
                .AsSingle();
            Container.Bind<MapModel>()
                .AsSingle();
            Container.Bind<GeneticAlgorithmConfig>().FromInstance(geneticAlgorithmConfig)
                .AsSingle();
            Container.Bind<CurrentRoundStatModel>().AsSingle();
            Container.Bind<GameAnalyzerModel>().AsSingle();
            Container.Bind<GameAnalyzerLogic>().AsSingle();
            InstallCharacterFactory();
        }

        private void InstallCharacterFactory()
        {
            switch (geneticAlgorithmConfig.geneticAlgorithmType)
            {
                case GeneticAlgorithmType.TimeAsState:
                    Container.BindFactory<GameObject, CharacterFactory>()
                        .FromSubContainerResolve()
                        .ByNewContextPrefabResource("Character/TimeAsState/Character").AsSingle();
                    break;
                case GeneticAlgorithmType.GridAsState:
                    Container.BindFactory<GameObject, CharacterFactory>()
                        .FromSubContainerResolve()
                        .ByNewContextPrefabResource("Character/GridAsState/Character").AsSingle();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
