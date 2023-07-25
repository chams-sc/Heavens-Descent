using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse_Follow : MonoBehaviour
{
    public float moveSpeed= 0.2f;
    public float rotateSpeed = 2f;
    public float radius = 400;
    public Rigidbody2D rb ;
    Vector2 position = new Vector2(0f, 0f);
    Vector2 movement;
    Vector2 mousePos;
    Vector2 initialPos;
    private GameObject player;
   
     void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    
    }

     void FixedUpdate()
    {
        rb.MovePosition(Vector2.Lerp(transform.position, mousePos, moveSpeed));

        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation =  Mathf.Lerp(rb.rotation, angle, rotateSpeed);

        var allowedPos = mousePos - initialPos;
        allowedPos = Vector2.ClampMagnitude(allowedPos, 2);
        transform.position = initialPos + allowedPos;

    }
}
