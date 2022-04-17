using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIController : MonoBehaviour
{

    [SerializeField] private GameHUDWidget GameCanvas;
    //[SerializeField] private GameHUDWidget PauseCanvas;
    [SerializeField] private GameHUDWidget InventoryCanvas;

    private GameHUDWidget ActiveWidget;
    GameObject player;

    public GameObject cureButton;
    CountdownTimerComponent countdownTimerComponent;

    

    private void Start()
    {
        DisableAllMenus();
        EnableGameMenu();
        player = GameObject.Find("Player");
        Cursor.lockState = CursorLockMode.Confined;
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