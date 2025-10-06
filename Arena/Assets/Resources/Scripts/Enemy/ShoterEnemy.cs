using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShoterEnemy : MonoBehaviour, IEnemy
{
    
    [Header("Base enemy settings")]
    [SerializeField] private float _speed;
    [SerializeField] private float _hp;
    [SerializeField] private float _currentHp;
    [SerializeField] private float _speedRotation;

    [Header("Atack Settings")]
    [SerializeField] private float _distanceForAtack;
    [SerializeField] private GameObject _bulletObj;
    [SerializeField] private Transform _fromWhere;
    [SerializeField] private float _hitCalmDown;

    [Header("Other settings")]
    [SerializeField] private Animator _animator;
    [SerializeField] private float _timeUntilDeath;
    [SerializeField] private GameObject _hpBarObject;

    private Image _healthBar;
    private Transform _target;
    private Rigidbody _rigid;
    private BoxCollider _colider;

    private bool _isAlive = true;
    private bool _canAtack = true;

    public IEnumerator Atack()
    {

        _animator.Play("Atack");
        _canAtack = false;
        Instantiate(_bulletObj, _fromWhere.position, transform.rotation);

        yield return new WaitForSeconds(_hitCalmDown);
        
        _canAtack = true;
    }

    public IEnumerator Death()
    {
        _colider.enabled = false;
        _rigid.isKinematic = true;
        _isAlive = false;
        _hpBarObject.SetActive(false);


        _animator.Play("Die");
        yield return new WaitForSeconds(_timeUntilDeath);
        LevelManager.Instance.CheckCount();
        Destroy(gameObject);
    }

    public void GetHit()
    {
        if (_isAlive)
        {
            _animator.Play("TakeHit");
            _currentHp -= 1;
            _healthBar.fillAmount = _currentHp / _hp;

            if (_currentHp <= 0)
            {
                StartCoroutine(Death());
            }
        }

    }
    private void Start()
    {
        _healthBar = _hpBarObject.transform.GetChild(0).GetComponent<Image>();
        _currentHp = _hp;
        _target = PlayerInfo.instance.gameObject.transform;
        _rigid = GetComponent<Rigidbody>();
        _colider = GetComponent<BoxCollider>();
    }
    public void Move()
    {
        Vector3 direction = (_target.position - transform.position);
        direction.y = 0f;
        direction.Normalize();

        _rigid.velocity = direction * _speed + Vector3.up * _rigid.velocity.y;

        Quaternion targetRot = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, _speedRotation * Time.deltaTime);

    }

    private void Rotate()
    {
        Vector3 direction = (_target.position - transform.position);

        Quaternion targetRot = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, _speedRotation * Time.deltaTime);
    }
    void Update()
    {
        if (_isAlive)
        {
            float distance = Vector3.Distance(_target.position, transform.position);
            if (distance <= _distanceForAtack && _canAtack && _canAtack)
            {
                _animator.SetBool("Moving", false);
                StartCoroutine(Atack());
            }
            else if(distance > _distanceForAtack)
            {
                _animator.SetBool("Moving", true);
                Move();
            }
            Rotate();
        }

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _distanceForAtack);
    }
}
