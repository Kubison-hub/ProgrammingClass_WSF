using UnityEngine;

public class Collectible : MonoBehaviour
{
    private CollectibleManager manager;

    void Start()
    {
        manager = Object.FindAnyObjectByType<CollectibleManager>();
    }

    void OnMouseDown()
    {
        if (manager != null)
        {
            manager.Collect();
        }

        Destroy(gameObject);
    }
}
