using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using ZergRush.ReactiveCore;
using ZergRush.ReactiveUI;

public class CategoryCell : LibraryItemView, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Text _name = default;
    [SerializeField] private GameObject _descriptionPanel = default;
    [SerializeField] private string _description = default;
    [SerializeField] private Text _descriptionText = default;
    [SerializeField] private Image _image = default;

    public override bool autoDisableOnRecycle => true;

    public void SetParameters(string name)
    {
        _name.text = name;
    }
    public void SetParameters(string name, string extImage, string description)
    {
        _name.text = name;
        if (!description.IsNullOrEmpty())
        {
            _description = description;
            _descriptionText.text = _description;
        }
        if (!extImage.IsNullOrEmpty())
        {
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(File.ReadAllBytes(Path.Combine(Application.persistentDataPath, $"{name}{extImage}")));
            Sprite NewSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height),new Vector2(0,0));
            _image.sprite = NewSprite;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _descriptionPanel.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        settingCell.SetActive(false);
        _descriptionPanel.SetActive(false);
    }
}