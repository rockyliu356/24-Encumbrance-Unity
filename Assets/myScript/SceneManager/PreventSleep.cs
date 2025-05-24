using UnityEngine;

public class PreventSleep : MonoBehaviour
{
    void Awake()
    {
        // Prevent Unity from ever turning the screen off due to inactivity
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        // Let the app continue running even if it technically loses focus
        Application.runInBackground = true;
    }
}
