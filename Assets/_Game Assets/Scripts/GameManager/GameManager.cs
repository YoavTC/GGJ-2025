using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Players")]
    [SerializeField] public PlayerObject Player1;
    [SerializeField] public PlayerObject Player2;

    [Header("Game")]
    [SerializeField] public TMP_Text timerText;

    [SerializeField] public TMP_Text gamneOverText;

    [SerializeField] public float timeRemaining = 300f; // Starting time in seconds
    
    private bool isGameOver = false;

    void Start()
    {
        Player1.Reset();
        Player2.Reset();
        isGameOver = false;
        gamneOverText.gameObject.SetActive(false);
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
                GameOver();
            }
            // if either player's health is 0, game over
            if (Player1.Health <= 0 || Player2.Health <= 0)
            {
                GameOver();
            }
        }
    }

    void UpdateCounterDisplay()
    {
        int seconds = Mathf.FloorToInt(timeRemaining);
        if (timerText)
            timerText.SetText($"{seconds}");
    }
    
    void GameOver()
    {
        isGameOver = true;
        gamneOverText.gameObject.SetActive(isGameOver);
    }
}
