using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SettingCell : MonoBehaviour
{
    [SerializeField] private Button _changeContent = default;
    [SerializeField] private Button _delete = default;
    
    private void Awake()
    {
        _changeContent.onClick.AddListener(WindowControll.Instance.OpenWindowChangeContent);
        _delete.onClick.AddListener(ServerController.Instance.videoLoader.DeleteCell);
    }
}
