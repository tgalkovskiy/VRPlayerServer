using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionChange : MonoBehaviour
{
    public GameObject DistantLocation;
    public void ChangePos()
    {
        StartCoroutine(DelayDo(1.0f));
    }

    IEnumerator DelayDo(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        transform.position = DistantLocation.transform.position;
    }
}
