using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace StackingNotifications
{
    public class NotificationHandler : MonoBehaviour
    {
        private static NotificationHandler instance;
        internal static GameObject NotificationLayer;
        private List<GameObject> notifications = new List<GameObject>();
        private readonly Queue<GameObject> incomingNotificationQueue = new Queue<GameObject>();

        private GameObject currentNotification;

        static NotificationHandler()
        {
        }

        public static NotificationHandler Instance
        {
            get
            {
                if (!instance) instance = new NotificationHandler();
                return instance;
            }
        }

        public void Remove(GameObject notification)
        {
            notifications.Remove(notification);
        }

        public void Awake()
        {
            instance = this;
        }

        public void Start()
        {
            Jotunn.Logger.LogDebug($"Start called on NotificationHandler.");
            NotificationLayer = GameObject.Instantiate(ModAssets.Instance.NotificationLayer, this.transform);
            NotificationLayer.SetActive(true);
            StartCoroutine("ProcessQueue");
        }

        public void AddNotification(string text, float duration = 3f, bool playSoundEffect = false)
        {
            var newNotification = GameObject.Instantiate(ModAssets.Instance.SkillUp, NotificationLayer.transform);
            var notifyComponent = newNotification.AddComponent<Notification>();

            newNotification.GetComponentInChildren<Text>().text = text;
            notifyComponent.timeLeft = duration;
            notifyComponent.PlaySoundEffect = playSoundEffect;
            newNotification.SetActive(false);
            incomingNotificationQueue.Enqueue(newNotification);
            Jotunn.Logger.LogDebug($"Queued message \"{text}\" for processing.");
        }


        public void Update()
        {
#if DEBUG
            if (Input.GetKeyDown(KeyCode.Keypad8)) NotificationHandler.Instance.AddNotification("TestNotification");
#endif
        }

        private IEnumerator ProcessQueue()
        {
            while (true)
            {
                //Jotunn.Logger.LogDebug($"Tick");
                if (incomingNotificationQueue.Count > 0)
                {
                    //Jotunn.Logger.LogDebug($"QueueCount: {incomingNotificationQueue.Count}");
                    //Jotunn.Logger.LogDebug($"Moving?: {currentNotification?.GetComponent<Notification>().isMoving()}");
                    if (!currentNotification) currentNotification = incomingNotificationQueue.Peek();
                    //Jotunn.Logger.LogDebug($"Moving?: {currentNotification.GetComponent<Notification>().isMoving()}");
                    var notComponent = currentNotification.GetComponent<Notification>();
                    if (!notComponent.IsMoving())
                    {
                        //Jotunn.Logger.LogDebug($"Not moving.");
                        var incNot = incomingNotificationQueue.Dequeue();
                        incNot.SetActive(true);
                        incNot.gameObject.transform.localPosition = new Vector3(-notComponent.size.x, -notComponent.size.y, 0); //Starting pos
                        notifications.Add(incNot);
                        currentNotification = incNot;
                        //Jotunn.Logger.LogDebug($"Move all up.");
                        foreach (var n in notifications)
                        {
                            if (n && n.GetComponent<RectTransform>() && n.GetComponent<Notification>()) n.GetComponent<Notification>().MoveUp();
                        }
                    }
                    //else yield return new WaitForSeconds(1f);
                }
                yield return new WaitForSeconds(0.2f);
            }
        }
    }
}
