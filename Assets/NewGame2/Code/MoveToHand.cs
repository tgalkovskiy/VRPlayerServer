using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToHand : MonoBehaviour
{
    // Start is called before the first frame update
    void Update()
    {
        gameObject.transform.position = GameObject.FindGameObjectWithTag("RightHand").transform.position; 
    }
}
