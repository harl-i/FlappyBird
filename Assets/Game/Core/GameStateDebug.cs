using UnityEngine;
using Zenject;
using Game.Core;

public class DebugGameState : MonoBehaviour
{
    private GameStateService _gameState;

    [Inject]
    public void Construct(GameStateService gameState)
    {
        _gameState = gameState;
    }

    private void Start()
    {
        _gameState.StartGame();
        Debug.Log(_gameState.Current);
    }
}