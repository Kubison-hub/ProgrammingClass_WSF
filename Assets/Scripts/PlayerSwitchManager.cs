using UnityEngine;

public class PlayerSwitchManager : MonoBehaviour
{
    [Header("Postacie")]
    public GameObject[] players;

    [Header("Kamery")]
    public Camera cameraA;
    public Camera cameraB;

    [Header("Sterowanie")]
    public KeyCode switchKey = KeyCode.Tab;

    private int currentIndex = 0;
    private bool usingCameraA = true;

    void Start()
    {
        UpdateControl();
        UpdateCamera();
    }

    void Update()
    {
        if (Input.GetKeyDown(switchKey))
        {
            currentIndex = (currentIndex + 1) % players.Length;
            usingCameraA = !usingCameraA;

            UpdateControl();
            UpdateCamera();
        }
    }

    void UpdateControl()
    {
        for (int i = 0; i < players.Length; i++)
        {
            var movement = players[i].GetComponent<PlayerMovement>();
            if (movement != null)
                movement.enabled = (i == currentIndex);
        }
    }

    void UpdateCamera()
    {
        if (cameraA != null) cameraA.enabled = usingCameraA;
        if (cameraB != null) cameraB.enabled = !usingCameraA;
    }
}
