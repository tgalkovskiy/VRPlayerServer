using System.Collections.Generic;
using UnityEngine;
using ZergRush;
using ZergRush.ReactiveCore;
using ZergRush.ReactiveUI;

public class DeviceCategory
{
    public string name;
    public ReactiveCollection<DeviceInfo> devices;
}

public class DeviceListController : ConnectableMonoBehaviour
{
    public RectTransform devicesRect;
    DeviceCategory allDevices => categories[0];
    public ReactiveCollection<DeviceCategory> categories = new ReactiveCollection<DeviceCategory>();
    public Cell<DeviceCategory> selectedCategory = new Cell<DeviceCategory>();

    void Awake()
    {
        categories.Add(new DeviceCategory {name = "All devices"});
        connections += categories.Present(devicesRect, PrefabRef<DeviceCategoryView>.Auto(), (category, view) =>
        {
            view.connections +=
                view.name.SetTextContent(category.devices.CountCell().Map(count => $"{category.name} ({count})"));
            view.connections += category.devices.Present(view.deviceContainer, PrefabRef<DeviceItemView>.Auto(),
                (info, itemView) =>
                {
                    itemView.batteryLevel.text = info.battery + "%";
                    itemView.deviceName.text = info.name;
                }, updater: d => d.updated);
            view.button.Subscribe(() => selectedCategory.value = category);
        });
    }

    public void AddCategory(string name, IEnumerable<DeviceInfo> devices)
    {
        var deviceCategory = new DeviceCategory {name = name};
        deviceCategory.devices.AddRange(devices);
        categories.Add(deviceCategory);
    }

    public void DeviceDisconnected(int connectionId)
    {
        foreach (var deviceCategory in categories)
        {
            deviceCategory.devices.RemoveAll(d => d.connectionId == connectionId);
        }
    }

    public void DeviceInfoReceived(DeviceInfo info)
    {
        foreach (var deviceCategory in categories)
        {
            var existentDevice = deviceCategory.devices.Find(d => d.connectionId == info.connectionId);
            if (existentDevice != null)
            {
                existentDevice.UpdateFrom(info);
                existentDevice.updated.Send();
            }
            else
            {
                allDevices.devices.Add(info);
            }
        }
    }
}