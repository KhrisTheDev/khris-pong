using UnityEngine;

public class ScoreManager : MonoBehaviour
{
  public static ScoreManager instance;

  public int playerScore;
  public int aiScore;

  public int winScore = 3;

  public ScoreUI scoreUI;
  public GameFlowManager gameFlowManager;

  private void Awake()
  {
    if (instance == null)
      instance = this; //explain this.
    else
      Destroy(gameObject);
  }

  public void PlayerScored()
  {
    playerScore++;
    scoreUI.SetScore(playerScore, aiScore);

    if (playerScore >= winScore)
    {
      gameFlowManager.GameOver(true);
    }
  }

  public void AIScored()
  {
    aiScore++;
    scoreUI.SetScore(playerScore, aiScore);

    if (aiScore >= winScore)
    {
      gameFlowManager.GameOver(false);
    }
  }
}
