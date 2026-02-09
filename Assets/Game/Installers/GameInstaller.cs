using Game.Core;
using Game.Core.Signals;
using Game.Obstacles;
using Game.Player;
using Game.Utils;
using Infrastructure.InputSystem;
using UnityEngine;
using Zenject;

namespace Game.Installers
{
    public class GameInstaller : MonoInstaller
    {
        public BirdConfig BirdConfig;
        public PipeConfig PipeConfig;
        public Transform PoolParent;

        private int _prewarmPoolSize = 5;

        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);

            Container.BindInterfacesAndSelfTo<GameStateService>().AsSingle();
            Container.Bind<IInputService>().To<KeyboardInputService>().AsSingle();
            Container.Bind<BirdConfig>().FromInstance(BirdConfig).AsSingle();
            Container.Bind<PipeConfig>().FromInstance(PipeConfig).AsSingle();

            Container.Bind<Pool<Pipe>>().AsSingle().WithArguments(PipeConfig.PipePrefab, PoolParent, _prewarmPoolSize);

            Container.BindInterfacesTo<PipeSpawner>().AsSingle();
            Container.BindInterfacesAndSelfTo<ScoreService>().AsSingle();

            Container.Bind<PlayerMark>().FromComponentInChildren().AsSingle();

            Container.DeclareSignal<BirdDiedSignal>();
            Container.DeclareSignal<PassedPipeSignal>();
            Container.DeclareSignal<ScoreChangedSignal>();
            Container.DeclareSignal<RestartGameSignal>();
        }
    }
}