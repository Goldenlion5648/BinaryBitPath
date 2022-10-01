using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayActions : MonoBehaviour
{
    public bool loading = true;
    public float delayTime = .2f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(delayActions());
    }

    IEnumerator delayActions()
    {

        yield return new WaitForSeconds(delayTime);
        loading = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
