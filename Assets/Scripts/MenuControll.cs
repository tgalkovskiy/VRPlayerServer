using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuControll : MonoBehaviour
{
    [SerializeField] private GameObject _chooseVideoPanel = default;
    [SerializeField] private GameObject _chooseExpPanel = default;
    [SerializeField] private GameObject _chooseFavorit = default;
    [SerializeField] private GameObject _chooseEvent = default;

    public void ShowPanels(GameObject panel)
    {
        _chooseVideoPanel.SetActive(false);
        _chooseExpPanel.SetActive(false);
        _chooseFavorit.SetActive(false);
        _chooseEvent.SetActive(false);
        panel.SetActive(true);
    }
}
