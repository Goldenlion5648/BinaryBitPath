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
    public string bitGroupBinaryString;

    public int maxDigits = 10;
    int oldBitGroupIntValue = -1;
    public int bitGroupIntValue;

    static int sizeToPadTo = 0;

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
        // reset();

        LoadBitLevel.resetForNewLevel.AddListener(reset);
        LoadBitLevel.resetForNewLevel.Invoke();

    }

    void setBinaryVersion()
    {
        bitGroupBinaryString = Convert.ToString(bitGroupIntValue, 2).PadLeft(sizeToPadTo, '0');
    }

    void generateBitGroup(int x)
    {
        bitGroupIntValue = x;
        setBinaryVersion();
        // print($"binary version of {x} is {binaryVersion}");
        float xOffset = 0;
        bitList.Clear();
        for (int i = bitGroupBinaryString.Length - 1; i >= 0; i--)
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
        sizeToPadTo = LoadBitLevel.getLongestStringLengthOnLevel();
        generateBitGroup(Convert.ToInt32(LoadBitLevel.getBitsForBitGroup(groupType), 2));
        oldBitGroupIntValue = -10;
        // updateBitColors();
    }

    void updateBitColors()
    {
        setBinaryVersion();
        for (int i = 0; i < bitList.Count; i++)
        {
            bitList[i].GetComponent<bitScript>().setBitState(bitGroupBinaryString[i] == '1');
        }
        oldBitGroupIntValue = bitGroupIntValue;
    }

    
    // Update is called once per frame
    void Update()
    {
        if (oldBitGroupIntValue != bitGroupIntValue)
            updateBitColors();
    }
}
