using UnityEngine;

public class GameSceneLoader : MonoBehaviour
{
    //index 2 and 3 are characters for testing only
    [SerializeField] private GameObject[] CharacterPrefabs;
    [SerializeField] private GameObject[] EnvironmentPrefabs;

    private void Start()
    {
        LoadEnvironment();
        LoadCharacter();
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
}
