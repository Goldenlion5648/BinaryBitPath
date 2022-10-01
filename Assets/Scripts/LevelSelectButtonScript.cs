using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class LevelSelectButtonScript : MonoBehaviour
{
    public int levelToChangeTo;
    Button thisButton;
    TMP_Text levelLabel;
    TMP_Text binaryLabel;
    TMP_Text symbolsLabel;
    // Start is called before the first frame update
    void Start()
    {
        thisButton = transform.GetComponent<Button>();
        // print("top" + buttonLabel);
        thisButton.onClick.AddListener(goToClickedLevel);
    }

    public void goToClickedLevel()
    {
        if(GetComponentInParent<DelayActions>().loading)
            return;
        LoadBitLevel.currentLevel = levelToChangeTo;
        // CustomEvents.resetForNewLevelEvent.Invoke();
        // print("current level name has been updated to " + Globals.currentLevelName);
        SceneManager.LoadScene("BitPath");
    }

    public void updateButtonText(int levelNum)
    {
        levelLabel = transform.GetChild(0).GetComponent<TMP_Text>();
        binaryLabel = transform.GetChild(1).GetComponent<TMP_Text>();
        symbolsLabel = transform.GetChild(2).GetComponent<TMP_Text>();
        // mappedLevel = newMappedLevel;
        var currentLevel = LoadBitLevel.levels[levelToChangeTo];

        var formattedBinary = string.Join("\n", currentLevel.propArray);
        // Array.ForEach(words, x => print(x));
        levelLabel.text = $"{levelNum}";
        binaryLabel.text = $"{formattedBinary}";
        symbolsLabel.text = $"{currentLevel.symbols}";

    }

    // Update is called once per frame
    void Update()
    {
        // updateButtonText();
    }
}
