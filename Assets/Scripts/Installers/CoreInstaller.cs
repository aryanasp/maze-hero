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
        private GeneticAlgorithmConfig _geneticAlgorithmConfig;
        [SerializeField] private MazeConfig mazeConfig;
        public override void InstallBindings()
        {
            Container.Bind<GameTimeModel>()
                .AsSingle();
            Container.Bind<MazeConfig>().FromInstance(mazeConfig)
                .AsSingle();
            Container.Bind<DesignModel>()
                .AsSingle();
            Container.Bind<MapModel>()
                .AsSingle();
            Container.Bind<AiGeneticAlgorithmModel>().AsSingle();
            Container.Bind<CurrentRoundStatModel>().AsSingle();
            Container.Bind<GameAnalyzerModel>().AsSingle();
            Container.Bind<GameAnalyzerLogic>().AsSingle().NonLazy();
            InstallCharacterFactory();
        }

        private void InstallCharacterFactory()
        {
            _geneticAlgorithmConfig = Resources.Load<GeneticAlgorithmConfig>("Ai/GeneticAlgorithmConfig");
            switch (_geneticAlgorithmConfig.geneticAlgorithmType)
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
