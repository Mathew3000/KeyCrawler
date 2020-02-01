using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeyCrawler
{

    public class GameLogicManager : MonoBehaviour
    {
        public enum EffectTypes
        {
            damage,
            door,
            fanfare,
            laser,
            scream,
            death
        }

        #region Debugging
        [Header("Debugging")]
        public EffectTypes debugType = EffectTypes.damage;
        public bool playEffect = false;
        public PlayerStage debugStage = PlayerStage.crawlingOneWay;
        public bool playBackground = false;
        #endregion

        #region EditorSettings
        [Header("Audio Sources")]
        public AudioSource backgroungPlayer;
        public AudioSource effectPlayer;

        [Header("Audio Clips for effects")]
        public AudioClip damageClip;
        public AudioClip doorClip;
        public AudioClip fanfareClip;
        public AudioClip laserClip;
        public AudioClip screamClip;
        public AudioClip deathClip;

        [Header("Background Tracks")]
        public AudioClip backgroundOne;
        public AudioClip backgroundTwo;
        public AudioClip backgroundThree;
        public AudioClip backgroundFour;
        #endregion

        #region PrivateMember
        // References
        private Player localPlayer;
        #endregion

        void Start()
        {
            // Find references
            localPlayer = FindObjectOfType<Player>();


        }

        void Update()
        {
            #region Debugging
            if (playEffect)
            {
                PlayEffect(debugType);
                playEffect = false;
            }

            if(playBackground)
            {
                PlayBackground(debugStage);
                playBackground = false;
            }
            #endregion


        }

        #region PrivateMember
        private void PlayEffect(EffectTypes sound)
        {
            if (effectPlayer.isPlaying)
            {
                effectPlayer.Stop();
            }

            switch (sound)
            {
                case EffectTypes.damage:
                    effectPlayer.clip = damageClip;
                    break;
                case EffectTypes.death:
                    effectPlayer.clip = deathClip;
                    break;
                case EffectTypes.door:
                    effectPlayer.clip = doorClip;
                    break;
                case EffectTypes.fanfare:
                    effectPlayer.clip = fanfareClip;
                    break;
                case EffectTypes.laser:
                    effectPlayer.clip = laserClip;
                    break;
                case EffectTypes.scream:
                    effectPlayer.clip = screamClip;
                    break;
            }

            if (effectPlayer.clip != null)
            {
                effectPlayer.Play();
            }
        }
        
        private void PlayBackground(PlayerStage stage)
        {
            if (backgroungPlayer.isPlaying)
            {
                backgroungPlayer.Stop();
            }

            switch (stage)
            {
                case PlayerStage.crawlingOneWay:
                case PlayerStage.crawlingTwoWay:
                    backgroungPlayer.clip = backgroundOne;
                    break;
                case PlayerStage.crawlingAll:
                    backgroungPlayer.clip = backgroundTwo;
                    break;
                case PlayerStage.walkingAll:
                    backgroungPlayer.clip = backgroundThree;
                    break;
                case PlayerStage.complete:
                    backgroungPlayer.clip = backgroundFour;
                    break;
            }

            if (backgroungPlayer.clip != null)
            {
                backgroungPlayer.Play();
            }
        }
        #endregion
    }
}