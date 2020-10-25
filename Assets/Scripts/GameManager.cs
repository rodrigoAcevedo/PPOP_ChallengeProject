using PathFinding;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    HexCell beginCell;
    HexCell endCell;

    IList<IAStarNode> cellPaths;

    bool hasDrawnPath;

    private void Awake()
    {
        hasDrawnPath = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleInput();
        }
    }

    void HandleInput()
    {
        Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(inputRay, out hit))
        {
            SelectCell(hit);
        }
    }

    void SelectCell(RaycastHit hit)
    {
        GameObject selectedGameObject = hit.transform.gameObject;
        HexCell selectedCell = selectedGameObject.GetComponent<HexCell>();

        if (selectedCell.isNavigable && !selectedCell.isSelected)
        {
            if (beginCell == null)
            {
                beginCell = selectedCell;
                beginCell.Select();
                return;
            }

            if (endCell == null)
            {
                endCell = selectedCell;
                endCell.Select();
            }
            // else
            else if (hasDrawnPath)
            {
                // Deseleccionamos las actuales referencias a celdas y reseteamos el camino.
                beginCell.Unselect();
                endCell.Unselect();
                beginCell = null;
                endCell = null;
                ResetPath();

                // Volvemos a llamar al mismo método para volver a empezar.
                SelectCell(hit);
            }

            if (beginCell != null && endCell != null)
            {
                FindPath();
            }
                
        }
    }

    void FindPath()
    {
        IAStarNode beginNode = beginCell as IAStarNode;
        IAStarNode endNode = endCell as IAStarNode;

        cellPaths = AStar.GetPath(beginNode, endNode);

        if (cellPaths != null)
        {
            foreach (IAStarNode cellNode in cellPaths)
            {
                // Ignoramos los nodos de inicio y fin.
                if (cellNode != beginNode && cellNode != endNode)
                {
                    HexCell cell = cellNode as HexCell;
                    cell.SelectAsPath();
                }
            }
        }
        hasDrawnPath = true;
    }

    void ResetPath()
    {
        if (cellPaths != null)
        {
            foreach (IAStarNode cellNode in cellPaths)
            {
                HexCell cell = cellNode as HexCell;

                // Si es uno que seleccionamos no queremos revertirlo
                if (!cell.isSelected)
                    cell.SetToDefault();
            }
        }
        
        hasDrawnPath = false;
    }
}
