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
        // transform.Translate(Vector3.right, Space.World);
        while (true)
        {
            transform.Rotate(new Vector3(0, 0, -2));
            yield return new WaitForSeconds(.01f);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
