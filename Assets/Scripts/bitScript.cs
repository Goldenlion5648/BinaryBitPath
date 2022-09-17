using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bitScript : MonoBehaviour
{
    Renderer rend;
    public Material onMaterial;
    public Material offMaterial;
    public Material goalMaterial;
    public bool isGoalBit;

    public bool isOn = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void setBitState(bool isOne)
    {
        if (rend == null)
            rend = GetComponent<Renderer>();
        isOn = isOne;
        if (!isOne)
        {
            rend.material = offMaterial;
        }
        else if (isGoalBit)
        {
            rend.material = goalMaterial;
        }
        else
        {
            rend.material = onMaterial;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
