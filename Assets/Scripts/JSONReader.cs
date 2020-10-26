using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class JSONReader : MonoBehaviour
{
    public TextAsset jsonFile;

    public GridInfo GetGridInfoFromJSON<GridInfo>(string jsonFileText)
    {
        return JsonConvert.DeserializeObject<GridInfo>(jsonFileText);
    }
}

public class GridInfo
{
    public List<CellRow> cellMap;
}

public class CellRow
{
    public int row;
    public List<HexCellData> rowCells;
}

public class HexCellData
{
    public int cellType;
}