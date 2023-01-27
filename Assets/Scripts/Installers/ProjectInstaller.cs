using Game;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<CursorVisible>().AsSingle();
            Container.Bind<GameStatModel>().AsSingle();
            Container.Bind<GameReportManager>().AsSingle();
        }
    }
}