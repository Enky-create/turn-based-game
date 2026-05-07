using System;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{
    private bool isPlayerTurn=true;
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
        isPlayerTurn = !isPlayerTurn;
        OnTurnChanged?.Invoke(this,EventArgs.Empty);
        
    }
    public int GetTurnCount()
    {
        return turnCount;
    }
    public bool IsPlayerTurn()
    {
        return isPlayerTurn;
    }
}
