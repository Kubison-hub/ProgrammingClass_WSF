using UnityEngine;

public class CollectibleManager : MonoBehaviour
{
    public GameObject portal;

    private int totalCollectibles;
    private int collected = 0;

    void Start()
    {
        // Zlicz obiekty z tagiem "Item"
        GameObject[] collectibles = GameObject.FindGameObjectsWithTag("Item");
        totalCollectibles = collectibles.Length;

        if (portal != null)
        {
            portal.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Portal nie zosta³ przypisany w Inspectorze!");
        }

        Debug.Log($"Znaleziono {totalCollectibles} znajdziek (Item) na poziomie.");
    }

    public void Collect()
    {
        collected++;
        Debug.Log($"Zebrano znajdŸkê: {collected}/{totalCollectibles}");

        if (collected >= totalCollectibles && portal != null)
        {
            Debug.Log("Wszystkie znajdŸki zebrane — aktywujê portal!");
            portal.SetActive(true);
        }
    }
}
