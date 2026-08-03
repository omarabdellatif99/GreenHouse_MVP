using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AudioSource musicAS;
    [SerializeField] private AudioSource waterAS;
    [SerializeField] private AudioSource sfxAS;
    [SerializeField] private AudioClip interactSFX;
    public static AudioManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartWaterSound()
    {
        waterAS.Play();
    }

    public void PlaySFX()
    {
        sfxAS.PlayOneShot(interactSFX);
    }
}