using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public Button resumeBtn;
    public Button gameMenuBtn;

    public GameObject gameMenuScreen;

    public bool resumePressed;

    //public GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Resume()
    {
        gameMenuScreen.SetActive(false);
        resumePressed = true;
        //Time.timeScale = 1f;

        //gameManager.gamePaused = false;
    }

    public void ToGameMenu()
    {
        SceneManager.LoadSceneAsync("GameMenu");
    }
}
