using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using ZergRush.ReactiveUI;

public class VideoCell : ReusableView
{
   [SerializeField]private Image _image = default;
   [SerializeField]private Text _name;
   [SerializeField]private Text _descriptionText;
   [SerializeField]private DateTime duration = default;

   public void SetParamertsCell(Sprite preview, string name, string _description)
    {
        _image.sprite = preview;
        this._descriptionText.text = _description;
        this._name.text = name;
    }
}
