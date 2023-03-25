using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private Rigidbody2D _rigidBody2D;
    [SerializeField] private WindowShower _windowShower;

    public bool _isGrounded;

    private void Update()
    {
        Vector2 heroPosition = transform.position;
        if (Input.GetKey(KeyCode.A))
            transform.position = new Vector2(heroPosition.x - _speed, heroPosition.y);
        else if (Input.GetKey(KeyCode.D))
            transform.position = new Vector2(heroPosition.x + _speed, heroPosition.y);
    }

    private void FixedUpdate()
    {
        Vector2 heroPosition = transform.position;
        if (Input.GetKey(KeyCode.W) && _isGrounded)
            _rigidBody2D.AddForce(transform.up * _jumpForce);
    }

    private void OnCollisionStay2D(Collision2D collision2D)
    {
        Enemy enemy = collision2D.gameObject.GetComponent<Enemy>();
        for (int i = 0; i < collision2D.contactCount; i++)
        {
            float angle = Vector2.Angle(collision2D.contacts[i].normal, Vector2.up);
            if (angle < 45f)
            {
                _isGrounded = true;
                if (enemy)
                    Destroy(collision2D.gameObject);
            }
            else if (enemy)
                _windowShower.ShowWindow(false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision2D)
    {
        _isGrounded = false;
    }
}