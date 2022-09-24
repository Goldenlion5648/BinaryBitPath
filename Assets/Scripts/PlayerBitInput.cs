using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBitInput : MonoBehaviour
{
    float oldRawInputHorizontal = 0;

    public Transform playerBitsTransform;

    public static int playerLevel = 0;

    public GameObject fallingBit;
    public GameObject hitWallParticlePrefab;
    public Vector3 particleOffset = new Vector3(-.5f, 0, -1);


    BitGroupScript playerBitGroupScript;
    public BitGroupScript toolBoxBitGroup;
    public BitGroupScript goalBitGroup;
    Dictionary<KeyCode, Action> keyToOperation = new Dictionary<KeyCode, Action>();
    bool isCorrect = false;

    Coroutine levelCompleteCoroutine = null;

    public List<uint> movesMadeStack = new List<uint>();

    List<KeyCode> allowedKeys = new List<KeyCode>();

    // Start is called before the first frame update
    void Start()
    {
        assignKeys();
        CustomEvents.resetForNewLevelEvent.AddListener(runOnReset);

        // keyToOperation.Add(KeyCode.A, shiftBitsLeft);
        // keyToOperation.Add(KeyCode.LeftArrow, shiftBitsLeft);
        // keyToOperation.Add(KeyCode.D, shiftBitsRight);
        // keyToOperation.Add(KeyCode.RightArrow, shiftBitsRight);

    }

    void assignKeys()
    {
        KeyCode undoKey = KeyCode.Z;
        KeyCode resetKey = KeyCode.C;
        allowedKeys.Add(undoKey);
        allowedKeys.Add(resetKey);
        playerBitGroupScript = GetComponent<BitGroupScript>();
        keyToOperation.Add(KeyCode.W, andBits);
        keyToOperation.Add(KeyCode.S, orBits);
        keyToOperation.Add(KeyCode.R, xorBits);
        keyToOperation.Add(KeyCode.F, complementBits);
        // keyToOperation.Add(KeyCode.E, nandBits);
        keyToOperation.Add(undoKey, undo);
        keyToOperation.Add(resetKey, reset);
        keyToOperation.Add(KeyCode.A, shiftBitsLeft);
        keyToOperation.Add(KeyCode.LeftArrow, shiftBitsLeft);
        keyToOperation.Add(KeyCode.D, shiftBitsRight);
        keyToOperation.Add(KeyCode.RightArrow, shiftBitsRight);
    }

    IEnumerator winAnimation()
    {
        // yield return new WaitForSeconds(.5f);
        CustomEvents.winAnimationEvent.Invoke();
        for (int i = 0; i < 5; i++)
        {
            foreach (var bit in playerBitGroupScript.bitList)
            {
                var curBitScript = bit.GetComponent<bitScript>();
                if (curBitScript.isOn)
                {
                    curBitScript.isGoalBit = !curBitScript.isGoalBit;
                    curBitScript.setBitState(true);
                }
            }
            yield return new WaitForSeconds(.2f);
        }

        isCorrect = false;
        movesMadeStack.Clear();
        LoadBitLevel.advanceLevel();
        print("increased level");
        levelCompleteCoroutine = null;
        CustomEvents.resetForNewLevelEvent.Invoke();
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

        if (movesMadeStack.Count == 0)
        {
            movesMadeStack.Add(playerBitGroupScript.bitGroupIntValue);
        }

        foreach (var keycode in keyToOperation.Keys)
        {
            uint before = playerBitGroupScript.bitGroupIntValue;
            if (Input.GetKeyDown(keycode))
            {
                // if(allowedKeys.Contains(keycode) == false && LoadBitLevel.getCurrentLevelData().symbols.Contains(key))
                // {
                //     //TODO: add sound
                //     continue;
                // }
                keyToOperation[keycode]();
                if (before == playerBitGroupScript.bitGroupIntValue)
                {
                    // Globals.audioManager.playSoundByName("notAvailable");
                }
                else
                {
                    // print("player board is now:");
                    // print(Convert.ToString(playerBitGroupScript.bitGroupIntValue, 2));
                }
            }
        }
        oldRawInputHorizontal = Input.GetAxisRaw("Horizontal");
    }

    void runOnReset()
    {
        movesMadeStack.Clear();

    }

    public void reset()
    {
        //only want the sound when player resets, not when new level start
        Globals.audioManager.playSoundByName("reset");
        CustomEvents.resetForNewLevelEvent.Invoke();
    }

    bool allowedToPress()
    {
        // print("stack count " + movesMadeStack.Count);
        // print("move limit" + LoadBitLevel.getCurrentLevelData().moveLimit);
        bool ret = movesMadeStack.Count < LoadBitLevel.getCurrentLevelData().moveLimit;
        if (!ret)
            Globals.audioManager.playSoundByName("notAvailable");

        return ret;
    }

    void endOfButtonPressCode()
    {
        if (movesMadeStack[movesMadeStack.Count - 1] != playerBitGroupScript.bitGroupIntValue)
            movesMadeStack.Add(playerBitGroupScript.bitGroupIntValue);
    }

    public void undo()
    {
        if (movesMadeStack.Count > 1)
        {
            movesMadeStack.RemoveAt(movesMadeStack.Count - 1);
            playerBitGroupScript.bitGroupIntValue = movesMadeStack[movesMadeStack.Count - 1];
            Globals.audioManager.playSoundByName("undo");
        }
    }

    public void shiftBitsLeft()
    {
        if (!allowedToPress())
            return;

        if (LoadBitLevel.getCurrentLevelData().Contains("<"))
        {
            if (playerBitGroupScript.bitGroupIntValue << 1 < 1 << playerBitGroupScript.bitGroupBinaryString.Length)
            {
                Globals.audioManager.playSoundByName("shiftLeft");
                playerBitGroupScript.bitGroupIntValue <<= 1;
                movesMadeStack.Add(playerBitGroupScript.bitGroupIntValue);

            }
            else
            {
                Globals.audioManager.playSoundByName("hitWall");
                Instantiate(hitWallParticlePrefab, playerBitsTransform.position + particleOffset, Quaternion.identity);

            }
        }
    }

    public void shiftBitsRight()
    {
        if (!allowedToPress())
            return;
        if (LoadBitLevel.getCurrentLevelData().Contains(">"))
        {
            Globals.audioManager.playSoundByName("shiftRight");
            if ((playerBitGroupScript.bitGroupIntValue & 1) > 0)
            {
                var currentFalling = Instantiate(fallingBit, playerBitsTransform.position + new Vector3(playerBitGroupScript.distBetween * BitGroupScript.sizeToPadTo, 0), Quaternion.Euler(new Vector3(0, 0, -20)));

                currentFalling.GetComponent<bitScript>().setBitState(true);
                Destroy(currentFalling, 5f);
                Globals.audioManager.playSoundByName("bitFalling");
            }
            playerBitGroupScript.bitGroupIntValue >>= 1;
            endOfButtonPressCode();

        }
    }

    public void andBits()
    {
        if (!allowedToPress())
            return;
        if (LoadBitLevel.getCurrentLevelData().Contains("a"))
        {
            playerBitGroupScript.bitGroupIntValue &= toolBoxBitGroup.bitGroupIntValue;
            Globals.audioManager.playSoundByName("andGate");
            endOfButtonPressCode();

        }
    }

    public void orBits()
    {
        if (!allowedToPress())
            return;
        if (LoadBitLevel.getCurrentLevelData().Contains(SingleLevelInput.OR_LETTER))
        {
            playerBitGroupScript.bitGroupIntValue |= toolBoxBitGroup.bitGroupIntValue;
            Globals.audioManager.playSoundByName("orGate");
            endOfButtonPressCode();
        }
    }

    public void xorBits()
    {
        if (!allowedToPress())
            return;
        if (LoadBitLevel.getCurrentLevelData().Contains("x"))
        {
            playerBitGroupScript.bitGroupIntValue ^= toolBoxBitGroup.bitGroupIntValue;
            Globals.audioManager.playSoundByName("xorGate");
            endOfButtonPressCode();
        }
    }

    void constrainSizeToBitsOnScreen()
    {
        playerBitGroupScript.bitGroupIntValue &= Convert.ToUInt32(Mathf.Pow(2, BitGroupScript.sizeToPadTo)) - 1;
    }

    public void complementBits()
    {
        if (!allowedToPress())
            return;

        if (LoadBitLevel.getCurrentLevelData().Contains(SingleLevelInput.NOT_LETTER))
        {
            playerBitGroupScript.bitGroupIntValue = ~(playerBitGroupScript.bitGroupIntValue);
            constrainSizeToBitsOnScreen();
            Globals.audioManager.playSoundByName("complementGate");
            endOfButtonPressCode();
        }
    }

    public void nandBits()
    {
        // if (LoadBitLevel.getCurrentLevelData().Contains("n"))
        // {
        //     // playerBitGroupScript.bitGroupIntValue = Mathf.Log()
        //     print("before " + playerBitGroupScript.bitGroupIntValue);
        //     print("binary " + Convert.ToString(playerBitGroupScript.bitGroupIntValue, 2));
        //     playerBitGroupScript.bitGroupIntValue &= toolBoxBitGroup.bitGroupIntValue;
        //     playerBitGroupScript.bitGroupIntValue = ~(playerBitGroupScript.bitGroupIntValue);
        //     constrainSizeToBitsOnScreen();
        //     print("after " + playerBitGroupScript.bitGroupIntValue);
        //     print("binary " + Convert.ToString(playerBitGroupScript.bitGroupIntValue, 2));
        //     Globals.audioManager.playSoundByName("nandGate");
        // }
    }

    // Update is called once per frame
    void Update()
    {
        playerInput();
        checkAnswer();

    }
}
