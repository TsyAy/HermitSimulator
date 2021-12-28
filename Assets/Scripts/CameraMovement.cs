using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private Vector3 _cameraDistance;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = _player.transform.position+_cameraDistance;
    }
}
