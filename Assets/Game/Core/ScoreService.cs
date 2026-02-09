using System;
using Game.Core.Signals;
using Zenject;

namespace Game.Core
{
    public class ScoreService : IInitializable, IDisposable
    {
        private SignalBus _signalBus;

        private int _score;

        public int Score => _score;

        public ScoreService(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<PassedPipeSignal>(OnPipePassed);
            _signalBus.Subscribe<RestartGameSignal>(OnRestart);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<PassedPipeSignal>(OnPipePassed);
            _signalBus.Unsubscribe<RestartGameSignal>(OnRestart);
        }

        private void OnPipePassed()
        {
            _score++;
            _signalBus.Fire<ScoreChangedSignal>();
        }

        private void OnRestart()
        {
            _score = 0;
            _signalBus.Fire<ScoreChangedSignal>();
        }
    }
}