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

    Coroutine levelCompleteCoroutine = null;

    // Start is called before the first frame update
    void Start()
    {
        playerBitGroupScript = GetComponent<BitGroupScript>();
        keyToOperation.Add(KeyCode.E, andBits);
        keyToOperation.Add(KeyCode.R, orBits);
        keyToOperation.Add(KeyCode.F, xorBits);
        keyToOperation.Add(KeyCode.C, reset);
        keyToOperation.Add(KeyCode.A, shiftBitsLeft);
        keyToOperation.Add(KeyCode.LeftArrow, shiftBitsLeft);
        keyToOperation.Add(KeyCode.D, shiftBitsRight);
        keyToOperation.Add(KeyCode.RightArrow, shiftBitsRight);
        // keyToOperation.Add(KeyCode.A, shiftBitsLeft);
        // keyToOperation.Add(KeyCode.LeftArrow, shiftBitsLeft);
        // keyToOperation.Add(KeyCode.D, shiftBitsRight);
        // keyToOperation.Add(KeyCode.RightArrow, shiftBitsRight);

    }

    IEnumerator winAnimation()
    {
        yield return new WaitForSeconds(.1f);
        isCorrect = false;
        LoadBitLevel.currentLevel += 1;
        print("increased level");
        levelCompleteCoroutine = null;

        LoadBitLevel.resetForNewLevel.Invoke();


    }

    void checkAnswer()
    {
        if (isCorrect)
            return;
        if (playerBitGroupScript.bitGroupIntValue == goalBitGroup.bitGroupIntValue && levelCompleteCoroutine == null)
        {
            Globals.audioManager.playSoundByName("levelComplete");
            print("was correct");
            isCorrect = true;
            levelCompleteCoroutine = StartCoroutine(winAnimation());
        }
    }

    void playerInput()
    {
        if (isCorrect)
            return;
        // if (Input.GetAxisRaw("Horizontal") < 0 && oldRawInputHorizontal >= 0)
        // {
        //     shiftBitsLeft();
        //     // print("new bit group value" + playerBitGroupScript.bitGroupIntValue);
        // }
        // if (Input.GetAxisRaw("Horizontal") > 0 && oldRawInputHorizontal <= 0)
        // {
        //     shiftBitsRight();
        //     // print("new bit group value" + playerBitGroupScript.bitGroupIntValue);
        // }

        foreach (var keycode in keyToOperation.Keys)
        {
            if (Input.GetKeyDown(keycode))
                keyToOperation[keycode]();
        }

        oldRawInputHorizontal = Input.GetAxisRaw("Horizontal");
    }

    public void reset()
    {
        LoadBitLevel.resetForNewLevel.Invoke();
    }

    public void shiftBitsLeft()
    {
        if (playerBitGroupScript.bitGroupIntValue << 1 < 1 << playerBitGroupScript.bitGroupBinaryString.Length && LoadBitLevel.getCurrentLevelData().Contains("<"))
        {
            Globals.audioManager.playSoundByName("shiftLeft");
            playerBitGroupScript.bitGroupIntValue <<= 1;
        }
    }

    public void shiftBitsRight()
    {
        if (LoadBitLevel.getCurrentLevelData().Contains(">"))
        {
            Globals.audioManager.playSoundByName("shiftRight");
            playerBitGroupScript.bitGroupIntValue >>= 1;
        }
            
    }

    public void andBits()
    {
        if (LoadBitLevel.getCurrentLevelData().Contains("a"))
        {
            playerBitGroupScript.bitGroupIntValue &= toolBoxBitGroup.bitGroupIntValue;
            Globals.audioManager.playSoundByName("andGate");
        }
    }

    public void orBits()
    {
        if (LoadBitLevel.getCurrentLevelData().Contains("o"))
        {
            playerBitGroupScript.bitGroupIntValue |= toolBoxBitGroup.bitGroupIntValue;
            Globals.audioManager.playSoundByName("orGate");
        }
    }

    public void xorBits()
    {
        if (LoadBitLevel.getCurrentLevelData().Contains("x"))
        {
            playerBitGroupScript.bitGroupIntValue ^= toolBoxBitGroup.bitGroupIntValue;
            Globals.audioManager.playSoundByName("xorGate");
        }
    }

    // Update is called once per frame
    void Update()
    {
        playerInput();
        checkAnswer();

    }
}
