using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private int _value;
    [SerializeField]
    private GameObject _scoreSystem;
    [SerializeField]
    private GameObject _physicalPickup;
    private bool used = false;
    #endregion

    #region Properties
    public int Value
    {
        get => _value;
        set => _value = value;
    }

    public GameObject ScoreSystem
    {
        get => _scoreSystem;
        set => _scoreSystem = value;
    }
    public GameObject PhysicalPickup
    {
        get => _physicalPickup;
        set => _physicalPickup = value;
    }
    private ScoreSystem _scoreSystemScript
    {
        get => _scoreSystem.GetComponent<ScoreSystem>();
    }
    #endregion

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player")
            return;
        lock (this)
        {
            if (used)
                return;
            _scoreSystemScript.AddToPickups(_value);
            _physicalPickup.SetActive(false);
            used = true;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
}
