using UnityEngine;
using TMPro;

public class PathfindingVisual : MonoBehaviour
{
    [SerializeField] private TextMeshPro coordinates;
    [SerializeField] private TextMeshPro fCost;
    [SerializeField] private TextMeshPro gCost;
    [SerializeField] private TextMeshPro hCost;
    private PathNode pathNode;
    public void SetPathNode(PathNode node)
    {
        pathNode=node;
    }

    // Update is called once per frame
    void Update()
    {
        coordinates.text = pathNode.GetGridPosition().ToString();
        fCost.text = pathNode.GetFcost().ToString();
        gCost.text = pathNode.GetGcost().ToString();
        hCost.text = pathNode.GetHcost().ToString();
    }
}
