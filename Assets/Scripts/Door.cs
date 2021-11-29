using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{

    private Player player = null;
    [SerializeField] GameObject door = null;
    [SerializeField] GameObject redCollectable = null;
    [SerializeField] GameObject greenCollectable = null;
    [SerializeField] GameObject blueCollectable = null;
    SpriteRenderer doorColor = null;
    SpriteRenderer redCollectableColor = null;
    SpriteRenderer blueCollectableColor = null;
    SpriteRenderer greenCollectableColor = null;


    // Start is called before the first frame update
    void Start()
    {
        doorColor = door.GetComponent<SpriteRenderer>();
        redCollectableColor = redCollectable.GetComponent<SpriteRenderer>();
        greenCollectableColor = greenCollectable.GetComponent<SpriteRenderer>();
        blueCollectableColor = blueCollectable.GetComponent<SpriteRenderer>();
        doorColor.color = Color.black;
        redCollectableColor.color = Color.white;
        greenCollectableColor.color = Color.white;
        blueCollectableColor.color = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            GameObject playerCharacter = GameObject.FindGameObjectWithTag("Player");
            if (playerCharacter != null)
            {
                player = playerCharacter.GetComponent<Player>();
            }

        }
        if(player != null)
        {
            if (player.hasBlueCollectable == true && blueCollectableColor.color == Color.white)
            {
                blueCollectableColor.color = Color.blue;
            }
            if(player.hasGreenCollectable == true && greenCollectableColor.color == Color.white)
            {
                greenCollectableColor.color = Color.green;
            }
            if(player.hasRedCollectable == true && redCollectableColor.color == Color.white)
            {
                redCollectableColor.color = Color.red;
            }
            if(player.hasBlueCollectable && player.hasRedCollectable && player.hasGreenCollectable && doorColor.color == Color.black)
            {
                doorColor.color = Color.white;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player"  && doorColor.color == Color.white)
        {
            SceneManager.LoadScene("Victory");
        }
    }
}
