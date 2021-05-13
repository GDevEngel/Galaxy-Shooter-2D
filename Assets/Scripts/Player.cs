using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //movement config
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _maxPosX = 11.4f;
    [SerializeField] private float _minPosX = -11.4f;
    [SerializeField] private float _maxPosY = 4f;
    [SerializeField] private float _minPosY = -4.2f;
    [SerializeField] private float _sprintModifier = 1.5f;

    //speed vfx config
    private Vector3 _scaleChange = new Vector3(0.2f, 0.4f, 0);
    private Vector3 _posChange = new Vector3(0, -0.25f, 0);
    private Vector3 _scaleChangeSpeedPowerUp = new Vector3(0.6f, 0.6f, 0);
    private Vector3 _posChangeSpeedPowerUp = new Vector3(0, -0.625f, 0);

    // Energy system
    [SerializeField] private float _energyMax = 10f;
    [SerializeField] private float _energyCurrent = 10f;
    [SerializeField] private float _energyCharge = 1f;
    [SerializeField] private float _sprintStaminaCost = 3f;
    private bool _isSprint = false;

    //laser var
    [SerializeField] private float _fireRate = 0.25f;
    private float _nextFire = 0f;
    private Vector3 _laserOffset = new Vector3(0f, 1f, 0f);
    [SerializeField] private GameObject LaserPrefab;

    //power ups
    [SerializeField] private GameObject LaserTriplePrefab;
    [SerializeField] private bool _isTripleLaserActive;
    [SerializeField] private float _speedPowerupModifier = 2f;
    [SerializeField] private bool _isShieldActive;
    private GameObject _shield; 

    [SerializeField] private int _health = 3;

    //handles
    private SpawnManager _spawnManager;
    private UIManager _uIManager;
    private ShakeBehaviour _camera;
    private Animator _animator;
    private GameObject _thruster;

    private GameObject _leftEngine;
    private GameObject _rightEngine;
    [SerializeField] private GameObject ExplosionPrefab;
    [SerializeField] private AudioClip _laserAudioClip;
    [SerializeField] private AudioClip _playerDamaged;
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        //set starting position
        transform.position = new Vector3(0, _minPosY, 0);

        _isTripleLaserActive = false;
        _isShieldActive = false;

        //find gameobject then get component
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uIManager = GameObject.FindObjectOfType<Canvas>().GetComponent<UIManager>();
        _camera = GameObject.FindObjectOfType<Camera>().GetComponent<ShakeBehaviour>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _thruster = GameObject.Find("Player/Thruster");
        _shield = GameObject.Find("Player/Shield");
        _shield.SetActive(false);

        _leftEngine = GameObject.Find("Left_Engine");
        _rightEngine = GameObject.Find("Right_Engine");
        _leftEngine.SetActive(false);
        _rightEngine.SetActive(false);


        //null checks
        if (_animator == null)
        {
            Debug.LogError("Player.animator is NULL");
        }
        if (_thruster == null)
        {
            Debug.LogError("Player.thruster is NULL");
        }
        if (_camera == null)
        {
            Debug.LogError("Player.camera is NULL");
        }
        if (_uIManager == null)
        {
            Debug.LogError("Player.uimanager is NULL");
        }
        if (_spawnManager == null)
        {
            Debug.LogError("Player.spawn manager is NULL");
        }
        if (_shield == null)
        {
            Debug.LogError("Player.shield is NULL");
        }
        if (LaserPrefab == null)
        {
            Debug.LogError("Player.Laserprefab is NULL");
        }
        if (LaserTriplePrefab == null)
        {
            Debug.LogError("Player.LaserTriplePrefab is NULL");
        }

        if (_audioSource == null)
        {
            Debug.LogError("Player.audiosource is NULL");
        }
        else
        {
            _audioSource.clip = _laserAudioClip;
        }

    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();

        //Sprint with Energy System
        if (_energyCurrent < 0 || Input.GetButtonUp("Fire3"))
        {
            if (_energyCurrent < 0)
            {
                _energyCurrent = 0;
            }
            if (_isSprint == true)
            {
                _isSprint = false;

                _speed /= _sprintModifier;
                //vfx
                _thruster.transform.localScale -= _scaleChange;
                _thruster.transform.position -= _posChange;
            }
        }
        else if (_energyCurrent > 3 && Input.GetButtonDown("Fire3"))
        {
            _isSprint = true;

            _speed *= _sprintModifier;
            //vfx
            _thruster.transform.localScale += _scaleChange;
            _thruster.transform.position += _posChange;
        }
        if (_isSprint == true)
        {
            //drain energy
            _energyCurrent -= _sprintStaminaCost * Time.deltaTime;            
        }
        else if (_isSprint == false)
        {
            if (_energyCurrent >= _energyMax)
            {
                _energyCurrent = _energyMax;
            }
            else
            {
                //recharge energy
                _energyCurrent += _energyCharge * Time.deltaTime;                
            }
        }
        _uIManager.UpdateUIEnergy(_energyCurrent);

        //When fire is pressed AND current game time is greater then previous laser fire time + fire rate(cooldown)
        if (Input.GetButton("Fire1") && Time.time > _nextFire)
        {
            FireLaser();
        }

    }

    public void AddScore(int scoreValue)
    {
        _uIManager.UpdateUIScore(scoreValue);
    }

    public void Damage()
    {
        AudioSource.PlayClipAtPoint(_playerDamaged, Camera.main.transform.position);

        //use shakebehaviour script method
        _camera.CameraShake();

        if (_isShieldActive == true)
        {
            //deactivate shield gameobject
            _shield.SetActive(false);
            _isShieldActive = false;
            //exit function with return
            return;
        }
        else
        {
            _health--;            
        }
        PlayerHealthVisual();
    }

    private void Repair()
    {
        if (_health < 3)
        {
            _health++;
            PlayerHealthVisual();
        }
    }
    private void PlayerHealthVisual()
    {
        //hurt animatoins
        switch (_health)
        { 
            case 3:
                _leftEngine.SetActive(false);
                _rightEngine.SetActive(false);
                break;
            case 2:
                _leftEngine.SetActive(true);
                _rightEngine.SetActive(false);
                break;
            case 1:
                _rightEngine.SetActive(true);
                break;
            case 0:
                Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
                _spawnManager.OnPlayerDeath();
                Destroy(gameObject);
                break;
                //same as case 0: just in case a player is damaged twice in a frame
            case -1: 
                Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
                _spawnManager.OnPlayerDeath();
                Destroy(gameObject);
                break;
            default:
                Debug.Log("Default value in switch statement in Player script for health");
                break;
        }
        //UI player health update
        _uIManager.UpdateUIHealth(_health);
    }

    private void FireLaser()
    {
        _nextFire = Time.time + _fireRate;

        //if is triple laser active
        if (_isTripleLaserActive == true)
        {
            //instantiate 3 lasers
            Instantiate(LaserTriplePrefab, transform.position + _laserOffset, Quaternion.identity);
        }
        //else fire 1 laser
        else {
            //instantite laser + Y offset
            Instantiate(LaserPrefab, transform.position + _laserOffset, Quaternion.identity);
        }
        //play audio (after light coz its slower, even if its space)
        _audioSource.Play();
    }

    private void PlayerMovement()
    {
        //move player based on user input
        transform.Translate(Input.GetAxis("Horizontal") * _speed * Time.deltaTime, Input.GetAxis("Vertical") * _speed * Time.deltaTime, 0);

        //animation turn left and right
        if (Input.GetAxis("Horizontal") < 0)
        {
            _animator.SetBool("TurningLeft", true);
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            _animator.SetBool("TurningRight", true);
        }
        else
        {
            _animator.SetBool("TurningLeft", false);
            _animator.SetBool("TurningRight", false);
        }

        //player position restriction on Y axis using Mathf.Clamp
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, _minPosY, _maxPosY), 0);

        //player position wrap around on X axis
        if (transform.position.x >= _maxPosX)
        {
            transform.position = new Vector3(_minPosX, transform.position.y, 0);
        }
        else if (transform.position.x <= _minPosX)
        {
            transform.position = new Vector3(_maxPosX, transform.position.y, 0);
        }
    }

    public void PowerUpPickUp(string PowerUpType)
    {
        switch (PowerUpType)
        {
            case "TripleShot":                
                StartCoroutine(TripleShotCooldown());
                break;
            case "SpeedUp":
                StartCoroutine(SpeedUpCooldown());
                break;
            case "Shield":
                _isShieldActive = true;
                //activate shield game object
                _shield.SetActive(true);
                break;
            case "Repair":
                Repair();
                break;
            default:
                Debug.Log("Default value in switch statement in Player script for PowerUpType");
                break;
        }
    }

    IEnumerator TripleShotCooldown()
    {
        _isTripleLaserActive = true;
        yield return new WaitForSeconds(5f);
        _isTripleLaserActive = false;
    }
    IEnumerator SpeedUpCooldown()
    {
        _thruster.transform.localScale += _scaleChangeSpeedPowerUp;
        _thruster.transform.position += _posChangeSpeedPowerUp;
                
        _speed *= _speedPowerupModifier;
        yield return new WaitForSeconds(5f);
        _speed /= _speedPowerupModifier;

        _thruster.transform.localScale -= _scaleChangeSpeedPowerUp;
        _thruster.transform.position -= _posChangeSpeedPowerUp;
        
    }
}
