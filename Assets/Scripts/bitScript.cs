using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bitScript : MonoBehaviour
{
    Renderer rend;
    public Material onMaterial;
    public Material offMaterial;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void setBitState(bool isOne)
    {
        if(rend == null)
            rend = GetComponent<Renderer>();
        rend.material = isOne ? onMaterial : offMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
