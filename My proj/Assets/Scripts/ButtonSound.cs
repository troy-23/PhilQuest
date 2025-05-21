using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    public AudioClip clickSound;

    public void PlaySound()
    {
        if (clickSound != null)
        {
            // ✅ Create a new GameObject just for playing sound
            GameObject tempGO = new GameObject("TempAudio");
            AudioSource audioSource = tempGO.AddComponent<AudioSource>();

            audioSource.clip = clickSound;
            audioSource.playOnAwake = false;
            audioSource.loop = false;

            // ✅ Make it 2D so it's not affected by position
            audioSource.spatialBlend = 0f;

            audioSource.Play();

            // ✅ Destroy the temp GameObject after clip finishes
            Destroy(tempGO, clickSound.length);
        }
    }
}
