using UnityEngine;

public class LobbyCameraRotate : MonoBehaviour
{
    public float rotationRange = 30f;
    public float speed = 1f;

    public float mouseSensitivity = 3f;
    public float mouseClamp = 10f;
    public float mouseSmoothTime = 0.2f;

    private float startY;

    private float mouseTarget;
    private float mouseCurrent;
    private float mouseVelocity;

    void Start()
    {
        startY = transform.eulerAngles.y;
    }

    void Update()
    {
        // Otomatik sağ-sol dönüş
        float autoOffset = Mathf.PingPong(Time.time * speed, rotationRange);
        float autoAngle = autoOffset - rotationRange / 2f;

        // Mouse input
        float mouseX = Input.GetAxis("Mouse X");
        mouseTarget += mouseX * mouseSensitivity;
        mouseTarget = Mathf.Clamp(mouseTarget, -mouseClamp, mouseClamp);

        // Smooth takip (kasma yok)
        mouseCurrent = Mathf.SmoothDamp(
            mouseCurrent,
            mouseTarget,
            ref mouseVelocity,
            mouseSmoothTime
        );

        float finalAngle = startY + autoAngle + mouseCurrent;
        transform.rotation = Quaternion.Euler(0f, finalAngle, 0f);
    }
}
