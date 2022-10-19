using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Menu = 0, Play = 1, BossBattle = 2, GameOver = 3, Victory = 4
}

[CreateAssetMenu(fileName = "New GameState", menuName = "GameManager/GameState", order = 1)]
public class GameStateSO : ScriptableObject
{
    [Header("Game State Settings"), SerializeField]
    private GameState gameState;
    

    [SerializeField, Stem.SoundID]
    private Stem.ID soundtrackID;

    public GameState GameState
    {
        get { return gameState; }
        private set { gameState = value; }
    }

    public Stem.ID SoundtrackID
    {
        get { return soundtrackID; }
        private set { soundtrackID = value; }
    }
}