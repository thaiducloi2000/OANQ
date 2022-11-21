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
    public string side;

    private void Start()
    {
        this.chessPrefab = Player.Instance.Chess;
        this.specialChessPrefab = Player.Instance.SpecialChess;
        this.gameObject.tag = side;
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
        specialChessPrefab.transform.localScale = new Vector3(3f,3f,3f);
        specialChessPrefab.GetComponent<SphereCollider>().radius = 0.35f;
        specialChessPrefab.GetComponent<SphereCollider>().center = new Vector3(0f,0.35f,0f);
        GameObject chess = Instantiate(specialChessPrefab, transform.position + new Vector3(0f,0.5f,0f), Quaternion.identity,parent);
        //chess.transform.parent = this.transform;
        chessList.Add(chess);
        numChesstxt.text = _CurrentNumchess.ToString();
    }

    public void AddChess()
    {
        this.Collider.SetActive(true);
        _CurrentNumchess++;
        Vector3 position = new Vector3(0f,2F, 0f);
        chessPrefab.GetComponent<SphereCollider>().radius = 0.3f;
        chessPrefab.GetComponent<SphereCollider>().center = new Vector3(0f, 0.3f, 0f);
        GameObject chess = Instantiate(chessPrefab, transform.position + position, Quaternion.identity,parent);
        chessList.Add(chess);
        numChesstxt.text = _CurrentNumchess.ToString();
    }

    public int getAllChess()
    {
        int num = _CurrentNumchess;
        for (int i = 0; i < chessList.Count; i++)
        {
            chessList[i].gameObject.GetComponent<Chess>().GetSlime();
        }
        chessList.Clear();
        _CurrentNumchess = 0;
        numChesstxt.text= _CurrentNumchess.ToString();
        return num;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (MoveManager.instance.isPlayerMove == true && this.gameObject.tag != "B_Side")
        {
            MoveManager.instance.CheckNode(eventData.pointerEnter.GetComponentInParent<Node>());
        }
    }

    public void HightLight()
    {
            this.GetComponent<Renderer>().material.color = Color.red;
            this.front.GetComponent<Renderer>().material.color = Color.green;
            this.back.GetComponent<Renderer>().material.color = Color.green;
    }

    public void EndHightlight()
    {
        this.GetComponent<Renderer>().material.color = defaultColor;
        this.front.GetComponent<Renderer>().material.color = this.front.defaultColor;
        this.back.GetComponent<Renderer>().material.color = this.back.defaultColor;
    }

}
