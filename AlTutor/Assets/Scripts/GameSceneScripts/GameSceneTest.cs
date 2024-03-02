using UnityEngine;
using System.IO;

public class GameSceneTest : MonoBehaviour
{
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) Debug.Log("Testing:  input test successed");
    }
}
