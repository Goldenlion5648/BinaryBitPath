using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeControlScipt : MonoBehaviour
{
    public Renderer rend;

    public int xCoord, yCoord;

    public Material youAreHereMaterial;
    public Material defaultMaterial;
    public Material selectedMaterial;

    public float changeColorTime = .6f;
    Coroutine selectedCoroutine = null;

    bool showNormalMaterial = true;
    // Start is called before the first frame update
    void Start()
    {
    }

    public void IslandSetup(int y, int x)
    {
        (yCoord, xCoord) = (y, x);
        rend = GetComponent<Renderer>();
        Material material = new Material(Shader.Find("Standard"));
        rend.material = material;
        // print("coords" + (xCoord, yCoord));

    }

    IEnumerator showAsSelected()
    {
        while (true)
        {
            if(!(GridUtility.islandFromX == xCoord && GridUtility.islandFromY == yCoord))
                break;
            rend.material = selectedMaterial;
            showNormalMaterial = false;
            yield return new WaitForSeconds(changeColorTime);
            showNormalMaterial = true;
            yield return new WaitForSeconds(changeColorTime);
        }
        selectedCoroutine = null;

    }

    void changeMaterialIfAtLocation()
    {
        if (GridUtility.islandFromX == xCoord && GridUtility.islandFromY == yCoord)
        {
            if (selectedCoroutine == null)
                selectedCoroutine = StartCoroutine(showAsSelected());
            if(!showNormalMaterial)
                return;
        }
        if (xCoord == Globals.playerCoordsX && yCoord == Globals.playerCoordsY)
        {
            rend.material = youAreHereMaterial;
        }
        else
        {
            rend.material = defaultMaterial;
        }
    }

    // Update is called once per frame
    void Update()
    {
        changeMaterialIfAtLocation();
    }
}
