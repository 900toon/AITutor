using UnityEngine;

public class QuitGame : MonoBehaviour
{
    private void OnApplicationQuit()
    {
        string path = Application.dataPath + "/BatFiles/KillPythonAPI.bat";
        try { System.Diagnostics.Process.Start(path); }
        catch {}
        
    }
}
