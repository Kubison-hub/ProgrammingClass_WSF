using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && gameObject.CompareTag("Guard"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
