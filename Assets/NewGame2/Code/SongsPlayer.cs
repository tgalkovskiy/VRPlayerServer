using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class SongsPlayer : MonoBehaviour
{
    public AudioClip [] songs;

    public RawImage UiPlay;
    public Texture2D playImage;
    public Texture2D pauseImage;

    private int _currentTrack;
    private AudioSource _source;

    // Start is called before the first frame update
    void Start()
    {
        _source = gameObject.GetComponent<AudioSource>();
        PlayMusic();
    }

    public void PlayToggle()
    {
        if (_source.isPlaying)
        {
            StopMusic();
        }
        else
        {
            PlayMusic();
        }
    }

    public void PlayMusic()
    {
        if (_source.isPlaying)
        {
            return;
        }

        UiPlay.texture = pauseImage;
        _currentTrack--;
        if (_currentTrack < 0)
        {
            _currentTrack = songs.Length - 1;
        }
        StartCoroutine("WaitForMusicEnd");
    }

    IEnumerator WaitForMusicEnd()
    {
        while (_source.isPlaying)
        {
            yield return null;
        }
        NextTitle();
    }

    public void NextTitle()
    {
        UiPlay.texture = pauseImage;
        _source.Stop();
        _currentTrack++;
        if (_currentTrack > songs.Length - 1)
        {
            _currentTrack = 0;
        }
        _source.clip = songs[_currentTrack];
        _source.Play();

        StartCoroutine("WaitForMusicEnd");
    }

    public void PreviousTitle()
    {
        UiPlay.texture = pauseImage;
        _source.Stop();
        _currentTrack--;
        if (_currentTrack < 0)
        {
            _currentTrack = songs.Length - 1;
        }
        _source.clip = songs[_currentTrack];
        _source.Play();

        StartCoroutine("WaitForMusicEnd");
    }

    public void StopMusic()
    {
        UiPlay.texture = playImage;
        _source.Stop();
        StopCoroutine("WaitForMusicEnd");
    }

    public void Mute()
    {
        _source.mute = !_source.mute;
    }
}
