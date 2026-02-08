using Game.Core;
using UnityEngine;
using Zenject;

namespace Game.Obstacles
{
    public class Pipe : MonoBehaviour
    {
        private float _speed;
        private float _despawnX;
        private PipePool _pool;
        private GameStateService _gameState;

        public void Init(float speed, float despawnX, PipePool pipePool, GameStateService gameState)
        {
            _speed = speed;
            _despawnX = despawnX;
            _pool = pipePool;
            _gameState = gameState;
        }

        private void Update()
        {
            if (_gameState.Current != GameState.Playing)
                return;

            transform.position += Vector3.left * _speed * Time.deltaTime;

            if (transform.position.x < _despawnX)
            {
                _pool.Despawn(this);
            }
        }

        public class PipePool : MonoMemoryPool<Pipe>
        {
        }
    }
}