using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

public class Player : MonoBehaviour
{
    private int _currentNumberOfCoins;

    public void TakeCoin()
    {
        _currentNumberOfCoins++;               
    }
}