using System.Collections.Generic;
using UnityEngine;

public class RowController : MonoBehaviour
{
    public GameObject cellPrefab;
    public bool Header {
        get { return header; }
    }

    private bool header;
    private List<CellController> cells;
    private int activeCells;

    private CellController GetCell()
    {
        CellController controller;

        if (cells.Count <= activeCells)
        {
            GameObject cell = Instantiate(cellPrefab, Vector3.zero, Quaternion.identity);
            cell.transform.SetParent(this.transform);
            controller = cell.GetComponent<CellController>();
            cells.Add(controller);
            activeCells++;
        }
        else
        {
            controller = cells[activeCells];
            controller.gameObject.SetActive(true);
            activeCells++;
        }

        return controller;
    }

    public void SetAsHeader(bool value)
    {
        header = value;

        if (cells.Count > 0)
        {
            for (int i = 0; i < cells.Count; i++)
            {
                cells[i].cellContent.fontStyle = FontStyle.Bold;
            }
        }
    }

    public void AddCell(string content)
    {
        CellController cell = GetCell();

        cell.cellContent.text = content;
        if (header)
        {
            cell.cellContent.fontStyle = FontStyle.Bold;
        }
    }

    public void Clear()
    {
        SetAsHeader(false);
        foreach (CellController cell in cells)
        {
            cell.Clear();
            cell.gameObject.SetActive(false);
            activeCells--;
        }
    }

    #region Monobehaviour Methods

    void Awake()
    {
        cells = new List<CellController>();
        activeCells = 0;
    }

    #endregion
}
