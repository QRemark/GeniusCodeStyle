using System.Collections;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;

    [SerializeField] private Transform _target;

    [SerializeField] private float _bulletSpeed = 2.0f;
    [SerializeField] private float _recoil = 0.1f;

    private WaitForSeconds _waitRecoil;

    private void Start()
    {
        _waitRecoil = new WaitForSeconds(_recoil);

        StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        while (enabled)
        {
            GenerateBullet();
            yield return _waitRecoil;
        }
    }

    private void GenerateBullet()
    {
        Vector3 targetDirection = (_target.position - transform.position).normalized;

        Rigidbody bulletRigidbody = Instantiate(_bulletPrefab, transform.position + targetDirection, Quaternion.identity).GetComponent<Rigidbody>();

        bulletRigidbody.transform.up = targetDirection;
        bulletRigidbody.velocity = targetDirection * _bulletSpeed;
    }
}