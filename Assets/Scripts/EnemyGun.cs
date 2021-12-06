using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    private float fireRate = .5f;
    private float nextFire; 
    [SerializeField] public GameObject enemyBulletPrefab;
    [SerializeField] public GameObject fireLocation;
    public LayerMask hitObject;
    [SerializeField] public GameObject player;
    private bool canFire;
    private AudioSource gunFire = null;

    // Start is called before the first frame update
    void Start()
    { 
       nextFire = Time.time;
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        gunFire = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }


        if (canFire)
        {
            Vector3 difference = (player.transform.position - transform.position);
            difference.Normalize();
            float gunRotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, gunRotationZ);

            if (Time.time > nextFire && canFire)
            {
                FireEnemyGun();
                nextFire = Time.time + fireRate;
            }
        }
    }

    public void FireEnemyGun()
    {
        Vector2 aimPos = new Vector2(player.transform.position.x + Random.Range(-2.0f,2.0f), player.transform.position.y + Random.Range(-2.0f,2.0f));
        Vector2 firePos = new Vector2(fireLocation.transform.position.x, fireLocation.transform.position.y);
        RaycastHit2D hit = Physics2D.Raycast(firePos, aimPos - firePos, 10.0f, hitObject);
        Debug.DrawLine(firePos, (aimPos - firePos) * 100, Color.green);
        gunFire.Play();
        GameObject newBullet = Instantiate(enemyBulletPrefab, firePos, transform.rotation);
        newBullet.GetComponent<Rigidbody2D>().velocity = (aimPos - firePos).normalized * 10.0f;

    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("CollisionDetected");
        if(collision.tag == "Player")
        {
            canFire = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canFire = false;
        }
    }

}
