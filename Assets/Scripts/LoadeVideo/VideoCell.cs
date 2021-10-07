using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using ZergRush.ReactiveCore;
using ZergRush.ReactiveUI;

public class VideoCell : ReusableView
{
   [SerializeField]private Image _image = default;
   [SerializeField]private Text _name;
   [SerializeField]private Text _descriptionText;
   [SerializeField]private DateTime duration = default;

   public Cell<bool> toggle = new Cell<bool>();
   public EventStream selected = new EventStream();

   public void SetParametersCell(Sprite preview, string name, string _description)
    {
        _image.sprite = preview;
        this._name.text = name;
        if (_descriptionText != null)
            this._descriptionText.text = _description;
    }

   public void OnPress()
   {
       selected.Send();
   }

   public void OnToggleValue(bool value)
   {
       toggle.value = value;
   }
}
