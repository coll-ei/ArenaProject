using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtackerScript : MonoBehaviour
{

    [Header("Range atack settings")]
    [SerializeField] private float _rangeCalmDown;
    [SerializeField] private GameObject _bulletPref;
    [SerializeField] private GameObject _fromWhere;
    


    [Header("Melee atack settings")]
    [SerializeField] private float _meleeCalmDown;
    [SerializeField] private float _atackTime;
    [SerializeField] private GameObject _damageZone;
    private bool _canAtack = true;

    private bool _isAnimActive;

    private void Update()
    {
        if (!_isAnimActive)
        {
            if (Input.GetMouseButtonDown(0) && _canAtack)
            {
                StartCoroutine(MelleAtack());
            }

            else if (Input.GetMouseButtonDown(1))
            {
                StartCoroutine(Shoot());
            }
        }

    }
    IEnumerator Shoot()
    {
        _isAnimActive = true;
        PlayerInfo.instance.AnimShoot();
        Instantiate(_bulletPref, _fromWhere.transform.position, transform.rotation);
        yield return new WaitForSeconds(_rangeCalmDown);
        _isAnimActive = false;

    }
    IEnumerator MelleAtack()
    {
        _isAnimActive = true;
        PlayerInfo.instance.AnimAtack();
        _damageZone.SetActive(true);
        _canAtack = false;

        yield return new WaitForSeconds(_atackTime);
        _isAnimActive = false;
        _damageZone.SetActive(false);

        yield return new WaitForSeconds(_meleeCalmDown);
        _canAtack = true;
    }

}
