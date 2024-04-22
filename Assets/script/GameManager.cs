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




    // Liste des matériaux partagés
    private Dictionary<Renderer, Material[]> originalMaterials = new Dictionary<Renderer, Material[]>();

    // Appelée lorsque la scène est sur le point de changer
    public void OnSceneChanging()
    {
        // Enregistrer les propriétés des matériaux
        originalMaterials.Clear();
        Renderer[] renderers = FindObjectsOfType<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            originalMaterials.Add(renderer, renderer.sharedMaterials);
        }
    }

    // Appelée lorsque la scène a changé
    public void OnSceneChanged()
    {
        // Restaurer les propriétés des matériaux
        foreach (KeyValuePair<Renderer, Material[]> pair in originalMaterials)
        {
            pair.Key.sharedMaterials = pair.Value;
        }
    }
}


