using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GAMESTATE gameState;
    public int bottleHeal;
    public int bottleSpeed;
    public int bottleAtk;
    public int coin;
    private void Start()
    {
        coin = 0;
        gameState = GAMESTATE.START;
    }
    public void InitBottle()
    {
        bottleHeal = 0;
        bottleSpeed = 0;
        bottleAtk = 0;
    }
    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(instance);
    }

    public void GamePause()
    {
        Time.timeScale = 0;
    }

    public void UnPause()
    {
        Time.timeScale = 1;
    }

    public void Restart()
    {
        Time.timeScale = 1;
        LevelManager.instance.RestartScene();
    }

    public void ExitMenu()
    {
        Time.timeScale = 1;
        //gameState = GAMESTATE.MENU;
        LevelManager.instance.LoadScene(0);
    }
}
public enum GAMESTATE
{
    MENU,
    START,
    END
}
