using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexCellFactory : MonoBehaviour
{
    [SerializeField]
    private List<HexCell> hexCells;

    private Dictionary<int, HexCell> hexCellDictionary;

    private void Awake()
    {
        hexCellDictionary = new Dictionary<int, HexCell>();

        foreach (HexCell cell in hexCells)
        {
            int type = (int)cell.cellType;
            hexCellDictionary.Add(type, cell);
        }
    }

    public HexCell CreateNewHexCell(int type)
    {
        HexCell newCell;
        if (hexCellDictionary.TryGetValue(type, out newCell))
        {
            return Instantiate(newCell);
        }
        else
        {
            throw new System.Exception("No existe el tipo de celda type=" + type);
        }
    }
}
