using UnityEngine;


public class FirstPersonCamera : MonoBehaviour
{
    private float cameraRotationX = 0f;
    private float cameraRotationY = 0f;

    // Update is called once per frame
    void Update()
    {
        HandleCameraRotation();
    }

    private void HandleCameraRotation()
    {

        float[] mouseInput = InputManager.GetMouseInput();
        cameraRotationX -= Mathf.Clamp(mouseInput[1], -90f, 90f);
        cameraRotationY += mouseInput[0];

        if (cameraRotationX >= 90) cameraRotationX = 90;
        else if (cameraRotationX <= -90) cameraRotationX = -90;

        Vector3 rotationVector3 = new Vector3(cameraRotationX, cameraRotationY, 0);
        transform.rotation = Quaternion.Euler(rotationVector3);
    }

    public Vector3 GetCameraForward()
    {
        return transform.forward;
    }
}
