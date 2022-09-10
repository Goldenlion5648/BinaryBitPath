using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameObject))]
public class GridUtility : MonoBehaviour
{
    public GameObject baseCubePrefab;

    public Vector3 upperLeftCorner;

    public Material nodeMaterial;

    public GameObject[,] grid;

    public static Material clickedMaterial;

    public static int dummyValue = -1000;

    public static int islandFromX;
    public static int islandFromY;

    public int dim = 3;


    public 
    Camera cam;


    // Start is called before the first frame update
    void Start()
    {
        grid = new GameObject[dim, dim];
        makeGrid();
        cam = Camera.main;
        islandFromX = dummyValue;
        islandFromY = dummyValue;

    }

    // Update is called once per frame
    void Update()
    {
        checkClick();
    }

    void checkClick()
    {
        var mouseRay = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0) && Physics.Raycast(mouseRay, out hit, 100f))
        {
            var cubeScript = hit.transform.GetComponent<CubeControlScipt>();
            if (cubeScript == null)
            {
                return;
            }

            print($"cube being hit y {cubeScript.yCoord} x {cubeScript.xCoord}");
            if (islandFromX == dummyValue)
            {
                print("case 1");
                islandFromX = cubeScript.xCoord;
                islandFromY = cubeScript.yCoord;
            }
            else if (islandFromY == cubeScript.yCoord && islandFromX == cubeScript.xCoord)
            {
                print("case 2");
                islandFromX = dummyValue;
                islandFromY = dummyValue;
            }
            else
            {
                print("case 3");
                var connectionsScript = GetComponent<ShowConnections>();
                connectionsScript.changeMap(islandFromY, islandFromX, cubeScript.yCoord, cubeScript.xCoord);
                islandFromX = dummyValue;
                islandFromY = dummyValue;
            }
        }
    }

    void makeGrid()
    {
        var connectionsScript = GetComponent<ShowConnections>();
        for (int y = 0; y < dim; y++)
        {
            for (int x = 0; x < dim; x++)
            {
                grid[y, x] = Instantiate(baseCubePrefab, upperLeftCorner +
                new Vector3(x * baseCubePrefab.transform.lossyScale.x * 3,
                -y * baseCubePrefab.transform.lossyScale.y * 3, -1), Quaternion.identity, transform);

                var cubeScript = grid[y, x].GetComponent<CubeControlScipt>();
                cubeScript.IslandSetup(y, x);
            }
        }
    }
}
