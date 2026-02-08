using Game.Core;
using Game.Core.Signals;
using UnityEngine;
using Zenject;

namespace Game.UI
{
    public class GameOverView : MonoBehaviour
    {
        [SerializeField] private GameObject _gameOverPanel;

        private GameStateService _gameState;
        private SignalBus _signalBus;

        [Inject]
        private void Construct(GameStateService gameState, SignalBus signalBus)
        {
            _gameState = gameState;
            _signalBus = signalBus;
        }

        private void OnEnable()
        {
            _signalBus.Subscribe<BirdDiedSignal>(OnBirdDied);
        }

        private void OnDisable()
        {
            _signalBus.Unsubscribe<BirdDiedSignal>(OnBirdDied);
        }

        private void OnBirdDied()
        {
            _gameOverPanel.SetActive(true);
        }

        public void OnRestartClicked()
        {
            _signalBus.Fire<RestartGameSignal>();
            _gameOverPanel.SetActive(false);
        }
    }
}