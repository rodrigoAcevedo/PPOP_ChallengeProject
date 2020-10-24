using System.Collections;
using System.Collections.Generic;
using System.IO;
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
        GameObject selectedCell = hit.transform.gameObject;


    }
}
