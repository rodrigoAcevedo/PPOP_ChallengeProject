using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathFinding;
using UnityEditor.UIElements;

public enum HexCellType
{
    Grass, Forest, Desert, Mountain, Water
}

public class HexCell : MonoBehaviour, IAStarNode
{
    public HexCoordinates coordinates;

    public HexCellType cellType;

    public IEnumerable<IAStarNode> Neighbours
    {
        get
        {
            foreach (HexCell neighbour in neighbours)
            {
                if (neighbour != null && neighbour.isNavigable)
                {
                    yield return neighbour;
                }
            }
        }
    }
    [SerializeField]
    List<HexCell> neighbours;

    public int travelCost;
    public bool isNavigable;

    public bool isSelected;

    Color selectedColor = Color.green;
    Color defaultColor = Color.white;
    Color pathColor = Color.red;
    Color defaultEmmissionColor = Color.black;
    float intensity = .4f;

    private void Awake()
    {
        gameObject.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
    }


    public void SelectAsPath()
    {
        gameObject.GetComponent<Renderer>().material.color = pathColor;
        gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", pathColor * intensity);
    }

    public void SetToDefault()
    {
        gameObject.GetComponent<Renderer>().material.color = defaultColor;
        gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", defaultEmmissionColor * 1f);
    }

    public void Select()
    {
        isSelected = true;
        gameObject.GetComponent<Renderer>().material.color = selectedColor;
        gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", selectedColor * intensity);
    }

    public void Unselect()
    {
        isSelected = false;
        SetToDefault();
    }

    public float CostTo(IAStarNode neighbour)
    {
        HexCell neighbourCell = neighbour as HexCell;
        if (neighbourCell != null)
        {
            Debug.Log("HexCell::CostTo neighbourCell.travelCost = " + neighbourCell.travelCost);
            return neighbourCell.travelCost;
        }
        else
        {
            throw new System.Exception("neighbour is not a valid HexCell");
        }
        
    }

    public float EstimatedCostTo(IAStarNode target)
    {
        HexCell targetCell = target as HexCell;

        // Calcular la distancia total
        //float distance = Mathf.Sqrt(Mathf.Pow(targetCell.coordinates.X - coordinates.X, 2) + Mathf.Pow(targetCell.coordinates.Y - coordinates.Y, 2) + Mathf.Pow(targetCell.coordinates.Z - coordinates.Z, 2));
        float x = coordinates.X;
        float y = coordinates.Y;
        float z = coordinates.Z;

        float targetX = targetCell.coordinates.X;
        float targetY = targetCell.coordinates.Y;
        float targetZ = targetCell.coordinates.Z;

        float distance = ((x < targetX ? targetX - x : x - targetX) + (y < targetY ? targetY - y : y - targetY) + (z < targetZ ? targetZ - z : z - targetZ)) / 2f;
        Debug.Log("HexCell::EstimatedCostTo distance = " + distance);
        return distance;
    }

    public void SetNeighbour(HexDirection direction, HexCell cell)
    {
        neighbours[(int)direction] = cell;
        // Ingresamos nuestra misma instancia de cell en la cell opuesta.
        cell.neighbours[(int)direction.Opposite()] = this;
    }
}
