using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager : MonoBehaviour
{
    public GameObject[] Rays;


    public SkinnedMeshRenderer bowBody;
    public SkinnedMeshRenderer bowString;

    public GameObject bow;


    [Header("DON'T TOUCH")]
    public Transform [] parts;

    public MeshRenderer shaft;
    public MeshRenderer tip;

    public static int sceneNumber;

    public void Toggle()
    {
        if (sceneNumber != 0)
        {
            foreach (GameObject item in Rays)
            {
                item.gameObject.SetActive(false);
            }
        }
        else
        {
            foreach (GameObject item in Rays)
            {
                item.gameObject.SetActive(true);
            }
        }

        if (sceneNumber != 3)
        {
            bowBody.enabled = false;
            bowString.enabled = false;

            StartCoroutine(DelayActionToFalse(0.5f));
        }
        else
        {
            bowBody.enabled = true;
            bowString.enabled = true;

            StartCoroutine(DelayActionToTrue(0.5f));
        }

    }

    IEnumerator DelayActionToFalse(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        parts = bow.GetComponentsInChildren<Transform>();
        foreach (Transform item in parts)
        {
            if (item.name == "Shaft")
            {
                shaft = item.GetComponent<MeshRenderer>();
                shaft.enabled = false;
            }

            if (item.name == "Tip")
            {
                tip = item.GetComponent<MeshRenderer>();
                tip.enabled = false;
            }
        }
    }

    IEnumerator DelayActionToTrue(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);


        parts = bow.GetComponentsInChildren<Transform>();
        foreach (Transform item in parts)
        {
            if (item.name == "Shaft")
            {
                shaft = item.GetComponent<MeshRenderer>();
                shaft.enabled = true;
            }

            if (item.name == "Tip")
            {
                tip = item.GetComponent<MeshRenderer>();
                tip.enabled = true;
            }
        }
    }
}
