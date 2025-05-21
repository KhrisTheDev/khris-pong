using UnityEngine;

public class SoundManager : MonoBehaviour
{
  public static SoundManager instance;

  public AudioClip paddleHitClip;
  public AudioClip wallBounceClip;
  public AudioClip buttonClickClip;
  public AudioClip goalClip;
  public AudioClip winClip;
  public AudioClip loseClip;

  private AudioSource audioSource;

  private void Awake()
  {
    if (instance == null)
      instance = this;
    else
      Destroy(gameObject);

    audioSource = GetComponent<AudioSource>();
  }

  public void PlaySound(AudioClip clip)
  {
    if (clip != null)
      audioSource.PlayOneShot(clip);
  }

  //Helper methods
  public void PlayPaddleHit() => PlaySound(paddleHitClip);
  public void PlayWallBounce() => PlaySound(wallBounceClip);
  public void PlayButtonClick() => PlaySound(buttonClickClip);
  public void PlayGoal() => PlaySound(goalClip);
  public void PlayWin() => PlaySound(winClip);
  public void PlayLose() => PlaySound(loseClip);
}
