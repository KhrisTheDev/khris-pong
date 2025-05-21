using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
  public TextMeshProUGUI playerScoreText;
  public TextMeshProUGUI aiScoreText;

  public void SetScore(int playerScore, int aiScore)
  {
    playerScoreText.text = playerScore.ToString();
    aiScoreText.text = aiScore.ToString();
  }

  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    SetScore(0, 0);
  }

  // Update is called once per frame
  void Update()
  {

  }
}
