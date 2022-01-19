using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterMovement : MonoBehaviour
{
    public CharacterController con;
    [SerializeField] float speed;
    [SerializeField] float gravity;
    [SerializeField] float jumpDis;

    [SerializeField] float groundDis;
    public Transform groundcheck;
    public LayerMask groundmask;

    bool isgrounded;
    Vector3 velocity;


    [SerializeField] KeyCode jumpKey;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isgrounded = Physics.CheckSphere(groundcheck.position, groundDis, groundmask);

        if (isgrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        con.Move(move * speed * Time.deltaTime);

        if(Input.GetKey(jumpKey)&& isgrounded)
        {
            velocity.y = Mathf.Sqrt(jumpDis * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        con.Move(velocity* Time.deltaTime);
    }
}
