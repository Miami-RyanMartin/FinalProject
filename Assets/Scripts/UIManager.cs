using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField] private Image healthBar = null;
    [SerializeField] private Button playButton = null;
    [SerializeField] private Button quitButton = null;
    [SerializeField] private Button resumeButton = null;
    [SerializeField] private GameObject hideButtonAbility = null;
    [SerializeField] private GameObject hideQuitButtonAbility = null;
    [SerializeField] private GameObject hideResumeButtonAbility = null;
    private GameManager GM = null;

    [SerializeField] Image greenCollectable = null;
    [SerializeField] Image redCollectable = null;
    [SerializeField] Image blueCollectable = null;
    [SerializeField] Sprite[] collectableImages = null;

    [SerializeField] GameObject deathText = null;
    [SerializeField] GameObject pausedText = null;

    public bool checkedPlayerLife = false;

    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        hideButtonAbility.SetActive(false);
        hideQuitButtonAbility.SetActive(false);
        deathText.SetActive(false);
        pausedText.SetActive(false);
        hideResumeButtonAbility.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GM.playerAlive == false)
        {
            hideButtonAbility.SetActive(true);
            hideQuitButtonAbility.SetActive(true);
            deathText.SetActive(true);
            playButton.onClick.AddListener(SpawnPlayer);
            checkedPlayerLife = false;
        }
        else if(!checkedPlayerLife)
        {
            hideButtonAbility.SetActive(false);
            hideQuitButtonAbility.SetActive(false);
            deathText.SetActive(false);
            checkedPlayerLife = true;
        }


    }
   
    public void UpdateHealth(int currentHealth, int maxHealth)
    {
        var lifeLeft = ((float)currentHealth) / ((float)maxHealth);
        healthBar.fillAmount = lifeLeft;
        if (lifeLeft > .6f)
        {
            healthBar.color = Color.green;
        }
        else if (lifeLeft > .3f)
        {
            healthBar.color = Color.yellow;
        }
        else
        {
            healthBar.color = Color.red;
        }
    }

    public void SpawnPlayer()
    {
        GM.canSpawn = true;
    }

    public void CollectedGreenCollectable()
    {
        greenCollectable.sprite = collectableImages[0];
    }

    public void CollectedRedCollectable()
    {
        redCollectable.sprite = collectableImages[1];
    }

    public void CollectedBlueCollectable()
    {
        blueCollectable.sprite = collectableImages[2];
    }

    public void QuitGame()
    {
        Application.Quit();
    }


}
