using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// A class to make objects switchable.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class SwitchableObject : MonoBehaviour
{
    // Reference to the rigidbody
    private Rigidbody rb;
    public Rigidbody Rb => rb;
    /// <summary>
    /// Method called on initialization.
    /// </summary>
    private void Awake()
    {
        // Get reference to the rigidbody
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
