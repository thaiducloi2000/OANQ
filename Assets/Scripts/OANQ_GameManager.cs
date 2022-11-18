using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OANQ_GameManager : MonoBehaviour
{
    public static OANQ_GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

}
