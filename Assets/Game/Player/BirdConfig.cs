using UnityEngine;

namespace Game.Player
{
    [CreateAssetMenu(
        fileName = "BirdConfig",
        menuName = "Game/Bird Config")]
    public class BirdConfig : ScriptableObject
    {
        public float JumpForce = 5f;
    }
}
