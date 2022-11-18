using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    public string name = "Little Kid";
    public int point = 0;

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
}
