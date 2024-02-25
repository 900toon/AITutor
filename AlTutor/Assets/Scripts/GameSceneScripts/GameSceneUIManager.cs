using UnityEngine;
using TMPro;
public class GameSceneUIManager : MonoBehaviour
{
    [SerializeField] private GameObject gamePauseCanvas;
    [SerializeField] private TMP_Text isRecordingSignal;
    private bool IsGamePaused = false;
    private void ToggleGamePauseUI()
    {
        if (InputManager.GetGamePauseInput())
        {
            if (!IsGamePaused)
            {
                IsGamePaused = true;
                gamePauseCanvas.SetActive(true);
                GameSettings.PauseGame();
            }
            else
            {
                IsGamePaused = false;
                gamePauseCanvas.SetActive(false);
                GameSettings.ResumeFromPause();
            }
        }
        
    }

    private void ToggleIsRecordingSignalUI()
    {
        if (Input.GetKeyDown(KeyCode.R)) isRecordingSignal.color = Color.red;
        if (Input.GetKeyUp(KeyCode.R)) isRecordingSignal.color = Color.black;
    }

    private void Update()
    {
        ToggleGamePauseUI();
        ToggleIsRecordingSignalUI();
    }



    //=================================UI Button=========================================>

    public void ResumeButton()
    {
        Debug.Log("GameSceneUIManager: Resume");
        GameSettings.ResumeFromPause();
        IsGamePaused = false;
        gamePauseCanvas.SetActive(false);
        
    }

    public void BackToMenuButton()
    {
        GameSettings.LoadMenuScene();
;    }

    public void QuitButton()
    {
        GameSettings.Quit();
    }
}
