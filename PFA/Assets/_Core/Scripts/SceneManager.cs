using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public void Load(string _scene)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(_scene);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
