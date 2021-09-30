using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class ActiveObject : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{

    object value;

    public float timePast = 0f;
    public float clickTimer = 2f;

    public UnityEvent actionClick;
    public UnityEvent actionOver;
    public UnityEvent actionOut;

    bool over = false;
    bool clicked = false;
    public bool oneClick = false; // only one time activation   
    public bool resizeColliderOnEnable = true;

    public EventTrigger eventTrigger;

    Button button;
    bool interactable = true;
    EventSystem es;
    public Image fill;

    bool debug = false;

    public void OnEnable()
    {
        button = GetComponent<Button>();
        if (resizeColliderOnEnable)
            StartCoroutine(UpdateCollider());
        es = FindObjectOfType<EventSystem>();
        if (eventTrigger == null)
            eventTrigger = GetComponent<EventTrigger>();

        over = false;
        clicked = false;

        if (fill != null)
            fill.fillAmount = 0f;
    }


    IEnumerator UpdateCollider() {

        yield return new WaitForEndOfFrame();

        if (LayerMask.NameToLayer("UI") == gameObject.layer)
        {
            BoxCollider bc = GetComponent<BoxCollider>();
            RectTransform rt = GetComponent<RectTransform>();
           // Debug.Log(rt.rect);
           if (bc != null)
                bc.size = new Vector3(rt.rect.width, rt.rect.height, 1f);
        }
    }

    public void Init(object v)
    {
        value = v;
    }


    void Update()
    {

        if (over && !clicked)
        {
            timePast += Time.deltaTime;

            if (fill != null)
                fill.fillAmount = timePast / clickTimer;
            else
            if (Reticle.instance != null)
                Reticle.instance.Fill(timePast / clickTimer);

            if (timePast >= clickTimer)
            {
                timePast = 0f;

                if (fill != null)
                    fill.fillAmount = 0f;
                else
                if (Reticle.instance != null)
                    Reticle.instance.Fill(0f);

                over = false;

                if (eventTrigger != null)
                    eventTrigger.OnPointerClick(new PointerEventData(es));

                actionClick.Invoke();
                clicked = true;                
            }            
        }

    }



    public void OnPointerClick(PointerEventData eventData)
    {
        if (!Interactible())
            return;

        actionClick.Invoke();
        clicked = true;

        if (debug) Debug.Log("pointer clicked");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!Interactible())
            return;

        over = true;
        actionOver.Invoke();

        if (debug) Debug.Log("pointer enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        over = false;
        timePast = 0f;        
        clicked = false;
        if (Reticle.instance != null)
            Reticle.instance.Fill(0f);

        if (debug) Debug.Log("pointer exit");
        actionOut.Invoke();

        if (fill != null)
            fill.fillAmount = 0f;
    }

    bool Interactible()
    {
        if (button)
        {
            return button.interactable;
        }
        else
        {
            return interactable;
        }
    }

    private void OnDisable()
    {
        OnPointerExit(new PointerEventData(FindObjectOfType<EventSystem>()));
    }
}
