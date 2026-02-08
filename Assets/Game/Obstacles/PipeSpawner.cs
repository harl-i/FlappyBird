using Random = UnityEngine.Random;
using UnityEngine;
using Zenject;
using Game.Core;
using Game.Core.Signals;
using System;
using System.Collections.Generic;

namespace Game.Obstacles
{
    public class PipeSpawner : ITickable, IInitializable, IDisposable
    {
        private Pipe.PipePool _pool;
        private PipeConfig _config;
        private GameStateService _gameState;
        private DiContainer _container;
        private SignalBus _signalBus;

        private List<Pipe> _pipes = new List<Pipe>();
        private float _timer;

        public PipeSpawner(
            Pipe.PipePool pool,
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

        private void SpawnPipe()
        {
            Pipe pipe = _pool.Spawn();
            _pipes.Add(pipe);

            _container.InjectGameObject(pipe.gameObject);

            float y = Random.Range(_config.MinY, _config.MaxY);

            pipe.transform.position = new Vector3(_config.SpawnX, y, 0f);

            pipe.Init(_config.MoveSpeed, _config.DespawnX, _pool, _gameState);
        }

        private void OnRestart()
        {
            _timer = 0;

            foreach (var pipe in _pipes)
            {
                if (pipe != null)
                {
                    _pool.Despawn(pipe);
                }
            }

            _pool.Clear();
        }
    }
}