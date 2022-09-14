using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals : MonoBehaviour
{


    public static int playerCoordsX = 0;
    public static int playerCoordsY = 0;

    public static AudioManager audioManager;
    

    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.Find("AudioController").GetComponent<AudioManager>();
        print("found this audio manager" + audioManager.gameObject.name);
        // makeInitialMap();
    }

    

    // public static void changeMap(int fromY, int fromX, int toY, int toX)
    // {
    //     roomMap[(fromY, fromX)] = (toY, toX);
    // }

    public static int doubleDimToSingle(int fromY, int fromX, int dim)
    {
        return fromY * dim + fromX;
    }

    public static (int, int) singleDimToDouble(int oneDim, int dim)
    {
        return (oneDim / dim, oneDim % dim);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
