using System.Collections;
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

    private void LoadTitle()
    {
        if (tableData.HasKey(TITLE))
        {
            title.text = tableData[TITLE];
        }
    }

    private RowController AddRow()
    {
        GameObject cell = Instantiate(rowPrefab, Vector3.zero, Quaternion.identity);
        RowController controller = cell.GetComponent<RowController>();
        cell.transform.SetParent(content);
        rows.Add(controller);
        return controller;
    } 

    private void LoadHeader()
    {
        RowController header = AddRow();
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
                    row = AddRow();
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
        LoadTable(FileManager.Instance.GetJson());
    }

    void Awake()
    {
        rows = new List<RowController>();
    }

    void Start()
    {
        LoadTable(FileManager.Instance.GetJson());
    }
}
