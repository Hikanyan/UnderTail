using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField, Tooltip("移動スピード")]
    private float _moveSpeed = 10.0f;

    [SerializeField, Tooltip("アニメーション")]
    private Animator _playerAnimator;

    Rigidbody2D _playerRigidbody;

    private void Start()
    {
        _playerRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _playerRigidbody.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * _moveSpeed;
        if (_playerRigidbody.velocity != Vector2.zero)
        {
            if (Input.GetAxisRaw("Horizontal") != 0)
            {
                if(Input.GetAxisRaw("Horizontal") > 0)
                {
                    _playerAnimator.SetFloat("X", -1f);
                    _playerAnimator.SetFloat("Y", 0);
                }
                else
                {
                    _playerAnimator.SetFloat("X", 1f);
                    _playerAnimator.SetFloat("Y", 0);
                }
            }
            else if(Input.GetAxisRaw("Vertical") > 0)
            {
                _playerAnimator.SetFloat("X", 0);
                _playerAnimator.SetFloat("Y", 1f);
            }
            else
            {
                _playerAnimator.SetFloat("X", 0);
                _playerAnimator.SetFloat("Y", -1f);
            }
        }
    }


}
