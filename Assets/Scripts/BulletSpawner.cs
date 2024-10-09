using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;

    [SerializeField] private Transform _target;

    [SerializeField] private float _bulletSpeed = 2.0f;
    [SerializeField] private float _recoil = 0.1f;

    private void Start()
    {
        StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        while (true)
        {
            GenerateBullet();

            yield return new WaitForSeconds(_recoil);
        }
    }

    private void GenerateBullet()
    {
        Vector3 targetDirection = (_target.position - transform.position).normalized;

        GameObject newBullet = Instantiate(_bulletPrefab, transform.position + 
            targetDirection, Quaternion.identity);

        if (newBullet.TryGetComponent(out Rigidbody bulletRb))
        {
            bulletRb.transform.up = targetDirection;
            bulletRb.velocity = targetDirection * _bulletSpeed;
        }
    }
}