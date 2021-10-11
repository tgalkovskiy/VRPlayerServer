using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassInsta : MonoBehaviour
{
    public GameObject glassWall;
    public GameObject pivot;
    public float showRate;
    private float time = 0.0f;
    private bool isActive;

    [Header("Debug values")]
    public GameObject glass;

    private void Start()
    {
        isActive = false;
        StartCoroutine(DelaySpawn(6.0f));
    }

    void Update()
    {
        if (isActive)
        {
            time += Time.deltaTime;

            if (time >= showRate)
            {
                time = 0.0f;

                // execute block of code here
                if (glass == null)
                {
                    CreatGlass();
                }
            }
        }
    }


    public void CreatGlass()
    {
        Instantiate(glassWall, pivot.transform.position, Quaternion.identity);
        glass = GameObject.FindGameObjectWithTag("GlassWall");
    }

    IEnumerator DelaySpawn(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        if (!isActive)
        {
            isActive = true;
        }
        CreatGlass();
    }
}
