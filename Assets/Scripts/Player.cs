using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeyCrawler
{
    public interface IPlayerLife
    {
        void AddHP(float value);

        void DoDamage(float value);

        void FallToDeath();

        float GetLife();
    }

    public enum PlayerStage
    {
        crawlingOneWay,
        crawlingTwoWay,
        crawlingAll,
        walkingAll
    }

    public enum PlayerDirection
    {
        up,
        down,
        left,
        right
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
        [Header("Shooting")]
        [Tooltip("SpawnPosition for projectile")]
        public Transform projectileSpawn;
        [Tooltip("Projectile prefab")]
        public GameObject projectilePrefab;
        [Tooltip("Projectile speed")]
        public float projectileSpeed = 5.0f;
        [Tooltip("Projectile cooldown")]
        public float projectileCooldown = 0.5f;
        [Tooltip("audio to play when walking")]
        public AudioSource walkingSound;
        public AudioClip walkingClip_1;
        public AudioClip walkingClip_2;
        #endregion

        #region Debug
        [Header("Debugging")]
        public bool CanDoEverything = false;
        public bool LowHpDie = false;
        public bool FallingToDeath = false;

        #endregion

        #region Properties
        public float PlayerLife { get; private set; }
        public float CurrentDmg { get; private set; }
        public float CurrentSpeed { get; private set; }
        public float CurrentJumpPower { get; private set; }

        public PlayerStage CurrentStage { get; private set; }
        public PlayerDirection CurrentDirection { get; private set; }
        #endregion

        #region PrivateVariables
        // weapon already found?
        private bool CanShoot = false;
        // maximum hp the player can have
        private float currentHpMax = 100.0f;
        // cooldown for shooting
        private float cooldownLeft = 0.0f;
        
        // used to alternate the walking sound
        private int currentStep = 0;

        // References
        CharacterController charController;
        GameLogicManager gameLogic;
        
        // Vector for playermovement
        private Vector3 movementVector = Vector3.zero;
        #endregion

        void Start()
        {
            // Set Fixed
            DontDestroyOnLoad(gameObject);

            // Find references
            charController = gameObject.GetComponent<CharacterController>();
            gameLogic = FindObjectOfType<GameLogicManager>();
            
            // Load settings
            currentHpMax = baseHP;
            PlayerLife = baseHP;
            CurrentDmg = baseDmg;
            CurrentSpeed = baseSpeed;
            CurrentStage = PlayerStage.crawlingOneWay;
            CurrentJumpPower = baseJumpPower;
            CurrentDirection = PlayerDirection.right;

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
                CurrentStage = PlayerStage.walkingAll;
                CanShoot = true;
            }

            if(LowHpDie)
            {
                LowHpDie = false;
                Die();
            }

            if(FallingToDeath)
            {
                FallingToDeath = false;
                FallToDeath();
            }
            #endregion

            // Decrease Cooldown
            if(cooldownLeft > 0)
            {
                cooldownLeft -= Time.deltaTime;
            }

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

            // Check if sound should be played
            PlayWalkingSound(movementVector);

            // Shoot If possible
            if (CanShoot)
            {
                if(Input.GetButton("Fire1") && (cooldownLeft <= 0))
                {
                    ShootProjectile();
                    cooldownLeft = projectileCooldown;
                }
            }

            // Map Movement
            MapDirection(movementVector);
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
        
        public void FallToDeath()
        {
            gameLogic.TriggerFallingSound();
            Die(true);
        }

        public float GetLife()
        {
            return PlayerLife;
        }
        #endregion

        #region PrivateMember

        private void PlayWalkingSound(Vector3 dir)
        {
            if (walkingSound != null)
            {
                if (dir.magnitude > 0.5) 
                {
                    if(!walkingSound.isPlaying)
                    {
                        if(currentStep == 0)
                        {
                            walkingSound.clip = walkingClip_2;
                            currentStep++;
                        }
                        else
                        {
                            walkingSound.clip = walkingClip_1;
                            currentStep = 0;
                        }
                        walkingSound.Play();
                    }

                }
                else
                {
                    walkingSound.Stop();
                }
            }
        }

        /// <summary>
        /// Checks if allo references are assigned
        /// </summary>
        private bool SanityCheck()
        {
            bool sane = true;

            sane &= (charController != null);
            sane &= (gameLogic != null);
            sane &= (projectileSpawn != null);
            sane &= (projectilePrefab != null);

            return sane;
        }

        /// <summary>
        /// Maps direction info
        /// </summary>
        /// <param name="dir">the direction to check</param>
        private void MapDirection(Vector3 dir)
        {
            if (dir.x > 0)
            {
                CurrentDirection = PlayerDirection.right;
            }
            else if(dir.x < 0)
            {
                CurrentDirection = PlayerDirection.left;
            }
            if(dir.z > 0)
            {
                CurrentDirection = PlayerDirection.up;
            }
            else if(dir.z < 0)
            {
                CurrentDirection = PlayerDirection.down;
            }
        }

        /// <summary>
        /// Handles an item collision
        /// </summary>
        /// <param name="item">the item to handle</param>
        private void HandleItem(Item item)
        {
            switch(item.getKind())
            {
                case itemKind.upgrade:
                    LevelUp();
                    try
                    {
                        gameLogic.KeyFound((KeyFunction)item.getValue());
                    }
                    catch
                    {
                        Debug.LogError("Player.HandleItem: Item.getValue(): Unexpected Type: KeyFunction vs " + item.getValue().GetType().ToString());
                    }
                    break;
                case itemKind.key:
                    try
                    {
                        gameLogic.KeyFound((KeyFunction)item.getValue());
                    }
                    catch
                    {
                        Debug.LogError("Player.HandleItem: Item.getValue(): Unexpected Type: KeyFunction vs " + item.getValue().GetType().ToString());
                    }
                    break;
                case itemKind.health:
                    try
                    {
                        AddHP((float)item.getValue());
                    }
                    catch
                    {
                        Debug.LogError("Player.HandleItem: Item.getValue(): Unexpected Type: float vs " + item.getValue().GetType().ToString());
                    }
                    break;
                case itemKind.weapon:
                    try
                    {
                        CanShoot = true;
                        gameLogic.WeaponFound();
                    }
                    catch
                    {
                        Debug.LogError("Player.HandleItem: Item.getValue(): Unexpected Type: weaponType vs " + item.getValue().GetType().ToString());
                    }
                    break;
                default:
                    Debug.LogWarning("Player.HandleItem: Unexpected itemKind received: " + item.getKind().ToString());
                    break;
            }

            // When finished delete the item
            item.Despawn();
        }

        private void ShootProjectile()
        {
            GameObject projectile = Instantiate(projectilePrefab, projectileSpawn.position, projectileSpawn.rotation);
            Vector3 velocityVector = new Vector3();
            switch (CurrentDirection)
            {
                case PlayerDirection.right:
                    velocityVector = Vector3.right;
                    break;
                case PlayerDirection.left:
                    velocityVector = Vector3.left;
                    break;
                case PlayerDirection.up:
                    velocityVector = Vector3.forward;
                    break;
                case PlayerDirection.down:
                    velocityVector = Vector3.back;
                    break;

            }
            projectile.GetComponent<Rigidbody>().velocity = velocityVector * projectileSpeed;

            gameLogic.TriggerShot();
        }

        /// <summary>
        /// Increases the current playerstage
        /// </summary>
        private void LevelUp()
        {
            if (CurrentStage != PlayerStage.walkingAll)
            {
                CurrentStage++;
                gameLogic.UpdateBackground();
            }
        }

        /// <summary>
        /// Kills the player
        /// </summary>
        private void Die(bool silent = false)
        {
            if(!silent)
            {
                gameLogic.TriggerDeathEffect();
            }
            gameLogic.PlayerDied();
        }
        #endregion

        #region EventHandler
        private void OnTriggerEnter(Collider other)
        {
            if(other.GetComponent<Item>() != null)
            {
                HandleItem(other.GetComponent<Item>());
            }
        }
        #endregion
    }
}