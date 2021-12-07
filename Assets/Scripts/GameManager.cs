using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool playerAlive = true;
    [SerializeField] GameObject player = null;
    [SerializeField] Player playerObject = null;
    Vector2 spawnSpot = new Vector2(0, -1.5f);
    public bool canSpawn = false;
    public bool respawnEnemies = false;
    public bool paused = false;
    [SerializeField] private GameObject gamePaused = null;
    [SerializeField] private GameObject resumeButton = null;
    [SerializeField] private GameObject quitButton = null;
    private AudioSource playerDeath = null;
    private AudioSource enemyDeath = null;
    [SerializeField] private AudioSource[] audioChoices = null;

    // Start is called before the first frame update
    void Start()
    {
        playerAlive = true;
        canSpawn = false;
        playerDeath = GetComponent<AudioSource>();
        playerDeath = audioChoices[0];
        enemyDeath = audioChoices[1];
 
    }

    // Update is called once per frame
    void Update()
    {
        if(!playerAlive && canSpawn)
        {
            playerObject.ReEnablePlayer();
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

    public void PlayDeathAudio()
    {
        playerDeath.Play();
    }

    public void PlayEnemyDeathAudio()
    {
        enemyDeath.Play();
    }
}
