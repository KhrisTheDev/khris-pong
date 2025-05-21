using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    private Vector2 moveInput;
    public float moveSpeed = 5f;
    public float minY = -4.5f;
    public float maxY = 4.5F;

    // Update is called once per frame
    void Update()
    {
      Vector2 movement = moveInput * moveSpeed * Time.deltaTime;
      transform.Translate(movement);

      //Clamps Y to stay on screen
      Vector2 clamped = transform.position;
      clamped.y = Mathf.Clamp(clamped.y, minY, maxY);
      transform.position = clamped;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
      moveInput = context.ReadValue<Vector2>();
    }
}
