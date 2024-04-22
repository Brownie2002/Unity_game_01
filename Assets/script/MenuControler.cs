using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControler : MonoBehaviour
{
    private GameManager manager;

    public void Start()
    {
        manager = GameManager.Instance;
    }

    public void ChangeScene(string _sceneName)
    {
        manager.ChangeScene(_sceneName);
    }

    public void Quit()
    {
        manager.Quit();
    }
}
