using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManagerScript : MonoBehaviour
{
    [SerializeField] GameObject optionsPanel;
    [SerializeField] GameObject buttonsPanel;

    public bool isPaused = false;

    void Start()
    {
        Time.timeScale = 1.0f;
        CloseOptions();
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0 && Input.GetKeyDown(KeyCode.P))
        {
            TimeManager();
        }
    }

    public void NewGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Options()
    {
        buttonsPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void CloseOptions()
    {
        buttonsPanel.SetActive(true);
        optionsPanel.SetActive(false);
    }

    public void TimeManager()
    {
        if (isPaused)
        {
            Time.timeScale = 1;
            CloseOptions() ;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Time.timeScale = 0;
            Options();
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
        isPaused = !isPaused;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
