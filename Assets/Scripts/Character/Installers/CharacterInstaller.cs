using UnityEngine;
using Zenject;

namespace Character
{
    public class CharacterInstaller : MonoInstaller<CharacterInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInstance(gameObject).AsSingle();
        }
    }
}