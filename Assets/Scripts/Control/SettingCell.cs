using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingCell : MonoBehaviour
{
    [SerializeField] private Button _rename = default;
    [SerializeField] private Button _description = default;
    [SerializeField] private Button _image = default;
    [SerializeField] private Button _soundTrack = default;
    [SerializeField] private Button _subs = default;

    private void Awake()
    {
        _rename.onClick.AddListener(WindowControll.Instance.OpenWindowChangeContent);
    }
}
