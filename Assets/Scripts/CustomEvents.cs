using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CustomEvents : MonoBehaviour
{
    public static UnityEvent resetForNewLevelEvent;
    public static UnityEvent winAnimationEvent;
    // Start is called before the first frame update
    void Start()
    {
        resetForNewLevelEvent = new UnityEvent();
        winAnimationEvent = new UnityEvent();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
