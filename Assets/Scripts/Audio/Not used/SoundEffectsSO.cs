using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SoundSystem")]
public class SoundEffectsSO : ScriptableObject
{
    public AudioClip[] clips;
    public Vector2 volume = new Vector2(0.5f, 0.5f);
    public Vector2 pitch = new Vector2(1, 1);

    public AudioSource Play(AudioSource audioSourceParam = null)
    {
        if (clips.Length == 0)
        {
            Debug.Log($"Missing sound clips for {name}");
            return null;
        }

        var source = audioSourceParam;
        if(source == null)
        {
            var obj = new GameObject("Sound", typeof(AudioSource));
            source = obj.GetComponent<AudioSource>();

            source.clip = clips[0];
            source.volume = Random.Range(volume.x, volume.y);
            source.pitch = Random.Range(pitch.x, pitch.y);

            source.Play();

            Destroy(source.gameObject, source.clip.length / source.pitch);

            return source;
        }

        return null;

    }
}
