using System;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class DataManager : MonoBehaviour
{
    public Slider _progressSlider;
    [FormerlySerializedAs("_lobbyManagerLocal")] public ServerController _serverMenu;
    public static DataManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    
    public async void SendDataFile(string _pathFile, string _name)
    {
        IProgress<int> _progress = new Progress<int>(i => _progressSlider.value = i);
        _progressSlider.gameObject.SetActive(true);
        
        await Task.Run(() =>
        {
            byte[] massByteToFile = File.ReadAllBytes(_pathFile);
            _serverMenu.SendData(massByteToFile, ".mp4", _name);
        }); 
        
        for (int i = 0; i <= 30; i++)
        {
            Debug.Log("Progress : " + i);
            _progress.Report(i);
            await Task.Delay(100);
        }
        _progressSlider.gameObject.SetActive(false);
    }
    public async void SaveDataFile(byte[] data, string format, string name)
    {
        await Task.Run(() =>
        {
            File.WriteAllBytes(Path.Combine(Application.persistentDataPath, $"{name}.{format}"), data);
        }); 
    }
}
