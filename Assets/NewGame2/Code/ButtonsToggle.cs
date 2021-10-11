using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsToggle : MonoBehaviour
{
    public GameObject [] buttons;
    public GameObject[] objectImages;


    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject item in buttons)
        {
            item.GetComponent<MeshRenderer>().enabled = false;
        }

        foreach (GameObject item in objectImages)
        {
            item.GetComponent<Canvas>().enabled = false;
        }
    }

    public void ToggleButtons()
    {
        foreach (GameObject item in buttons)
        {
            item.GetComponent<MeshRenderer>().enabled = true;
        }

        foreach (GameObject item in objectImages)
        {
            item.GetComponent<Canvas>().enabled = true;
        }
    }
}
