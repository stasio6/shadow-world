using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    // TODO: Reloading first screen makes more audio objects
    private static Audio _instance;
    public static Audio Instance()
    {
        return _instance;
    }

    public AudioClip[] soundtrackClips;
    private Dictionary<string, int> soundtrack;
    private Dictionary<int, float> soundtrackVolume;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        if (_instance != null)
        {
            Destroy(this);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(this);

        audioSource = GetComponent<AudioSource>();

        soundtrack = new Dictionary<string, int>()
        {
            { "Main Menu",       0 },
            { "Level Select",    0 },
            { "Level1-1",        1 },
            { "Level1-2",        1 },
            { "Level1-3",        1 },
            { "Level1-4",        1 },
            { "Level1-5",        2 },
            { "Level1-6",        2 },
            { "Level1-7",        2 },
            { "Level1-8",        2 },
            { "Victory Credits", 5 },
        };

        soundtrackVolume= new Dictionary<int, float>()
        {
            { 0, 1 },
            { 1, 0.5f },
            { 2, 0.5f },
            { 3, 0.5f },
            { 4, 0.5f },
            { 5, 1 },
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateSoundtrack(string level)
    {
        if (soundtrack.ContainsKey(level) && audioSource.clip.name != soundtrackClips[soundtrack[level]].name)
        {
            audioSource.clip = soundtrackClips[soundtrack[level]];
            audioSource.volume = soundtrackVolume[soundtrack[level]];
            audioSource.Play();
        }
    }
}
