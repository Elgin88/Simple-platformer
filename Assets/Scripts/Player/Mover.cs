using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]

public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _forceJump;
    [SerializeField] private float _delayAfterJump;
    [SerializeField] private float _maxRightRotationZ;
    [SerializeField] private float _maxLeftRotationZ;

    private Animator _animator;
    private Rigidbody2D _rigidbody;

    private Quaternion _maxRightQuaternion;
    private Quaternion _maxLeftQuaternion;

    private bool _isRight = true;
    private bool _isLeft = false;

    private string _idleLeft = "IdleLeft";
    private string _idleRight = "IdleRight";
    private string _moveLeft = "MoveLeft";
    private string _moveRight = "MoveRight";

    private string _currentAnimation = "IdleRight";    

    public void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        _maxRightQuaternion = Quaternion.Euler(0, 0, _maxRightRotationZ);
        _maxLeftQuaternion = Quaternion.Euler(0, 0, _maxLeftRotationZ);
        
        StartCoroutine(MoveRight());
        StartCoroutine(MoveLeft());
        StartCoroutine(Jump());
        StartCoroutine(SetRotation());
        StartCoroutine(PlayEdle());
    }

    public void Update()
    {
        _animator.Play(_currentAnimation);
    }

    private IEnumerator PlayEdle()
    {
        while (true)
        {
            if (_isLeft & _rigidbody.velocity.x == 0 & _rigidbody.velocity.y == 0)
            {
                _animator.StopPlayback();
                _currentAnimation = _idleLeft;
            }
            
            else if (_isRight & _rigidbody.velocity.x == 0 & _rigidbody.velocity.y == 0)
            {
                _animator.StopPlayback();
                _currentAnimation = _idleRight;
            }

            yield return null;
        }
    }

    private IEnumerator Jump()
    {
        while (true)
        {
            if (Input.GetKey(KeyCode.Space) & _rigidbody.velocity.y == 0)
            {
                _rigidbody.AddForce(Vector2.up * _forceJump, ForceMode2D.Force);
            }

            yield return null;
        }
    }

    private IEnumerator SetRotation()
    {
        while (true)
        {
            if (transform.rotation.z < _maxRightRotationZ)
                transform.rotation = _maxRightQuaternion;

            if (transform.rotation.z > _maxLeftRotationZ)
                transform.rotation = _maxLeftQuaternion;

            yield return null;
        }
    }

    private IEnumerator MoveRight()
    {
        while (true)
        {
            if (Input.GetKey(KeyCode.D))
            {
                _isRight = true;
                _isLeft = false;

                transform.Translate(_speed * Time.deltaTime, 0, 0);
                _currentAnimation = _moveRight;
            }

            yield return null;
        }       
    }

    private IEnumerator MoveLeft()
    {
        while (true)
        {
            if (Input.GetKey(KeyCode.A))
            {
                _isRight = false;
                _isLeft = true;

                transform.Translate(_speed * Time.deltaTime * -1, 0, 0);
                _currentAnimation = _moveLeft;
            }

            yield return null;
        }
    }
}
