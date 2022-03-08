using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class player : MonoBehaviour
{
    Vector2 minBounds;
    Vector2 maxBounds;
    Vector2 rawInput;
    shooter shooter;
    
    [Header("Ship Features")]
    [SerializeField] float flySpeed = 2f;
    
    [Header("Map padding")]
    [SerializeField] float paddingTop;
    [SerializeField] float paddingBottom;
    [SerializeField] float paddingLeft;
    [SerializeField] float paddingRight;  
    
    
    void Awake() 
    {
        shooter = GetComponent<shooter>();    
    }
    
    void Start(){InitBounds();}

    void Update()
    {
        Move();
    }

    void Move()
    {
        Vector3 delta = rawInput * flySpeed * Time.deltaTime;
        Vector2 newPos = new Vector2();
        newPos.x = Mathf.Clamp(transform.position.x + delta.x,minBounds.x + paddingLeft,maxBounds.x - paddingRight);
        newPos.y = Mathf.Clamp(transform.position.y + delta.y,minBounds.y + paddingBottom,maxBounds.y - paddingTop);
        transform.position = newPos;
    }

    void OnMove(InputValue value)
    {   
        rawInput = value.Get<Vector2>();
    }
    
    void InitBounds()
    {
        Camera main = Camera.main;
        minBounds = main.ViewportToWorldPoint(new Vector2(0f,0f));
        maxBounds = main.ViewportToWorldPoint(new Vector2(1f,1f));

    }

    void OnFire(InputValue value)
    {
        if (shooter != null)
        {
            shooter.isFiring = value.isPressed;
        }
    }

}
