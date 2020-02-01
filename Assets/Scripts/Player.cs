using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeyCrawler
{
    public interface IPlayerLife
    {
        void AddHP(float value);

        void DoDamage(float value);

        float GetLife();
    }

    public enum PlayerStage
    {
        crawlingOneWay,
        crawlingTwoWay,
        crawlingAll,
        walkingAll,
        complete
    }

    public class Player : MonoBehaviour, IPlayerLife
    {
        #region EditorSettings
        [Header("World Settings")]
        [Tooltip("World Gravity")]
        public float gravity = 20.0f;
        [Header("Base Settings")]
        [Tooltip("How much Damage the Player deals without addons")]
        public float baseDmg = 1.0f;
        [Tooltip("How much HP the player has at start")]
        public float baseHP = 100.0f;
        [Tooltip("How fast the player is at start")]
        public float baseSpeed = 5.0f;
        [Tooltip("How much jumppower the player has")]
        public float baseJumpPower = 8.0f;

        #endregion

        #region Debug
        [Header("Debugging")]
        public bool CanDoEverything = false;
        #endregion

        #region Properties
        public float PlayerLife { get; private set; }
        public float CurrentDmg { get; private set; }
        public float CurrentSpeed { get; private set; }
        public float CurrentJumpPower { get; private set; }

        public PlayerStage CurrentStage { get; private set; }
        #endregion

        #region PrivateVariables

        private float currentHpMax = 100.0f;

        // References
        CharacterController charController;

        private Vector3 movementVector = Vector3.zero;
        #endregion

        void Start()
        {
            // Find references
            charController = gameObject.GetComponent<CharacterController>();
            
            // Load settings
            currentHpMax = baseHP;
            PlayerLife = baseHP;
            CurrentDmg = baseDmg;
            CurrentSpeed = baseSpeed;
            CurrentStage = PlayerStage.crawlingOneWay;
            CurrentJumpPower = baseJumpPower;

            if(!SanityCheck())
            {
                Debug.LogError("Player failed SanityCheck!");
            }
        }

        void Update()
        {
            #region Debug
            if(CanDoEverything)
            {
                CurrentStage = PlayerStage.complete;
            }
            #endregion

            // Player Movement
            // depending on current stage
            float hor = Input.GetAxis("Horizontal");
            float vert = Input.GetAxis("Vertical");
            switch (CurrentStage)
            {
                case PlayerStage.crawlingOneWay:
                    vert = 0.0f;
                    if(hor < 0)
                    {
                        hor = 0.0f;
                    }
                    break;
                case PlayerStage.crawlingTwoWay:
                    vert = 0.0f;
                    break;
            }


            if (charController.isGrounded)
            {
                movementVector = new Vector3(hor, 0.0f, vert);
                movementVector *= CurrentSpeed;

                // Jump
                if (CurrentStage >= PlayerStage.walkingAll)
                {
                    if (Input.GetButton("Jump"))
                    {
                        movementVector.y = CurrentJumpPower;
                    }
                }
            }

            // Calculate Gravity
            movementVector.y -= gravity * Time.deltaTime;

            // Apply Movement
            charController.Move(movementVector * Time.deltaTime);
        }

        #region InterfaceMember
        public void AddHP(float value)
        {
            if(PlayerLife < currentHpMax)
            {
                PlayerLife += value;
            }
            // Cap
            if(PlayerLife > currentHpMax)
            {
                PlayerLife = currentHpMax;
            }
        }

        public void DoDamage(float value)
        {
            PlayerLife -= value;

            if(PlayerLife <= 0)
            {
                Die();
            }
        }
        
        public float GetLife()
        {
            return PlayerLife;
        }
        #endregion

        #region PrivateMember
        /// <summary>
        /// Checks if allo references are assigned
        /// </summary>
        private bool SanityCheck()
        {
            bool sane = true;

            sane &= (charController != null);

            return sane;
        }

        /// <summary>
        /// Increases the current playerstage
        /// </summary>
        private void LevelUp()
        {
            if (CurrentStage != PlayerStage.complete)
            {
                CurrentStage++;
            }
        }

        /// <summary>
        /// Kills the player
        /// </summary>
        private void Die()
        {
            Debug.LogError("Missing Function Player.Die()");
        }
        #endregion
    }
}