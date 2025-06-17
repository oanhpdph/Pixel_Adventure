using System;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Pause,
    GameOver,
    Setting,
    GameFinish,
    Menu,
    Play,
    SelectCharacter
}
public class GameManager : MonoBehaviour
{
    private static GameManager instance { get; set; }
    public static GameManager Instance => instance;
    public event Action<GameState> OnStateChange = delegate { };

    private GameState curState;
    public Stack<GameState> stackGameState = new();
    private void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public GameState CurrentState
    {
        get { return curState; }
        set
        {
            curState = value;
            OnStateChange(curState);
        }
    }

    public GameState GetTopStack()
    {
        if (stackGameState.Count == 0)
        {
            return GameState.Play;
        }
        return stackGameState.Peek();
    }
}
