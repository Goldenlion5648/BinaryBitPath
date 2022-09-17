using System.Collections;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LoadBitLevel : MonoBehaviour
{

    string bitLevelPath = System.IO.Path.Combine(Application.streamingAssetsPath, "bitLevels", "levels.txt");

    public static List<SingleLevelInput> levels = new List<SingleLevelInput>();

    public static int currentLevel = 0;
    public bool playNewest;
    public bool testingMode;
    public bool skipAhead;
    public int levelToSkipTo;

    public static UnityEvent resetForNewLevel;

    // Start is called before the first frame update
    void Start()
    {
        resetForNewLevel = new UnityEvent();
        currentLevel = 0;
        if (testingMode)
            bitLevelPath = System.IO.Path.Combine(Application.streamingAssetsPath, "bitLevels", "testing.txt");
        else if (playNewest)
            currentLevel = levels.Count - 1;
        else if (skipAhead)
            currentLevel = levelToSkipTo;

        parse();
    }

    void advanceLevel()
    {
        currentLevel += 1;
    }

    string getLevelLine(string line)
    {
        var noCommentRegex = new Regex(@"^[^#]+");
        var ret = noCommentRegex.Match(line).ToString();
        Debug.Assert(ret != "");
        print("line is " + ret);
        return ret;
    }

    void parse()
    {
        Regex doubleNewLineRegex = new Regex(@"[\n]{2,}");
        string[] contents = doubleNewLineRegex.Split(File.ReadAllText(bitLevelPath).Trim());
        print("contents follow:");
        Array.ForEach(contents, x => print(x));
        print("content len" + contents.Length);
        foreach (var level in contents)
        {
            print("level is " + level);
            var cur = new SingleLevelInput(level.Split('\n').Select(x => getLevelLine(x)).ToArray());

            levels.Add(cur);
        }

        print("levels loaded are:");

        int levelNum = 0;
        foreach(var level in levels)
        {
            print(levelNum);
            print(level);
            levelNum += 1;
            
        }

        // for (int i = 0; i < contents.Length - numLinesInLevel; i += numLinesInLevel + 1)
        // {
        //     var cur = new SingleLevelInput(
        //         getLevelLine(contents[i]),
        //         getLevelLine(contents[i + 1]),
        //         getLevelLine(contents[i + 2])
        //     );
        //     levels.Add(cur);
        // }
    }

    public static string getBitsForBitGroup(BitGroupScript.bitGroupType pos)
    {
        // print("running getBitsForBitGroup for current level " + currentLevel + " with position " + pos);
        return levels[currentLevel][(int)pos];
    }

    public static int getLongestStringLengthOnLevel()
    {
        // print("running getLongestStringLengthOnLevel for current level " + currentLevel );
        return levels[currentLevel].getMaxBitStringLengthOnLevel();
    }

    public static SingleLevelInput getCurrentLevelData()
    {
        return levels[currentLevel];
    }

    // Update is called once per frame
    void Update()
    {

    }
}
