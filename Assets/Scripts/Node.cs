using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public enum NodeType { chess, SpecialChess }
public class Node : MonoBehaviour,IPointerClickHandler
{
    public NodeType nodeType;
    private Transform parent;
    private int _Numchess;
    public int _CurrentNumchess;
    public Node front;
    public Node back;
    public TMPro.TextMeshPro numChesstxt;
    public GameObject chessPrefab;
    public GameObject specialChessPrefab;
    public List<GameObject> chessList = new List<GameObject>();
    public bool isSelected = false;
    public Color defaultColor;
    public GameObject Collider;

    private void Start()
    {
        if(nodeType == NodeType.chess)
        {
            _Numchess = 5;
        }else if(nodeType == NodeType.SpecialChess)
        {
            _Numchess = 1;
        }
        parent = GameObject.Find("Chessboard").transform;
        this.GetComponent<Renderer>().material.color = defaultColor;
        onStartGame();
        
    }


    private void onStartGame()
    {
        numChesstxt.raycastTarget = false;
        for (int i = 0; i < _Numchess; i++)
        {
            if(nodeType == NodeType.SpecialChess)
            {
                spawnAtSide();
            }
            else
            {
                AddChess();
            }
        }
    }



    private void spawnAtSide()
    {
        _CurrentNumchess++;
        GameObject chess = Instantiate(specialChessPrefab, transform.position + new Vector3(0f,0.5f,0f), Quaternion.identity,parent);
        //chess.transform.parent = this.transform;
        chessList.Add(chess);
        numChesstxt.text = _CurrentNumchess.ToString();
    }

    public void AddChess()
    {
        this.Collider.SetActive(true);
        _CurrentNumchess++;
        //float scaleSize = Chessboard.Instance.scaleSize;
        Vector3 position = new Vector3(0f,2F, 0f);
        GameObject chess = Instantiate(chessPrefab, transform.position + position, Quaternion.identity,parent);
        //chess.transform.parent = this.transform;
        chessList.Add(chess);
        numChesstxt.text = _CurrentNumchess.ToString();
    }

    public int getAllChess()
    {
        int num = _CurrentNumchess;
        for (int i = 0; i < chessList.Count; i++)
        {
            Destroy(chessList[i]);
        }
        chessList.Clear();
        //StartCoroutine(RemoveChess());
        _CurrentNumchess = 0;
        numChesstxt.text= _CurrentNumchess.ToString();
        return num;
    }

    //IEnumerator RemoveChess()
    //{
    //    for (int i = 0; i < chessList.Count; i++)
    //    {
    //        Animator ani = chessList[i].GetComponent<Animator>();
    //        ani.SetBool("isDestroy", true);
    //        Destroy(chessList[i]);
    //        yield return new WaitForSeconds(1f);
    //    }
    //    chessList.Clear();
    //}

    public void OnPointerClick(PointerEventData eventData)
    {
        if (MoveManager.instance.isPlayerMove == true)
        {
            HightLight();
            MoveManager.instance.CheckNode(eventData.pointerEnter.GetComponentInParent<Node>());
        }
    }

    public void HightLight()
    {
        isSelected = !isSelected;
        if (isSelected == true)
        {
            this.GetComponent<Renderer>().material.color = Color.red;
            this.front.GetComponent<Renderer>().material.color = Color.green;
            this.back.GetComponent<Renderer>().material.color = Color.green;
        }
        else
        {
            EndHightlight();
        }
    }

    public void EndHightlight()
    {
        this.GetComponent<Renderer>().material.color = defaultColor;
        this.front.GetComponent<Renderer>().material.color = this.front.defaultColor;
        this.back.GetComponent<Renderer>().material.color = this.back.defaultColor;
    }

}
