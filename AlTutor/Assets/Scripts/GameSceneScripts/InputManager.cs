using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static float[] GetMouseInput()
    {
        // X+ : Right side, Y+ : Upward
        float inputX = Input.GetAxis("Mouse X");
        float inputY = Input.GetAxis("Mouse Y");

        return new float[2] { inputX, inputY };
    }

    public static Vector2 GetMovementInput()
    {
        Vector2 moveDir = new Vector2(0, 0);

        if (Input.GetKey(KeyCode.W)) moveDir += new Vector2(1, 0);
        if (Input.GetKey(KeyCode.S)) moveDir += new Vector2(-1, 0);
        if (Input.GetKey(KeyCode.D)) moveDir += new Vector2(0, 1);
        if (Input.GetKey(KeyCode.A)) moveDir += new Vector2(0, -1);

        return moveDir;
    }


}
