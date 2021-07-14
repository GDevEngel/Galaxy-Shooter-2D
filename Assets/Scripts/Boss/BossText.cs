using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossText : MonoBehaviour
{
    private float _timer = 3.5f;
    private float _flicker = 0.5f;
    private float _nextFlicker = 0f;
    private Text _bossText;
    private string _text;

    //handles
    private GameObject _bgm;

    // Start is called before the first frame update
    void Start()
    {
        _bgm = GameObject.Find("Background_Music");
        _bgm.SetActive(false);

        Destroy(gameObject, _timer);
        _bossText = GetComponent<Text>();
        _text = _bossText.text;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > _nextFlicker)
        {
            _nextFlicker = Time.time + _flicker;
            if (_bossText.text == "")
            {
                _bossText.text = _text;
            }
            else
            {
                _bossText.text = "";
            }
        }
    }
}
