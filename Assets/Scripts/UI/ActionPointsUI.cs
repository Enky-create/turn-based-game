using UnityEngine;
using TMPro;
using System;
public class ActionPointsUI : MonoBehaviour
{
    private TextMeshProUGUI textMeshProUGUI;
    void Awake()
    {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }
    public void Start()
    {
        UnitActionSystem.Instance.OnActionStart += Instance_OnActionStarts;
        UnitActionSystem.Instance.OnSelectedUnit += Instance_OnActionStarts;
        TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;
        UpdateActionPointsUI(); 
    }
    void OnDestroy()
    {
        UnitActionSystem.Instance.OnActionStart -= Instance_OnActionStarts;
        UnitActionSystem.Instance.OnSelectedUnit -= Instance_OnActionStarts;
        TurnSystem.Instance.OnTurnChanged -= TurnSystem_OnTurnChanged;
    }
    private void Instance_OnActionStarts(object sender,EventArgs e)
    {
        UpdateActionPointsUI();
    }
    private void TurnSystem_OnTurnChanged(object sender, EventArgs e)
    {
        UpdateActionPointsUI();
    }
    public void UpdateActionPointsUI()
    {
        int points = UnitActionSystem.Instance.GetSelectedUnit().GetActionPoints();
        textMeshProUGUI.text = $"Action Points: {points}";
    }
}
