using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    void Start()
    {
        if (ItemTracker.Instance != null)
        {
            ItemTracker.Instance.RegisterItem();
        }
    }

    void OnMouseDown() // lub inna logika zbierania
    {
        if (ItemTracker.Instance != null)
        {
            ItemTracker.Instance.UnregisterItem();
        }

        Destroy(gameObject);
    }
}
