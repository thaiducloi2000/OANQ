using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    public string Player_Name = "Little Kid";
    public int point = 0;
    public GameObject Chess;
    public GameObject SpecialChess;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        this.name = "Little Kid";
        this.point = 0;
    }

    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (OANQ_GameManager.instance.status == OANQ_GameManager.GameStatus.Playing)
            {
                OANQ_GameManager.instance.status = OANQ_GameManager.GameStatus.Pause;
            }
            else
            {
                OANQ_GameManager.instance.status = OANQ_GameManager.GameStatus.Playing;
            }
        }
    }
}
