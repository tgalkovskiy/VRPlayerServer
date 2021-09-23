using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseTypeConnection : MonoBehaviour
{
    [SerializeField] private GameObject _buttonOnline = default;
    [SerializeField] private GameObject _buttonOffline = default;
    [SerializeField] private GameObject _buttonApply = default;
    [SerializeField] private InputField _ipField = default;
    private LobyManagerNetWork _netWork;
    private LobyManagerLocal _managerLocal;
    public static bool OnLine;
    private void Awake()
    {
        _netWork = GetComponent<LobyManagerNetWork>();
        _managerLocal = GetComponent<LobyManagerLocal>();
    }

    public void OnlineConnection()
    {
        _netWork.enabled = true;
        _managerLocal.enabled = false;
        _buttonOnline.SetActive(false);
        _buttonOffline.SetActive(false);
        OnLine = true;
    }
    public void OfflineConnection()
    {
        _netWork.enabled = false;
        _managerLocal.enabled = true;
        _buttonOnline.SetActive(false);
        _buttonOffline.SetActive(false);
        _ipField.gameObject.SetActive(true);
        _buttonApply.SetActive(true);
        OnLine = false;
    }
    public void ApplyIp()
    {
        _buttonApply.SetActive(false);
        _managerLocal.remoteAddress = _ipField.text;
        _ipField.gameObject.SetActive(false);
        _managerLocal.OfflineStart();
    }
}
