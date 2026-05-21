using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class TurnSystemUI : MonoBehaviour
{
    [SerializeField] private Button endOfTurnButton;
    [SerializeField] private TextMeshProUGUI currentTurnText;
    [SerializeField] private GameObject enemyTurnBanner;
    private TurnSystem turnSystemInstance;
    void Start()
    {

        turnSystemInstance = TurnSystem.Instance;
        turnSystemInstance.OnTurnChanged+=TurnSystem_OnTurnChanged;
        endOfTurnButton.onClick.AddListener(()=>{
            turnSystemInstance.NextTurn();
        });
        
        Unit.OnAnyActionPointChange += TurnSystem_OnTurnChanged;
        UpdateTurnText();
        UpdateEnemyTurn();
        UpdateEndOfTurnButton();
    }
    private void TurnSystem_OnTurnChanged(object sender, EventArgs e)
    {
        UpdateTurnText();
        UpdateEnemyTurn();
        UpdateEndOfTurnButton();
    }
    private void UpdateTurnText()
    {
        currentTurnText.text = $"Turn: {turnSystemInstance.GetTurnCount()}";
    }
    private void UpdateEnemyTurn()
    {
        enemyTurnBanner.SetActive(!TurnSystem.Instance.IsPlayerTurn());
    }
    private void UpdateEndOfTurnButton()
    {
        endOfTurnButton.gameObject.SetActive(TurnSystem.Instance.IsPlayerTurn());
    }
}
