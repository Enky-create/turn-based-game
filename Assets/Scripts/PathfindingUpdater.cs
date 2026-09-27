using System;
using UnityEngine;

public class PathfindingUpdater : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Crate.ON_ANY_CRATE_DESTROYED+=Crate_ON_ANY_CRATE_DESTROYED;
    }

    private void Crate_ON_ANY_CRATE_DESTROYED(object sender, EventArgs e)
    {
        Crate crate = sender as Crate;
        var cratePosition = LevelGrid.Instance.GetGridPosition(crate.transform.position);
        Pathfinding.Instance.SetIsWalkable(cratePosition,true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
