using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
  private Vector3 originalScale;

  public float pulseMagnitude = 0.1f; //determines how big it gets.
  public float pulseSpeed = 5f; //how quickly it expands and shrinks.

  private bool isHover = false;
  private float pulseTimer = 0f;

  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    originalScale = transform.localScale;
  }

  // Update is called once per frame
  void Update()
  {
    if (isHover) //if hover enable pulse effect.
    {
      pulseTimer += Time.deltaTime * pulseSpeed;
      float scaleOffset = Mathf.Sin(pulseTimer) * pulseMagnitude;
      transform.localScale = originalScale * (1f + scaleOffset);
    }
    else //if not, return to original scale.
    {
      transform.localScale = Vector3.Lerp(transform.localScale, originalScale, Time.deltaTime * 10f);
    }
  }

  public void OnPointerEnter(PointerEventData eventData)
  {
    isHover = true;
    pulseTimer = 0f; //resets pulse so it's smooth each time
  }

  public void OnPointerExit(PointerEventData eventData)
  {
    isHover = false;
  }
}
