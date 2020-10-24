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

        HexCell cell = cells[i] = cellFactory.CreateNewHexCell(type);
        cell.transform.SetParent(transform, false);
        cell.transform.localPosition = position;
        cell.coordinates = HexCoordinates.FromOffsetCoordinates(x, z);

        // Seteamos a los neighbours de la cell
        if (x > 0)
        {
            cell.SetNeighbour(HexDirection.W, cells[i - 1]);
        }
        if (z > 0)
        {
            // Primero verificamos las filas pares
            if ((z & 1) == 0)
            {
                cell.SetNeighbour(HexDirection.SE, cells[i - width]);
                // Si x > 0 significa que también tenemos una celda en la posición SW
                if (x > 0)
                {
                    cell.SetNeighbour(HexDirection.SW, cells[i - width - 1]);
                }
            }
            else // Hacemos lo mismo para las columnas impares, pero de forma espejada
            {
                cell.SetNeighbour(HexDirection.SW, cells[i - width]);
                if (x < width - 1)
                {
                    cell.SetNeighbour(HexDirection.SE, cells[i - width + 1]);
                }
            }
        }
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
