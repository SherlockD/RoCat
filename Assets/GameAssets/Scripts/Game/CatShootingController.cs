using Game.Generic.FunctionTimer;
using Game.Generic.InputManager;
using UniRx;
using UnityEngine;

public class CatShootingController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _shootingSpeed;

    [SerializeField] private Transform _bulletSpawnPosition;
    [SerializeField] private Bullet _bullet;

    [SerializeField] private FunctionTimer _shootingTimer;

    private bool _isShooting = false;

    private void Awake()
    {
        Recive();

        _shootingTimer.OnTimerEnd(() =>
        {
            ShootBullet();
            _shootingTimer.StartTimer(_shootingSpeed);
        });
    }

    private void ShootBullet()
    {
        Debug.Log("bullet");
    }

    private void Shoot(MouseClickData data)
    {
        if (_isShooting == data.IsClicked)
            return;

        _isShooting = data.IsClicked;

        switch (_isShooting)
        {
            case true:
                _shootingTimer.StartTimer(_shootingSpeed);
                break;
            case false:
                _shootingTimer.StopTimer();
                break;
        }
    }

    private void Recive()
    {
        MessageBroker.Default
            .Receive<MouseClickData>()
            .Subscribe(Shoot);
    }
}
