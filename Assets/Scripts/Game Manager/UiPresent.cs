using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiPresent : MonoBehaviour
{
    public static UiPresent Instance;
    [SerializeField] private Slider healBar;
    public GameObject gameOverPanel;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI bottleHealText;
    public TextMeshProUGUI bottleSpeedText;
    public TextMeshProUGUI bottleAtkText;
    private void Awake()
    {
        Instance = this;
    }

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

    public void ShowPopUp()
    {
        gameOverPanel.SetActive(true);
    }
    
    public void UpdateCoinText()
    {
        coinText.SetText(GameManager.instance.coin.ToString());
    }

    public void UpdateUiPresent()
    {
        coinText.SetText(GameManager.instance.coin.ToString());
        bottleHealText.SetText(GameManager.instance.bottleHeal.ToString());
        bottleSpeedText.SetText(GameManager.instance.bottleSpeed.ToString());
        bottleAtkText.SetText(GameManager.instance.bottleAtk.ToString());
    }
}
