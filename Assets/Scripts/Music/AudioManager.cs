using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    static public AudioManager instance;
    private List<GameObject> activeAudioGameObjects;
    private GameObject currentAudioOnLoop;

    void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            activeAudioGameObjects = new List<GameObject>();
            DontDestroyOnLoad(gameObject);
        }
    }

    // volume: [0, 1]

    public AudioSource PlayAudio(AudioClip clip, float volume = 1)
    {

        GameObject sourceObj = new GameObject(clip.name);
        activeAudioGameObjects.Add(sourceObj);
        sourceObj.transform.SetParent(this.transform);
        AudioSource source = sourceObj.AddComponent<AudioSource>();
        source.clip = clip;
        source.volume = volume;
        source.Play();
        StartCoroutine(PlayAudio(source));
        return source;
    }

    public AudioSource PlayAudio3D(AudioClip clip, Vector3 position, float volume = 1)
    {
        AudioSource source = PlayAudio(clip, volume);
        source.spatialBlend = 1;
        source.gameObject.transform.position = position;
        return source;
    }



    public AudioSource PlayAudioOnLoop(AudioClip clip, float volume = 1)
    {
        if (currentAudioOnLoop)
        {
            Destroy(currentAudioOnLoop);
        }
        GameObject sourceObj = new GameObject(clip.name);
        activeAudioGameObjects.Add(sourceObj);
        sourceObj.transform.SetParent(this.transform);
        AudioSource source = sourceObj.AddComponent<AudioSource>();
        source.clip = clip;
        source.volume = volume;
        source.loop = true;
        source.Play();
        currentAudioOnLoop = sourceObj;
        return source;
    }

    public void ClearAudioList()
    {
        foreach (GameObject go in activeAudioGameObjects)
        {
            Destroy(go);
        }
        activeAudioGameObjects.Clear();
    }

    IEnumerator PlayAudio(AudioSource source)
    {
        while (source && source.isPlaying)
        {
            yield return null;
        }

        if (source)
        {
            activeAudioGameObjects.Remove(source.gameObject);
            Destroy(source.gameObject);
        }
    }
}
