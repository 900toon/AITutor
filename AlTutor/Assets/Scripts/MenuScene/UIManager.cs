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

    //buttons
    //start button
    public void StartButton()
    {
        GameSettings.LoadGameScene();
        WriteInitializationDocument();
    
    }

    public void QuitButton()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}
