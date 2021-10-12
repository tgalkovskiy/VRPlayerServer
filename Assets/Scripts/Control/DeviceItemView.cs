using UnityEngine.UI;
using ZergRush.ReactiveUI;

public class DeviceItemView : ReusableView
{
    public Text batteryLevel;
    public Text deviceName;
    public Image batteryFull;
    public Image selectedBg;
    public Button click;


    public override bool autoDisableOnRecycle => true;

    public void Show(DeviceInfo info)
    {
        batteryLevel.text = info.battery + "%";
        batteryFull.fillAmount = info.battery / 100f;
        deviceName.text = info.name;
    }
}