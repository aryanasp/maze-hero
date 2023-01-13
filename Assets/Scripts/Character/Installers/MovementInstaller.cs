using UnityEngine;
using Zenject;

namespace Character
{
    public class MovementInstaller : MonoInstaller<MovementInstaller>
    {
        [SerializeField] private MoveConfig moveConfig;
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<MoveConfig>().FromInstance(moveConfig).AsSingle();
            Container.BindInterfacesAndSelfTo<MoveModel>().AsSingle();
        }
    }
}