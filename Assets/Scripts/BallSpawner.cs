using UnityEngine;

public class BallSpawner : MonoBehaviour
{
  public GameObject ballPrefab;
  public AIControls aiPaddle;

  private void Start()
  {
    RespawnBall();
  }

  public void RespawnBall()
  {
    GameObject ball = Instantiate(ballPrefab, transform.position, Quaternion.identity);
    aiPaddle.SetBall(ball.transform);
  }
}
