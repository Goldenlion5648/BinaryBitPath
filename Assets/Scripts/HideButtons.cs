using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideButtons : MonoBehaviour
{

    public GameObject andButton;
    public GameObject orButton;
    public GameObject xorButton;
    public GameObject leftShiftButton;
    public GameObject rightShiftButton;
    public GameObject notButton;
    Dictionary<string, GameObject> toCheck = new Dictionary<string, GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        toCheck.Add(SingleLevelInput.AND_LETTER, andButton);
        toCheck.Add(SingleLevelInput.OR_LETTER, orButton);
        toCheck.Add(SingleLevelInput.XOR_LETTER, xorButton);
        toCheck.Add(SingleLevelInput.LEFT_SHIFT_LETTER, leftShiftButton);
        toCheck.Add(SingleLevelInput.RIGHT_SHIFT_LETTER, rightShiftButton);
        toCheck.Add(SingleLevelInput.NOT_LETTER, notButton);
        CustomEvents.resetForNewLevelEvent.AddListener(reloadVisibility);

    }

    public void reloadVisibility()
    {
        foreach (var key in toCheck.Keys)
        {
            if (LoadBitLevel.getSymbolsSeenUntilCurrentLevel().Contains(key) == false)
            {
                toCheck[key].SetActive(false);
                continue;
            }
            toCheck[key].SetActive(true);
            toCheck[key].transform.Find("xSymbol").gameObject.SetActive(!LoadBitLevel.getCurrentLevelData().Contains(key));
            // if(LoadBitLevel.getCurrentLevelData().Contains(key) == false)
            // {
            // }
            // else {

            //     toCheck[key].transform.Find("xSymbol").gameObject.SetActive(false);    
            // }
            toCheck[key].GetComponent<clickableObject>().allowClicking = LoadBitLevel.getCurrentLevelData().Contains(key);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
