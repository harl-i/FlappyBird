using Game.Core;
using Game.Core.Signals;
using TMPro;
using UnityEngine;
using Zenject;

namespace Game.UI
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;

        private SignalBus _signalBus;
        private ScoreService _scoreService;

        [Inject]
        public void Construct(SignalBus signalBus, ScoreService scoreService)
        {
            _signalBus = signalBus;
            _scoreService = scoreService;
        }

        private void OnEnable()
        {
            _signalBus.Subscribe<ScoreChangedSignal>(OnScoreChanged);
        }

        private void OnDisable()
        {
            _signalBus.Unsubscribe<ScoreChangedSignal>(OnScoreChanged);
        }

        private void OnScoreChanged(ScoreChangedSignal signal)
        {
            _text.text = _scoreService.Score.ToString();
        }
    }
}