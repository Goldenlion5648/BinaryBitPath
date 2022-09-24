using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurvedLineScript : MonoBehaviour
{
    public GameObject goal;
    public GameObject player;
    public List<Vector3> points;

    public Material correctMaterial;
    public Material incompleteMaterial;


    LineRenderer lineRend;

    // Start is called before the first frame update
    void Start()
    {
        lineRend = GetComponent<LineRenderer>();
        // points.Add(goal.transform.position);
        updatePositions();
        CustomEvents.resetForNewLevelEvent.AddListener(updatePositions);
        CustomEvents.winAnimationEvent.AddListener(showCorrectCheckboxColors);
        CustomEvents.winAnimationEvent.AddListener(defaultCheckboxColors);

        // points.Add(player.transform.position);
    }

    void showCorrectCheckboxColors()
    {
        print("changed to correct colors");
        lineRend.material = correctMaterial;
    }

    void defaultCheckboxColors()
    {
        lineRend.material = incompleteMaterial;
    }

    void updatePositions()
    {
        points.Clear();
        addPositionFromGroup(goal);
        points.Add(transform.parent.position);
        addPositionFromGroup(player);
    }

    void addPositionFromGroup(GameObject bitGroup)
    {
        var bitScript = bitGroup.GetComponent<BitGroupScript>();
        int numBits = bitScript.bitGroupBinaryString.Length;
        var singleBitXScale = bitScript.bitPrefab.gameObject.transform.lossyScale.x + bitScript.distBetween;
        print("single bit scale" + singleBitXScale);

        Vector3 offsetPos = bitGroup.transform.position + new Vector3(singleBitXScale / 2 * (float)numBits, 0, 0);
        points.Add(offsetPos);

    }

    // Update is called once per frame
    void Update()
    {
        lineRend.positionCount = points.Count;
        lineRend.SetPositions(points.ToArray());
    }
}
