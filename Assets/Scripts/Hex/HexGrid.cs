using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGrid : MonoBehaviour
{

    public int width = 8;
    public int height = 8;

    public HexCell cellPrefab;

    public HexCellFactory cellFactory;

    HexCell[] cells;
    void Awake()
    {
        cells = new HexCell[height * width];

        GenerateRandomGrid();
    }

    void CreateCell(int x, int z, int i, int type)
    {
        Vector3 position;

        position.x = (x + z * .5f - z / 2) * (HexMetrics.innerRadius * 2f);
        position.y = 0f;
        // TODO: Explicar porque esta hardcodeado
        position.z = z * .744f;

        Debug.Log("Cell type = " + type);
        HexCell cell = cells[i] = cellFactory.CreateNewHexCell(type);
        cell.transform.SetParent(transform, false);
        cell.transform.localPosition = position;
        cell.coordinates = HexCoordinates.FromOffsetCoordinates(x, z);
    }

    void GenerateRandomGrid()
    {
        for (int z = 0, i = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                int cellType = Random.Range(0, 5);
                CreateCell(x, z, i++, cellType);
            }
        }
    }

}
