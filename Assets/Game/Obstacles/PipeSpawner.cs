using System;
using System.Collections.Generic;
using Game.Core;
using Game.Core.Signals;
using Game.Utils;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Game.Obstacles
{
    public class PipeSpawner : ITickable, IInitializable, IDisposable
    {
        private Pool<Pipe> _pool;
        private PipeConfig _config;
        private GameStateService _gameState;
        private DiContainer _container;
        private SignalBus _signalBus;

        private List<Pipe> _pipes = new List<Pipe>();
        private float _timer;

        public PipeSpawner(
            Pool<Pipe> pool,
            PipeConfig config,
            GameStateService gameState,
            DiContainer container,
            SignalBus signalBus)
        {
            _pool = pool;
            _config = config;
            _gameState = gameState;
            _container = container;
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<RestartGameSignal>(OnRestart);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<RestartGameSignal>(OnRestart);
        }

        public void Tick()
        {
            if (_gameState.Current != GameState.Playing)
                return;

            _timer += Time.deltaTime;

            if (_timer >= _config.SpawnInterwal)
            {
                SpawnPipe();
                _timer = 0;
            }
        }

        public void Despawn(Pipe pipe)
        {
            if (pipe == null)
                return;

            _pipes.Remove(pipe);
            _pool.Return(pipe);
        }


        private void SpawnPipe()
        {
            if (_gameState.Current != GameState.Playing)
                return;

            Pipe pipe = _pool.Get();
            _pipes.Add(pipe);

            _container.InjectGameObject(pipe.gameObject);

            float y = Random.Range(_config.MinY, _config.MaxY);

            pipe.transform.position = new Vector3(_config.SpawnX, y, 0f);

            pipe.Init(_config.MoveSpeed, _config.DespawnX, this, _gameState);
        }

        private void OnRestart()
        {
            _timer = 0;

            foreach (var pipe in _pipes)
            {
                if (pipe != null)
                    _pool.Return(pipe);
            }

            _pipes.Clear();
        }
    }
}