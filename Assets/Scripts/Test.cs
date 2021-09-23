using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    private FMNetworkManager _fmNetworkManager;
    public Text _text;
    private byte[] a;

    private void Awake()
    {
        _fmNetworkManager = GetComponent<FMNetworkManager>();
    }

    void Start()
    {
       
    }

    public void Send()
    {
         a = new byte[10];
                a[1] = 10;
                if (FMNetworkManager.instance.Server)
                {
                    FMNetworkManager.instance.SendToOthers(a);
                    _text.text = a[1].ToString();
                }
    }
    public void getByte(byte[] a)
    {
        Debug.Log(a);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
