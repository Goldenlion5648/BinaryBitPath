using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleLevelInput
{
    public string goal { get; private set; }
    public string constant { get; private set; }
    public string playerBoard { get; private set; }

    string[] propArray = new string[3];
    public SingleLevelInput(string goal, string constant, string playerBoard)
    {
        this.goal = goal;
        this.constant = constant;
        this.playerBoard = playerBoard;

        propArray[0] = goal;
        propArray[1] = constant;
        propArray[2] = playerBoard;
    }

    public string this[int i]
    {
        get { return propArray[i]; }
    }
}
