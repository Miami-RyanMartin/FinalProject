using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] GameObject playerBulletPrefab;
    [SerializeField] GameObject fireLocation;
    float speed;
    private float fireRate = .3f;
    private float nextFire;
    private GameManager GM = null;
    private AudioSource gunFire = null;

    public LayerMask hitObject;
    Camera mainCamera;

    // Start is called before the first frame update

    void Awake()
    {
        mainCamera = Camera.main;
    }

    void Start()
    {
        nextFire = Time.time;
        speed = 16.0f;
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        gunFire = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (GM.paused == false)
        {
            Vector3 difference = mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            difference.Normalize();
            float gunRotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, gunRotationZ);
            if (Time.time > nextFire && Input.GetButton("Fire1"))
            {
                FireGun();
                nextFire = Time.time + fireRate;
            }
        }

    }

    public void FireGun()
    {
        Vector2 mousePos = new Vector2(mainCamera.ScreenToWorldPoint(Input.mousePosition).x, mainCamera.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 firePos = new Vector2(fireLocation.transform.position.x, fireLocation.transform.position.y);
        RaycastHit2D hit = Physics2D.Raycast(firePos, mousePos - firePos, 10.0f, hitObject);
        Debug.DrawLine(firePos, (mousePos - firePos) * 100, Color.green);
        gunFire.Play();
        GameObject newBullet = Instantiate(playerBulletPrefab, firePos,  transform.rotation);
        newBullet.GetComponent<Rigidbody2D>().velocity = (mousePos - firePos).normalized * speed;
        if (hit.collider != null)
        {
            //Destroy(hit.transform.gameObject);
           
            Debug.DrawLine(firePos, hit.point, Color.red);
            Debug.Log("We Hit" + hit.collider.name);
        }
    }
}
