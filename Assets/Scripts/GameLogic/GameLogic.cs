using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{
    public static GameLogic instance { get; protected set; }
    bool isStart = false;
    bool isEnd = false;
    bool isPause = false;
    public GameObject gameOverUI;
    public GameObject gameRestartUI;
    
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        GameStart();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void Die()
    {
        GameOver();
        GlobalTimeController.instance.Pause();
        gameOverUI.SetActive(true);
        gameRestartUI.SetActive(true);
    }
    public void GameStart()
    {
        gameOverUI.SetActive(false);
        gameRestartUI.SetActive(false);
        isStart = true;
        isEnd = isPause = false;
    }

    public void GameRestart()
    {
        gameOverUI.SetActive(false);
        gameRestartUI.SetActive(false);
        isStart = true;
        isEnd = isPause = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
        Debug.Log("重新开始");
        SceneManager.LoadScene("Level1");
    }
    public void GameOver()
    {
        isEnd = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
