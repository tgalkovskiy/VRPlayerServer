using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using RenderHeads.Media.AVProVideo;

public class PlayerNetBehavior : MonoBehaviour
{
   [SerializeField] private GameObject _controlCanvasPanel = default;
   [SerializeField] private MediaPlayer _mediaPlayer = default;
   public string[] path;
   
   public void GetCommand(string command)
   {
        if(command == "Play")
        {
            _mediaPlayer.Play();
        }
        if(command == "Stop")
        {
            _mediaPlayer.Stop();
        }
        if(command == "UnShow")
        {
            _controlCanvasPanel.SetActive(false);
        }
   }
}
