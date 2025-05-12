using UnityEngine;

public class ClickDissapear : MonoBehaviour
{
    public float interactionRange = 3f;
    public Color highlightColor = Color.yellow;
    public Color defaultColor = Color.white;

    private Vector3 startPosition;
    private bool isPlayerInRange = false;

    private Transform player;
    private Renderer objectRenderer;
    private Material objectMaterial;
    private CollectibleManager manager;

    void Start()
    {
        startPosition = transform.position;

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogWarning("Gracz (tag: Player) nie zosta³ znaleziony!");
        }

        manager = Object.FindAnyObjectByType<CollectibleManager>();

        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer != null)
        {
            objectMaterial = objectRenderer.material;
        }
    }

    void Update()
    {
        if (player == null || objectMaterial == null) return;

        float distance = Vector3.Distance(startPosition, player.position);
        bool nowInRange = distance <= interactionRange;

        if (nowInRange != isPlayerInRange)
        {
            isPlayerInRange = nowInRange;
            UpdateHighlight(isPlayerInRange);
        }
    }

    void UpdateHighlight(bool highlight)
    {
        objectMaterial.color = highlight ? highlightColor : defaultColor;
    }

    void OnMouseDown()
    {
        ClickManager.clickBlocked = true;

        if (player == null) return;

        float distance = Vector3.Distance(startPosition, player.position);
        if (distance <= interactionRange)
        {
            if (manager != null) manager.Collect();
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Gracz jest zbyt daleko, interakcja zablokowana.");
        }
    }
}
