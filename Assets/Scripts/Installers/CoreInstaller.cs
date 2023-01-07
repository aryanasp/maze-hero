using Design;
using Map;
using Zenject;

namespace Installers
{
    public class CoreInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<DesignModel>().AsSingle();
            Container.Bind<MapModel>().AsSingle();
        }
    }
}
