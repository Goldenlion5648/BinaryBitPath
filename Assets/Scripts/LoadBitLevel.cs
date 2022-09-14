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
    public bool skipAhead;
    public int levelToSkipTo;

    public static UnityEvent resetForNewLevel;

    // Start is called before the first frame update
    void Start()
    {
        resetForNewLevel = new UnityEvent();
        parse();
        currentLevel = 0;
        if (playNewest)
            currentLevel = levels.Count - 1;
        else if(skipAhead)
            currentLevel = levelToSkipTo;


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
        return ret;
    }

    void parse()
    {
        var contents = File.ReadAllText(bitLevelPath).Trim().Split(new string[]{"\n\n"}, StringSplitOptions.None);
        foreach (var level in contents)
        {
            // print("level is " + level);
            var cur = new SingleLevelInput(level.Split('\n').Select(x => getLevelLine(x)).ToArray());
            levels.Add(cur);

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
        return levels[currentLevel][(int)pos];
    }

    public static int getLongestStringLengthOnLevel()
    {
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
