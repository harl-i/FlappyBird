using Game.Core.Signals;
using UnityEngine;
using Zenject;

namespace Game.Obstacles
{
    public class PipeScoreTrigger : MonoBehaviour
    {
        private SignalBus _signalBus;

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out PlayerMark _))
            {
                _signalBus.Fire<PassedPipeSignal>();
            }
        }
    }
}