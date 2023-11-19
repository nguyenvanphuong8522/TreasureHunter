using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int bottleHeal;
    public int bottleSpeed;
    public int bottleAtk;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(instance);
    }


}
