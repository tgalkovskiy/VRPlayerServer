using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetShip : MonoBehaviour
{
    public ParticleSystem conffeti;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.tag == "Tip")
        {
            conffeti.gameObject.transform.position = this.gameObject.transform.position;
            conffeti.Play();

            gameObject.GetComponent<AudioSource>().Play();
            TargetScore.hitCount++;
            gameObject.GetComponent<MoveToSides>().lerpTime = 4;
        }
    }
}
