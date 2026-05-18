using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyAI : MonoBehaviour
{
    private float timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TurnSystem.Instance.OnTurnChanged+= OnTurnChanged;
    }
    void Update()
    {
        if (TurnSystem.Instance.IsPlayerTurn())
        {
            return;
        }
        timer-=Time.deltaTime;
        if (timer <= 0)
        {
            TurnSystem.Instance.NextTurn();
        }
    }
    private void OnTurnChanged(object sender,EventArgs e)
    {
        timer=2f;
    }
}
