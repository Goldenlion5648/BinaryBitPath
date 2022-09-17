using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

public class SingleLevelInput
{
    public static readonly string AND_LETTER = "a";
    public static readonly string OR_LETTER = "o";
    public static readonly string XOR_LETTER = "x";
    public static readonly string LEFT_SHIFT_LETTER = "<";
    public static readonly string RIGHT_SHIFT_LETTER = ">";

    // public string goal { get; private set; }
    // public string constant { get; private set; }
    // public string playerBoard { get; private set; }
    public string symbols { get; private set; }

    string[] propArray = new string[3];
    public SingleLevelInput(params string[] lines)
    {
        symbols = lines[0];
        // (this.symbols, this.goal, this.constant, this.playerBoard) = lines.to;
        // this.goal = goal;
        // this.constant = constant;
        // this.playerBoard = playerBoard;

        propArray = lines.Skip(1).ToArray();
        // Debug.Log("set prop array to" + )

    }

    public string this[int i]
    {
        get
        {
            // Debug.Log("asking about position " + i);
            // Debug.Log("prop array length" + propArray.Length);
            return propArray[i];
        }
    }

    public bool Contains(string x)
    {
        return symbols.Contains(x);
    }

    public int getMaxBitStringLengthOnLevel()
    {
        return Mathf.Max(propArray.Select(x => x.Length).ToArray());
    }

    public override string ToString()
    {
        // Debug.Log("prop array len" + propArray.Length);
        return String.Join("\n", symbols, propArray);

    }
}
