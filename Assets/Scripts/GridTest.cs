using Unity.VisualScripting;
using UnityEngine;

public class GridTest : MonoBehaviour
{
    private GridSystem grid;
    
    private void OnDrawGizmos()
    {
        if (grid == null)
        {
            grid = new GridSystem(10,10,2f,Vector3.zero);
        }

        for (int x = 0; x < 10; x++)
        {
            for (int z = 0; z < 10; z++)
            {
                Vector3 pos = grid.GetWorldPosition(new GridPosition(x, z));
                Gizmos.color = Color.white;
                Gizmos.DrawLine(pos, pos + Vector3.right);
            }
        }
    }
}
