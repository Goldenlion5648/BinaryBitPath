using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CustomEvents : MonoBehaviour
{
    public static UnityEvent resetForNewLevelEvent;
    public static UnityEvent winAnimationEvent;
    public static UnityEvent madeMoveEvent;
    // Start is called before the first frame update
    void Start()
    {
        resetForNewLevelEvent = new UnityEvent();
        winAnimationEvent = new UnityEvent();
        madeMoveEvent = new UnityEvent();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
