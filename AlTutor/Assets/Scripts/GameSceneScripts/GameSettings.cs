using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSettings : MonoBehaviour
{
    private static int MENU_SCENE_CODE_NUMBER = 0;
    public static bool LoadMenuScene()
    {
        SceneManager.LoadScene(MENU_SCENE_CODE_NUMBER);
        UnLockMouse();
        return true;
    }

    public static bool PauseGame()
    {
        UnLockMouse();
        Time.timeScale = 0;
        return true;
    }
    public static bool ResumeFromPause()
    {
        LockMouse();
        Time.timeScale = 1;
        return true;
    }

    public static void Quit()
    {
        Application.Quit();
    }

    public static void LockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public static void UnLockMouse()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;  
    }
    
}
