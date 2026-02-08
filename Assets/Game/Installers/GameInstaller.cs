using Game.Core;
using Game.Core.Signals;
using Game.Player;
using Game.Obstacles;
using Infrastructure.InputSystem;
using Zenject;

namespace Game.Installers
{
    public class GameInstaller : MonoInstaller
    {
        public BirdConfig BirdConfig;
        public PipeConfig PipeConfig;

        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);

            Container.BindInterfacesAndSelfTo<GameStateService>().AsSingle();
            Container.Bind<IInputService>().To<KeyboardInputService>().AsSingle();
            Container.Bind<BirdConfig>().FromInstance(BirdConfig).AsSingle();
            Container.Bind<PipeConfig>().FromInstance(PipeConfig).AsSingle();

            Container.BindMemoryPool<Pipe, Pipe.PipePool>().FromComponentInNewPrefab(PipeConfig.PipePrefab)
                .UnderTransformGroup("Pipes");

            Container.BindInterfacesTo<PipeSpawner>().AsSingle();
            Container.BindInterfacesAndSelfTo<ScoreService>().AsSingle();

            Container.DeclareSignal<BirdDiedSignal>();
            Container.DeclareSignal<PassedPipeSignal>();
            Container.DeclareSignal<ScoreChangedSignal>();
            Container.DeclareSignal<RestartGameSignal>();
        }
    }
}