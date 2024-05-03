using UnityEngine;

public class GameSceneLoader : MonoBehaviour
{
    //index 2 and 3 are characters for testing only
    [SerializeField] private GameObject[] CharacterPrefabs;
    [SerializeField] private GameObject[] EnvironmentPrefabs;
    [SerializeField] private GameObject[] CameraInQueue;

    private void Start()
    {
        LoadEnvironment();
        LoadCharacter();
        LoadCameraMode();
    }

    private void LoadCharacter()
    {
        //instantiate character prefab
        Instantiate(CharacterPrefabs[GameSettings.GetCharacterSelected()], new Vector3(7,0,10), Quaternion.identity);
    }
    private void LoadEnvironment()
    {
        
        if (EnvironmentPrefabs[GameSettings.GetEnvironmentSelected()].name == "Level")
        {
            Instantiate(EnvironmentPrefabs[GameSettings.GetEnvironmentSelected()], new Vector3(0, 0.25f, -20f), Quaternion.identity);

        }
        else Instantiate(EnvironmentPrefabs[GameSettings.GetEnvironmentSelected()]);

    }

    private void LoadCameraMode()
    {
        foreach (GameObject i in CameraInQueue) i.SetActive(false);
        //0 for pc 
        //1 for vr
        CameraInQueue[GameSettings.GetGameInputMode()].SetActive(true);
    }
}
