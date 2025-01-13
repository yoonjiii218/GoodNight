using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }

    public void StartNew()
    {
        PlayerPrefs.SetInt("continue", 0);
        PlayerPrefs.SetInt("savePoint", 0);
        PlayerPrefs.SetInt("save_Back", 0);
        SceneManager.LoadScene("SampleScene");
    }
    public void Continue()
    {
        PlayerPrefs.SetInt("continue", 1);
        SceneManager.LoadScene("SampleScene");
    }

    public void Home()
    {
        SceneManager.LoadScene("Title");
    }

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
