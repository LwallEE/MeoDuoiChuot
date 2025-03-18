using System.Collections;
using System.Collections.Generic;
using ReuseSystem.ObjectPooling;
using UnityEngine;

namespace ReuseSystem.AudioSystem
{ 
    public class AudioManager : Singleton<AudioManager>
    {
        private List<SoundPlayer> currentLoopSound = new List<SoundPlayer>();
        [SerializeField] private GameObject soundPrefab;
        [SerializeField] private List<AudioInfo> infos;

        private Dictionary<AudioInfo, int> currentSoundPlay = new Dictionary<AudioInfo, int>();
        public bool IsPlayingMusic { get; private set; }
        public bool IsPlaySfx { get; private set; }
        public float SoundVolume { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
        }
        

        //play clip according to audio Info name
        public void PlayClip(string audioInfoName)
        {
            foreach (var sound in infos)
            {
                if (sound.name == audioInfoName && CheckAudioInfoCanPlay(sound))
                {
                    if ( (!sound.isLoop && !IsPlaySfx))
                    {
                        break;
                    }
                    AddCurrentSoundPlay(sound);
                    PlayAudioInfoClip(sound);
                    break;
                }
            }
        }

        public void SetIsPlayMusic(bool value)
        {
            IsPlayingMusic = value;
            foreach (var sound in currentLoopSound)
            {
                sound.ActiveMusicShortly(IsPlayingMusic);
            }
        }

        public void SetIsPlaySfx(bool value)
        {
            IsPlaySfx = value;
        }
        public void SetSoundVolume(float volume)
        {
            SoundVolume = volume;
            foreach (var sound in currentLoopSound)
            {
                sound.UpdateSoundVolume();
            }
        }
        

        void AddCurrentSoundPlay(AudioInfo info)
        {
            if (!currentSoundPlay.ContainsKey(info))
            {
                currentSoundPlay.Add(info, 1);
            }
            else
            {
                currentSoundPlay[info] += 1;
            }
        }

        public void RemoveSoundPlay(AudioInfo info)
        {
            if (currentSoundPlay.ContainsKey(info))
            {
                currentSoundPlay[info] -= 1;
            }
        }

        private bool CheckAudioInfoCanPlay(AudioInfo info)
        {
            return !currentSoundPlay.ContainsKey(info) || currentSoundPlay[info] < info.maxNumberOfSoundPlayTheSameTime;
        }
        
        private void PlayAudioInfoClip(AudioInfo audioInfo)
        {
            var sound = LazyPool.Instance.GetObj<SoundPlayer>(soundPrefab);
            sound.InitData(audioInfo);
            if (audioInfo.isLoop)
            {
                currentLoopSound.Add(sound);
            }
        }

      

        public void StopMusic(AudioClip clip)
        {
            foreach (var sound in currentLoopSound)
            {
                if (sound.IsPlayingClip(clip))
                {
                    sound.StopMusic();
                    currentLoopSound.Remove(sound);
                    return;
                }
            }
        }

        public void StopMusic(string audioInfoName)
        {
            foreach (var sound in infos)
            {
                if (sound.name == audioInfoName)
                {
                    foreach (var item in sound.soundVariation)
                    {
                        StopMusic(item);
                    }
                    break;
                }
            }
        }

        public void StopAllMusic()
        {
            foreach (var sound in currentLoopSound)
            {
                sound.StopMusic();
            }
            currentLoopSound.Clear();
        }
        
    }
}
