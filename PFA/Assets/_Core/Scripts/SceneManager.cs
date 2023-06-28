using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    private void Start()
    {
        Cursor();
    }

    void Cursor()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
    }

    public void Load(string _scene)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(_scene);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
