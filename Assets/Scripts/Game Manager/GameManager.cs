using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GAMESTATE gameState;
    public int bottleHeal;
    public int bottleSpeed;
    public int bottleAtk;
    private void Start()
    {
        gameState = GAMESTATE.START;
    }
    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(instance);
    }
}
public enum GAMESTATE
{
    MENU,
    START,
    END
}
