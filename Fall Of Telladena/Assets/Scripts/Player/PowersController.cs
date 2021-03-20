using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowersController : MonoBehaviour
{
    [SerializeField]
    GameObject lightPowerZone;
    [SerializeField]
    ParticleSystem lightParticles;
    [SerializeField]
    float timerLightPower = 2.0f;

    [SerializeField]
    GameObject powerGraphicsSerenity;

    public float secondsBeforeDestruction = 1;
    bool isOnLight = false;
    bool isOnSerenity = false;
    float timeoutDestructor = 0;
    public bool sameLightImpulse = false;

    //Fertility
    public GameObject particles;
    bool fertilityPowerActivated = false;
    //public Transform spawnPos;
    public FertilityZone spawnPos;
    //public GameObject spawnee;
    public StateController[] listOfBridges;
    int nextBridge = 0;

    void Start()
    {
        lightPowerZone.SetActive(false);
        lightParticles.Stop();
        /*
        powerGraphicsSerenity.SetActive(false);
        particles.SetActive(false);
        foreach(var obj in listOfBridges)
        {
            obj.ShowObject(false);
        }
        */
    }

    // every 2 seconds perform the print()
    private IEnumerator ActiveLightPower()
    {
        lightPowerZone.SetActive(true);
        lightPowerZone.GetComponent<Animation>().Play("lightPower");
        lightParticles.Play();
        yield return new WaitForSeconds(timerLightPower);
        //lightPowerZone.transform.localScale = new Vector3(0,0,0);
        lightPowerZone.GetComponent<Animation>().Play("lightPower_reverse");
        lightParticles.Stop();
        //lightPowerZone.SetActive(false);
    }

    void Update()
    {

        if (Input.GetButtonDown("Action") && Inventory.instance.isUsingTool("ClarityStone") && !GetComponent<ToolsManager>().usingATool)
        {
            GetComponent<ToolsManager>().StartCoroutine("UseTool");

            sameLightImpulse = !sameLightImpulse;
            StartCoroutine(ActiveLightPower());
        }

        /*
        if (Input.GetKeyDown(KeyCode.L) && !isOnLight)
        {
            powerGraphicsLight.SetActive(true);
            timeoutDestructor = 60 * secondsBeforeDestruction;
            isOnLight = true;
        }
        if (timeoutDestructor>=0 && isOnLight)
        {
            --timeoutDestructor;
        }
        
        if(timeoutDestructor<=0)
        {
            powerGraphicsLight.SetActive(false);
            isOnLight = false;
        }
        */

        if (Input.GetKeyDown(KeyCode.V) && !isOnSerenity)
        {
            powerGraphicsSerenity.SetActive(true);
            isOnSerenity = true;
        }
        if(Input.GetKeyUp(KeyCode.V) && isOnSerenity)
        {
            powerGraphicsSerenity.SetActive(false);
            isOnSerenity = false;
        }


        if (Input.GetKeyDown(KeyCode.F))
        {
            particles.SetActive(true);
            fertilityPowerActivated = true;
        }else if(Input.GetKeyUp(KeyCode.F) && fertilityPowerActivated)
        {
            if(!spawnPos.aBridgeIsSelected)
            {
                //UnityEngine.Debug.Log("create");
                // instanciate
                bool isFound = false;
                foreach (var obj in listOfBridges)
                {
                    if (!obj.isActive() && !isFound)
                    {
                        obj.transform.position = spawnPos.transform.position;
                        obj.transform.rotation = spawnPos.transform.rotation;
                        obj.SetRotation(spawnPos.transform.rotation);
                        obj.ResetState();
                        //obj.ShowObject(true);
                        isFound = true;
                    }
                }
                if (!isFound)
                {
                    listOfBridges[nextBridge].transform.position = spawnPos.transform.position;
                    listOfBridges[nextBridge].transform.rotation = spawnPos.transform.rotation;
                    listOfBridges[nextBridge].SetRotation(spawnPos.transform.rotation);
                    listOfBridges[nextBridge].ResetState();
                    ++nextBridge;
                    if (nextBridge >= listOfBridges.Length)
                    {
                        nextBridge = 0;
                    }
                }
            }
            else
            {
                //change position of the selected bridge
                //spawnPos.selectedBridge.transform.position += new Vector3(0, 1, 0);
                //spawnPos.selectedBridge.IncreaseState(); // not working because of Collider inheritance
                //UnityEngine.Debug.Log("changing");
                switch (spawnPos.selectedBridge)
                {
                    case "Bridge1":
                        listOfBridges[0].IncreaseState();
                        break;
                    case "Bridge2":
                        listOfBridges[1].IncreaseState();
                        break;
                    case "Bridge3":
                        listOfBridges[2].IncreaseState();
                        break;
                    default:
                        break;
                }
            }
            
            //listOfBridges[listOfBridges.Length] = Instantiate(spawnee, spawnPos.position, spawnPos.rotation);
            particles.SetActive(false);
            fertilityPowerActivated = false;
            /*
            if (listOfBridges.Length >= 3)
            {
                Destroy(listOfBridges[0]);
                listOfBridges = listOfBridges.Shift();
            }
            */
        }
    }
}
