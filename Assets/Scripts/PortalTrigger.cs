using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalTrigger : MonoBehaviour
{
    [SerializeField] private string nextSceneName = "Level 2"; // Ustaw nazwê sceny w Inspectorze lub tutaj

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Gracz wszed³ w portal — ³adowanie sceny: " + nextSceneName);
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
