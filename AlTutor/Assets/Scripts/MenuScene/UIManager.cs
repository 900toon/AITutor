using System.IO;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown accentDropdown;

    private void WriteInitializationDocument()
    {
        string directoryPath = Application.dataPath + @"/DataTransfer";

        /*
        //clean the folder
        DirectoryInfo directoryInfo = new DirectoryInfo(directoryPath);
        foreach(FileInfo file in directoryInfo.GetFiles())  file.Delete();
        */

        //write initialization txt
        using(StreamWriter writer = new StreamWriter(Path.Combine(directoryPath, "Initialization.txt")))
        {
            writer.WriteLine($"{accentDropdown.value}");
        }
    }
    private void SelectCharacters()
    {
        if (accentDropdown.value == 0) GameSettings.SelectCharacter(1);
        else GameSettings.SelectCharacter(0);

    }
    //buttons
    //start button
    public void StartButton()
    {
        WriteInitializationDocument();
        SelectCharacters();
        GameSettings.LoadGameScene();
    }

    public void QuitButton()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}
