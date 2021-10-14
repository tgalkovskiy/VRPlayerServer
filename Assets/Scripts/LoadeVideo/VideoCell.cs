using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;
using ZergRush.ReactiveCore;
using ZergRush.ReactiveUI;

public class LibraryItemView : ReusableView, IPointerClickHandler
{ 
    public Image selectedImage;
   public Cell<bool> toggle = new Cell<bool>();
   public EventStream selected = new EventStream();

   public void OnToggleValue(bool value)
   {
       toggle.value = value;
   }

   public void OnPointerClick(PointerEventData eventData)
   {
       selected.Send();
   }

   public void SetSelection(bool selected)
   {
       selectedImage.SetActiveSafe(selected);
   }
}

public class VideoCell : LibraryItemView, IPointerEnterHandler, IPointerExitHandler
{
   [SerializeField]private Image _image = default;
   [SerializeField]private Text _name;
   [SerializeField] private GameObject _iconDescription = default;
   [SerializeField]private Text _descriptionText;
   [SerializeField]private DateTime duration = default;
   public string nameVideo;

   public override bool autoDisableOnRecycle => true;

   public void SetParametersCell(Sprite preview, string name, string _description)
    {
        _image.sprite = preview;
        this._name.text = name;
        nameVideo = $"{name}.mp4";
        if (_descriptionText != null)
            this._descriptionText.text = _description;
    }

   public void OnPointerEnter(PointerEventData eventData)
   {
       _iconDescription.SetActive(true);
   }

   public void OnPointerExit(PointerEventData eventData)
   {
       _iconDescription.SetActive(false);
   }
}
