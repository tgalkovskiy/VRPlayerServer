using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;
using ZergRush.ReactiveCore;
using ZergRush.ReactiveUI;

public class VideoCell : ReusableView, IPointerClickHandler
{
   [SerializeField]private Image _image = default;
   [SerializeField]private Text _name;
   [SerializeField]private Text _descriptionText;
   [SerializeField]private DateTime duration = default;
   public string nameVideo;
   public Cell<bool> toggle = new Cell<bool>();
   public EventStream selected = new EventStream();

   public override bool autoDisableOnRecycle => true;

   public void SetParametersCell(Sprite preview, string name, string _description)
    {
        _image.sprite = preview;
        this._name.text = name;
        nameVideo = $"{name}.mp4";
        if (_descriptionText != null)
            this._descriptionText.text = _description;
    }

   public void OnToggleValue(bool value)
   {
       toggle.value = value;
   }

   public void OnPointerClick(PointerEventData eventData)
   {
       selected.Send();
       LoaderVideo._selectedVideo = this;
      
   }
}
