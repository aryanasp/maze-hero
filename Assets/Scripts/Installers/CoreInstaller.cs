using Design;
using Map;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class CoreInstaller : MonoInstaller
    {
        [SerializeField] public MazeConfig mazeConfig;
        public override void InstallBindings()
        {
            Container.Bind<MazeConfig>().FromInstance(mazeConfig);
            Container.Bind<DesignModel>().AsSingle();
            Container.Bind<MapModel>().AsSingle();
        }
    }
}
