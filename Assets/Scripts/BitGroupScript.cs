using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

public class BitGroupScript : MonoBehaviour
{
    public GameObject bitPrefab;
    public GameObject brokenBitOverlayPrefab;

    GameObject trashCan;
    public List<GameObject> bitList;
    public float distBetween;

    public int startValue;
    public string bitGroupBinaryString;

    uint oldBitGroupIntValue = UInt32.MaxValue;
    uint dummyOldValue = UInt32.MaxValue;
    public uint bitGroupIntValue;

    public static int sizeToPadTo = 0;

    public enum bitGroupType
    {
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

        CustomEvents.resetForNewLevelEvent.AddListener(reset);
        // CustomEvents.resetForNewLevel.Invoke();

    }

    void setBinaryVersion()
    {
        bitGroupBinaryString = Convert.ToString(bitGroupIntValue, 2).PadLeft(sizeToPadTo, '0');
    }

    void generateBitGroup(uint x)
    {
        bitGroupIntValue = x;
        setBinaryVersion();
        // print($"binary version of {x} is {binaryVersion}");
        float xOffset = 0;
        bitList.Clear();
        for (int i = bitGroupBinaryString.Length - 1; i >= 0; i--)
        {
            GameObject currentBit;
            // else
            // {
            currentBit = Instantiate(bitPrefab, transform.position + new Vector3(xOffset, 0, 0), Quaternion.identity, transform);
            // }
            if (i == 0 && groupType == bitGroupType.player)
            {
                var overlay = Instantiate(brokenBitOverlayPrefab, currentBit.transform.position + new Vector3(0, 0, -5), Quaternion.identity, currentBit.transform);
            }

            currentBit.GetComponent<bitScript>().isGoalBit = groupType == bitGroupType.goal;
            currentBit.GetComponent<bitScript>().isLockedBit = groupType == bitGroupType.constant;

            if (groupType == bitGroupType.constant && bitGroupIntValue == 0)
            {
                currentBit.GetComponent<Renderer>().enabled = false;
            }

            bitList.Add(currentBit);
            xOffset += distBetween;
        }
        spawnTrashCan();
        // print("new bit list length" + bitList.Count);

    }

    void spawnTrashCan()
    {
        // trashCan = Instantiate()
    }

    void reset()
    {
        for (int i = 0; i < bitList.Count; i++)
        {
            Destroy(bitList[i]);
        }
        sizeToPadTo = LoadBitLevel.getLongestStringLengthOnLevel();
        // print("trying to parse line" + LoadBitLevel.getBitsForBitGroup(groupType));
        string filtered = String.Join("", LoadBitLevel.getBitsForBitGroup(groupType).TakeWhile(x => "10".Contains(x)).ToArray());
        // print("filtered version" + filtered);
        generateBitGroup(Convert.ToUInt32(filtered, 2));
        oldBitGroupIntValue = dummyOldValue;
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
