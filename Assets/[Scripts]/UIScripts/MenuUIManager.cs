using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Title Sceen
    public void OnTitleScreenClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }

    //Main Menu
    public void OnPlayButtonPressed()
    {
        SceneManager.LoadScene("Opening");
    }


    //Opening
    public void OnSkipButtonPressed()
    {
        SceneManager.LoadScene("Hospital");
    }

    public void OnQuitButtonPressed()
    {
        Application.Quit();
    }

}
