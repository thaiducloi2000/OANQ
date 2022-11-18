using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{
    public enum Level { Easy,Normal,Hard}

    public static Bot Instance;
    public new const string name = "Chu Be Dan";
    public int point;
    public const float waitTime = 10f;
    public float countTime = 10f;
    public bool isBot = false;
    public Level level = Level.Easy;

    public List<GameObject> nodes;

    public void loadNode()
    {
        for(int i = 7; i <= Chessboard.Instance.listNode.Count-1; i++)
        {
            nodes.Add(Chessboard.Instance.listNode[i]);
        }
    }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        this.point = 0;
    }

    private void Start()
    {
        nodes = new List<GameObject>();
        loadNode();
    }

    private void Update()
    {
        isBot = !MoveManager.instance.isPlayerMove;
        if (isBot == true)
        {
            if (countTime <= 0f)
            {
                BotMove();
                this.isBot = false;
                countTime = waitTime;
            }
            countTime -= Time.deltaTime;
        }
    }


    public void BotMove()
    {
        System.Random rand = new System.Random();

        isBot = false;
        int num_1 = rand.Next(0, 2);
        int num_2;
        do
        {
            num_2 = rand.Next(0, nodes.Count);
            Debug.Log(nodes[num_2].name + " Is 1");
            Debug.Log(nodes[num_2].GetComponentInChildren<Node>().name + " Is 2");
        } while (nodes[num_2].GetComponentInChildren<Node>()._CurrentNumchess == 0);
        if (num_1 == 0)
        {
            MoveManager.instance.CheckNode(nodes[num_2].GetComponentInChildren<Node>());
            MoveManager.instance.CheckNode(nodes[num_2].GetComponentInChildren<Node>().front);
        }
        else if (num_1 == 1)
        {
            MoveManager.instance.CheckNode(nodes[num_2].GetComponentInChildren<Node>());
            MoveManager.instance.CheckNode(nodes[num_2].GetComponentInChildren<Node>().back);
        }
    }
}
