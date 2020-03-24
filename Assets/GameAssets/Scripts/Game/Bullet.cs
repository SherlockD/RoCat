using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    public float Damage { get; private set; }

    private Rigidbody2D _rigidbody2D;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ObjectsPool.AddObjectToPool(this);
    }
}
