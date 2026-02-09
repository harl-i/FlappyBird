using Game.Core;
using UnityEngine;
using Zenject;

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