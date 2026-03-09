using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    public GameObject Container;
    public GameObject loseContainer;
    public bool PlayerDead = false;
    public bool ShelterDestroyed = false;

    string LevelName;

    private void Update()
    {


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            Container.SetActive(true);

        }

        if (PlayerDead == true || ShelterDestroyed == true)
        {
            Time.timeScale = 0;
            loseContainer.SetActive(true);

        }
        LevelName = SceneManager.GetActiveScene().name;
    }

    public void ResumeUI()
    {
        Container.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void RestartUI()
    {
        SceneManager.LoadScene(LevelName);
        PlayerDead = false;
        ShelterDestroyed = false;
        Time.timeScale = 1.0f;
    }

    public void MainMenuUI()
    {
        PlayerDead = false;
        ShelterDestroyed = false;
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenu");
    }

}
