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

    public static bool GetGamePauseInput()
    {
        //KeyCode is going to be "Escape" eventually
        // using "P" as temporary pause button to avoid confliction between unity's own escape key
        if (Input.GetKeyDown(KeyCode.P)) return true;

        return false;
    }


}
