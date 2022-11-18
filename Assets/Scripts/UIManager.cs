using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI title;
    [SerializeField] public TextMeshProUGUI name;
    [SerializeField] public TextMeshProUGUI point;
    [SerializeField] public TextMeshProUGUI Botpoint;
    public static UIManager instance;
    // Start is called before the first frame update

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("There are more  than 1 instance");
            return;
        }
        instance = this;
    }
    void Start()
    {
        this.title.gameObject.SetActive(false);
    }

    public void setPoint(string point,string name,string botPoint)
    {
        this.point.text = "Point  : " + point;
        this.name.text = "Player Name  : " + name;
        this.Botpoint.text = "Point  : " + botPoint;
    }

}
