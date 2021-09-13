using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VideoCell : MonoBehaviour
{
   [SerializeField]private Image _image = default;
   [SerializeField]private int number = default;
   [SerializeField]private string discription = default;
   
    public void SetParamertsCell(Sprite sprite, int number, string discription)
    {
        _image.sprite = sprite;
        this.number = number;
        this.discription = discription;
    }
    public void SendNumberVideo()
    {
        LobyManager.Instance.CommandVideo(number);
    }
}
