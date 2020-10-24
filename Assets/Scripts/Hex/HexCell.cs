using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HexCellType
{
    Grass, Forest, Desert, Mountain, Water
}

public class HexCell : MonoBehaviour
{
    public HexCoordinates coordinates;

    public HexCellType cellType;
    public int travelCost;
}
