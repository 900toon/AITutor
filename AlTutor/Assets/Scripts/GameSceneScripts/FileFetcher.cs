using System.IO;
using System.Text;
using UnityEngine;

public class FileFetcher : MonoBehaviour
{
    private string[] txtFileList;
    private string path = GameSettings.GetResponseTextDirectory();

    void Start()
    {
       
    }
    private void Update()
    {
        ReadContentFromDirectory_TxtFile();
        Test();
    }

    private bool ReadContentFromDirectory_TxtFile()
    {
        if (Directory.Exists(path))  txtFileList = Directory.GetFiles(path,"*.txt");

        if (txtFileList.Length == 0) return false;

        string content = GetTxtFileContent(txtFileList[0]);
        RemoveFile(txtFileList[0]);
        

        Debug.Log(content);

        return true;

    }

    private static string GetTxtFileContent(string filePath)
    {
        string fileContents = File.ReadAllText(filePath);
        return fileContents;
    }

    private static void RemoveFile(string filePath)
    {
        File.Delete(filePath);
    }

    private void Test()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            foreach (string i in txtFileList) Debug.Log(i);
        }
    }
}
