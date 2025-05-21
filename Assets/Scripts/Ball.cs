using UnityEngine;

public class Ball : MonoBehaviour
{
  public float speed = 5f;
  public float maxSpeed = 12f;
  private float currentSpeed;
  private Rigidbody2D rb;

  void Start()
  {
    rb = GetComponent<Rigidbody2D>();
    currentSpeed = speed;
    Launch();
  }

  void Launch()
  {
    float x;
    float y;

    if (Random.value < 0.5f)
      x = -1f;
    else
      x = 1f;

    y = Random.Range(-.5f, 0.5f);

    Vector2 direction = new Vector2(x, y).normalized;
    rb.linearVelocity = direction * speed;
  }

  void FixedUpdate()
  {
    Vector2 velocity = rb.linearVelocity;

    //if Y's velocity is too close to 0, nudge it slightly
    if (Mathf.Abs(velocity.y) < 0.1f)
    {
      velocity.y += Random.Range(-0.3f, 0.3f);
      rb.linearVelocity = velocity.normalized * speed;
    }
  }

  void OnCollisionEnter2D(Collision2D collision)
  {
    //gradually increase speed
    currentSpeed = Mathf.Min(currentSpeed + 0.25f, maxSpeed);

    //Normalize and apply updated speed
    Vector2 velocity = rb.linearVelocity.normalized * currentSpeed;

    //Prevent horizontal trapping
    float minY = 0.3f; //minimum vertical component
    if (Mathf.Abs(velocity.y) < minY)
    {
      float sign = Mathf.Sign(velocity.y);

      if (sign == 0)
        velocity.y = Random.Range(-1f, 1f) * minY;
      else
        velocity.y = sign * minY;

      velocity = velocity.normalized * currentSpeed;
    }

    rb.linearVelocity = velocity;

    //PLays sound depending on what was hit.
    if (collision.gameObject.CompareTag("Player"))
    {
      SoundManager.instance.PlayPaddleHit();
    }

    if (collision.gameObject.CompareTag("Wall"))
    {
      SoundManager.instance.PlayWallBounce();
    }
  }
}
