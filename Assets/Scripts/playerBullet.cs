using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBullet : MonoBehaviour
{
    private Vector3 bulletDirection = Vector3.zero;
    [SerializeField] Rigidbody2D rigidBody = null;
    private float speed = 5.5f;
    private Vector3 currentVelocity = Vector3.zero;
    private float bulletTime = 2.0f;
   

    // Start is called before the first frame update
    void Start()
    {
        //bulletDirection = new Vector3(1.0f, 0.0f, 0).normalized;
        currentVelocity = this.GetComponent<Rigidbody2D>().velocity;
    }

    // Update is called once per frame
    private void Update()
    {
       var newDelta = currentVelocity * Time.deltaTime * speed;
       rigidBody.MovePosition(transform.position + newDelta);
       bulletTime -= Time.deltaTime;
       if(bulletTime<=0.0)
       {
            Destroy(this.gameObject);
       }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Collision Occured");
        if(!collision.gameObject.CompareTag("Player"))
        {
            bulletDirection = Vector2.Reflect(currentVelocity, collision.contacts[0].normal);
            currentVelocity = bulletDirection;
        }
    }

    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
        }
    }
    */
}
