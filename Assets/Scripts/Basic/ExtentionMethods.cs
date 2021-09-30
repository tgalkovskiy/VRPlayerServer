using UnityEngine;
using System.Collections;

public static class ExtensionMethods
{
    
    public static void Reset(this Transform trans)
    {
        trans.position = Vector3.zero;
        trans.localRotation = Quaternion.identity;
        trans.localScale = new Vector3(1, 1, 1);
    }

    public static void Clear(this Transform trans)
    {
        foreach (Transform t in trans)
            UnityEngine.MonoBehaviour.Destroy(t.gameObject);
    }

    public static void HideAll(this Transform trans)
    {
        foreach (Transform t in trans)
            t.gameObject.SetActive(false);
    }
}