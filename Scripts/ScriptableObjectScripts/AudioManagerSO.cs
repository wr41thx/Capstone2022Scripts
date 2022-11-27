using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Audio Manager SO")]
public class AudioManagerSO : ScriptableObject
{
    // Class to hold audio clips that can be referenced by name 
    [System.Serializable]
    public class SoundFX
    {
        public string _clipName;
        public AudioClip _soundClip;
    }
 
    [SerializeField]
    public List<SoundFX> soundEffects = new();

    public void PlayAudio(string clip_name, Vector3 position, float volume = 1f)
    {
        AudioClip sound_clip = null;

        foreach (SoundFX sound in soundEffects)
        {
            if (clip_name == sound._clipName)
            {
                sound_clip = sound._soundClip;
            }
        }

        if (sound_clip)
        {
            
            // Plays audio at the specified position
            AudioSource.PlayClipAtPoint(sound_clip, position, volume);
        }
        else
        {
            Debug.Log("No sound clip found in 'soundEffects' list matching" + clip_name);
        }
    }
}
