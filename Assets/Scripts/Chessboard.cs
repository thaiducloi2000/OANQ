using UnityEngine;
using System.Collections.Generic;

public class Chessboard : MonoBehaviour
{
    public static Chessboard Instance;
    [SerializeField] private GameObject Node;
    public float scaleSize = 1;
    public int numNode = 12;
    public List<GameObject> listNode;
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        SpawnChessBoard();
    }


    public void SpawnChessBoard()
    {
        listNode = new List<GameObject>();
        for (int i = 0; i < numNode; i++)
        {
            GameObject node;
            if (i == 0)
            {
                node = Instantiate(Node, new Vector3(i * scaleSize, 0f, 0.5f * scaleSize), Quaternion.identity);
                node.transform.GetChild(0).localScale = new Vector3(1 * scaleSize, 1f, 2f * scaleSize);
            }
            else if (i == 6)
            {
                node = Instantiate(Node, new Vector3(i * scaleSize, 0f, 0.5f * scaleSize), Quaternion.identity);
                node.transform.GetChild(0).localScale = new Vector3(1 * scaleSize, 1f, 2f * scaleSize);
            }
            else if (i > 6)
            {
                node = Instantiate(Node, new Vector3((numNode - i) * scaleSize, 0f, 1f * scaleSize), Quaternion.identity);
            }
            else
            {
                node = Instantiate(Node, new Vector3(i * scaleSize, 0f, 0f * scaleSize), Quaternion.identity);
            }
            node.transform.parent = this.transform;
            if (i == 0|| i == 6)
            {
                node.GetComponentInChildren<Node>().nodeType = NodeType.SpecialChess;
            }
            else
            {
                node.GetComponentInChildren<Node>().nodeType = NodeType.chess;
            }
            node.name = "Node_Parent " + i.ToString();
            node.transform.GetChild(0).name = "Node_Child " + i.ToString();
            listNode.Add(node);
        }
        for(int i = 0; i < listNode.Count; i++)
        {
            if(i == 0)
            {
                listNode[i].GetComponentInChildren<Node>().front = listNode[listNode.Count-1].GetComponentInChildren<Node>();
                listNode[i].GetComponentInChildren<Node>().back = listNode[i+1].GetComponentInChildren<Node>();
            }else if(i == listNode.Count - 1)
            {
                listNode[i].GetComponentInChildren<Node>().front = listNode[i - 1].GetComponentInChildren<Node>();
                listNode[i].GetComponentInChildren<Node>().back = listNode[0].GetComponentInChildren<Node>();
            }
            else
            {
                listNode[i].GetComponentInChildren<Node>().front = listNode[i - 1].GetComponentInChildren<Node>();
                listNode[i].GetComponentInChildren<Node>().back = listNode[i + 1].GetComponentInChildren<Node>();
            }
            if(i >= 6)
            {
                listNode[i].GetComponentInChildren<Node>().defaultColor = Color.black;
                listNode[i].GetComponentInChildren<Node>().numChesstxt.color = Color.white;
            }
            else
            {
                listNode[i].GetComponentInChildren<Node>().defaultColor = Color.white;
                listNode[i].GetComponentInChildren<Node>().numChesstxt.color = Color.black;
            }
        }
    }
}
