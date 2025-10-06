using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _lifeTime;
    [SerializeField] private float _damage;
    [SerializeField] private bool EnemysBul;
    private Rigidbody _rb;

    private void Start()
    {
        StartCoroutine(DeletObj());
        _rb = GetComponent<Rigidbody>();
    }

    private void Update() => _rb.velocity = transform.forward * _speed;

    IEnumerator DeletObj()
    {
        yield return new WaitForSeconds(_lifeTime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!EnemysBul && other.TryGetComponent<IEnemy>(out IEnemy enemy) )
        {
            enemy.GetHit();
            Destroy(gameObject);
        }
        if(EnemysBul && other.TryGetComponent<PlayerInfo>(out PlayerInfo player) )
        {
            player.GetDamage(_damage);
        }
    }
}
