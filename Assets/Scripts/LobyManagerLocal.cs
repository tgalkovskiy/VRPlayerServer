using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class LobyManagerLocal : MonoBehaviour
{
    [SerializeField] private MenuBehavior _menuBehavior = default;
    [SerializeField] private bool isServer;
    public string remoteAddress; // host to send data
    public int remotePort; // port to send data
    public int localPort; // port to listen to messages 
    public static LobyManagerLocal Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void OfflineStart()
    {
        if(isServer)
        {
            _menuBehavior.ShowControlMenu();
        }
        else
        {
            _menuBehavior.UnShowControllMenu();
        }
        Thread receiveThread = new Thread(new ThreadStart(ReceiveMessage));
        receiveThread.Start();
    }
    private void SendMessage(string command)
    {
        UdpClient sender = new UdpClient(); // создаем UdpClient для отправки сообщений
        byte[] data = Encoding.Unicode.GetBytes(command);
        sender.Send(data, data.Length, remoteAddress, remotePort); // отправка
        sender.Close();
    }
    private void ReceiveMessage()
    {
        while (true)
        {
          UdpClient receiver = new UdpClient(localPort); // UdpClient для получения данных
          IPEndPoint remoteIp = null; // адрес входящего подключения
          byte[] data = receiver.Receive(ref remoteIp); // получаем данные
          string message = Encoding.Unicode.GetString(data);
          receiver.Close();
          _menuBehavior.ControllVideo(message);  
        }
    }

    public void CommandPlay()
    {
        _menuBehavior.ControllVideo("Play");
        SendMessage("Play");
        
    }
    public void CommandStop()
    {
        _menuBehavior.ControllVideo("Stop");
        SendMessage("Stop");
    }
    public void CommandMuteAudio()
    {
        _menuBehavior.ControllVideo("Mute");
        SendMessage("Mute");
    }
    public void CommandRebootVideo()
    {
        _menuBehavior.ControllVideo("Reboot");
        SendMessage("Reboot");
    }
    public void CommandVideo(int index)
    {
        _menuBehavior.ChooseVideo(index);
        SendMessage("Reboot");
    }
    
    
}
