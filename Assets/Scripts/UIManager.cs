using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI title;
    [SerializeField] public TextMeshProUGUI PlayerName;
    [SerializeField] public TextMeshProUGUI point;
    [SerializeField] public TextMeshProUGUI Botpoint;
    public static UIManager instance;
    // Start is called before the first frame update

    public GameObject GamePlay_UI;
    public GameObject Pause_UI;

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
        this.GamePlay_UI.SetActive(true);
        this.Pause_UI.SetActive(false);
        this.title.gameObject.SetActive(false);
    }

    private void Update()
    {
        setPoint(Player.Instance.point.ToString(), Bot.Instance.point.ToString());
        UI_State(OANQ_GameManager.instance.status);
    }

    public void setPoint(string point,string botPoint)
    {
        this.point.text = "Point  : " + point;
        this.PlayerName.text = "You";
        this.Botpoint.text = "Point  : " + botPoint;
    }

    private void UI_State(OANQ_GameManager.GameStatus status)
    {
        if(status == OANQ_GameManager.GameStatus.Playing)
        {
            this.GamePlay_UI.SetActive(true);
            this.Pause_UI.SetActive(false);
        }
        else
        {
            this.GamePlay_UI.SetActive(false);
            this.Pause_UI.SetActive(true);
        }
    }

    public void ResumeBtn()
    {
        OANQ_GameManager.instance.status = OANQ_GameManager.GameStatus.Playing;
    }
    public void QuitBtn()
    {
        Application.Quit();
    }

    public void PauseGameBtn()
    {
        OANQ_GameManager.instance.status = OANQ_GameManager.GameStatus.Pause;
    }
}
