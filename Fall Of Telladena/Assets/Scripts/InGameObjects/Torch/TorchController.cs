using UnityEngine;

public class TorchController : MonoBehaviour

{
    //Light myLight;
    public GameObject myLightZone;
    //public float timeBeforeNewLightInput;
    float timerBeforeNewLightInput;
    bool hasReceivedLight = false;
    bool sameLightImpulseTmp = false;

    PowersController player;

    // Start is called before the first frame update
    void Start()
    {
        //myLight = GetComponent<Light>();
        //myLightZone = GameObject.Find("LightPower");
        myLightZone.SetActive(false);
        //Get the Renderer component from the new cube
        var myRenderer = GetComponent<Renderer>();
        //Call SetColor using the shader property name "_Color" and setting the color to red
        myRenderer.material.SetColor("_EmissionColor", Color.white * 0);

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PowersController>();
    }

    /*
    // Update is called once per frame
    void Update()
    {
        //myLight.intensity = 1; // Mathf.PingPong(Time.time, 8);
        if (timerBeforeNewLightInput > 0)
        {
            --timerBeforeNewLightInput;
        }
    }
    */

    /*
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("LightInput") && myLight.intensity <= 0 && !hasReceivedLight && timerBeforeNewLightInput <= 0)
        {
            myLight.intensity = 1;
            myLightZone.SetActive(true);

            timerBeforeNewLightInput = 60 * timeBeforeNewLightInput;

            //Get the Renderer component from the new cube
            var myRenderer = GetComponent<Renderer>();
            //Call SetColor using the shader property name "_Color" and setting the color to red
            myRenderer.material.SetColor("_EmissionColor", Color.white*2);

            hasReceivedLight = true;
        }
    }
    */

    void SwitchLight()
    {
        if (hasReceivedLight)
        {
            myLightZone.SetActive(false);

            //Get the Renderer component from the new cube
            var myRenderer = GetComponent<Renderer>();
            //Call SetColor using the shader property name "_Color" and setting the color to red
            myRenderer.material.SetColor("_EmissionColor", Color.white * 0);

            hasReceivedLight = false;

        }
        else
        {
            myLightZone.SetActive(true);

            //Get the Renderer component from the new cube
            var myRenderer = GetComponent<Renderer>();
            //Call SetColor using the shader property name "_Color" and setting the color to red
            myRenderer.material.SetColor("_EmissionColor", Color.white * 2);

            hasReceivedLight = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LightInput") && Vector3.Distance(other.transform.position, transform.position) != 0)
        {
            SwitchLight();
        }
        if (other.CompareTag("LightInputPlayer") && player.sameLightImpulse != sameLightImpulseTmp)
        {
            SwitchLight();
            sameLightImpulseTmp = player.sameLightImpulse;
        }
    }
}
