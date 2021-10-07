using RenderHeads.Media.AVProVideo;
using UnityEngine;
using ZergRush.CodeGen;
using ZergRush.ReactiveCore;

[GenTask(GenTaskFlags.UpdateFrom)]
public partial class ClientState : NetworkCommand, ISerializable
{
    public Cell<VideoItem> playingItem;
    public Cell<bool> playing;
    public Cell<float> time;

    public void BindToPlayer(MediaPlayer _mediaPlayer)
    {
         playingItem.Bind(v =>
         {
             if (v == null) _mediaPlayer.CloseMedia();
             if (v != null) _mediaPlayer.OpenMedia(new MediaPath(v.filePath, MediaPathType.AbsolutePathOrURL), false);
         });
         playing.Bind(playing =>
         {
             if (playing) _mediaPlayer.Play();
             else _mediaPlayer.Stop();
         });
         time.ListenUpdates(time =>
         {
             Debug.Log($"todo: seek function here {time}");
         });
    }

    public IEventStream updated => playingItem.updates.MergeWith(playing.updates, time.updates);
}