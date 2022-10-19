using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [Header("Game States"), SerializeField]
    private GameState GameState;

    [Space, SerializeField]
    private GameStateSO menuGameState;
    [SerializeField]
    private GameStateSO playGameState;
    [SerializeField]
    private GameStateSO bossBattleGameState;
    [SerializeField] 
    private GameStateSO gameOverGameState;
    [SerializeField] 
    private GameStateSO victoryGameState;

    private GameStateSO activeGameState;

    public GameStateSO ActiveGameState
    {
        get { return activeGameState; }
        private set { activeGameState = value; }
    }

    public delegate void OnStateChangeHandler();
    public  event OnStateChangeHandler OnStateChange;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    public void SetGameState(GameState state)
    {
        GameState = state;
       
        switch (state)
        {
            case GameState.Menu:
                OnMenuState();
                break;
            case GameState.Play:
                OnPlayState();
                break;
            case GameState.BossBattle:
                OnBossBattleState();
                break;
            case GameState.GameOver:
                OnGameOverState();
                break;
            case GameState.Victory:
                OnVictoryState();
                break;
            default:
                Debug.LogWarning("GameState not found!");
                break;
        }

        /* Trigger Event for any subscribed functions */
        OnStateChange();
    }

    private void OnMenuState()
    {
        activeGameState = menuGameState;
    }

    private void OnPlayState()
    {
        activeGameState = playGameState;
    }

    private void OnBossBattleState()
    {
        activeGameState = bossBattleGameState;
    }

    private void OnGameOverState()
    {
        activeGameState = gameOverGameState;
    }

    private void OnVictoryState()
    {
        activeGameState = victoryGameState;
    }
}