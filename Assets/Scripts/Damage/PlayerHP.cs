using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHP : MonoBehaviour, IDamagable
{
    [SerializeField] private float _playerHP = 100f;
    [SerializeField] private Slider _slider;
    public void GetDamage(float damage)
    {
        _playerHP -= damage;
    }

    private void Start()
    {
        _slider.maxValue = _playerHP;
        _slider.value = _playerHP;
    }

    private void Update()
    {
        _slider.value = _playerHP;
        if (_playerHP <= 0 || transform.position.y < -5f)
        {
            int sceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(sceneIndex);
        }
    }
}
