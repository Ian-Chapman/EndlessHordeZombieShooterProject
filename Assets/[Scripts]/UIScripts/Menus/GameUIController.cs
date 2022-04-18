using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameUIController : MonoBehaviour
{
    PlayerController playerController;
    MovementComponent movementComponent;

    [SerializeField] private GameHUDWidget GameCanvas;
    //[SerializeField] private GameHUDWidget PauseCanvas;
    [SerializeField] private GameHUDWidget InventoryCanvas;

    private GameHUDWidget ActiveWidget;
    GameObject player;

    public GameObject cureButton;
    public GameObject pausePanel;
    public GameObject pauseButton;
    CountdownTimerComponent countdownTimerComponent;
    public int numCured;
    public int healthyPatientsRemaining;
    public TextMeshProUGUI curedText;
    public TextMeshProUGUI healthyText;

    private void Start()
    {
        movementComponent = GameObject.Find("Player").GetComponent<MovementComponent>();
        playerController = GetComponent<PlayerController>();
        DisableAllMenus();
        EnableGameMenu();
        player = GameObject.Find("Player");
        Cursor.lockState = CursorLockMode.Confined;
        pausePanel.SetActive(false);
    }

    public void Update()
    {
        if (healthyPatientsRemaining < 14)
        {
            //game over
        }

        if (numCured >= 14)
        {
            //game win
        }
    }

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
            movementComponent.aimSensitivity = 4;
            player.GetComponent<PlayerController>().characterOverlap.GetComponentInChildren<CountdownTimerComponent>().isCured = true;
            Debug.Log("Cure button pressed");
            Destroy(player.GetComponent<PlayerController>().characterOverlap.GetComponentInChildren<CountdownTimerComponent>().timerText);
            player.GetComponent<PlayerController>().characterOverlap.GetComponentInChildren<CountdownTimerComponent>().healingTriggerBox.enabled = false;
            cureButton.SetActive(false);
            numCured++;
            curedText.text = numCured.ToString();
            Debug.Log("Cure Button Pressed");
        }
    }

    public void OnPauseButtonPressed()
    {
        Time.timeScale = 0;
        movementComponent.aimSensitivity = 0;
        pausePanel.SetActive(true);
        pauseButton.SetActive(false);
    }

    public void OnResumeButtonPressed()
    {
        Time.timeScale = 1;
        movementComponent.aimSensitivity = 4;
        pausePanel.SetActive(false);
        pauseButton.SetActive(true);
    }

    public void OnMainMenuButtonPressed()
    {
        movementComponent.aimSensitivity = 4;
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