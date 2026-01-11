using Unity.VisualScripting;
using UnityEngine;

public class GridTest : MonoBehaviour
{
    private GridSystem grid;
    [SerializeField]private VisualGridCell testVisualGridCell;
    
    private void Awake()
    {
        grid = new GridSystem(10,10,2f,Vector3.zero);
    }
    void Start()
    {
         for (int x = 0; x < grid.GetWidth(); x++)
        {
            for (int z = 0; z < grid.GetLength(); z++)
            {
                var gridPosition = new GridPosition(x, z);
                Vector3 pos = grid.GetWorldPositionWithOffset(gridPosition);
                VisualGridCell gridcell = Instantiate(testVisualGridCell,pos,Quaternion.identity);
                gridcell.SetGridObject(grid.GetGridObject(gridPosition));
            }
        }
    }
}
