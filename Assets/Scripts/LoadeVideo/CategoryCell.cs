using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using ZergRush.ReactiveCore;
using ZergRush.ReactiveUI;

public class CategoryCell : ReusableView, IPointerClickHandler
{
    [SerializeField] private Text _name = default;

    public override bool autoDisableOnRecycle => true;

    public EventStream selected = new EventStream();

    public void SetName(string name)
    {
        _name.text = name;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        selected.Send();
        LoaderVideo._selectedCategory = this;
    }
}