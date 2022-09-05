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
    private string _currentAnimation;

    private float _timeAfterLastJump;        

    public void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        _maxRightQuaternion = Quaternion.Euler(0, 0, _maxRightRotationZ);
        _maxLeftQuaternion = Quaternion.Euler(0, 0, _maxLeftRotationZ);

        _currentAnimation = "IdleRight";
        
        StartCoroutine(Jump());
        StartCoroutine(MoveRight());
        StartCoroutine(MoveLeft());
        StartCoroutine(BlockRotation());
        StartCoroutine(PlayAnimation());
    }

    private IEnumerator PlayAnimation()
    {
        while (true)
        {
            _animator.Play(_currentAnimation);

            yield return null;
        }        
    }

    public void Update()
    {
        if (_isLeft)
        {
            _currentAnimation = _idleLeft;
        }

        else if (_isRight)
        {
            _currentAnimation = _idleRight;
        }
    }

    private IEnumerator Jump()
    {
        while (true)
        {
            _timeAfterLastJump += Time.deltaTime;

            if (Input.GetKey(KeyCode.Space) & _timeAfterLastJump > _delayAfterJump)
            {
                _rigidbody.AddForce(Vector2.up * _forceJump * Time.deltaTime, ForceMode2D.Force);
                _timeAfterLastJump = 0;
            }

            yield return null;
        }
    }

    private IEnumerator BlockRotation()
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
