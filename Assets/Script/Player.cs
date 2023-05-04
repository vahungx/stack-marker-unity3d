using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Transform checkPoint;
    public Transform stackPoint;
    public Transform playerPoint;
    public Transform finishPoint;
    private Rigidbody rb;
    public LayerMask whatIsTerrain;
    public List<Brick> brickStackedList;
    public List<Brick> brickFnishedList;

    private bool isTriggered;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        brickStackedList = new List<Brick>();
        brickFnishedList = new List<Brick>();
        isTriggered = false;
    }

    // Update is called once per frame
    void Update()
    {
        PickUp();
        FinishDrop();
    }


    private void PickUp()
    {
        foreach (Collider collider in Physics.OverlapBox(checkPoint.transform.position,checkPoint.localScale / 2, checkPoint.rotation,whatIsTerrain) )
        {
            if (collider.CompareTag("Brick"))
            {
                if (collider.TryGetComponent(out Brick brick))
                {
                    brick.PickUp(this);
                }
            }
        }
    }

    private void FinishDrop ()
    {           
        if (isTriggered)
        {
            Debug.Log("Finish Drop");
            GetComponent<PlayerMovement>().Stop();
            foreach (Brick brick in brickStackedList)
            {
                brick.FinishDrop(this);
            }
        }
    }

    private void Drop ()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        isTriggered = true;
        Debug.Log(isTriggered);
    }

    private void OnTriggerExit(Collider other)
    {
        isTriggered = false;
        Debug.Log(isTriggered);
    }
}