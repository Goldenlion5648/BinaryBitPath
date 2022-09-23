using System;
using System.Linq;
using UnityEngine;
using TMPro;

public class UpdateMovesRemainingText : MonoBehaviour
{
    TMP_Text movesRemainingTextMesh;
    int prevCount = 0;

    public PlayerBitInput playerInputScript;
    // Start is called before the first frame update
    void Start()
    {
        movesRemainingTextMesh = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        movesRemainingTextMesh.text = (LoadBitLevel.getCurrentLevelData().moveLimit == SingleLevelInput.DUMMY_LIMIT) ? "" : (LoadBitLevel.getCurrentLevelData().moveLimit - playerInputScript.movesMadeStack.Count).ToString();

    }
}
