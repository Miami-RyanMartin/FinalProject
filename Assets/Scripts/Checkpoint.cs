using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] Vector2 checkpoint = new Vector2(0,0);

    private Vector2 playerCheckpoint;
    private Player player = null;


    void Start()
    {
        SpriteRenderer checkpointColor = GetComponent<SpriteRenderer>();
        checkpointColor.color = Color.yellow;
       
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            player.setSpawnLocation(checkpoint);
            //SpriteRenderer checkpointColor = GetComponent<SpriteRenderer>();
            //checkpointColor.color = Color.green;
        }
         
    }

    void Update()
    {
        if (player == null)
        {
            GameObject playerCharacter = GameObject.FindGameObjectWithTag("Player");
            if(playerCharacter != null)
            {
                player = playerCharacter.GetComponent<Player>();
            }
           
        }
        if (player != null)
        {
            playerCheckpoint = player.getSpawnLocation();
            if (playerCheckpoint == checkpoint)
            {
                SpriteRenderer checkpointColor = GetComponent<SpriteRenderer>();
                checkpointColor.color = Color.green;
            }
            else
            {
                SpriteRenderer checkpointColor = GetComponent<SpriteRenderer>();
                checkpointColor.color = Color.red;
            }
        }
    }
}
