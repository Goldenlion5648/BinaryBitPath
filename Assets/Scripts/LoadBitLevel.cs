using System.Collections;
using System;
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

    public static UnityEvent levelCompleteEvent;

    // Start is called before the first frame update
    void Start()
    {
        currentLevel = 0;
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
        return ret;
    }

    void parse()
    {
        var contents = File.ReadAllText(bitLevelPath).Split('\n');
        for (int i = 0; i < contents.Length - 3; i += 4)
        {
            var cur = new SingleLevelInput(getLevelLine(contents[i]), getLevelLine(contents[i + 1]),
            getLevelLine(contents[i + 2]));
            levels.Add(cur);
        }
    }

    public static string getBitsForBitGroup(BitGroupScript.bitGroupType pos)
    {
        return levels[currentLevel][(int)pos];
    }

    // Update is called once per frame
    void Update()
    {

    }
}
