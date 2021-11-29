using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool playerAlive = false;
    [SerializeField] GameObject player = null;
    Vector2 spawnSpot = new Vector2(0, -1.5f);
    public bool canSpawn = false;
    public bool respawnEnemies = false;
    public bool paused = false;
    [SerializeField] private GameObject gamePaused = null;
    [SerializeField] private GameObject resumeButton = null;
    [SerializeField] private GameObject quitButton = null;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(player, spawnSpot, transform.rotation);
        playerAlive = true;
        canSpawn = false;
    }

    // Update is called once per frame
    void Update()
    {

        if(!playerAlive && canSpawn)
        {
            Instantiate(player, spawnSpot, transform.rotation);
            playerAlive = true;
            canSpawn = false;
           
        }



        if (Input.GetKeyDown(KeyCode.Escape))

        {
            if (paused == false)
            {
                gamePaused.SetActive(true);
                resumeButton.SetActive(true);
                quitButton.SetActive(true);
                Time.timeScale = 0;
                paused = true;
            }
        }
    }

    public void UpdateSpawnLocation(Vector2 spawnLocation)
    {
        spawnSpot = spawnLocation;
    }

    public void ResumeGame()
    {
        gamePaused.SetActive(false);
        resumeButton.SetActive(false);
        quitButton.SetActive(false);
        Time.timeScale = 1;
        paused = false;
    }
}
