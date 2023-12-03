using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Button playButton;
    public Button settingButton;
    public Button exitButton;
    public Button shopButton;
    public Button levelButton;

    private void Start()
    {
        //playButton.onClick.AddListener(() => { LevelManager.instance.LoadScene(1); });
        exitButton.onClick.AddListener(() => {Application.Quit(); });
    }
}