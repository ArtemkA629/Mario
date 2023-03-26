using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _friction;
    [SerializeField] private Rigidbody2D _rigidBody2D;
    [SerializeField] private WindowShower _windowShower;

    private float _horizontal;

    public bool grounded;

    private void Update()
    {
        _horizontal = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
            Jump();
    }

    private void FixedUpdate()
    {
        float speedMultiplier = 1f;

        if (!grounded)
        {
            speedMultiplier = 0.2f;
            if (Mathf.Abs(_rigidBody2D.velocity.x) > Mathf.Abs(_maxSpeed) && _horizontal != 0)
                speedMultiplier = 0f;
        }
        else
            _rigidBody2D.AddForce(new Vector2(-_rigidBody2D.velocity.x * _friction, 0f), ForceMode2D.Impulse);

        _rigidBody2D.AddForce(new Vector2(_horizontal * _speed * speedMultiplier, 0f), ForceMode2D.Impulse);
    }

    private void OnCollisionStay2D(Collision2D collision2D)
    {
        Enemy enemy = collision2D.gameObject.GetComponent<Enemy>();
        for (int i = 0; i < collision2D.contactCount; i++)
        {
            float angle = Vector2.Angle(collision2D.contacts[i].normal, Vector2.up);
            if (angle < 45f)
            {
                grounded = true;
                if (enemy)
                    Destroy(collision2D.gameObject);
            }
            else if (enemy)
                _windowShower.ShowWindow(false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision2D)
    {
        grounded = false;
    }

    private void Jump()
    {
        _rigidBody2D.AddForce(new Vector2(0f, _jumpForce), ForceMode2D.Impulse);
    }
}