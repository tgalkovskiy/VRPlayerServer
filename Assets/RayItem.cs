using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayItem : MonoBehaviour
{
    public LineRenderer ray;
    public CameraPointer camPointer;
    public float defaultLength = 1f;

    float currentLength = 1f;
    float targetLength = 1f;

    // Start is called before the first frame update
    void Start()
    {
        if (ray == null)
            ray = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ray != null)
        {
            ray.SetPosition(0, Vector3.zero); // camPointer.transform.position);

            if (camPointer.m_GazedAtObject != null)
                targetLength = camPointer.distance;
            else
                targetLength = defaultLength;

            currentLength = Mathf.Lerp(currentLength, targetLength, Time.deltaTime * 2f);

            ray.SetPosition(1, Vector3.forward * currentLength); // camPointer.transform.forward * currentLength);
        }
    }
}
