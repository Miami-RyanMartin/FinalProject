using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private GameObject _mainCamera;
    private Rigidbody2D playerRigidbody;
    public float moveSpeed = 100;
    Vector2 playerInput;
    private Ray mouseRay;
    private RaycastHit hit;
    private BoxCollider2D playerBoxCollider;
    public LayerMask jumpLayerMask;
    private int playerMaxHealth = 10;
    private int playerCurrentHealth = 10;
    private float invincibility = .1f;
    private bool isInvincible = false;
    public Vector2 spawnLocation = new Vector2(0,1.5f);
    public bool hasRedCollectable = false;
    public bool hasGreenCollectable = false;
    public bool hasBlueCollectable = false;
    private SpriteRenderer playerSpriteRender = null;
    [SerializeField] GameObject playerGun;

    private UIManager UI = null;

    private GameManager GM = null;

    GameObject blueCollectable = null;
    GameObject redCollectable = null;
    GameObject greenCollectable = null;

    private AudioSource playerHit = null;
    private AudioSource collectableNoise = null;
    [SerializeField] private AudioSource[] audioChoices = null;

    // Start is called before the first frame update
    void Start()
    {
        playerSpriteRender = gameObject.GetComponent<SpriteRenderer>();
        blueCollectable = GameObject.FindGameObjectWithTag("BlueCollectable");
        if (blueCollectable == null)
        {
            hasBlueCollectable = true;
           
        }
        redCollectable = GameObject.FindGameObjectWithTag("RedCollectable");
        if (redCollectable == null)
        {
            hasRedCollectable = true;
        }
        greenCollectable = GameObject.FindGameObjectWithTag("GreenCollectable");
        if (greenCollectable == null)
        {
            hasGreenCollectable = true;
        }

        playerRigidbody = GetComponent<Rigidbody2D>();
        playerBoxCollider = transform.GetComponent<BoxCollider2D>();
        playerRigidbody.freezeRotation = true;
        isInvincible = false;

        UI = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (UI != null)
        {
            UI.UpdateHealth(playerCurrentHealth,playerMaxHealth);
        }
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerHit = audioChoices[0];
        collectableNoise = audioChoices[1];

    }

    

    void FixedUpdate()
    {
        if (GM.paused == false && GM.playerAlive)
        {
            playerRigidbody.velocity = new Vector2(playerInput.x * Time.deltaTime * moveSpeed, playerRigidbody.velocity.y);
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (GM.paused == false && GM.playerAlive)
        {
            IsGrounded();
            Jump();
            playerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if (isInvincible)
            {
                playerSpriteRender.color = Color.red;
                invincibility -= Time.deltaTime;
                if (invincibility <= 0.0f)
                {
                    isInvincible = false;
                    invincibility = .1f;
                }
            }
            else
            {
                playerSpriteRender.color = Color.white;
            }
        }

        
    }

    public void Jump()
    {
        if(Input.GetButtonDown("Jump") && IsGrounded() && GM.playerAlive)
        {
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, 20.0f);
        }
    }

    private bool IsGrounded()
    {
        float extraHeight = 1.5f;
        RaycastHit2D raycastHit = Physics2D.Raycast(playerBoxCollider.bounds.center, Vector2.down, playerBoxCollider.bounds.extents.y + extraHeight, jumpLayerMask);
        Color rayColor;
        if(raycastHit.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }
        Debug.DrawRay(playerBoxCollider.bounds.center, Vector2.down * (playerBoxCollider.bounds.extents.y + extraHeight),rayColor);
        return raycastHit.collider != null;

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            if(!isInvincible)
            {
                isInvincible = true;
                playerCurrentHealth--;
                UI.UpdateHealth(playerCurrentHealth, playerMaxHealth);
                playerHit.Play();
                GameObject enemyBullet = collision.gameObject;
                Destroy(enemyBullet);
                if (playerCurrentHealth == 0)
                {
                    GM.PlayDeathAudio();
                    GM.playerAlive = false;
                    playerSpriteRender.enabled = false;
                    playerBoxCollider.enabled = false;
                    playerGun.SetActive(false);
                    playerRigidbody.velocity = new Vector2(0f, 0f);
                    playerRigidbody.isKinematic = true;

                }
            }
        }

        if(collision.gameObject.CompareTag("BlueCollectable"))
        {
           
            hasBlueCollectable = true;
            GameObject Collectable = collision.gameObject;
            collectableNoise.Play();
            UI.CollectedBlueCollectable();
            Destroy(Collectable);
        }
        if(collision.gameObject.CompareTag("GreenCollectable"))
        {
            
            hasGreenCollectable = true;
            GameObject Collectable = collision.gameObject;
            UI.CollectedGreenCollectable();
            collectableNoise.Play();
            Destroy(Collectable);
        }
        if(collision.gameObject.CompareTag("RedCollectable"))
        {
            collectableNoise.Play();
            hasRedCollectable = true;
            GameObject Collectable = collision.gameObject;
            UI.CollectedRedCollectable();
            Destroy(Collectable);
        }
    }


    public void SetSpawnLocation(Vector2 spawn)
    {
        spawnLocation = spawn;
        GM.UpdateSpawnLocation(spawnLocation);
    }

    public Vector2 getSpawnLocation()
    {
        return spawnLocation;
    }

    public void SetPlayerHealth(int playerHealth)
    {
        playerCurrentHealth = playerHealth;
        UI.UpdateHealth(playerCurrentHealth, playerMaxHealth);
    }

    public void ReEnablePlayer()
    {
        playerSpriteRender.enabled = true;
        playerBoxCollider.enabled = true;
        playerGun.SetActive(true);
        playerRigidbody.isKinematic = false;
        playerCurrentHealth = playerMaxHealth;
        transform.position = spawnLocation;
    }

}
