using System;
using System.Linq;
using UnityEngine;
using TMPro;

public class CurrentLevelText : MonoBehaviour
{
    TMP_Text currentLevelTextMesh;

    // Start is called before the first frame update
    void Start()
    {
        currentLevelTextMesh = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        currentLevelTextMesh.text = $"Level: {LoadBitLevel.currentLevel}";
    }
}
