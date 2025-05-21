using TMPro;
using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UIElements;

public class GameFlowManager : MonoBehaviour
{
  //Countdown variables
  public BallSpawner ballSpawner;
  public TextMeshProUGUI countdownText;
  public float countdownDuration = 3f;

  //Player variables
  public Transform playerPaddle;
  public Transform aiPaddle;
  private Vector3 playerStartPos;
  private Vector3 aiStartPos;

  //Game Over variables
  public TextMeshProUGUI endGameText;
  public bool gameOver = false;
  public GameObject returnToMenuButton;

  private void Start()
  {
    //stores starting positions
    playerStartPos = playerPaddle.position;
    aiStartPos = aiPaddle.position;

    //sets difficulty for AI
    string difficulty = PlayerPrefs.GetString("difficulty", "Normal");

    switch (difficulty)
    {
      case "Easy":
        aiPaddle.GetComponent<AIControls>().speed = 2.5f;
        aiPaddle.GetComponent<AIControls>().reactionTime = 0.06f;
        aiPaddle.GetComponent<AIControls>().aimOffset = 0.15f;
        break;

      case "Normal":
        aiPaddle.GetComponent<AIControls>().speed = 4f;
        aiPaddle.GetComponent<AIControls>().reactionTime = 0.04f;
        aiPaddle.GetComponent<AIControls>().aimOffset = 0.1f;
        break;

      case "Hard":
        aiPaddle.GetComponent<AIControls>().speed = 6f;
        aiPaddle.GetComponent<AIControls>().reactionTime = 0.02f;
        aiPaddle.GetComponent<AIControls>().aimOffset = 0.05f;
        break;
    }
  }

  public void StartCountdownAndRespawn()
  {
    StartCoroutine(CountdownRoutine());
  }

  private IEnumerator CountdownRoutine()
  {
    //prevent countdown if game is over
    if (gameOver)
      yield break;

    //freeze paddles by disabling movement scripts
    playerPaddle.GetComponent<PlayerControls>().enabled = false;
    aiPaddle.GetComponent<AIControls>().enabled = false;

    //reset positions
    playerPaddle.position = playerStartPos;
    aiPaddle.position = aiStartPos;

    //Show countdown
    countdownText.gameObject.SetActive(true);

    for (int i = (int)countdownDuration; i > 0; i--)
    {
      countdownText.text = i.ToString();
      countdownText.transform.localScale = Vector3.one * 0.5f;

      //Reset alpha to 0 before animating
      Color resetColor = countdownText.color;
      resetColor.a = 0f;
      countdownText.color = resetColor;

      //Animating countdown scaling up and fading in
      float animationTime = 0f;
      while (animationTime < 1f)
      {
        animationTime += Time.deltaTime * 5f;
        float scale = Mathf.Lerp(0.5f, 1f, animationTime);
        countdownText.transform.localScale = Vector3.one * scale;

        Color color = countdownText.color;
        color.a = Mathf.Lerp(0f, 1f, animationTime);
        countdownText.color = color;

        yield return null;
      }
      yield return new WaitForSeconds(0.55f); //ask about this

      //Countdown fading out
      while (animationTime < 1f)
      {
        animationTime += Time.deltaTime * 4f;
        Color color = countdownText.color;
        color.a = Mathf.Lerp(1f, 0f, animationTime);
        countdownText.color = color;

        yield return null;
      }

      if (gameOver)
      {
        countdownText.gameObject.SetActive(false);
        yield break; //stop mid-countdown if game ended
      }
    }

    countdownText.gameObject.SetActive(false);

    if (!gameOver)
    {
      //unfreeze paddles
      playerPaddle.GetComponent<PlayerControls>().enabled = true;
      aiPaddle.GetComponent<AIControls>().enabled = true;

      ballSpawner.RespawnBall();
    }
  }

  public void GameOver(bool playerWon)
  {
    gameOver = true;

    //stops paddles
    playerPaddle.GetComponent<PlayerControls>().enabled = false;
    aiPaddle.GetComponent<AIControls>().enabled = false;

    //show message
    if (playerWon == true)
    {
      SoundManager.instance.PlayWin();
      endGameText.text = "VICTORY";
      endGameText.color = Color.green;
    }
    else
    {
      SoundManager.instance.PlayLose();
      endGameText.text = "DEFEAT";
      endGameText.color = Color.red;
    }

    StartCoroutine(AnimateEndGameText());
    returnToMenuButton.SetActive(true);
  }

  private IEnumerator AnimateEndGameText()
  {
    endGameText.gameObject.SetActive(true);
    endGameText.transform.localScale = Vector3.one * 0.5f;

    Color color = endGameText.color;
    color.a = 0f;
    endGameText.color = color;

    float progress = 0f;
    while (progress < 1f)
    {
      progress += Time.deltaTime * 2f;

      float scale = Mathf.Lerp(0.5f, 1f, progress);
      endGameText.transform.localScale = Vector3.one * scale;

      color.a = Mathf.Lerp(0.5f, 1f, progress);
      endGameText.color = color;

      yield return null;
    }

    //Ensure it's exactly full at the end
    endGameText.transform.localScale = Vector3.one;
    color.a = 1f;
    endGameText.color = color;
  }
}
