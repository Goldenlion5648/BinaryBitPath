using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    List<string> soundNamesPlayed = new List<string>();
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
        print("about to play sound" + name);
        soundNamesPlayed.Add(name);
        if (name == "reset")
        {
            soundNamesPlayed.Clear();
        }
        if (name == "levelComplete")
        {
            // print("sequence used:");
            // Array.ForEach(soundNamesPlayed.ToArray(), x => print(x));
            soundNamesPlayed.RemoveAt(soundNamesPlayed.Count - 1);
            print("sequence used:\n" + String.Join("\n", soundNamesPlayed));
            soundNamesPlayed.Clear();

        }
        Sound toPlay = Array.Find(sounds, x => x.name == name);

        if (varyPitch)
        {
            toPlay.source.pitch = .5f + .2f * UnityEngine.Random.Range(1, 6);
        }
        toPlay.source.Play();
    }
}
