using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleItem : MonoBehaviour
{
    public Renderer rend;
    public ParticleSystem boomFx;

    float maxForceFaktor = .05f;
    public GameObject outline;

    Color currentColor;
    public AudioClip popSound;
    public AudioClip wrongSound;

    Transform cam;
    ConstantForce cForce;

    public LayerMask handLayer;

    private void Awake()
    {
        if (Camera.main != null)
            cam = Camera.main.transform;
        else
            cam = FindObjectOfType<CameraFollow>().transform;

        cForce = GetComponent<ConstantForce>();
    }

    void Start()
    {
        
    }

    public void Init(Color color)
    {
        rend.material.color = color;
        transform.position = ProperRandomPosition();        
        boomFx.startColor = color;
        RandomizeForce();
        currentColor = color;
        outline.SetActive(false);
    }

    void Update()
    {
        rend.transform.LookAt(cam);
        outline.transform.LookAt(cam);

        if (Random.value < 0.001f)
            RandomizeForce();
    }

    void RandomizeForce()
    {
        cForce.relativeForce = Random.insideUnitSphere * maxForceFaktor;
    }
    Vector3 ProperRandomPosition()
    {
        Vector3 pos = Vector3.zero;
        float minRadius = Controller.instance.minDistance;

        while (pos.x > -minRadius && pos.x < minRadius && pos.z > -minRadius && pos.z < minRadius) // retry in it is inside min radius cylinder
            pos = Random.onUnitSphere* Random.Range(Controller.instance.minDistance, Controller.instance.maxDistance);

        return pos;
    }

    public void Pop()
    {
        SoundController.instance.PlayFx(popSound);
        rend.enabled = false;

        boomFx.transform.parent = null;
        boomFx.Play();

        GetComponent<SphereCollider>().enabled = false;
        Destroy(gameObject, 1.5f);

        outline.SetActive(false);
    }

    public void Over()
    {
        CheckTouchAction();
    }

    void CheckTouchAction()
    {
        if (Controller.instance.CheckBubbleColor(currentColor))
        {
            Pop();
            Controller.instance.ClickedProperBubble(gameObject);
        }
        else
        {
            SoundController.instance.PlayFx(wrongSound);
            Controller.instance.ClickedWrongBubble(gameObject);
        }
    }

    public void Clicked()
    {
        if (Controller.instance.round != 2)
        {
            if (Controller.instance.CheckBubbleColor(currentColor))
            {
                Pop();
                Controller.instance.ClickedProperBubble(gameObject);

            }
        }
        else  //grab bubble
        {
            Controller.instance.GrabBubble(gameObject);
            outline.SetActive(true);
        }

    }

    float lastCollisionTime = 0;
    private void OnCollisionEnter(Collision collision)
    {
        if (Time.time - lastCollisionTime > 1f)
        {
            Debug.Log("collision enter: " + collision.gameObject.layer.ToString("G"));
            if (collision.gameObject.layer.ToString("G") == "27")
                CheckTouchAction();

            lastCollisionTime = Time.time;
        }
    }

    void DestroyThis()
    {
        Pop();
        Controller.instance.ClickedProperBubble(gameObject);
    }

    void ScaleThis(float targetScale)
    {
        transform.localScale = transform.localScale * (targetScale + 0.1f);
    }
}
