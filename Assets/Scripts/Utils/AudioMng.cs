using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Utils
{
    public class AudioMng : MonoBehaviour
    {
        [SerializeField] private List<AudioClip> audioClips;
        private static AudioMng _audioMng;
        public static AudioMng GetInstance()
        {
            return _audioMng;
        }

        private AudioSource _audioSource;
        private void Start()
        {
            _audioMng = this;
            _audioSource = GetComponent<AudioSource>();
            foreach (var btn in Resources.FindObjectsOfTypeAll<Button>())
                btn.onClick.AddListener(() =>
                {
                    _audioSource.PlayOneShot(audioClips[0]);
                });
        }

        public void PlaySoundAsset(int idx)
        {
            _audioSource.PlayOneShot(audioClips[idx]);
        }

    }
}