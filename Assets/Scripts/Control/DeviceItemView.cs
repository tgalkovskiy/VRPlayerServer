using UnityEngine;
using UnityEngine.UI;
using ZergRush.ReactiveUI;

public class DeviceItemView : ReusableView
{
    public Text batteryLevel;
    public Text deviceName;
    public Image batteryFull;
    public Image selectedBg;
    public Button click;
    public Image syncIcon;

    public override bool autoDisableOnRecycle => true;

    public void Show(DeviceInfo info)
    {
        batteryLevel.text = info.battery + "%";
        batteryFull.fillAmount = info.battery / 100f;
        deviceName.text = info.name;
        syncIcon.SetActiveSafe(info.syncInProcess);
        deviceName.color = info.disconnected ? new Color(0.68f, 0f, 0f) : Color.white;
    }
}