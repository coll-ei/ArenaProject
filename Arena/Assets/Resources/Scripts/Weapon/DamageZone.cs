using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    [SerializeField] private bool _isEnemy;
    [SerializeField] private float _damage;

    private void OnTriggerEnter(Collider other)
    {
        if (!_isEnemy && other.TryGetComponent<IEnemy>(out IEnemy enemy))
        {
            enemy.GetHit();
        }
        if (_isEnemy && other.TryGetComponent<PlayerInfo>(out PlayerInfo player))
        {
            player.GetDamage(_damage);
        }
    }
}
