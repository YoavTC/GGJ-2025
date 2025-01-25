using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Players")]
    [SerializeField] private playerScriptable Player1;
    [SerializeField] private playerScriptable Player2;

    [Header("Timer")]
    [SerializeField] private floatScriptable timerText;

    [Header("GameOver")]
    [SerializeField] private textScriptable gameOverText;

    private PlayerJoinScript playerJoin;

    [SerializeField] public float timeRemaining = 300f; // Starting time in seconds
    
    private bool isGameOver = false;

    void Start()
    {
        resetPlayers();
        resetGameover();
    }

    void Update()
    {
        // update time only if not game over and not paused
        if (!isGameOver)
        {
            // Timer condition
            timeRemaining -= Time.deltaTime;

            if (timeRemaining >= 0)
            {
                UpdateCounterDisplay();
            }
            else
            {
                GameOver("Draw");
            }
            // if either player's health is 0, game over
            if (Player1.Value >= 3)
            {
                GameOver("Player 1 Wins");
            }
            else if (Player2.Value >= 3)
            {
                GameOver("Player 2 wins");
            }
        }
    }

    void resetPlayers()
    {
        PlayerJoinScript playerJoin = FindFirstObjectByType<PlayerJoinScript>();
        Player1.StartPosition = playerJoin.spawnPoints[0].position;
        Player2.StartPosition = playerJoin.spawnPoints[1].position;


        //foreach (PlayerController go in FindObjectsByType<PlayerController>(FindObjectsSortMode.None))
        //{
        //    if (go.gameObject.name.Contains("Player1"))
        //        Player1.StartPosition = go.gameObject.transform.position;
        //    else
        //        Player2.StartPosition = go.gameObject.transform.position;
        //}

        Player1.Value = 0;
        Player2.Value = 0;
    }

    void resetGameover()
    {
        isGameOver = false;
        gameOverText.Value = "";
    }

    void UpdateCounterDisplay()
    {
        int seconds = Mathf.FloorToInt(timeRemaining);
        if (timerText)
            timerText.Value = seconds;
    }
    
    void GameOver(string text)
    {
        isGameOver = true;
        gameOverText.Value = text;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            
            other.GetComponent<PlayerController>().ResetMomentum();

            if (other.GetComponent<PlayerController>().PlayerIndex==0)
            {
                Player2.Value++;
                other.transform.position = Player1.StartPosition;
                
            }
            else
            {
                Player1.Value++;
                other.transform.position = Player2.StartPosition;
                
            }
                
        }
    }
}
