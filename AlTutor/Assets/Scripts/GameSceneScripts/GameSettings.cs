using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class GameSettings : MonoBehaviour
{
    private static int MENU_SCENE_CODE_NUMBER = 0;
    private static int GAME_SCENE_CODE_NUMBER = 1;
    
    public static bool LoadMenuScene()
    {
        SceneManager.LoadScene(MENU_SCENE_CODE_NUMBER);
        UnLockMouse();
        return true;
    }
    public static bool LoadGameScene()
    {
        SceneManager.LoadScene(GAME_SCENE_CODE_NUMBER);
        LockMouse();
        Time.timeScale = 1;
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
    
    public static string GetResponseTextDirectory()
    {
        return Application.dataPath + @"/DataTransfer/ServerOutput_Text";
    }
    public static string GetResponseSoundDirectory()
    {
        return Application.dataPath + @"/DataTransfer/ServerOutput_Sound";
    }
    public static string GetPlayerInputDirectory()
    {
        return Application.dataPath + @"/DataTransfer/PlayerInput";
    }

    public static bool IfPlayerInputFolderIsEmpty()
    {
        if (Directory.GetFiles(GetPlayerInputDirectory()).Length == 0) return true;

        return false;
    }

    //choose which character to load due to the selection from initialization
    private static int CharacterSelected = 0;
    public static void SelectCharacter(int i)
    {
        CharacterSelected = i;
    } 

    public static int GetCharacterSelected()
    {
        return CharacterSelected;
    }

    //choose which environment is going to be loaded
    private static int EnvironmentSelected = 0;

    public static void SelectEnvironment(int i)
    {
        //0 for space chamber
        //1 for pixel medieval room
        EnvironmentSelected = i;
    }
    public static int GetEnvironmentSelected()
    {
        return EnvironmentSelected;
    }

    //switch between vr and pc mode
    private static int GameInputMode = 0;
    public static void SetGameInputMode(int mode)
    {
        GameInputMode = mode;
    }
    public static int GetGameInputMode()
    {
        return GameInputMode;
    }

    //temp variable for ForceToStayFunction
    public static bool NPCForceToStay = false;
    public static void SwitchBetweenNPCForToStay()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            if (NPCForceToStay == false)
            {
                NPCForceToStay = true;
            }
            else
            {
                NPCForceToStay = false;
            }
            
        }
    }
}
