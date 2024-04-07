using UnityEngine;

public class GameSceneLoader : MonoBehaviour
{
    //index 2 and 3 are characters for testing only
    [SerializeField] private GameObject[] CharacterPrefabs;

    private void Start()
    {
        InitCharacter();
    }

    private void InitCharacter()
    {
        //instantiate character prefab
        Instantiate(CharacterPrefabs[GameSettings.GetCharacterSelected()], new Vector3(7,0,10), Quaternion.identity);
        
    }
}
