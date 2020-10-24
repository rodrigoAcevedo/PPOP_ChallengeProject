using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    HexCell beginCell;
    HexCell endCell;

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
            }

            if (beginCell != null && endCell != null)
                FindPath();
        }
    }

    void FindPath()
    {
        // TODO: Buscar el camino
    }

    void ResetPath()
    {
        // TODO: Resetear el camino
    }
}
