using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallingBitScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spin());
    }

    IEnumerator spin()
    {
        while (true)
        {
            transform.Rotate(new Vector3(0, 0, -1));
            yield return new WaitForSeconds(.02f);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}