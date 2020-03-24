using Game.Generic.InputManager;
using UniRx;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CatController : MonoBehaviour
{
    [Header("Settings")]
    [Range(0,10)]
    [SerializeField] private float _forceMultiplayer;
    [Range(0, 100)]
    [SerializeField] private float _maxSpeed;

    private Rigidbody2D _rigidbody2D;

    private Vector2 _upForce;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();

        Recive();
    }

    private void FixedUpdate()
    {
        if(_rigidbody2D.velocity.magnitude > _maxSpeed)
        {
            _rigidbody2D.velocity = _rigidbody2D.velocity.normalized * _maxSpeed;
        }

        _rigidbody2D.AddForce(_upForce * _forceMultiplayer);
    }

    private void AddForce(float value)
    {
       _upForce = Vector2.up * value;
    }

    private void Recive()
    {
        MessageBroker.Default
            .Receive<PowerSliderData>()
            .Subscribe(data => AddForce(data.Value));
    }
}
