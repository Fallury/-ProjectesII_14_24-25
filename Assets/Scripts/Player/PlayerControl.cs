using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D playerRb;
    private Vector2 moveInput;
    private Animator myAnimator;
    private SpriteRenderer mySpriteRender;

    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRender = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float movementX = Input.GetAxisRaw("Horizontal");
        float movementY = Input.GetAxisRaw("Vertical");
        moveInput = new Vector2(movementX, movementY).normalized;

        myAnimator.SetFloat("moveX", movementX);
        myAnimator.SetFloat("moveY", movementY);
    }

    private void FixedUpdate()
    {
        playerRb.MovePosition(playerRb.position + moveInput * speed * Time.fixedDeltaTime);
    }
}
