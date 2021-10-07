using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using UnityEngine;
using UnityEngine.UI;

public class CategoryCell : MonoBehaviour
{
    [SerializeField] private Text _name = default;

    public void SetName(string name)
    {
        _name.text = name;
    }
}
