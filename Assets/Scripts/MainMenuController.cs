using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject instructionsPanel;

    public void PlayGame ()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void ShowInstructions ()
    {
        mainPanel.SetActive(false);
        instructionsPanel.SetActive(true);
    }

    public void HideInstructions ()
    {
        instructionsPanel.SetActive(false);
        mainPanel.SetActive(true);
        Debug.Log("Click");
    }

    public void ExitGame ()
    {
        Application.Quit();
    }
    
}
