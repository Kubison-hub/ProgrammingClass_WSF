using UnityEngine;
using UnityEngine.AI;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class TriggerMover : MonoBehaviour
{
    [Header("Obiekt do poruszenia")]
    public Transform targetObject;

    [Header("Ruch")]
    public Vector3 moveDirection = Vector3.up;
    public float moveDistance = 1f;
    public float stayTime = 2f;
    public float moveDuration = 0.5f;

    [Header("Interakcja")]
    public float interactionDistance = 5f;

    private Vector3 initialPosition;
    private bool isMoving = false;
    private Transform player;

    private NavMeshObstacle wallObstacle;

    void Start()
    {
        if (targetObject != null)
        {
            initialPosition = targetObject.position;
            wallObstacle = targetObject.GetComponent<NavMeshObstacle>();

            if (wallObstacle != null)
            {
                wallObstacle.carving = true;
                wallObstacle.carveOnlyStationary = false; // upewnij siê ¿e mo¿e carvingowaæ podczas ruchu
                wallObstacle.enabled = true; // Na starcie zablokowane
            }
        }

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;
    }

    void OnMouseDown()
    {
        ClickManager.clickBlocked = true;

        if (isMoving || targetObject == null || player == null)
            return;

        float distance = Vector3.Distance(transform.position, player.position);
        if (distance > interactionDistance)
            return;

        StartCoroutine(MoveAndStay());
    }

    IEnumerator MoveAndStay()
    {
        isMoving = true;

        CloseWall(); // zablokuj dostêp

        Vector3 targetPosition = initialPosition + moveDirection.normalized * moveDistance;
        Vector3 startPos = targetObject.position;
        float time = 0f;

        while (time < moveDuration)
        {
            targetObject.position = Vector3.Lerp(startPos, targetPosition, time / moveDuration);
            time += Time.deltaTime;
            yield return null;
        }

        targetObject.position = targetPosition;

        yield return new WaitForSeconds(stayTime);

        StartCoroutine(BackToStart());
    }

    IEnumerator BackToStart()
    {
        Vector3 startPos = targetObject.position;
        float time = 0f;

        while (time < moveDuration)
        {
            targetObject.position = Vector3.Lerp(startPos, initialPosition, time / moveDuration);
            time += Time.deltaTime;
            yield return null;
        }

        targetObject.position = initialPosition;

        CloseWall(); // znowu zablokuj NavMesh
        isMoving = false;
    }

    void CloseWall()
    {
        if (wallObstacle != null)
            StartCoroutine(ResetCarving());
    }

    IEnumerator ResetCarving()
    {
        wallObstacle.enabled = false;
        yield return new WaitForSeconds(0.05f); // krótka przerwa by Unity zresetowa³ carving
        wallObstacle.carving = true;
        wallObstacle.carveOnlyStationary = false;
        wallObstacle.enabled = true;
    }

    void OpenWall()
    {
        if (wallObstacle != null)
        {
            wallObstacle.enabled = false;
            wallObstacle.carving = false;
        }
    }
}
