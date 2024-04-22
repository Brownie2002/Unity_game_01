using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoBehaviour
{
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
