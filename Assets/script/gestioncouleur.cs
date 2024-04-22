using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoBehaviour
{
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
