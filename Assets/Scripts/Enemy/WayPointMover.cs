using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]

[RequireComponent(typeof(Animator))]

public class WayPointMover : MonoBehaviour
{
    [SerializeField] private Transform _path;
    [SerializeField] private float _speed;

    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private Transform[] _points;
    private int _currentPoint;

    private float _currentX;
    private float _previousX;

    private string _moveLeft = "MoveLeft";
    private string _moveRight = "MoveRight";
    private string _currentAnimation;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();

        _points = new Transform[_path.childCount];

        for (int i = 0; i < _path.childCount; i++)
        {
            _points[i] = _path.GetChild(i);
        }

        StartCoroutine(Move());

        _currentX = transform.position.x;
    }

    private void Update()
    {
        _animator.Play(_currentAnimation);
    }

    private IEnumerator Move()
    {
        while (true)
        {
            _previousX = _currentX;
            transform.position = Vector2.MoveTowards(transform.position, _points[_currentPoint].position, _speed * Time.deltaTime);
            _currentX = transform.position.x;

            if (transform.position == _points[_currentPoint].position)
            {
                _currentPoint++;

                if (_currentPoint == _points.Length)
                    _currentPoint = 0;
            }

            if (_previousX < _currentX)
                _currentAnimation = _moveRight;

            else
                _currentAnimation = _moveLeft;

            yield return null;
        }               
    }
}
