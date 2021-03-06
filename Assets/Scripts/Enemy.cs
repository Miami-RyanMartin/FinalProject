using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] int enemyCurrentHealth;
    [SerializeField] int enemyMaxHealth;
    private float invincibility = .1f;
    public bool isInvincible = false;
    private GameManager GM = null;
    public float respawnDelayAmount = 5.0f;
    [SerializeField] GameObject enemyGun = null;
    private SpriteRenderer enemySpriteRenderer;
    private BoxCollider2D enemyBoxCollider;
    [SerializeField] GameObject bulletCollider = null;
    public bool notStartedRespawn = true;
    [SerializeField] Color startColor;
    private AudioSource enemyHit = null;

    // Start is called before the first frame update
    void Start()
    {
        enemyCurrentHealth = enemyMaxHealth;
        isInvincible = false;
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        enemySpriteRenderer = GetComponent<SpriteRenderer>();
        enemyBoxCollider = GetComponent<BoxCollider2D>();
        Color startColor = this.gameObject.GetComponent<SpriteRenderer>().color;
        enemyHit = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isInvincible)
        {
            enemySpriteRenderer.color = Color.yellow;
            invincibility -= Time.deltaTime;
            if (invincibility <= 0.0f)
            {
                isInvincible = false;
                invincibility = .1f;
            }
        }
        else
        {
            enemySpriteRenderer.color = startColor;
        }

        if(enemyCurrentHealth<=0 && notStartedRespawn)
        {
            GM.PlayEnemyDeathAudio();
            GetComponent<BoxCollider2D>().enabled = false;
            enemySpriteRenderer.enabled = false;
            enemyGun.SetActive(false);
            bulletCollider.SetActive(false);
            StartCoroutine(respawnDelay());
            notStartedRespawn = false;
        }
        
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            if (!isInvincible)
            {
                enemyHit.Play();
                isInvincible = true;
                enemyCurrentHealth--;
                GameObject playerBullet = collision.gameObject;
                Destroy(playerBullet);
            }
        }
    }



    IEnumerator respawnDelay()
    {
        yield return new WaitForSeconds(respawnDelayAmount);
        enemyCurrentHealth = enemyMaxHealth;
        enemyBoxCollider.enabled = true;
        enemySpriteRenderer.enabled = true;
        enemyGun.SetActive(true);
        bulletCollider.SetActive(true);
        notStartedRespawn = true;
    }


}
