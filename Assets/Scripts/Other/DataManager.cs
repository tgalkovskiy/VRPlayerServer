using System;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class DataManager : MonoBehaviour
{
    public Slider _progressSlider;
    public LobbyManagerLocal _lobbyManagerLocal;
    private LoaderVideo _loaderVideo;
    public static DataManager Instance;
    private void Awake()
    {
        Instance = this;
        _loaderVideo = GetComponent<LoaderVideo>();
    }
    
    public async void SendDataFile(string _pathFile, string _name)
    {
        
        IProgress<int> _progress = new Progress<int>(i => _progressSlider.value = i);
        _progressSlider.gameObject.SetActive(true);
        for (int i = 0; i <= 30; i++)
        {
            await Task.Run(() =>
            {
                byte[] massByteToFile = File.ReadAllBytes(_pathFile);
                _lobbyManagerLocal.SendData(massByteToFile, ".mp4", _name);
            }); 
            Debug.Log("Progress : " + i);
            _progress.Report(i);
        }
        _progressSlider.gameObject.SetActive(false);
    }
    public async void GetDataFile(byte[] data, string format, string name)
    {
        IProgress<int> _progress = new Progress<int>(i => _progressSlider.value = i);
        _progressSlider.gameObject.SetActive(true);
        for (int i = 0; i <= 100; i++)
        {
            await Task.Run(() =>
            {
                File.WriteAllBytes(Path.Combine(Application.persistentDataPath, $"{name}.mp4"), data);
                File.WriteAllText(Path.Combine(Application.persistentDataPath, "ListVideo.Json"), name);
            }); 
            Debug.Log("Progress : " + i);
            _progress.Report(i);
        }
    }
}
