using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(instance);
        } 
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadSceneAsync(index);
    }

    public void RestartScene()
    {
        int curScene = SceneManager.GetActiveScene().buildIndex;
        LoadScene(curScene);
    }
}
