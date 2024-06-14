using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;
using UnityEditor;

namespace TemplateFx
{
    [System.Serializable]
    public class SoundClass
    {
        public string soundName;
        public AudioClip soundSource;
    }

    public class SoundManager : Singleton<SoundManager>
    {
        private const string soundPath = "Assets/_Game/Audio/SoundEffects/";
        public List<SoundClass> listOfSoundClasses = new List<SoundClass>();
        private AudioSource audioSource;
        private bool isPlayedSound;
        private bool isPlayingSound;

        private void OnValidate()
        {
#if UNITY_EDITOR
            foreach (SoundClass soundClass in listOfSoundClasses)
            {

                if (soundClass.soundSource == null)
                {
                    string path = soundPath + soundClass.soundName + ".mp3";
                    Debug.Log(path);
                   soundClass.soundSource = AssetDatabase.LoadAssetAtPath<AudioClip>(path);
                }
            }
#endif

        }

        void Start()
        {
            if (audioSource == null)
            {
                AudioSource temporaryAudioSource = GetComponent<AudioSource>();
                if (temporaryAudioSource != null)
                {
                    audioSource = GetComponent<AudioSource>();
                }
                else
                {
                    Debug.Log("AudioSource is null (Template added AudioSource)");
                    audioSource = gameObject.AddComponent<AudioSource>();
                    audioSource.playOnAwake = false;

                }

            }
        }

        public void SoundPlay(string audioName)
        {
            if(isPlayingSound)
            {
                return;
            }
            isPlayedSound = false;
            foreach (SoundClass soundClass in listOfSoundClasses)
            {
                if (audioName == soundClass.soundName)
                {
                    if (soundClass.soundSource != null)
                    {
                        StartCoroutine(PlaySoundCoroutine(soundClass.soundSource));
                        isPlayedSound = true;
                    }
                    else
                    {
                        Debug.Log(audioName + " source is NULL");
                    }

                }
            }
            if (!isPlayedSound)
            {
                Debug.Log(">" + audioName + "< not valuable in list");
            }
        }

        private IEnumerator PlaySoundCoroutine(AudioClip clip)
        {
            isPlayingSound = true;
            audioSource.PlayOneShot(clip);
            yield return new WaitForSeconds(clip.length);
            isPlayingSound = false;
        }

        // An Example
        // private void Update()
        // {
        //     if(Input.GetKeyDown(KeyCode.C))
        //     {
        //         SoundPlay("Sound");
        //     }
        // }


    }
}


