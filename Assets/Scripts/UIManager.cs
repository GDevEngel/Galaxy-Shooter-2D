using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private int _score;
    [SerializeField] private Text _scoreText;
    [SerializeField] private GameObject _GameOverText;
    [SerializeField] private GameObject _RestartText;

    [SerializeField] private Image _healthImage;
    [SerializeField] private Sprite[] _healthSprites;

    [SerializeField] private Image _shieldImage;
    [SerializeField] private Sprite[] _shieldSprites;

    [SerializeField] private Text _ammoText;


    private Slider _slider;

    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _score = 0;
        _scoreText.text = "Score: " + 0;

        _GameOverText.SetActive(false);
        _RestartText.SetActive(false);

        _slider = FindObjectOfType<Slider>();
        if (_slider == null)
        {
            Debug.LogError("UIManager.slider is NULL");
        }

        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        if (_gameManager == null)
        {
            Debug.LogError("UIManger.gamemanager is NULL");
        }
    }

    public void UpdateUIScore(int scoreUpdate)
    {
        _score += scoreUpdate;
        _scoreText.text = "Score: " + _score;
    }

    public void UpdateUIHealth(int currentHealth)
    {
        //assign sprite to image component
        _healthImage.sprite = _healthSprites[currentHealth];
        //if currenthealth <= 0
        if (currentHealth <= 0)
        {
            //flicker game over with coroutine
            StartCoroutine(GameOver());
        }
    }

    public void UpdateUIAmmo(int currentAmmo, int maxAmmo)
    {
        _ammoText.text = currentAmmo +"/"+ maxAmmo;
    }

    public void UpdateUIShield (int currentShield)
    {
        _shieldImage.sprite = _shieldSprites[currentShield];
    }

    public void UpdateUIEnergy(float CurrentEnergy)
    {
        _slider.value = CurrentEnergy;
    }

    IEnumerator GameOver()
    {
        _gameManager.GameOver();
        _RestartText.SetActive(true);
        while (true) //game over text flicker
        {
            _GameOverText.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _GameOverText.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
