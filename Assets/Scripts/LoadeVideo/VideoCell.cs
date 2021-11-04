using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;
using ZergRush.ReactiveCore;
using ZergRush.ReactiveUI;
using System.IO;

public class LibraryItemView : ReusableView, IPointerClickHandler
{ 
    public Image selectedImage;
   public Cell<bool> toggle = new Cell<bool>();
   public GameObject settingCell;
   public EventStream selectedLeftMouse = new EventStream();
   public EventStream selectedRigthMouse = new EventStream();
   
   public void OnToggleValue(bool value)
   {
       toggle.value = value;
   }

   public void OnPointerClick(PointerEventData eventData)
   {
       if (eventData.button == PointerEventData.InputButton.Left)
       {
           selectedLeftMouse.Send();
       }
       if (eventData.button == PointerEventData.InputButton.Right)
       {
           selectedRigthMouse.Send();
           settingCell.SetActive(true);
       }
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
   [SerializeField]private GameObject _settingCell = default;
   [SerializeField]private GameObject _iconDescription = default;
   [SerializeField]private Text _descriptionText;
   [SerializeField]private DateTime duration = default;
   public string nameVideo;
   
   public override bool autoDisableOnRecycle => true;

   public void SetParametersCell(string name, string _description, string extImage)
   {
        //_image.sprite = preview;
        this._name.text = name;
        nameVideo = name;
        if (_descriptionText != null)
            this._descriptionText.text = _description;
        if (!extImage.IsNullOrEmpty())
        {
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(File.ReadAllBytes(Path.Combine(Application.persistentDataPath, extImage)));
            Sprite NewSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height),new Vector2(0,0));
            _image.sprite = NewSprite;
        }
    }

   public void OnPointerEnter(PointerEventData eventData)
   {
       _iconDescription.SetActive(true);
   }

   public void OnPointerExit(PointerEventData eventData)
   {
       _iconDescription.SetActive(false);
      settingCell.SetActive(false);
   }
}
