using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OANQ_GameManager : MonoBehaviour
{
    public static OANQ_GameManager instance;
    public enum GameStatus { Pause,Playing,End}
    public GameStatus status;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        this.status = GameStatus.Playing;
    }


    private void Update()
    {
        if (isEndGame())
        {
            this.status = GameStatus.End;
            EndGame();
        }
    }
    public void EndGame()
    {
        UIManager.instance.QuitBtn();
    }

    private bool isEndGame()
    {
        return Chessboard.Instance.listNode[0].GetComponentInChildren<Node>()._CurrentNumchess == 0 && Chessboard.Instance.listNode[6].GetComponentInChildren<Node>()._CurrentNumchess == 0;
    }
}
