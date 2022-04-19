using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIManager : MonoBehaviour
{
    public GameObject howToPlayPanel;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        howToPlayPanel.SetActive(false);
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
        Debug.Log("Play Button Pressed");
        StartCoroutine(TransitionToOpeningScene());
    }


    //Opening
    public void OnSkipButtonPressed()
    {
        Debug.Log("Skip Button Pressed");
        SceneManager.LoadScene("Hospital");
    }

    public void OnCreditsButtonPressed()
    {
        SceneManager.LoadScene("Credits");
    }

    public void OnCreditsBackButtonPressed()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnRetryButtonPressed()
    {
        SceneManager.LoadScene("Hospital");
    }

    public void OnMenuButtonPressed()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void OnHowToPlayButtonPressed()
    {
        howToPlayPanel.SetActive(true);
    }

    public void OnHowToPlayBackButtonPressed()
    {
        howToPlayPanel.SetActive(false);
    }

    public void OnQuitButtonPressed()
    {
        Application.Quit();
    }


    //public IEnumerator DelayForFadeFromBlack()
    //{
    //    yield return new WaitForSeconds(2f);
    //    SceneManager.LoadScene("Opening");

    //}

    public IEnumerator TransitionToOpeningScene()
    {
        //yield return new WaitForSeconds(2f);
        animator.SetBool("isPlayButtonPressed", true);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Opening");
        yield return new WaitForSeconds(70f);
        SceneManager.LoadScene("Hospital");
    }

}
