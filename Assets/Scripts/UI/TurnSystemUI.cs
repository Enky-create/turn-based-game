using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class TurnSystemUI : MonoBehaviour
{
    [SerializeField] private Button endOfTurnButton;
    [SerializeField] private TextMeshProUGUI currentTurnText;
    private TurnSystem turnSystemInstance;
    void Start()
    {

        turnSystemInstance = TurnSystem.Instance;
        turnSystemInstance.OnTurnChanged+=TurnSystem_OnTurnChanged;
        endOfTurnButton.onClick.AddListener(()=>{
            turnSystemInstance.NextTurn();
            currentTurnText.text = $"Turn: {turnSystemInstance.GetTurnCount()}";
        });
        UpdateTurnText();
        Unit.OnAnyActionPointChange += TurnSystem_OnTurnChanged;
    }
    private void TurnSystem_OnTurnChanged(object sender, EventArgs e)
    {
        UpdateTurnText();
    }
    private void UpdateTurnText()
    {
        currentTurnText.text = $"Turn: {turnSystemInstance.GetTurnCount()}";
    }
}
