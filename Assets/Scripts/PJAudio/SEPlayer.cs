﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DataManagement;
using ConstCollections.PJPaths;
using Common;

namespace PJAudio
{
  public class SEPlayer : SingletonObject<SEPlayer> 
  {
    public float Volume
    {
      set{this.audioSource.volume = value;}
      get{return this.audioSource.volume;}
    }

    #region UNITY_FUNCTION
    // Use this for initialization(self)
    protected override void Awake()
    {
      this.audioSource = GetComponent<AudioSource> ();
      this.audioSource.loop = false;
      base.Awake ();
    }

    void OnEnable()
    {
      UserData.Instance.SEVolumeChangedEvents += OnVolumeChanged;
    }

    // Use this for initialization(work with others)
    void Start () 
    {
      this.audioSource.volume = UserData.Instance.SEVolume;
      this.audioDataManager =  FindObjectOfType<AudioDataManager> ();
    }
      
    void OnDisable()
    {
      UserData.Instance.SEVolumeChangedEvents -= OnVolumeChanged;
    }
    #endregion

    public void Play(string filePath)
    {
      this.audioSource.PlayOneShot(this.audioDataManager.LoadCachedSE(filePath));
    }

    public void Play(AudioClip clip)
    {
      if (clip == null)
        return;
      
      this.audioSource.PlayOneShot(this.audioDataManager.LoadCachedSE(clip));
    }

    public void PlayRandomly(string[] filePathArray)
    {
      if (filePathArray == null)
        throw new System.NullReferenceException();

      if (filePathArray.Length == 0)
        throw new System.IndexOutOfRangeException ();

      Play (filePathArray [Random.Range (0, filePathArray.Length)]);
    }

    void OnVolumeChanged(float value)
    {
      this.Volume = value;
    }
    AudioSource audioSource;
    AudioDataManager audioDataManager;
  }
}
