using System.IO;
public class FileFetcher
{
    private static string[] txtFileList;
    private static string path = GameSettings.GetResponseTextDirectory();
    private static string responseTxtContent = "";

    public static bool ReadContentFromDirectory_TxtFile()
    {
        if (Directory.Exists(path))  txtFileList = Directory.GetFiles(path,"*.txt");

        if (txtFileList.Length == 0) return false;

        responseTxtContent = GetTxtFileContent(txtFileList[0]);
        RemoveFile(txtFileList[0]);
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

    public static string GetResponseTxtContent()
    {
        return responseTxtContent;
    }
    

    
}
