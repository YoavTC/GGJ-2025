using UnityEngine;
using TMPro;

public class PlayerObject : MonoBehaviour
{
    [SerializeField] public int WinRounds = 0;

    [Header("Player Health")]
    [ReadOnly]
    [SerializeField] private int _health = 100;
    public int Health { get => _health; private set => _health = value; }

    [SerializeField] public TMP_Text playerHealthText;

    [Header("Player Stamina")]
    [ReadOnly]
    [SerializeField] private int _stamina = 0;
    public int Stamina  { get => _stamina; private set => _stamina = value; }

    [SerializeField] public TMP_Text playerStaminaText;

    public void Start()
    {
        Debug.Log($"Start Health: {Health} Stamina: {Stamina}");
        Reset();
    }

    public void Reset()
    {
        Health = 100;
        Stamina  = 0;
        WinRounds = 0;
        // force UI update
        AddStamina(0);
        AddHealth(0);
    }

    public void AddStamina(int value)
    {
        Stamina += value;
        if (playerStaminaText)
            playerStaminaText.SetText($"{Stamina}");
        Debug.Log($"Add Stamina {Stamina}");
    }

    public void AddHealth(int value)
    {
        Health += value;
        if (playerHealthText)
            playerHealthText.SetText($"{Health} %");

        Debug.Log($"Add Health {Health}");
    }

}
