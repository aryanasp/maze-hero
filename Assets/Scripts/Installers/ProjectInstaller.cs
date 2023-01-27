using Game;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private GameConfig gameConfig;
        public override void InstallBindings()
        {
            Container.Bind<GameConfig>().FromInstance(gameConfig).AsSingle();
            Container.Bind<CursorVisible>().AsSingle();
            Container.Bind<GameStatModel>().AsSingle();
            Container.Bind<GameReportManager>().AsSingle().NonLazy();
        }
    }
}