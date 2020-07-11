using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowController : MonoBehaviour
{
    public GameObject cellPrefab;
    public bool Header {
        get { return header; }
    }

    private bool header;
    private List<CellController> cellControllers;

    public void SetAsHeader(bool value)
    {
        header = value;

        if (cellControllers.Count > 0)
        {
            for (int i = 0; i < cellControllers.Count; i++)
            {
                cellControllers[i].cellContent.fontStyle = FontStyle.Bold;
            }
        }
    }

    public void AddCell(string content)
    {
        GameObject cell = Instantiate(cellPrefab, Vector3.zero, Quaternion.identity);
        CellController controller = cell.GetComponent<CellController>();
        cell.transform.SetParent(this.transform);
        controller.cellContent.text = content;
        if (header)
        {
            controller.cellContent.fontStyle = FontStyle.Bold;
        }

        cellControllers.Add(controller);
    }

    void Awake()
    {
        cellControllers = new List<CellController>();
    }
}
