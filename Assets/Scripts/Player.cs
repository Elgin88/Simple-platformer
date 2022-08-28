using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]

public class Player : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private float _speed;
    [SerializeField] private float _force;
    [SerializeField] private float _delayAfterJump;

    private Rigidbody2D _rigidbody;
    private int _currentHealth;
    private string _moveLeft;
    private string _moveRight;
    private float _timeAfterLastJump;

    private bool _isLeft = false;
    private bool _isRight = false;

    public event UnityAction<int> HealthChanged;

    public void Start()
    {
        _currentHealth = _health;
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        _timeAfterLastJump += Time.deltaTime;

        if (Input.GetKey(KeyCode.D))
        {
            _isRight = true;
            _isLeft = false;
            transform.Translate(_speed * Time.deltaTime, 0,0);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            _isLeft = true;
            _isRight = false;
            transform.Translate(_speed * Time.deltaTime * -1, 0, 0);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            if (_timeAfterLastJump > _delayAfterJump)
            {
                _rigidbody.AddForce(Vector2.up * _force, ForceMode2D.Force);
                _timeAfterLastJump = 0;
            }
        }
    }
}
