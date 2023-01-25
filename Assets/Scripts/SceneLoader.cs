using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public int currentSceneIndex;

    private void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }
    public void LoadNextScene()
    {
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void LoadStartScene()
    {
        SceneManager.LoadScene(0);
        FindObjectOfType<GameSession>().ResetGame();
    }
    public void LoadLevel1()
    {
        SceneManager.LoadScene(1);
    }

    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void LoadLevel6()
    {
        SceneManager.LoadScene(7);
    }

    public void LoadCreditScene()
    {
        SceneManager.LoadScene(5);
    }

    public void LoadGameOverScreen()
    {
        SceneManager.LoadScene("Game Over");
        Cursor.visible = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
