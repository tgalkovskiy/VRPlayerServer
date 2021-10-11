using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fracture : MonoBehaviour
{
    public int maxHit;
    public GameObject window;
    public GameObject fractured;

    public AudioSource hitSource;
    public AudioSource shutterSource;

    public float timeToDestroy;

    private int hitCount;
    private bool isBroken = false;
    [ContextMenu("Do it!!!")]
    public void BreakGlass()
    {
        window.SetActive(false);
        gameObject.GetComponent<Rigidbody>().useGravity = true;
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        fractured.SetActive(true);
        if (isBroken == false)
        {
            TargetScore.hitCount += 2;
            isBroken = true;
        }
        StartCoroutine(Die(timeToDestroy));
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.tag == "RightHand")
        {
            hitCount++;
            if (hitCount >= maxHit)
            {
                shutterSource.Play();
                BreakGlass();
            }
            else
            {
                hitSource.Play();
            }
        }
    }

    IEnumerator Die(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        Destroy(gameObject);
    }
}
