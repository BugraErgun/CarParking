using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public bool forward;
    public float speed;
    bool startPosState = false;   

    public Transform parent;

    public GameObject[] trails;

    public GameManager gameManager;

    public GameObject crashPoint;

    void Start()
    {
       
    }

    void Update()
    {
        if (!startPosState)
            transform.Translate((speed/2) * Time.deltaTime * transform.forward);
        if (forward)
            transform.Translate(speed*Time.deltaTime*transform.forward);
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        
         if (collision.gameObject.tag == "Parking")
        {
            forward = false;
            trails[0].SetActive(false);
            trails[1].SetActive(false);
            transform.SetParent(parent);

            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

            gameManager.GetNewCar();
        }
        
        else if (collision.gameObject.tag == "Car")
        {
            gameManager.endGame = true;

            gameObject.SetActive(false);
            gameManager.Lose();
            gameManager.crash.transform.position = crashPoint.transform.position;
            gameManager.crash.Play();
            gameManager.SFX[3].Play();
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Destroyer")
        {
            gameObject.SetActive(false);
        }
        else if (other.gameObject.tag == "StartPos")
        {
            startPosState = true;
            
        }
        else if (other.tag == "Diamond")
        {
           other.gameObject.SetActive(false);
            gameManager.diamondCount++;
            gameManager.SFX[1].Play();

        }
        else if (other.tag == "Mid")
        {
            gameManager.endGame = true;
            gameObject.SetActive(false);
            gameManager.Lose();
            gameManager.crash.transform.position = crashPoint.transform.position;
            gameManager.crash.Play();
            gameManager.SFX[3].Play();
        }
    }

}
