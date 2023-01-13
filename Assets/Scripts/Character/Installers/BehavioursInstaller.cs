using UnityEngine;
using Zenject;

namespace Character.Installers
{
    public class BehavioursInstaller : MonoInstaller<BehavioursInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ScoreModel>().AsSingle();
        }
    }
}