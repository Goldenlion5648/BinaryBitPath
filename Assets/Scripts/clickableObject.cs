using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class clickableObject : MonoBehaviour
{
    public UnityEvent OnClick = new UnityEvent();


    public bool allowClicking = true;

    Camera cam;

    // Use this for initialization
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        var ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0))
        {
            if(!allowClicking)
            {
                //TODO: play sound
                return;
            }
            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
            {
                // Debug.Log("Button Clicked");
                OnClick.Invoke();
            }
        }
    }
}
