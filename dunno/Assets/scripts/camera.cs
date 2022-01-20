using UnityEngine;

public class camera : MonoBehaviour
{

    public float mouseSens = 100f;
    public Transform player;
    float xRotation = 0f;



    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X")* mouseSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y")* mouseSens * Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        player.Rotate(Vector3.up* mouseX);
    }
}