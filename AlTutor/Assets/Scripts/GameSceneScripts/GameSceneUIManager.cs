using UnityEngine;

public class GameSceneUIManager : MonoBehaviour
{
    [SerializeField] private GameObject gamePauseCanvas;
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

    private void Update()
    {
        ToggleGamePauseUI();
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
