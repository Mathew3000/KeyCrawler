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

        [Header("Settings")]
        [Tooltip("Prefab for the Player Object")]
        public GameObject playerPrefab;
        public EffectTypes ItemFoundEffect;
        public EffectTypes PlayerDeathEffect;
        public EffectTypes ShootEffect;
        public EffectTypes FallingEffect;
        #endregion

        #region PrivateMember
        // References
        private Player localPlayer;
        private Keyboard localKeyboard;
        #endregion

        void Start()
        {
            // Make sure GO is persistent
            DontDestroyOnLoad(gameObject);

            // Find references
            localPlayer = FindObjectOfType<Player>();
            localKeyboard = FindObjectOfType<Keyboard>();

            if(!SanityCheck())
            {
                Debug.LogError("GameLocigManager SanityCheck failed!");
            }
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

        #region PublicMember
        /// <summary>
        /// found a KeyItem
        /// </summary>
        /// <param name="keyFunction">the keytype</param>
        public void KeyFound(KeyFunction keyFunction)
        {
            localKeyboard.AddKey(keyFunction);
            PlayEffect(ItemFoundEffect);
        }

        /// <summary>
        /// found a key with a weapon
        /// </summary>
        /// <param name="keyFunction"></param>
        public void WeaponFound(KeyFunction keyFunction)
        {
            KeyFound(keyFunction);
        }

        /// <summary>
        /// found a weapon
        /// </summary>
        public void WeaponFound()
        {
            PlayEffect(ItemFoundEffect);
        }

        public void TriggerShot()
        {
            PlayEffect(ShootEffect);
        }

        public void TriggerFallingSound()
        {
            PlayEffect(FallingEffect);
        }

        public void TriggerDeathEffect()
        {
            PlayEffect(EffectTypes.death);
        }
        
        public void PlayerDied()
        {
            Debug.LogError("Missing Function GameLogicManager.PlayerDied()");
        }
        #endregion

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
                    backgroungPlayer.clip = backgroundOne;
                    break;
                case PlayerStage.crawlingTwoWay:
                    backgroungPlayer.clip = backgroundTwo;
                    break;
                case PlayerStage.crawlingAll:
                    backgroungPlayer.clip = backgroundThree;
                    break;
                case PlayerStage.walkingAll:
                    backgroungPlayer.clip = backgroundFour;
                    break;
            }

            if (backgroungPlayer.clip != null)
            {
                backgroungPlayer.Play();
            }
        }
        
        private bool SanityCheck()
        {
            bool sane = true;

            sane &= (localPlayer != null);
            sane &= (localKeyboard != null);

            return sane;
        }
        #endregion
    }
}