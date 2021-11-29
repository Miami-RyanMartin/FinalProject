using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update

    private Transform PlayerTransform;
    private GameObject player = null;


    void Start()
    {
        if(PlayerTransform == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                PlayerTransform = player.GetComponent<Transform>();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerTransform == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                PlayerTransform = player.GetComponent<Transform>();
            }
        }
        if (PlayerTransform != null)
        {
            transform.position = new Vector3(PlayerTransform.position.x, PlayerTransform.position.y + 1.5f, -10);
        }
    }

}
