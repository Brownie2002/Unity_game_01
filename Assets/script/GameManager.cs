using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { private set; get; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void ChangeScene(string _sceneName)
    {
        SceneManager.LoadScene(_sceneName);
    }

    public void Quit()
    {
        Application.Quit();
    }




    // Liste des mat�riaux partag�s
    private Dictionary<Renderer, Material[]> originalMaterials = new Dictionary<Renderer, Material[]>();

    // Appel�e lorsque la sc�ne est sur le point de changer
    public void OnSceneChanging()
    {
        // Enregistrer les propri�t�s des mat�riaux
        originalMaterials.Clear();
        Renderer[] renderers = FindObjectsOfType<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            originalMaterials.Add(renderer, renderer.sharedMaterials);
        }
    }

    // Appel�e lorsque la sc�ne a chang�
    public void OnSceneChanged()
    {
        // Restaurer les propri�t�s des mat�riaux
        foreach (KeyValuePair<Renderer, Material[]> pair in originalMaterials)
        {
            pair.Key.sharedMaterials = pair.Value;
        }
    }
}


