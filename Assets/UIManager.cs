using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Timer")]
    [SerializeField] floatScriptable timer;
    [SerializeField] TMPro.TMP_Text timerText;

    [Header("Players")]
    [SerializeField] playerScriptable player1;
    [SerializeField] TMPro.TMP_Text player1Text;

    [SerializeField] playerScriptable player2;
    [SerializeField] TMPro.TMP_Text player2Text;

    [Header("GameOver")]
    [SerializeField] textScriptable gameOver;
    [SerializeField] TMPro.TMP_Text gameOverText;

    [SerializeField] GameObject restartButton, exitButton;

    [Header("WinColors")]
    [SerializeField] Color player1Color, player2Color, drawColor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //gameOverText.gameObject.SetActive(false);
        gameOverText.color = drawColor;
    }

    // Update is called once per frame
    void Update()
    {
        timerText.text = timer.Value.ToString();
        player1Text.text = player1.Value.ToString();
        player2Text.text = player2.Value.ToString();
        if (gameOverText.text.Contains("Player 1"))
            gameOverText.color = player1Color;
        if (gameOverText.text.Contains("Player 2"))
            gameOverText.color = player2Color;

        if (gameOverText.text != "")
        {
            restartButton.SetActive(true);
            exitButton.SetActive(true);
        }
            

        gameOverText.text = gameOver.Value.ToString();
    }
}
