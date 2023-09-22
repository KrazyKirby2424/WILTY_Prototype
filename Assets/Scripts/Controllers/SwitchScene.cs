using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{

    public void NextScene(String name)
    {
        Debug.Log("Loading Scene: " + name);
        SceneManager.LoadScene(name);
    }
    public void InfoClear()
    {
        GameInfo.instance.Clear();
    }
    public void Quit()
    {
        Application.Quit();
    }
}
