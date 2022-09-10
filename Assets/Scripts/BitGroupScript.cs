using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class BitGroupScript : MonoBehaviour
{
    public GameObject bitPrefab;
    public List<GameObject> bitList;
    public float distBetween;

    public int startValue;
    public string binaryVersion;

    public int maxDigits = 10;
    int oldBitGroupValue = -1;
    public int bitGroupValue;

    public enum bitGroupType {
        goal = 0,
        constant = 1,
        player = 2
    }

    public bitGroupType groupType;
    // Start is called before the first frame update
    void Start()
    {
        bitList = new List<GameObject>();
        reset();

        // LoadBitLevel.levelCompleteEvent.AddListener(reset);

    }

    void setBinaryVersion()
    {
        binaryVersion = Convert.ToString(bitGroupValue, 2);
    }

    void generateBitGroup(int x)
    {
        bitGroupValue = x;
        setBinaryVersion();
        // print($"binary version of {x} is {binaryVersion}");
        float xOffset = 0;
        bitList.Clear();
        for (int i = binaryVersion.Length - 1; i >= 0; i--)
        {
            bitList.Add(Instantiate(bitPrefab, transform.position + new Vector3(xOffset, 0, 0), Quaternion.identity, transform));
            xOffset += distBetween;
        }
        print("new bit list length" + bitList.Count);

    }

    void reset()
    {
        for (int i = 0; i < bitList.Count; i++)
        {
            Destroy(bitList[i]);
        }
        generateBitGroup(Convert.ToInt32(LoadBitLevel.getBitsForBitGroup(groupType), 2));
        oldBitGroupValue = -10;
        // updateBitColors();
    }

    void updateBitColors()
    {
        setBinaryVersion();
        for (int i = 0; i < bitList.Count; i++)
        {
            bitList[i].GetComponent<bitScript>().setBitState(binaryVersion[i] == '1');
        }
        oldBitGroupValue = bitGroupValue;
    }

    
    // Update is called once per frame
    void Update()
    {
        if (oldBitGroupValue != bitGroupValue)
            updateBitColors();
    }
}
