using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideToolbox : MonoBehaviour
{


    BitGroupScript toolboxBitGroup;
    // Start is called before the first frame update
    void Start()
    {
        toolboxBitGroup = GetComponent<BitGroupScript>();
    }

    void hideToolbox()
    {
        foreach (var child in transform.GetComponentsInChildren<Transform>())
        {
            if (child == transform)
                continue;
            child.GetComponent<Renderer>().enabled = toolboxBitGroup.bitGroupIntValue != 0;
        }
        if (toolboxBitGroup.bitGroupIntValue != 0)
        {
            // print("toolbox value" + toolboxBitGroup.bitGroupIntValue);
        }
    }
    // Update is called once per frame
    void Update()
    {
        hideToolbox();
    }
}
