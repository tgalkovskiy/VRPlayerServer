using System.Collections;
using System.IO;
using RenderHeads.Media.AVProVideo;
using UnityEngine;
using ZergRush;
using ZergRush.CodeGen;
using ZergRush.ReactiveCore;

[GenTask(GenTaskFlags.UpdateFrom)]
public partial class ClientState : NetworkCommand, ISerializable
{
    [CanBeNull]
    public Cell<VideoItem> playingItem;
    public Cell<bool> playing;
    public Cell<float> time;
    public Cell<float> volume;
    public Cell<bool> mute;

    [GenIgnore]
    public Connections conns = new Connections();
    public void BindToPlayer(MediaPlayer _mediaPlayer)
    {
        conns.DisconnectAll();
         conns += playingItem.Bind(v =>
         {
             Debug.Log("Bind");
             if (v == null) _mediaPlayer.CloseMedia();
             if (v != null)
             {
                 var pathVideo = LoaderVideo.GetFillVideoPath(v.fileName);
                 if (File.Exists(pathVideo))
                 {
                     _mediaPlayer.OpenMedia(MediaPathType.AbsolutePathOrURL, pathVideo, false);
                 }
                 if (v.subtitlesFileName.IsNullOrEmpty() == false)
                 {
                      var pathSub = LoaderVideo.GetFillVideoPath(v.subtitlesFileName);
                      _mediaPlayer.EnableSubtitles(new MediaPath(pathSub, MediaPathType.AbsolutePathOrURL));
                 }
                 if (v.soundFilename.IsNullOrEmpty() == false)
                 {
                     Debug.Log("AUDIO!");
                     //AudioLoad(Path.Combine(Application.persistentDataPath, $"{v.soundFilename}.mp3"), _mediaPlayer);
                 }
             }
         });
         conns += playing.Bind(playing =>
         {
             if (playing) _mediaPlayer.Play();
             else _mediaPlayer.Stop();
         });
         conns += volume.Bind(v =>
         {
             _mediaPlayer.AudioVolume = v;
         });
         conns += mute.Bind(v =>
         {
             _mediaPlayer.AudioMuted = v;
         });
         conns += time.ListenUpdates(time =>
         {
             Debug.Log($"todo: seek function here {time}");
         });
    }

    public IEventStream updated => playingItem.updates.MergeWith(playing.updates, time.updates);

    private IEnumerator AudioLoad(string path, MediaPlayer _mediaPlayer)
    {
        Debug.Log("AudioLoad");
        Debug.Log(path);
        WWW load = new WWW(path);
        yield return load;
        AudioClip clip = load.GetAudioClip(false, false);
        _mediaPlayer.AudioSource.clip = clip;
    }
}