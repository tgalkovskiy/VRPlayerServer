using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class DissolveOverTime : MonoBehaviour
{
    private MeshRenderer meshRenderer;

    public float speed = .5f;

    private bool isClicked = false;

    private void Start()
    {
        meshRenderer = this.GetComponent<MeshRenderer>();
    }

    private float t = 0.0f;
    private void Update()
    {
        if (isClicked)
        {
            Dissolve();
        }
    }

    public void Dissolve()
    {
        isClicked = true;
        Material[] mats = meshRenderer.materials;

        mats[0].SetFloat("_Cutoff", (t * speed));
        t += Time.deltaTime;

        // Unity does not allow meshRenderer.materials[0]...
        meshRenderer.materials = mats;
    }
}
