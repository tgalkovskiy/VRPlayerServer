using RenderHeads.Media.AVProVideo;
using UnityEngine;
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

    public void BindToPlayer(MediaPlayer _mediaPlayer)
    {
         playingItem.Bind(v =>
         {
             if (v == null) _mediaPlayer.CloseMedia();
             if (v != null)
             {
                 _mediaPlayer.OpenMedia(new MediaPath(v.filePath, MediaPathType.AbsolutePathOrURL), false);
                 if (v.subtitlesFileName.IsNullOrEmpty() == false)
                     _mediaPlayer.EnableSubtitles(new MediaPath(v.subFilePath, MediaPathType.AbsolutePathOrURL));
             }
         });
         playing.Bind(playing =>
         {
             if (playing) _mediaPlayer.Play();
             else _mediaPlayer.Stop();
         });
         volume.Bind(v =>
         {
             _mediaPlayer.AudioVolume = v;
         });
         time.ListenUpdates(time =>
         {
             Debug.Log($"todo: seek function here {time}");
         });
    }

    public IEventStream updated => playingItem.updates.MergeWith(playing.updates, time.updates);
}