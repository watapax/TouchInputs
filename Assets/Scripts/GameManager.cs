using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public UnityEvent onGameOver, onGameRestart, onGameWin;

    private void Awake()
    {
        instance = this;
    }

    public void GameOver()
    {
        onGameOver.Invoke();
    }

    public void GameRestart()
    {
        onGameRestart.Invoke();
    }

    public void GameWin()
    {
        onGameWin.Invoke();
    }
}
