using UnityEngine;
using System.Collections;

public class SmoothToggleMove : MonoBehaviour
{
    public float moveDistance = 2f;
    public float moveDuration = 1f;
    public float interactionRange = 3f;
    public Color highlightColor = Color.yellow;
    public Color defaultColor = Color.white;

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private bool isAtStart = true;
    private bool isMoving = false;
    private bool isPlayerInRange = false;

    private Transform player;
    private Renderer objectRenderer;
    private Material objectMaterial;

    void Start()
    {
        startPosition = transform.position;
        targetPosition = startPosition + new Vector3(moveDistance, 0, 0);

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogWarning("Gracz (tag: Player) nie zosta³ znaleziony!");
        }

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
        if (highlight)
        {
            objectMaterial.color = highlightColor;
            // lub u¿yj: objectMaterial.SetColor("_EmissionColor", highlightColor);
        }
        else
        {
            objectMaterial.color = defaultColor;
            // lub: objectMaterial.SetColor("_EmissionColor", Color.black);
        }
    }

    void OnMouseDown()
    {
        ClickManager.clickBlocked = true;

        if (isMoving || player == null)
            return;

        float distance = Vector3.Distance(startPosition, player.position);
        if (distance <= interactionRange)
        {
            Vector3 destination = isAtStart ? targetPosition : startPosition;
            StartCoroutine(MoveToPosition(destination, moveDuration));
            isAtStart = !isAtStart;
        }
        else
        {
            Debug.Log("Gracz jest zbyt daleko, interakcja zablokowana.");
        }
    }

    IEnumerator MoveToPosition(Vector3 target, float duration)
    {
        isMoving = true;
        Vector3 start = transform.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            transform.position = Vector3.Lerp(start, target, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = target;
        isMoving = false;
    }
}
