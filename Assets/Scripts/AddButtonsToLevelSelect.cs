using UnityEngine;
using System;

public class AddButtonsToLevelSelect : MonoBehaviour
{
    public GameObject defaultButton;
    // Start is called before the first frame update
    void Start()
    {
        /* Globals.setCurrentLevelToHighestInPack();
        // Debug.Log("pack is " + Globals.levelPack);
        // Debug.Log("total levels" + Globals.allLevels.Length);
        // Debug.Log("current level name" + Globals.currentLevelName);
        // Globals.printDict(Globals.currentSave.customHighestLevelDict);
        // print(string.Join("\n", Globals.allLevels));


        string highestLevelInSaveData = "";
        if (Array.IndexOf(Globals.allLevels, Globals.currentLevelName) == -1)
        {
            print("current level not found, only showing first");
            highestLevelInSaveData = Globals.allLevels[0];
        }
        else if (Globals.levelPack == Globals.defaultPack)
        {
            highestLevelInSaveData = Globals.currentSave.highestLevel;
        }
        else
        {
            highestLevelInSaveData = Globals.currentSave.customHighestLevelDict.GetValueOrDefault(Globals.levelPack, Globals.allLevels[0]);

        } */
        // foreach (var level in LoadBitLevel.levels)
        print("adding buttons to level select");
        print("LoadBitLevel.levels.Count " + LoadBitLevel.levels.Count);


        for (int levelNum = 0; levelNum < LoadBitLevel.levels.Count; levelNum++)
        {
            // print("running");

            var currentButton = Instantiate(defaultButton, transform);
            var levelSelectScript = currentButton.GetComponent<LevelSelectButtonScript>();
            levelSelectScript.levelToChangeTo = levelNum;
            levelSelectScript.updateButtonText(levelNum);
            if (levelNum == SaveManager.Load().highestLevel)
                break;
        }
    }
}
