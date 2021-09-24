using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseTypeConnection : MonoBehaviour
{
    [SerializeField] private GameObject _buttonOnline = default;
    [SerializeField] private GameObject _buttonOffline = default;
    public GameObject _netWork;
    public GameObject _managerLocal;
    public static bool OnLine;
    public void OnlineConnection()
    {
        _netWork.gameObject.SetActive(true);
        _buttonOnline.SetActive(false);
        _buttonOffline.SetActive(false);
        OnLine = true;
    }
    public void OfflineConnection()
    {
        _managerLocal.gameObject.SetActive(true);
        _buttonOnline.SetActive(false);
        _buttonOffline.SetActive(false);
        //_managerLocal.OfflineStart();
        OnLine = false;
    }
}
