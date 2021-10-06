using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class VideoCell : MonoBehaviour
{
   [SerializeField]private Image _image = default;
   [SerializeField]private Text _descriptionText;
   [SerializeField]private int number = default;
   [SerializeField]private string _description = default;
   [SerializeField]private DateTime duration = default;
   private CommandControl _command;
   private void Start()
   {
       _command = CommandControl.Instance;
   }

   public void SetParametrsCell(Sprite sprite, int number, string _description)
    {
        _image.sprite = sprite;
        this.number = number;
        this._description = _description;
        this._descriptionText.text = _description;
    }
    public void SendNumberVideo()
    {
        WindowControll.Instance.ChangeVideoPanel(_image.sprite, _description);
        _command.CommandVideo(_description);
    }
}
