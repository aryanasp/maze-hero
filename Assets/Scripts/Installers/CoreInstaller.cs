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
        [SerializeField] public MazeConfig mazeConfig;
        public override void InstallBindings()
        {
            Container.Bind<GameConfig>().FromInstance(gameConfig);
            Container.Bind<GameModel>().AsSingle();
            Container.Bind<MazeConfig>().FromInstance(mazeConfig);
            Container.Bind<DesignModel>().AsSingle();
            Container.Bind<MapModel>().AsSingle();
        }
    }
}
