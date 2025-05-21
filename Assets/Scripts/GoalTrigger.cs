using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.CompareTag("Ball"))
    {
      if (gameObject.name == "LeftGoal")
      {
        ScoreManager.instance.AIScored();
      }
      else if (gameObject.name == "RightGoal")
      {
        ScoreManager.instance.PlayerScored();
      }
      SoundManager.instance.PlayGoal();

      Destroy(collision.gameObject);
      FindFirstObjectByType<GameFlowManager>().StartCountdownAndRespawn();
    }
  }
}
