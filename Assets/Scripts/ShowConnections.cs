using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowConnections : MonoBehaviour
{
    List<LineRenderer> lines;

    public GameObject linePrefab;

    int dim;

    public static Dictionary<(int, int), (int, int)> roomMap = new Dictionary<(int, int), (int, int)>();


    // public List<Transform> connections;
    // Start is called before the first frame update
    void Start()
    {
        // line = GetComponent<LineRenderer>();
        dim = GetComponent<GridUtility>().dim;
        lines = new List<LineRenderer>();
        CreateLines();
        makeInitialMap();
    }

    public void changeMap(int fromY, int fromX, int toY, int toX)
    {
        roomMap[(fromY, fromX)] = (toY, toX);
    }

    void makeInitialMap()
    {
        for (int y = 0; y < dim; y++)
        {
            for (int x = 0; x < dim; x++)
            {
                if (y == dim - 1 && x == dim - 1)
                    return;
                roomMap[(y, x)] = Globals.singleDimToDouble(y * dim + x + 1, dim);
            }
        }
    }

    void CreateLines()
    {
        for (int i = 0; i < dim * dim; i++)
        {
            LineRenderer cur = Instantiate(linePrefab, transform.position,
            Quaternion.identity, transform).GetComponent<LineRenderer>();
            lines.Add(cur);
        }
    }

    void makeRoomMapToLines()
    {
        // for (int i = 0; i < Globals.roomMap.Count; i++)
        int i = 0;
        var gridUtilScript = GetComponent<GridUtility>();

        foreach (var (y, x) in roomMap.Keys)
        {
            LineRenderer cur = lines[i];
            for (int j = 0; j < 2; j++)
            {
                if (j == 1)
                {
                    var (toY, toX) = roomMap[(y, x)];
                    cur.SetPosition(j, gridUtilScript.grid[toY, toX].transform.position);
                }
                else
                {
                    cur.SetPosition(j, gridUtilScript.grid[y, x].transform.position);
                }
            }
            i += 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        makeRoomMapToLines();
        // lines[0].SetPosition(0, GridUtility.grid[0,0].transform.position);
        // lines[0].SetPosition(1, GridUtility.grid[0,1].transform.position);

    }
}
