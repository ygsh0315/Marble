using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceScriptTwo1 : MonoBehaviour
{
    static Rigidbody rb;
    public static Vector3 diceVelocity;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();   
    }

    // Update is called once per frame
    void Update()
    {
        //diceVelocity = rb.velocity;

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    DiceNumberTextScript.diceNumber = 0;
        //    float dirX = 100;
        //    float dirY = 500;
        //    float dirZ = 100;
        //    transform.position = new Vector3(0, transform.position.y + 2, 0);
        //    transform.rotation = Quaternion.identity;
        //    //transform.rotation = Quaternion.Euler(-17.66f,10.672f,-31.847f);
        //    rb.AddForce(transform.up * 500);
        //    rb.AddTorque(dirX, dirY, dirZ);
        //}

    }

    public void Dice(int dice2)
    {
        diceVelocity = rb.velocity;

    
            DiceNumberTextScript.diceNumber = 0;
        if (dice2 == 1)
        {
            float dirX = 50;
            float dirY = 300;
            float dirZ = 250;
            transform.position = new Vector3(17, transform.position.y + 2, 15);
            transform.rotation = Quaternion.Euler(-17.66f, 10.672f, -31.847f);
            rb.AddForce(transform.up * 500);
            rb.AddTorque(dirX, dirY, dirZ);
        }
        else if (dice2 == 2)
        {
            float dirX = 0;
            float dirY = 0;
            float dirZ = 250;
            transform.position = new Vector3(17, transform.position.y + 2, 15);
            transform.rotation = Quaternion.Euler(-17.66f, 10.672f, -31.847f);
            rb.AddForce(transform.up * 500);
            rb.AddTorque(dirX, dirY, dirZ);
        }
        else if (dice2 == 3)
        {
            float dirX = 100;
            float dirY = 500;
            float dirZ = 100;
            transform.position = new Vector3(17, transform.position.y + 2, 15);
            transform.rotation = Quaternion.Euler(-17.66f, 10.672f, -31.847f);
            rb.AddForce(transform.up * 500);
            rb.AddTorque(dirX, dirY, dirZ);
        }
        else if (dice2 == 4)
        {
            float dirX = 500;
            float dirY = 300;
            float dirZ = 800;
            transform.position = new Vector3(17, transform.position.y + 2, 15);
            transform.rotation = Quaternion.Euler(-17.66f, 10.672f, -31.847f);
            rb.AddForce(transform.up * 500);
            rb.AddTorque(dirX, dirY, dirZ);
        }
        else if (dice2 == 5)
        {
            float dirX = 100;
            float dirY = 200;
            float dirZ = 400;
            transform.position = new Vector3(17, transform.position.y + 2, 15);
            transform.rotation = Quaternion.Euler(-17.66f, 10.672f, -31.847f);
            rb.AddForce(transform.up * 500);
            rb.AddTorque(dirX, dirY, dirZ);
        }
        else if (dice2 == 6)
        {
            float dirX = 400;
            float dirY = 200;
            float dirZ = 300;
            transform.position = new Vector3(17, transform.position.y + 2, 15);
            transform.rotation = Quaternion.Euler(-17.66f, 10.672f, -31.847f);
            rb.AddForce(transform.up * 500);
            rb.AddTorque(dirX, dirY, dirZ);
        }

    }
}
