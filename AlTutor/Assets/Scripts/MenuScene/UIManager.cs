using System.IO;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown accentDropdown;
    [SerializeField] private TMP_Dropdown characterDropdown;
    [SerializeField] private TMP_Dropdown environmentDropdown;
    [SerializeField] private TMP_Dropdown chooseAIPromptDropdown;
    [SerializeField] private Toggle loverModeToggle;
    [SerializeField] private Toggle vrModeToggle;

    private void WriteInitializationDocument()
    {
        string directoryPath = Application.dataPath + @"/DataTransfer";
        int loverModeToggleValue = 0;

        if (loverModeToggle.isOn) loverModeToggleValue = 1;
        else loverModeToggleValue = 0;
        //write initialization txt
        using(StreamWriter writer = new StreamWriter(Path.Combine(directoryPath, "Initialization.txt")))
        {
            writer.WriteLine($"accent: {accentDropdown.value}");
            writer.WriteLine($"PromptMode: {chooseAIPromptDropdown.value}");
        }
    }
    private void SelectCharacters()
    {
        GameSettings.SelectCharacter(characterDropdown.value);
    }
    private void SelectEnvironment()
    {
        GameSettings.SelectEnvironment(environmentDropdown.value);

    }
    //buttons
    //start button
    public void StartButton()
    {
        WriteInitializationDocument();
        SelectCharacters();
        SelectEnvironment();
        SetCameraMode();
        GameSettings.LoadGameScene();
    }

    public void QuitButton()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

    private int cameraMode = 0;
    private void VRModeToggle()
    {
        if (vrModeToggle.isOn) cameraMode = 1;
        else cameraMode = 0;
    }

    private void SetCameraMode()
    {
        VRModeToggle();
        GameSettings.SetGameInputMode(cameraMode);
    }
}
