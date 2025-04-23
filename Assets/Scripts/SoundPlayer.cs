using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    private AudioSource[] source;
    [SerializeField] LevelController levelController;

    void OnTriggerEnter(Collider other)
    {
        levelController.RemoveAllSounds();

        foreach (AudioSource source in gameObject.GetComponents<AudioSource>()){
            levelController.AddSound(source);
        }
        
    }
}
