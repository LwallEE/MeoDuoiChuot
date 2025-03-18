using System;
using System.Collections;
using System.Collections.Generic;
using ReuseSystem;
using ReuseSystem.ObjectPooling;
using UnityEngine;

namespace ReuseSystem.AudioSystem
{
   public class SoundPlayer : MonoBehaviour
   {
      private AudioSource source;
      private AudioInfo currentAudioInfo;
      private void Awake()
      {
         source = GetComponent<AudioSource>();
      }

      public void InitData(AudioInfo audioInfo)
      {
         this.currentAudioInfo = audioInfo;
         source.loop = audioInfo.isLoop;
         source.clip = audioInfo.TakeRandom();
         source.volume = audioInfo.volume * AudioManager.Instance.SoundVolume;

         if ((audioInfo.isLoop && AudioManager.Instance.IsPlayingMusic) ||
             (!audioInfo.isLoop && AudioManager.Instance.IsPlaySfx))
         {
            source.Play();
         }
         else
         {
            source.Stop();
         }
      }

      public void UpdateSoundVolume()
      {
         source.volume = currentAudioInfo.volume * AudioManager.Instance.SoundVolume;

      }

      public bool IsPlayingClip(AudioClip clip)
      {
         return source.clip == clip && gameObject.activeSelf;
      }

      public void StopMusic()
      {
         source.Stop();
         AudioManager.Instance.RemoveSoundPlay(this.currentAudioInfo);
         LazyPool.Instance.AddObjectToPool(gameObject);
      }

      public void ActiveMusicShortly(bool value)
      {
         if (!value)
         {
            source.Stop();
         }
         else
         {
            source.Play();
         }
      }

      private void FixedUpdate()
      {
         //automatic add to pool if it is play one shot, after it finish play
         if (!source.loop)
         {
            //check if it has finished play
            if (!source.isPlaying)
            {
               LazyPool.Instance.AddObjectToPool(gameObject);
               AudioManager.Instance.RemoveSoundPlay(this.currentAudioInfo);
            }
         }
      }
   }
}
