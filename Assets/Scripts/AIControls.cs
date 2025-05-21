using UnityEngine;

public class AIControls : MonoBehaviour
{
  //to assign the ball in inspector
  public Transform ball;

  public float speed = 4f;
  public float minY = -4.5f;
  public float maxY = 4.5f;

  [HideInInspector] public float reactionTime = 0.05f; //how slaw AI is to react
  [HideInInspector] public float aimOffsetRange = 0.1f; //tweak for more/less error
  private float reactionTimer = 0f;
  public float aimOffset = 0f;

  public void SetBall(Transform newBall)
  {
    ball = newBall;
  }

  void Update()
  {
    //reaqcuire ball if it's missing
    if (ball == null) return;

    //Update reaction timer
    reactionTimer += Time.deltaTime;
    if (reactionTimer >= reactionTime) //when reactionTimer is equal to or greater than reactionTime
    {
      reactionTimer = 0f; //resets timer
      aimOffset = Random.Range(-aimOffsetRange, aimOffsetRange); //small, subtle offset
    }

    // Follow the ball's Y position.
    float targetY = ball.position.y + aimOffset;
    // Vector3 targetPosition = new Vector3(transform.position.x, ball.position.y, transform.position.z);
    Vector3 targetPosition = new Vector3(transform.position.x, targetY, transform.position.z);
    transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

    //Clamping Y position
    Vector3 clamped = transform.position;
    clamped.y = Mathf.Clamp(clamped.y, minY, maxY);
    transform.position = clamped;
  }
}
