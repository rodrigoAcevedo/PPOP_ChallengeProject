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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
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
            else
            {
                beginCell.Unselect();
                beginCell = endCell;
                endCell = selectedCell;
                endCell.Select();
                ResetPath();
            }

            if (beginCell != null && endCell != null)
                FindPath();
        }
    }

    void FindPath()
    {
        IAStarNode beginNode = beginCell as IAStarNode;
        IAStarNode endNode = endCell as IAStarNode;

        cellPaths = AStar.GetPath(beginNode, endNode);

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

    void ResetPath()
    {
        foreach (IAStarNode cellNode in cellPaths)
        {
            HexCell cell = cellNode as HexCell;

            // Si es uno que seleccionamos no queremos revertirlo
            if(!cell.isSelected)
                cell.SetToDefault();
        }
    }
}
