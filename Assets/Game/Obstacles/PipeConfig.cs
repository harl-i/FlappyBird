using UnityEngine;

namespace Game.Obstacles
{
    [CreateAssetMenu(
        fileName = "PipeConfig",
        menuName = "Game/Pipe Config")]
    public class PipeConfig : ScriptableObject
    {
        public Pipe PipePrefab;
        public float SpawnInterwal = 2f;
        public float MoveSpeed = 2f;
        public float SpawnX = 10f;
        public float DespawnX = -15f;
        public float MinY = -2f;
        public float MaxY = 2f;
    }
}