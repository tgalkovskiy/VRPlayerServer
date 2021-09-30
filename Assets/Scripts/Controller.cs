using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Controller : MonoBehaviour
{

    public static Controller instance;

    public int round = 0;
    public int maxRounds = 9;
    public float minDistance = .3f; //2
    public float maxDistance = 1f; //4

    public GameObject startPanel;

    public GameObject roundGameUI;
    public TMP_Text roundTimeUI;
    public TMP_Text roundAmountUI;
    public TMP_Text roundPointsUI;
    public GameObject roundResultUI;
    public TMP_Text resultPointsUI;

    public GameObject timerPanel;
    public TMP_Text timerlabel;

    public Transform bubblesContainer;
    public GameObject bubblePrefab;

    public bool randomColors = false;
    public Color[] bubblesColors;

    public bool playing = false;
    List<Color> targetColors = new List<Color>();

    public float targetTimer = -1f;
    public int targetAmount = 0;

    float defautReadInfoDelay = 5f;
    float defautRoundStartDelay = 5f;
    float defaultRoundTime = 90f;

    public AudioClip startSound;
    public AudioClip endSound;

    int selectedAmount = 0;
    int points = 0;

    private void Awake()
    {
        instance = this;

#if UNITY_EDITOR
        defaultRoundTime *= 0.3f;
        defautReadInfoDelay *= 0.3f;
        defautRoundStartDelay *= 0.3f;
#endif

    }

    void Start()
    {
        startPanel.SetActive(false);
        timerPanel.SetActive(false);

        roundGameUI.SetActive(false);
        roundResultUI.SetActive(false);

        bubblesContainer.Clear();

        if (randomColors)
        {
            bubblesColors = new Color[20];
            for (int i = 0; i < 20; i++)
                bubblesColors[i] = new Color(
                    Random.value > 0.5f ? Random.value * 2f + 0.5f : Random.value * 0.5f + 0.5f,
                    Random.value > 0.5f ? Random.value * 2f + 0.5f : Random.value * 0.5f + 0.5f,
                    Random.value > 0.5f ? Random.value * 2f + 0.5f : Random.value * 0.5f + 0.5f,
                    1f);
        }

        StartGame();
    }

    void ShuffleColors()
    {
        //shuffle colors
        int limit = 10;
        while (limit > 0)
        {
            int f = Random.Range(0, bubblesColors.Length);
            int s = Random.Range(0, bubblesColors.Length);
            Color sc = bubblesColors[s];
            bubblesColors[s] = bubblesColors[f];
            bubblesColors[f] = sc;
            limit--;
        }
    }

    void Update()
    {
        if (playing)
        {
            roundPointsUI.text = points.ToString();

            if (targetTimer > 0f)
            {
                targetTimer -= Time.deltaTime;
                roundTimeUI.text = Mathf.Round(targetTimer).ToString("00");
            }
            else
                FinishRound();

            if (targetAmount > 0)
            {
                roundAmountUI.text = Mathf.Round(targetAmount).ToString("00");
            }
            else
                FinishRound();
        }
    }

    public void StartGame()
    {
        startPanel.SetActive(true);
        roundResultUI.SetActive(false);
        round = 0;
        points = 0;

        ShuffleColors();
        targetColors = new List<Color>();
        for (int i = 0; i < maxRounds; i++)
            targetColors.Add(bubblesColors[i]);
    }

    public void InitRound()
    {
        startPanel.SetActive(false);
        roundGameUI.SetActive(false);

        targetAmount = round + 1;
        targetTimer = defaultRoundTime;
        selectedAmount = 0;        

        bubblesContainer.Clear();

        StartCoroutine(ShowInfoTimer());
    }

    IEnumerator ShowInfoTimer()
    {
        timerPanel.SetActive(true);

        float timer = defautRoundStartDelay;
        while (timer > 0f)
        {
            timerlabel.text = Mathf.Round(timer).ToString("00");
            yield return new WaitForEndOfFrame();
            timer -= Time.deltaTime;
        }

        timerPanel.SetActive(false);
        ShowGameplay();
    }

    void ShowGameplay()
    {
        roundGameUI.SetActive(true);

        for (int i = 0; i <= round; i++)        
            SpawnBubble(targetColors[i]);              

        playing = true;
    }
        void SpawnBubble(Color color)
    {
        Instantiate(bubblePrefab, bubblesContainer).GetComponent<BubbleItem>().Init(color);
    }


    void FinishRound()
    {
        Debug.Log("ROUND Finished ID: " + round);

        playing = false;        
        round++;

        if (round == maxRounds - 1)
            FinishGame();
        else
            InitRound(); // next round
    }

    void FinishGame()
    {
        roundGameUI.SetActive(false);
        roundResultUI.SetActive(true);

        resultPointsUI.text = "Your points:" + points.ToString();

        StartCoroutine(DelayExit());
    }

    IEnumerator DelayExit()
    {
        yield return new WaitForSeconds(defautRoundStartDelay);
        StartGame();
    }
    

    public bool CheckBubbleColor(Color color)
    {        
        return targetColors[selectedAmount] == color; 
    }

    
    public void ClickedProperBubble(GameObject bubble)
    {
        targetAmount--;
        selectedAmount++;
        points++;
    }

    public void ClickedWrongBubble(GameObject bubble)
    {
        points--;
        if (points < 0)
            points = 0;
    }

    Transform bubbleToMove;
    public float moveFaktor = 15f;

    public void GrabBubble(GameObject bubble)
    {
        if (bubbleToMove == null)
        {
            bubbleToMove = bubble.transform;
        }
        else
        {
            if (bubble.transform != bubbleToMove)  // new bubble
            {
                bubble.GetComponent<BubbleItem>().outline.SetActive(false);
                bubbleToMove.gameObject.GetComponent<BubbleItem>().outline.SetActive(false);

                Rigidbody rb = bubbleToMove.gameObject.GetComponent<Rigidbody>();
                rb.velocity = Vector3.zero;
                rb.AddForce((bubble.transform.position - bubbleToMove.position).normalized * moveFaktor);
                bubbleToMove = null;

                bubble.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }
    }
    
}
