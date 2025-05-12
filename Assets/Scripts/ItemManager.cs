using UnityEngine;

public class ItemTracker : MonoBehaviour
{
    public static ItemTracker Instance;

    private int itemCount = 0;
    private bool revealed = false;

    public GameObject objectToReveal;

    void Awake()
    {
        Instance = this;
    }

    public void RegisterItem()
    {
        itemCount++;
    }

    public void UnregisterItem()
    {
        itemCount--;
        if (itemCount <= 0 && !revealed)
        {
            RevealObject();
        }
    }

    private void RevealObject()
    {
        if (objectToReveal != null)
        {
            objectToReveal.SetActive(true);
            revealed = true;
        }
    }
}
