using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Notifications.Android;
using UnityEngine.Android;

public class NotificationManager : MonoBehaviour
{

    [SerializeField] AndroidNotifications androidNotifications;


    private void Start()
    {
        androidNotifications.RequestAuthorization();
        androidNotifications.RegisterNotificationChannel();
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus == false)
        {
            AndroidNotificationCenter.CancelAllNotifications();
            androidNotifications.SendNotification("Come Play!", "Your regulars are waiting for you!", 10);
            androidNotifications.SendNotification("Come Play!", "The shop has reset!", 20);
        }
    }
}
