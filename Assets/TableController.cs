using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;

public class TableController : MonoBehaviour
{
    public Text title;
    public RectTransform content;
    public GameObject rowPrefab;

    private const string TITLE = "Title";
    private const string COLUMNS = "ColumnHeaders";
    private const string DATA = "Data";

    private JSONNode tableData;
    private List<RowController> rows;
    private int activeRows;

    private RowController GetRow()
    {
        RowController controller;

        if (rows.Count <= activeRows)
        {
            GameObject cell = Instantiate(rowPrefab, Vector3.zero, Quaternion.identity);
            cell.transform.SetParent(content);
            controller = cell.GetComponent<RowController>();
            rows.Add(controller);
            activeRows++;
        }
        else
        {
            controller = rows[activeRows];
            controller.gameObject.SetActive(true);
            activeRows++;
        }

        return controller;
    }

    private void LoadTitle()
    {
        if (tableData.HasKey(TITLE))
        {
            title.text = tableData[TITLE];
        }
    }

    private void LoadHeader()
    {
        RowController header = GetRow();
        header.SetAsHeader(true);
        if (tableData.HasKey(COLUMNS))
        {
            if (tableData[COLUMNS].Count > 0)
            {
                for (int i = 0; i < tableData[COLUMNS].Count; i++)
                {
                    header.AddCell(tableData[COLUMNS][i]);
                }
            }
        }
    }

    private void LoadRows()
    {
        RowController row;
        string columnAux;

        if (tableData.HasKey(DATA))
        {
            if (tableData[DATA].Count > 0)
            {
                for (int i = 0; i < tableData[DATA].Count; i++)
                {
                    row = GetRow();
                    for (int j = 0; j < tableData[COLUMNS].Count; j++)
                    {
                        columnAux = tableData[COLUMNS][j];
                        row.AddCell(tableData[DATA][i][columnAux]);
                    }
                }
            }
        }
    }

    private void LoadTable(JSONNode data)
    {
        tableData = data;
        LoadTitle();
        LoadHeader();
        LoadRows();
    }

    public void LoadTable()
    {
        foreach (RowController row in rows)
        {
            row.Clear();
            row.gameObject.SetActive(false);
            activeRows--;
        }

        LoadTable(FileManager.Instance.GetJson());
    }

    public void CloseProgram()
    {
        Application.Quit();
    }

    #region Monobehaviour Methods

    void Awake()
    {
        rows = new List<RowController>();
        activeRows = 0;
    }

    void Start()
    {
        LoadTable(FileManager.Instance.GetJson());
    }

    #endregion
}
