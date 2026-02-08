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

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
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
            _text.text = signal.Score.ToString();
        }
    }
}