using Zenject;

namespace Game.Obstacles
{
    public class PipeInstaller : MonoInstaller<PipeInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<Pipe>().FromComponentOnRoot().AsSingle();
            Container.Bind<PipeScoreTrigger>().FromComponentInChildren().AsSingle();
        }
    }
}