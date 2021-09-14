using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuControll : MonoBehaviour
{
    [SerializeField] private GameObject _chooseVideoPanel = default;
    [SerializeField] private GameObject _chooseExpPanel = default;
    [SerializeField] private GameObject _chooseFavorit = default;
    [SerializeField] private GameObject _chooseEvent = default;
    [SerializeField] private Image _videoIcon = default;
    [SerializeField] private Text _videoDescription = default;
    public static MenuControll Instance;
    private void Awake()
    {
        Instance = this;
    }
    public void ShowPanels(GameObject panel)
    {
        _chooseVideoPanel.SetActive(false);
        _chooseExpPanel.SetActive(false);
        _chooseFavorit.SetActive(false);
        _chooseEvent.SetActive(false);
        panel.SetActive(true);
    }
    public void ChangeVideoPanel(Sprite sprite, string description)
    {
        _videoIcon.sprite = sprite;
        _videoDescription.text = description;
    }
}
