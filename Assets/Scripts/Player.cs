using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private float _speed;

    private int _currentHealth;
    private string _moveLeft;
    private string _moveRight;

    private bool _isLeft = false;
    private bool _isRight = false;

    public event UnityAction<int> HealthChanged;

    public void Start()
    {
        _currentHealth = _health;
    }

    public void Update()
    {
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
    }

    private IEnumerator MoveLeft()
    {
        yield return null;
    }
}
