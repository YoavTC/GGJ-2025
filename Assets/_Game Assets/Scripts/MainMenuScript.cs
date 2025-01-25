using UnityEngine;
using System;
using UnityEngine.SceneManagement;
public class MainMenuScript : MonoBehaviour
{
    //public AudioManager AudioManager;[]

    [SerializeField] private AudioClip theme;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioManager.Instance.PlayAudioClip(theme);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void GoToGame()
    {
        SceneManager.LoadScene("GameScene");    
    }
}
