using UnityEngine;
using UnityEngine.UI;

public class HandleNPC_UI : MonoBehaviour
{
    [SerializeField] private Text temp;
    [SerializeField] private GameObject panel;

    private void Update()
    {
        temp.text = FileFetcher.GetResponseTxtContent();
    }

    private void Start()
    {
        if (GameSettings.GetGameInputMode() == 0) {
            temp.gameObject.SetActive(false);
            panel.SetActive(false);
        }
    }
}
