using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float _speed = 10f;
    private float _currentTime = 2;
    private GameObject _enemy;

    private Bounds _boundsProjectile;
    private Bounds _boundsEnemy;
    private void Start()
    {
        _boundsProjectile = new Bounds(transform.position, new Vector3(1f, 1, 0.5f));
        _boundsEnemy = new Bounds(_enemy.transform.position, new Vector3(1f, 1, 1f));
    }
    private void Update()
    {
        transform.position += transform.forward * Time.deltaTime * _speed;

        _boundsProjectile = new Bounds(transform.position, new Vector3(1f, 1, 0.5f));
        _boundsEnemy = new Bounds(_enemy.transform.position, new Vector3(1f, 1, 1f));
        if (_boundsProjectile.Intersects(_boundsEnemy))
        {
            Destroy(gameObject);
            return;
        }
        _currentTime -= Time.deltaTime;
        if (_currentTime <= 0)
            Destroy(gameObject);
    }
    public void SetEnemy(GameObject enemy)
    {
        _enemy = enemy;
    }
}
