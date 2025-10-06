using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    [Header("Base player settings")]
    [SerializeField] private Animator _animator;

    [Header("Health bar")]
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _currentHealth;
    [SerializeField] private Image _healthBar;


    public static PlayerInfo instance;

    private void Awake()
    {
        instance = this;
        _currentHealth = _maxHealth;
    }
    
    public void GetDamage(float damage)
    {
        AnimHit();
        _currentHealth -= damage;
        _healthBar.fillAmount = _currentHealth / _maxHealth;
        if (_currentHealth <= 0)  Death(); 
    }
    private void Death()
    {
        LevelManager.Instance.PlayerDeath();
        GetComponent<PlayerMove>().enabled = false;
        GetComponent<AtackerScript>().enabled = false;
    }


    public void AnimAtack()
    {
        _animator.Play("WingStabAttack");
    }
    public void AnimShoot()
    {
        _animator.Play("ProjectileAttack");
    }
    public void AnimMoving(bool isActive)
    {
        _animator.SetBool("isMoving", isActive);
    }
    public void AnimHit()
    {
        _animator.Play("TakeDamage");
    }
}
