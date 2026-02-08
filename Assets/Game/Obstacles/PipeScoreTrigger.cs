using UnityEngine;
using Zenject;
using Game.Core.Signals;

namespace Game.Obstacles
{
    public class PipeScoreTrigger : MonoBehaviour
    {
        private SignalBus _signalBus;

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            Debug.Log("PipeScoreTrigger injected");
            _signalBus = signalBus;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                _signalBus.Fire<PassedPipeSignal>();
            }
        }
    }
}