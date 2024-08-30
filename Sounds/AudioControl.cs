using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControl : MonoBehaviour
{
    [SerializeField] private AudioClip bgmMusic;
    private AudioManeger audioM;
    void Start()
    {
        audioM = FindObjectOfType<AudioManeger>();
        audioM.PlayPGM(bgmMusic);
    }
}
