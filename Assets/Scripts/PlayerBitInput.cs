using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBitInput : MonoBehaviour
{
    float oldRawInputHorizontal = 0;

    public static int playerLevel = 0;

    BitGroupScript playerBitGroupScript;
    public BitGroupScript toolBoxBitGroup;
    public BitGroupScript goalBitGroup;
    Dictionary<KeyCode, Action> keyToOperation = new Dictionary<KeyCode, Action>();
    bool isCorrect = false;
    // Start is called before the first frame update
    void Start()
    {
        playerBitGroupScript = GetComponent<BitGroupScript>();
        keyToOperation.Add(KeyCode.E, andBits);
        keyToOperation.Add(KeyCode.R, orBits);
        keyToOperation.Add(KeyCode.F, xorBits);
        // keyToOperation.Add(KeyCode.A, shiftBitsLeft);
        // keyToOperation.Add(KeyCode.LeftArrow, shiftBitsLeft);
        // keyToOperation.Add(KeyCode.D, shiftBitsRight);
        // keyToOperation.Add(KeyCode.RightArrow, shiftBitsRight);

    }

    IEnumerator winAnimation()
    {
        yield return new WaitForSeconds(1);
        isCorrect = false;

    }

    void checkAnswer()
    {
        if (isCorrect)
            return;
        if (playerBitGroupScript.bitGroupValue == goalBitGroup.bitGroupValue)
        {
            print("was correct");
            isCorrect = true;
            StartCoroutine(winAnimation());
        }
    }

    void playerInput()
    {
        if (isCorrect)
            return;
        if (Input.GetAxisRaw("Horizontal") < 0 && oldRawInputHorizontal >= 0)
        {
            shiftBitsLeft();
            print("new bit group value" + playerBitGroupScript.bitGroupValue);
        }
        if (Input.GetAxisRaw("Horizontal") > 0 && oldRawInputHorizontal <= 0)
        {
            shiftBitsRight();
            print("new bit group value" + playerBitGroupScript.bitGroupValue);
        }

        foreach (var keycode in keyToOperation.Keys)
        {
            if (Input.GetKeyDown(keycode))
                keyToOperation[keycode]();
        }

        oldRawInputHorizontal = Input.GetAxisRaw("Horizontal");
    }

    public void shiftBitsLeft()
    {
        if (playerBitGroupScript.bitGroupValue << 1 < 1 << playerBitGroupScript.binaryVersion.Length)
            playerBitGroupScript.bitGroupValue <<= 1;
    }

    public void shiftBitsRight()
    {
        playerBitGroupScript.bitGroupValue >>= 1;
    }

    public void andBits()
    {
        playerBitGroupScript.bitGroupValue &= toolBoxBitGroup.bitGroupValue;
    }

    public void orBits()
    {
        playerBitGroupScript.bitGroupValue |= toolBoxBitGroup.bitGroupValue;
    }

    public void xorBits()
    {
        playerBitGroupScript.bitGroupValue ^= toolBoxBitGroup.bitGroupValue;
    }

    // Update is called once per frame
    void Update()
    {
        playerInput();
        checkAnswer();
    }
}
