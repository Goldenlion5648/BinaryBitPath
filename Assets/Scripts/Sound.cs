using UnityEngine;

[System.Serializable]
public class Sound
{
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 1;
    [Range(0f, 2f)]
    public float pitch = 1;
    [Range(-1f, 1f)]
    public float pan = 1;
    public string name;

    [HideInInspector]
    public AudioSource source;
}