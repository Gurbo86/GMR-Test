using UnityEngine;
using UnityEngine.UI;

public class CellController : MonoBehaviour
{
    public Image cellBackground;
    public Text cellContent;

    public void Clear()
    {
        cellContent.text = string.Empty;
        cellContent.fontStyle = FontStyle.Normal;
    }
}
