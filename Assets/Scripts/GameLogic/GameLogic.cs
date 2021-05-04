using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public static GameLogic instance { get; protected set; }
    bool isStart = false;
    bool isEnd = false;
    bool isPause = false;
    public GameObject gameOverUI;
    
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
    }
    public void GameStart()
    {
        gameOverUI.SetActive(false);
        isStart = true;
        isEnd = isPause = false;

    }
    public void GameOver()
    {
        isEnd = true;
    }
}
