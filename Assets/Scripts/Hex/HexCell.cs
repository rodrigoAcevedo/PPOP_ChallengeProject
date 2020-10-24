using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathFinding;

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
                if (neighbour.isNavigable)
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
    Color defaultEmmissionColor = Color.black;
    float intensity = .4f;

    private void Awake()
    {
        gameObject.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
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
        gameObject.GetComponent<Renderer>().material.color = defaultColor;
        gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", defaultEmmissionColor * 1f);
    }

    public float CostTo(IAStarNode neighbour)
    {
        return 0f;
    }

    public float EstimatedCostTo(IAStarNode target)
    {
        return 0f;
    }

    public void SetNeighbour(HexDirection direction, HexCell cell)
    {
        neighbours[(int)direction] = cell;
        // Ingresamos nuestra misma instancia de cell en la cell opuesta.
        cell.neighbours[(int)direction.Opposite()] = this;
    }
}
