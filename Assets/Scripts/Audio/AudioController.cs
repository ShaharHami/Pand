using System;
using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    public enum AudioType
    {
        BGM,
        UI,
        PLAYER,
        OBSTACLE,
        POWERUP
    }
    [Serializable]
    public class AudioItem
    {
        public string name;
        public AudioType type;
        public AudioClip clip;
        [Range(0f, 1f)] public float volume = 1;
    }
    public class AudioController : MonoBehaviour
    {
        public static AudioController Instance;
        [SerializeField] private List<AudioItem> audioItems;
        [SerializeField] private AudioSource 
            uiAudioSource,
            playerAudioSource,
            obstacleAudioSource,
            bgmAudioSource,
            powerUpsAudioSource;

        private AudioSource currentSource;
        
        // Setting up the audio controller as a singleton so that it would carry on playing between scenes
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(this);
            }
        }

        public void PlayAudio(string item)
        {
            foreach (var audioItem in audioItems)
            {
                if (audioItem.name == item)
                {
                    PlayAudioItem(audioItem);
                }
            }

        }
        // Assign the audio source by the type
        private void PlayAudioItem(AudioItem item)
        {
            switch (item.type)
            {
                case AudioType.BGM:
                    // For BGM we want to attach it to the source and play it
                    if (!bgmAudioSource.isPlaying)
                    {
                        bgmAudioSource.clip = item.clip;
                        bgmAudioSource.Play();
                    }
                    return;
                case AudioType.UI:
                    currentSource = uiAudioSource;
                    break;
                case AudioType.PLAYER:
                    currentSource = playerAudioSource;
                    break;
                case AudioType.OBSTACLE:
                    currentSource = obstacleAudioSource;
                    break;
                case AudioType.POWERUP:
                    currentSource = powerUpsAudioSource;
                    break;
            }
            currentSource.volume = item.volume;
            currentSource.PlayOneShot(item.clip); 
        }
    }
}