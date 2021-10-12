using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZergRush;
using ZergRush.ReactiveCore;
using ZergRush.ReactiveUI;

public class DeviceCategory
{
    public string name;
    public ReactiveCollection<DeviceInfo> devices = new ReactiveCollection<DeviceInfo>();
}

public class DeviceListController : ConnectableMonoBehaviour
{
    public RectTransform devicesRect;
    DeviceCategory allDevices => categories[0];
    public ReactiveCollection<DeviceCategory> categories = new ReactiveCollection<DeviceCategory>();
    public Cell<DeviceCategory> selectedCategory = new Cell<DeviceCategory>();
    public Button createCat;
    public AddDeviceCategoryWindow addCatWindow;

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
                    itemView.Show(info);
                }, updater: d => d.updated);
            view.button.Subscribe(() => selectedCategory.value = category);
        });

        connections += createCat.Subscribe(() =>
        {
            var selectedDevices = new ReactiveCollection<DeviceInfo>();
            addCatWindow.DisconnectAll();
            addCatWindow.connections += addCatWindow.exitButton.Subscribe(() =>
            {
                addCatWindow.SetActiveSafe(false);
            });
            addCatWindow.connections += addCatWindow.createButton.Subscribe(() =>
            {
                AddCategory(addCatWindow.name.text, selectedDevices);
                addCatWindow.SetActiveSafe(false);
            });
            addCatWindow.connections += allDevices.devices.Present(addCatWindow.deviceContainer, PrefabRef<DeviceItemView>.Auto(), (info, view) =>
            {
                view.Show(info);
                view.click.Subscribe(() =>
                {
                    if (selectedDevices.Contains(info)) selectedDevices.Remove(info);
                    else selectedDevices.Add(info);
                });
                view.selectedBg.SetActive(selectedDevices.ContainsReactive(info));
            });
            addCatWindow.SetActiveSafe(true);
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