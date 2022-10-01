using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveObject
{
    public int highestLevel;

    public SaveObject(int level)
    {
        highestLevel = level;
    }

    public override string ToString()
    {
        return $"{highestLevel}";
    }
}
