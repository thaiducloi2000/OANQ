using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveManager : MonoBehaviour
{
    public static MoveManager instance;
    public Node StartNode;
    public Node EndNode;
    public bool isPlayerMove = true;
    public bool isPlayingAnimation = false;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void CheckNode(Node node)
    {

        if (StartNode == null && (node != Chessboard.Instance.listNode[0] || node != Chessboard.Instance.listNode[6]) && node._CurrentNumchess != 0)
        {
            node.HightLight();
            StartNode = node;
            return;
        }
        else if (EndNode == null && (node == StartNode.front || node == StartNode.back))
        {
            EndNode = node;
        }if(node == StartNode)
        {
            node.EndHightlight();
            StartNode = null;
        }
        if (StartNode != null && EndNode != null)
        {
            Move(StartNode, EndNode);
            this.isPlayingAnimation = true;
            StartNode = null;
            EndNode = null;
        }
        else
        {
            StartNode = null;
            EndNode = null;
            return;
        }
    }

    private void Move(Node startNode, Node endNode)
    {
        this.isPlayerMove = !isPlayerMove;
        if (startNode.front == endNode)
        {
            StartCoroutine(MovingFront((startNode)));
        }
        else if(startNode.back == endNode)
        {
            StartCoroutine(MovingBack((startNode)));
        }
        if (isPlayingAnimation == false)
        {
            startNode.EndHightlight();
            endNode.EndHightlight();
        }
    }

    IEnumerator MovingFront(Node StartNode)
    {
        float waitTime = .5f;
        int count = StartNode.getAllChess();
        for (int i = 0; i < Chessboard.Instance.listNode.Count; i++)
        {
            if (Chessboard.Instance.listNode[i].GetComponentInChildren<Node>() == StartNode.front)
            {
                int pos = 0;
                for (int j = 0; j < count; j++)
                {
                    pos = i-j;
                    if (pos < 0)
                    {
                        pos += 12;
                        Chessboard.Instance.listNode[pos].GetComponentInChildren<Node>().AddChess();
                    }
                    else
                    {
                        Chessboard.Instance.listNode[i - j].GetComponentInChildren<Node>().AddChess();
                    }
                    yield return new WaitForSeconds(waitTime);
                }
                if (isSide(Chessboard.Instance.listNode[pos].GetComponentInChildren<Node>().front))
                {
                    isPlayingAnimation = false;
                    yield break;
                }
                else if (overNumChess(Chessboard.Instance.listNode[pos].GetComponentInChildren<Node>().front, Chessboard.Instance.listNode[pos].GetComponentInChildren<Node>().front.front))
                {
                    isPlayingAnimation = false;
                    yield break;
                }
                else if (GetPoint(Chessboard.Instance.listNode[pos].GetComponentInChildren<Node>().front, Chessboard.Instance.listNode[pos].GetComponentInChildren<Node>().front.front))
                {
                    if(isPlayerMove == false)
                    {
                        Player.Instance.point += isGetPoint(Chessboard.Instance.listNode[pos].GetComponentInChildren<Node>().front.front);
                    }
                    else
                    {
                        Bot.Instance.point += isGetPoint(Chessboard.Instance.listNode[pos].GetComponentInChildren<Node>().front.front);
                    }
                    isPlayingAnimation = false;
                    yield break;
                }
                else //if (isCountinue(Chessboard.Instance.listNode[pos].GetComponentInChildren<Node>().front))
                {
                    StartCoroutine(MovingFront(Chessboard.Instance.listNode[pos].GetComponentInChildren<Node>().front));
                }
            }
        }
    }

    IEnumerator MovingBack(Node StartNode)
    {
        float waitTime = .5f;
        int count = StartNode.getAllChess();
        for (int i = 0; i < Chessboard.Instance.listNode.Count; i++)
        {   if (Chessboard.Instance.listNode[i].GetComponentInChildren<Node>() == StartNode.back ) {
                int pos = 0;
                for (int j = 0; j < count; j++)
                {
                    pos = i + j;
                    if (pos > 11)
                    {
                        pos -= 12;
                        Chessboard.Instance.listNode[pos].GetComponentInChildren<Node>().AddChess();
                    }
                    else
                    {
                        Chessboard.Instance.listNode[i + j].GetComponentInChildren<Node>().AddChess();
                    }
                    yield return new WaitForSeconds(waitTime);
                }
                if (isSide(Chessboard.Instance.listNode[pos].GetComponentInChildren<Node>().back))
                {
                    isPlayingAnimation = false;
                    yield break;
                }else if (overNumChess(Chessboard.Instance.listNode[pos].GetComponentInChildren<Node>().back, Chessboard.Instance.listNode[pos].GetComponentInChildren<Node>().back.back))
                {
                    isPlayingAnimation = false;
                    yield break;
                }
                else if(GetPoint(Chessboard.Instance.listNode[pos].GetComponentInChildren<Node>().back, Chessboard.Instance.listNode[pos].GetComponentInChildren<Node>().back.back))
                {
                    if (isPlayerMove == false)
                    {
                        Player.Instance.point += isGetPoint(Chessboard.Instance.listNode[pos].GetComponentInChildren<Node>().back.back);
                    }
                    else
                    {
                        Bot.Instance.point += isGetPoint(Chessboard.Instance.listNode[pos].GetComponentInChildren<Node>().back.back);
                    }
                    isPlayingAnimation = false;
                    yield break;
                }
                else // if(isCountinue(Chessboard.Instance.listNode[pos].GetComponentInChildren<Node>().back))
                {
                    StartCoroutine(MovingBack(Chessboard.Instance.listNode[pos].GetComponentInChildren<Node>().back));
                } 
            }
        }
    }

    private bool isSide(Node lastNode)
    {
        return lastNode == Chessboard.Instance.listNode[0].GetComponentInChildren<Node>() || lastNode == Chessboard.Instance.listNode[6].GetComponentInChildren<Node>();
    }

    private bool overNumChess(Node lastNode, Node nextLastnode)
    {
        return lastNode._CurrentNumchess == 0 && nextLastnode._CurrentNumchess == 0;
    }

    private bool GetPoint(Node lastNode, Node nextLastnode)
    {
        return lastNode._CurrentNumchess == 0 && nextLastnode._CurrentNumchess != 0;
    }

    private int isGetPoint(Node lastNode)
    {
        return lastNode.getAllChess();
    }
}
