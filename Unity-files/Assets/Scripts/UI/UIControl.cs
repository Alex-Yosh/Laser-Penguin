using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIControl : MonoBehaviour
{
    [SerializeField] GameObject gameplayCanvas;
    [SerializeField] Text pointsText;

    bool isGameOverScreenShowing = false;

    public void ShowGameOverScreen()
    {
        transform.Find("BG").gameObject.SetActive(true);
        gameplayCanvas.SetActive(false);

        pointsText.text = "Score: " + GameplayUI.points;
        isGameOverScreenShowing = true;
    }

    private void Update()
    {
        if (isGameOverScreenShowing && Input.GetKeyDown(KeyCode.R))
        {
            isGameOverScreenShowing = false;
            OnRestartClick();
        } 
    }

    public void OnRestartClick()
    {
        SceneManager.LoadScene("Gameplay");
        Time.timeScale = 1;
    }

}
