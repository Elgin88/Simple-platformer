using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed = 7f;
    [SerializeField] private float _force = 1500f;
    [SerializeField] private float _delayAfterJump = 0.5f;
    [SerializeField] private float _maxRightRotationZ = -0.1f;
    [SerializeField] private float _maxLeftRotationZ = 0.1f;

    private Rigidbody2D _rigidbody;
    private Quaternion _maxRightQuaternion;
    private Quaternion _maxLeftQuaternion;
    private string _moveLeft;
    private string _moveRight;
    private float _timeAfterLastJump;

    private bool _isLeft = false;
    private bool _isRight = false;

    public void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _maxRightQuaternion = Quaternion.Euler(0, 0, _maxRightRotationZ);
        _maxLeftQuaternion = Quaternion.Euler(0, 0, _maxLeftRotationZ);
    }

    public void Update()
    {
        _timeAfterLastJump += Time.deltaTime;

        if (Input.GetKey(KeyCode.D))
        {
            _isRight = true;
            _isLeft = false;
            transform.Translate(_speed * Time.deltaTime, 0, 0);
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

        if (transform.rotation.z < _maxRightRotationZ)
            transform.rotation = _maxRightQuaternion;

        if (transform.rotation.z > _maxLeftRotationZ)
            transform.rotation = _maxLeftQuaternion;
    }
}
