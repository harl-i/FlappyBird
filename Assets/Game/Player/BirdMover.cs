using Game.Core;
using Game.Core.Signals;
using Infrastructure.InputSystem;
using UnityEngine;
using Zenject;

namespace Game.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Transform))]
    public class BirdMover : MonoBehaviour
    {
        private Rigidbody2D _rigidbody;
        private IInputService _inputService;
        private GameStateService _gameStateService;
        private BirdConfig _birdConfig;
        private SignalBus _signalBus;
        private Transform _transform;

        private Vector2 _startPosition = new Vector2(-3.796631f, -0.3190476f);

        [Inject]
        public void Construct(
            IInputService inputService,
            GameStateService gameStateService,
            BirdConfig birdConfig,
            SignalBus signalBus)

        {
            _inputService = inputService;
            _gameStateService = gameStateService;
            _birdConfig = birdConfig;
            _signalBus = signalBus;
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _transform = GetComponent<Transform>();
        }

        private void OnEnable()
        {
            _signalBus.Subscribe<RestartGameSignal>(OnGameRestart);
        }

        private void OnDisable()
        {
            _signalBus.Unsubscribe<RestartGameSignal>(OnGameRestart);
        }

        private void Update()
        {
            if (_gameStateService.Current != GameState.Playing)
                return;

            if (_inputService.JumpPressed)
            {
                Jump();
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            _signalBus.Fire<BirdDiedSignal>();
        }

        private void Jump()
        {
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.AddForce(Vector2.up * _birdConfig.JumpForce, ForceMode2D.Impulse);
        }

        private void OnGameRestart()
        {
            _transform.position = _startPosition;
        }
    }
}