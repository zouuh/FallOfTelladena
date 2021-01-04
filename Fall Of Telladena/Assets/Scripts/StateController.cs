using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
    public int state = 0;
    public Vector3 sizeStep1 = new Vector3(1, 10, 1);
    public Vector3 sizeStep2 = new Vector3(1, 2, 1);
    public Vector3 identity = new Vector3(1, 1, 1);
    public Vector3 scaleSpeed = new Vector3(0, 4, 0);
    public Vector3 initialScale;
    public Quaternion rotationStep3 = new Quaternion(10, 0, 0, 1);
    public Quaternion rotationIdentity = new Quaternion(0, 1, 1, 1);
    Quaternion initialRotation;
    public GameObject rootObject;
    public GameObject firstRootObject;
    public GameObject rotatingObject;
    //CharacterController playerController;
    public GameObject player;

    private void Start()
    {
        initialScale = rootObject.transform.localScale;
        initialRotation = rootObject.transform.rotation;
    }

    private void Update()
    {
        if(this.transform.position.y <= -10)
        {
            if(this.state != 0)
            {
                IncreaseState();
            }
        }
    }

    public void IncreaseState()
    {
        ++this.state;
        if (this.state > 3)
        {
            this.state = 0;
        }
        UnityEngine.Debug.Log("state++ : " + state);
        ChangeState();
    }
    public void SetRotation(Quaternion rotation)
    {
        this.initialRotation = rotation;
    }
    public void ResetState()
    {
        this.state = 0;
        UnityEngine.Debug.Log("state++ : " + state);
        ChangeState();
        this.state = 1;
        ChangeState();
    }
    public void ShowObject(bool TrueOrFalse)
    {
        rootObject.SetActive(TrueOrFalse);
    }
    public bool isActive()
    {
        return rootObject.activeSelf;
    }

    void ChangeState()
    {
        switch (this.state)
        {
            case 0:
                rootObject.SetActive(false);
                rootObject.transform.localScale = initialScale;
                rotatingObject.transform.rotation = initialRotation;
                Rigidbody rb = this.GetComponent<Rigidbody>();
                rb.useGravity = false;
                rb.velocity = new Vector3(0, 0, 0);
                rb.angularVelocity = new Vector3(0, 0, 0);
                break;
            case 1:
                rootObject.SetActive(true);
                rootObject.transform.localScale = Vector3.Scale(rootObject.transform.localScale, sizeStep1);
                //rootObject.transform.localScale += scaleSpeed;
                //UnityEngine.Debug.Log(identity+" * "+sizeStep1+" = "+Vector3.Scale(identity, sizeStep1));
                //this.GetComponent<Rigidbody>().useGravity = false;
                break;
            case 2:
                rootObject.SetActive(true);
                rootObject.transform.localScale = Vector3.Scale(rootObject.transform.localScale, sizeStep2);
                //rootObject.transform.localScale += scaleSpeed;
                //this.GetComponent<Rigidbody>().useGravity = false;
                break;
            case 3:
                rootObject.SetActive(true);
                //rootObject.transform.localScale += sizeStep2;
                //rootObject.transform.rotation = rotationStep3;
                //rootObject.GetComponent<Rigidbody>().AddRelativeForce(1, 1, 5, ForceMode.Impulse);
                //rootObject.transform.position += new Vector3(0, 2, 0);
                //rootObject.transform.rotation = rotationStep3;
                //rotatingObject.transform.parent = null;
                rootObject.transform.Rotate(10f, 0f, 0f, Space.Self);
                //rotatingObject.transform.parent = rootObject.transform;
                this.GetComponent<Rigidbody>().useGravity = true;
                break;
            default:
                break;
        }
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("FlowerContactZone") && Input.GetKeyDown(KeyCode.K))
        {
            //if (Input.GetKeyUp(KeyCode.K))
            //{
            //UnityEngine.Debug.Log("climb");
            //player.transform.position = new Vector3(this.transform.position.x, this.transform.localScale.y, this.transform.position.z);
            //player.transform.position = new Vector3(10, 10, 10);
            //player.GetComponent<CharacterController>().Move(new Vector3(0, 5, 0));
            //playerController = player.GetComponent<CharacterController>();
            //player.GetComponent<CharacterController>().Move(new Vector3(0, 5, 0));
            //player.GetComponent<MovementInput>().Climb(new Vector3(0, 5, 0));
            //player.GetComponent<CharacterController>().enabled = false;
            //player.transform.position = new Vector3(5, 5, 5);
            //player.GetComponent<CharacterController>().enabled = true;
            //player.GetComponent<CharacterController>().transform.Translate(5, 5, 5);
            player.GetComponent<MovementInput>().Climb(new Vector3(0, 5, 0));
            //}

        }
    }
}
