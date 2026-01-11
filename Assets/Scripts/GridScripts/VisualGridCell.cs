using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class VisualGridCell : MonoBehaviour
{
    [SerializeField] private TextMeshPro textMeshPro;
    [SerializeField]private GridObject gridObject;
    void Update()
    {
        if(textMeshPro!=null)
        textMeshPro.text=gridObject.ToString();
    }
    public void SetGridObject(GridObject gridObject)
    {
        this.gridObject = gridObject;
    }
}
