using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public static class SaveManager
{
    public static string saveDir = System.IO.Path.Combine(
        Application.persistentDataPath, "bitLevels"
    );

    //default used in case custom level ability added later
    public static string saveLocation = System.IO.Path.Combine(
        saveDir,
        "save.txt"
    );
    public static string fullPathOfSaveFile = saveLocation;


    public static void Save(bool forceOverwrite = false)
    {
        SaveObject newSaveData = Load();

        //current level the player is on is higher than their previous highest
        if (newSaveData.highestLevel < LoadBitLevel.currentLevel || forceOverwrite)
        {
            Debug.Log("writing level: " + LoadBitLevel.currentLevel);
            newSaveData.highestLevel = LoadBitLevel.currentLevel;
        }
        else
        {
            //no reason to save, they have been farther than this
            return;
        }

        Debug.Log("writing new save with dict:");

        // }
        writeToSaveFile(newSaveData);

        // Debug.Log("save existed");
    }

    public static void writeToSaveFile(SaveObject saveToWrite)
    {
        if (!Directory.Exists(saveDir))
        {
            Directory.CreateDirectory(saveDir);
        }

        File.WriteAllText(fullPathOfSaveFile, saveToWrite.ToString());
    }

    public static SaveObject makeNewSave()
    {
        SaveObject blankSave = new SaveObject(0);
        // Save(Globals.allLevels[0]);
        writeToSaveFile(blankSave);
        return blankSave;

    }

    public static bool saveFileExists()
    {
        // Debug.Log("checking if save exists" + fullPathOfSaveFile);
        return File.Exists(fullPathOfSaveFile);
    }


    public static SaveObject Load()
    {
        //make new save, one did not exist
        if (!saveFileExists())
            return makeNewSave();

        //TODO: if more options added, don't just read single number, read lines
        int highestLevel = Convert.ToInt32(File.ReadAllText(fullPathOfSaveFile).Trim());

        if (highestLevel < 0 || highestLevel >= LoadBitLevel.levels.Count)
        {
            highestLevel = 0;
        }

        return new SaveObject(highestLevel);
    }
}
