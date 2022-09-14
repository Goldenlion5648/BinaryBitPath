using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideToolbox : MonoBehaviour
{

    [SerializeField]
    bool isOn = true;
    BitGroupScript toolboxBitGroup;
    // Start is called before the first frame update
    void Start()
    {
        toolboxBitGroup = GetComponent<BitGroupScript>();
    }

    void hideToolbox()
    {
        if (isOn == (toolboxBitGroup.bitGroupIntValue == 0))
        {
            isOn = !isOn;
            foreach (var child in transform.GetComponentsInChildren<Transform>())
            {
                if(child == transform)
                    continue;
                child.GetComponent<Renderer>().enabled = isOn;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        hideToolbox();
    }
}
