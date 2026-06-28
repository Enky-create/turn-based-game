using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance;
    private List<Unit> allUnits;
    private List<Unit> enemyUnits;
    private List<Unit> playerUnits;
    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one UnitManager "
                + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance=this;
        allUnits= new List<Unit>();
        enemyUnits= new List<Unit>();
        playerUnits= new List<Unit>();
    }
    void Start()
    {
        Unit.OnAnyUnitSpawn+= Unit_OnAnyUnitSpawn;
        Unit.OnAnyUnitDied+=Unit_OnAnyUnitDied;
    }
    private void Unit_OnAnyUnitDied(object sender, EventArgs e)
    {
        Unit unit = sender as Unit;
        allUnits.Remove(unit);
        if (unit.IsEnemy())
        {
            enemyUnits.Remove(unit);
        }
        else
        {
            playerUnits.Remove(unit);
        }
    }
    private void Unit_OnAnyUnitSpawn(object sender, EventArgs e)
    {
        Unit unit = sender as Unit;
        allUnits.Add(unit);
        if (unit.IsEnemy())
        {
            enemyUnits.Add(unit);
        }
        else
        {
            playerUnits.Add(unit);
        }
    }
    public List<Unit> GetAllUnits()
    {
        return allUnits;
    }
    public List<Unit> GetPlayerUnits()
    {
        return playerUnits;
    }
    public List<Unit> GetEnemyUnits()
    {
        return enemyUnits;
    }
}
