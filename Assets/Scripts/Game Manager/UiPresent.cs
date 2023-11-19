using UnityEngine;
using UnityEngine.UI;

public class UiPresent : MonoBehaviour
{
    [SerializeField] private Slider healBar;
    private void Start()
    {
        SetMaxHealBar(100);
    }
    private void OnEnable()
    {
        EventManagerFuong<int>.RegisterEvent("UpdateHealBar", UpdateHealBarUi);
    }
    private void OnDestroy()
    {
        EventManagerFuong<int>.UnregisterEvent("UpdateHealBar", UpdateHealBarUi);
    }
    public void UpdateHealBarUi(int heal)
    {
        healBar.value = heal;
    }

    public void SetMaxHealBar(int maxHeal)
    {
        healBar.maxValue = maxHeal;
    }    
}
