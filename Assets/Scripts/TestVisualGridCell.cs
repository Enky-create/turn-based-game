using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TestVisualGridCell : MonoBehaviour
{
    [SerializeField] private Unit unit;
    [SerializeField] private GridSystemVisual gridSystemVisual;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            var validList = unit.GetComponent<MoveAction>().GetValidGridPositionList();
            gridSystemVisual.HideAllGridPositions();
            //gridSystemVisual.ShowVisualsOnCertainPositions(validList);
        }
    }
}
