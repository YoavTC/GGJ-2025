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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //gameOverText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        timerText.text = timer.Value.ToString();
        player1Text.text = player1.Value.ToString();
        player2Text.text = player2.Value.ToString();
        gameOverText.text = gameOver.Value.ToString();
    }
}
