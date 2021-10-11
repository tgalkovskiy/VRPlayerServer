using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToSides : MonoBehaviour
{
    [Range(0f, 4f)] 
    public float lerpTime;

    [SerializeField] 
    private GameObject startPositions, endPosition;

    float t = 0f;
    
    public bool isActive;

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            Move();
        }
    }


    public void Move()
    {
        t += Time.deltaTime;
        if (transform.parent.name == "Start")
        {
            this.transform.position = Vector3.Lerp(startPositions.transform.position, endPosition.transform.position, lerpTime * t);
        }
        else
        {
            this.transform.position = Vector3.Lerp(endPosition.transform.position, startPositions.transform.position, lerpTime * t);

        }
    }
}
