using System;
using Game.Core.Signals;
using Zenject;

namespace Game.Core
{
    public class GameStateService : IInitializable, IDisposable
    {
        private readonly SignalBus _signalBus;

        public GameState Current { get; private set; } = GameState.Idle;

        public GameStateService(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<BirdDiedSignal>(OnBirdDied);
            _signalBus.Subscribe<RestartGameSignal>(OnRestart);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<BirdDiedSignal>(OnBirdDied);
            _signalBus.Unsubscribe<RestartGameSignal>(OnRestart);
        }

        public void StartGame()
        {
            Current = GameState.Playing;
        }

        private void OnBirdDied()
        {
            Current = GameState.GameOver;
        }

        private void OnRestart()
        {
            Current = GameState.Playing;
        }
    }
}