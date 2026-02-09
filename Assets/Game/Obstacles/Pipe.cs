using Game.Core;
using UnityEngine;

namespace Game.Obstacles
{
    public class Pipe : MonoBehaviour
    {
        private float _speed;
        private float _despawnX;
        private GameStateService _gameState;
        private PipeSpawner _spawner;

        public void Init(float speed, float despawnX, PipeSpawner spawner, GameStateService gameState)
        {
            _speed = speed;
            _despawnX = despawnX;
            _spawner = spawner;
            _gameState = gameState;
        }

        private void Update()
        {
            if (_gameState.Current != GameState.Playing)
                return;

            transform.position += Vector3.left * _speed * Time.deltaTime;

            if (transform.position.x < _despawnX)
            {
                _spawner.Despawn(this);
            }
        }
    }
}