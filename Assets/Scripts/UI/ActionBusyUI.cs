using UnityEngine;

public class ActionBusyUI : MonoBehaviour
{
    [SerializeField] private GameObject wrapperUI;
    void Start()
    {
        UnitActionSystem.Instance.OnIsBusyChanged+= Instance_OnIsBusyChanged;
        wrapperUI.SetActive(false);
    }
    void OnDestroy()
    {
        UnitActionSystem.Instance.OnIsBusyChanged-= Instance_OnIsBusyChanged;
    }
    private void Instance_OnIsBusyChanged(object sender,UnitActionSystem.IsBusyChangedEventArgs e)
    {
        wrapperUI.SetActive(e.isBusy);
    }
}
