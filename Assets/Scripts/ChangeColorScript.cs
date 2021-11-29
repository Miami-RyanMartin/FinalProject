using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SpriteRenderer renderer = collision.gameObject.GetComponent<SpriteRenderer>();
            Color newSpriteColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
            renderer.color = newSpriteColor;
        }
    }


    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SpriteRenderer renderer = collision.gameObject.GetComponent<SpriteRenderer>();
            Color newSpriteColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
            renderer.color = newSpriteColor;
        }
    }
}
