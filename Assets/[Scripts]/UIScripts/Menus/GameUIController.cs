using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIController : MonoBehaviour
{
    PlayerController playerController;

    [SerializeField] private GameHUDWidget GameCanvas;
    //[SerializeField] private GameHUDWidget PauseCanvas;
    [SerializeField] private GameHUDWidget InventoryCanvas;

    private GameHUDWidget ActiveWidget;
    GameObject player;

    public GameObject cureButton;
    public GameObject pausePanel;
    public GameObject pauseButton;
    CountdownTimerComponent countdownTimerComponent;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        DisableAllMenus();
        EnableGameMenu();
        player = GameObject.Find("Player");
        Cursor.lockState = CursorLockMode.Confined;
        pausePanel.SetActive(false);
    }

    //public void EnablePauseMenu()
    //{
    //    if (ActiveWidget) ActiveWidget.DisableWidget();

    //    //ActiveWidget = PauseCanvas;
    //    ActiveWidget.EnableWidget();
    //}

    public void EnableGameMenu()
    {
        if (ActiveWidget) ActiveWidget.DisableWidget();

        ActiveWidget = GameCanvas;
        ActiveWidget.EnableWidget();
    }

    public void EnableInventoryMenu()
    {
        if (ActiveWidget) ActiveWidget.DisableWidget();

        ActiveWidget = InventoryCanvas;
        ActiveWidget.EnableWidget();
    }

    public void DisableAllMenus()
    {
        GameCanvas.DisableWidget();
        //PauseCanvas.DisableWidget();
        InventoryCanvas.DisableWidget();
    }

    public void OnCureButtonPressed()
    {
        if (player.GetComponent<PlayerController>().characterOverlap != null && 
            player.GetComponent<PlayerController>().characterOverlap.tag == "CountdownTimer")
        {
            player.GetComponent<PlayerController>().characterOverlap.GetComponentInChildren<CountdownTimerComponent>().isCured = true;
            Debug.Log("Cure button pressed");
            Destroy(player.GetComponent<PlayerController>().characterOverlap.GetComponentInChildren<CountdownTimerComponent>().timerText);
            player.GetComponent<PlayerController>().characterOverlap.GetComponentInChildren<CountdownTimerComponent>().healingTriggerBox.enabled = false;
            cureButton.SetActive(false);
        }
    }

    public void OnPauseButtonPressed()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        pauseButton.SetActive(false);
        playerController.isFiring = false;
    }

    public void OnResumeButtonPressed()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        pauseButton.SetActive(true);
    }

    public void OnMainMenuButtonPressed()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

}

public abstract class GameHUDWidget : MonoBehaviour
{
    public virtual void EnableWidget() 
    {
        gameObject.SetActive(true);
    }
    public virtual void DisableWidget()
    {
        gameObject.SetActive(false);
    }

}