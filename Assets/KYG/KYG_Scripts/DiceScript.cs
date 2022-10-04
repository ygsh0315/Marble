using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceScript : MonoBehaviour
{
    static Rigidbody rb;
    public static Vector3 diceVelocity;
    //public float dirX = 0;
    //public float dirY = 0;
    //public float dirZ = 0;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        transform.rotation = Quaternion.identity;
    }

    // Update is called once per frame
    void Update()
    {
        //diceVelocity = rb.velocity;

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    DiceNumberTextScript.diceNumber = 0;
        //    //dirX = 0;
        //    //dirY = 0;
        //    //dirZ = 250;
        //    transform.position = new Vector3(12, transform.position.y + 2, 15);
        //    transform.rotation = Quaternion.identity;
        //    rb.AddForce(transform.up * 500);
        //    rb.AddTorque(dirX, dirY, dirZ);
        //}
    }
    public void Dice(int dice1)
    {
        diceVelocity = rb.velocity;


        DiceNumberTextScript.diceNumber = 0;
        if (dice1 == 1)
        {
            float dirX = 50;
            float dirY = 300;
            float dirZ = 50;
            transform.position = new Vector3(11, transform.position.y + 2, 15);
            transform.rotation = Quaternion.identity;
            rb.AddForce(transform.up * 500);
            rb.AddTorque(dirX, dirY, dirZ);
        }
        else if (dice1 == 2)
        {
            float dirX = 0;
            float dirY = 0;
            float dirZ = 250;
            transform.position = new Vector3(11, transform.position.y + 2, 15);
            transform.rotation = Quaternion.identity;
            rb.AddForce(transform.up * 500);
            rb.AddTorque(dirX, dirY, dirZ);
        }
        else if (dice1 == 3)
        {
            float dirX = 100;
            float dirY = 500;
            float dirZ = 100;
            transform.position = new Vector3(11, transform.position.y + 2, 15);
            transform.rotation = Quaternion.identity;
            rb.AddForce(transform.up * 500);
            rb.AddTorque(dirX, dirY, dirZ);
        }
        else if (dice1 == 4)
        {
            float dirX = 500;
            float dirY = 300;
            float dirZ = 800;
            transform.position = new Vector3(11, transform.position.y + 2, 15);
            transform.rotation = Quaternion.identity;
            rb.AddForce(transform.up * 500);
            rb.AddTorque(dirX, dirY, dirZ);
        }
        else if (dice1 == 5)
        {
            float dirX = 400;
            float dirY = 200;
            float dirZ = 300;
            transform.position = new Vector3(11, transform.position.y + 2, 15);
            transform.rotation = Quaternion.identity;
            rb.AddForce(transform.up * 500);
            rb.AddTorque(dirX, dirY, dirZ);
        }
        else if (dice1 == 6)
        {
            float dirX = 100;
            float dirY = 200;
            float dirZ = 100;
            transform.position = new Vector3(11, transform.position.y + 2, 15);
            transform.rotation = Quaternion.identity;
            rb.AddForce(transform.up * 500);
            rb.AddTorque(dirX, dirY, dirZ);
        }

    }
}
