using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    // Start is called before the first frame update
    private void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.panStereo = s.pan;
        }
    }

    public void playSoundByName(string name, float newPitch = 1f, bool varyPitch = false)
    {
        // if (Globals.soundsEnabled == false)
        // {
        //     return;
        // }
        Sound toPlay = Array.Find(sounds, x => x.name == name);

        if (varyPitch)
        {
            toPlay.source.pitch = .5f + .2f * UnityEngine.Random.Range(1, 6);
        }
        toPlay.source.Play();
    }
}
