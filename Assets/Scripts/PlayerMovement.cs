using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // Jeœli klikniêcie zosta³o chwilowo zablokowane (np. animacja œciany), nie wykonuj ruchu
        if (ClickManager.clickBlocked)
            return;

        // Sprawdzenie klikniêcia myszk¹
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                Vector3 clickPoint = hit.point;

                // Utworzenie i sprawdzenie œcie¿ki
                NavMeshPath path = new NavMeshPath();
                if (NavMesh.CalculatePath(agent.transform.position, clickPoint, NavMesh.AllAreas, path))
                {
                    if (path.status == NavMeshPathStatus.PathComplete)
                    {
                        agent.SetDestination(clickPoint);
                    }
                    else
                    {
                        Debug.Log("Brak dostêpnej œcie¿ki – cel zablokowany.");
                    }
                }
            }
        }
    }
}
