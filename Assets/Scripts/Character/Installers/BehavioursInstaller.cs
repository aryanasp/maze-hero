using UnityEngine;
using Zenject;

namespace Character
{
    public class BehavioursInstaller : MonoInstaller<BehavioursInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ScoreModel>().AsSingle();
        }
    }
}