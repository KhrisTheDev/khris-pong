using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
  public GameObject difficultyPanel;

  //Main Menu Options
  public void StartGame()
  {
    SoundManager.instance.PlayButtonClick();
    difficultyPanel.gameObject.SetActive(true);
  }

  public void QuitGame()
  {
    SoundManager.instance.PlayButtonClick();
    Application.Quit();
    Debug.Log("Game has Quitted");
  }

  public void DifficultySetting(string difficulty)
  {
    SoundManager.instance.PlayButtonClick();
    PlayerPrefs.SetString("difficulty", difficulty);
    SceneManager.LoadScene("GameScene");
  }

  //Other Menu Options
  public void ReturnToMenu()
  {
    SoundManager.instance.PlayButtonClick();
    SceneManager.LoadScene("MainMenu");
  }
}
