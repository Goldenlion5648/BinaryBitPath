using System;
using System.Linq;
using UnityEngine;
using TMPro;

public class UpdateMovesRemainingText : MonoBehaviour
{
    TMP_Text movesRemainingTextMesh;
    int prevCount = -6;

    public PlayerBitInput playerInputScript;
    // Start is called before the first frame update
    void Start()
    {
        movesRemainingTextMesh = GetComponent<TMP_Text>();
        CustomEvents.madeMoveEvent.AddListener(updateMovesMadeText);
        // CustomEvents.resetForNewLevelEvent.AddListener(updateMovesMadeText);
        updateMovesMadeText();
    }

    void updateMovesMadeText()
    {
        int curCount = LoadBitLevel.getCurrentLevelData().moveLimit - playerInputScript.movesMadeStack.Count;
        if(curCount == prevCount)
            return;
        prevCount = curCount;
        int moveLimit = LoadBitLevel.getCurrentLevelData().moveLimit;
        print("moves remaining");
        print("move limit" + moveLimit);
        movesRemainingTextMesh.text = (moveLimit == SingleLevelInput.DUMMY_LIMIT) ? "" : (curCount).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        updateMovesMadeText();

    }
}
