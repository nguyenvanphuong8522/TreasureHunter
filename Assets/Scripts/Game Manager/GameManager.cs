using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GAMESTATE gameState;
    public int bottleHeal;
    public int bottleSpeed;
    public int bottleAtk;
    public int coin;
    public int score;
    public int kill;

    public void UpdateScore(int value)
    {
        score += value;
        int curScene = SceneManager.GetActiveScene().buildIndex;
        UiPresent.Instance.txtScore.SetText("Score: " + score);
        UiPresent.Instance.txtScoreCur.SetText("Score: " + score);
        UiPresent.Instance.txtKill.SetText("Kill: " + kill);
        UiPresent.Instance.txtScoreLose.SetText("Score: " + score);
        if (score > PlayerPrefs.GetInt("highScore" + curScene))
        {
            UiPresent.Instance.txthighScore.SetText("High Score: " + score);
            PlayerPrefs.SetInt("highScore" + curScene, score);
        }
        else
        {
            int highScore = PlayerPrefs.GetInt("highScore" + curScene);
            UiPresent.Instance.txthighScore.SetText("High Score: " + highScore);
        }
    }
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
        if (instance == null)
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

    public void NextLevel()
    {
        Time.timeScale = 1;
        int curScene = SceneManager.GetActiveScene().buildIndex;
        if (curScene < 6)
        {
            curScene += 1;
        }
        else
        {
            curScene = 0;
        }
        LevelManager.instance.LoadScene(curScene);
    }
}
public class DataPersist
{
    public static int highScore1
    {
        get => PlayerPrefs.GetInt("highScore1", 0);
        set => PlayerPrefs.SetInt("highScore1", value);
    }
    public static int highScore2
    {
        get => PlayerPrefs.GetInt("highScore2", 0);
        set => PlayerPrefs.SetInt("highScore2", value);
    }
    public static int highScore3
    {
        get => PlayerPrefs.GetInt("highScore3", 0);
        set => PlayerPrefs.SetInt("highScore3", value);
    }
    public static int highScore4
    {
        get => PlayerPrefs.GetInt("highScore4", 0);
        set => PlayerPrefs.SetInt("highScore4", value);
    }
    public static int highScore5
    {
        get => PlayerPrefs.GetInt("highScore5", 0);
        set => PlayerPrefs.SetInt("highScore5", value);
    }
    public static int highScore6
    {
        get => PlayerPrefs.GetInt("highScore6", 0);
        set => PlayerPrefs.SetInt("highScore6", value);
    }
}
public enum GAMESTATE
{
    MENU,
    START,
    END
}
