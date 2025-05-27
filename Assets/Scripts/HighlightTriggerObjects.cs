using UnityEngine;
using System.Collections.Generic;

public class HighlightTriggerObjects : MonoBehaviour
{
    public Color highlightColor = Color.yellow;
    private Dictionary<GameObject, Material[]> originalMaterials = new Dictionary<GameObject, Material[]>();

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            HighlightAll();
        }
        else if (Input.GetKeyUp(KeyCode.V))
        {
            RestoreAll();
        }
    }

    void HighlightAll()
    {
        GameObject[] triggers = GameObject.FindGameObjectsWithTag("Trigger");

        foreach (GameObject obj in triggers)
        {
            Renderer renderer = obj.GetComponent<Renderer>();
            if (renderer != null)
            {
                if (!originalMaterials.ContainsKey(obj))
                    originalMaterials[obj] = renderer.materials;

                Material[] highlightMats = new Material[renderer.materials.Length];
                for (int i = 0; i < highlightMats.Length; i++)
                {
                    Material mat = new Material(renderer.materials[i]);
                    mat.color = highlightColor;
                    highlightMats[i] = mat;
                }
                renderer.materials = highlightMats;
            }
        }
    }

    void RestoreAll()
    {
        foreach (var pair in originalMaterials)
        {
            Renderer renderer = pair.Key.GetComponent<Renderer>();
            if (renderer != null)
                renderer.materials = pair.Value;
        }

        originalMaterials.Clear();
    }
}
