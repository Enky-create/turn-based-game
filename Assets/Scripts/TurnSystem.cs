using System;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{
    public static TurnSystem Instance {private set; get; }
    public event EventHandler OnTurnChanged;
    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Instance already exist");
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    private int turnCount = 1;
    public void NextTurn()
    {
        turnCount++;
        OnTurnChanged?.Invoke(this,EventArgs.Empty);
    }
    public int GetTurnCount()
    {
        return turnCount;
    }
}
