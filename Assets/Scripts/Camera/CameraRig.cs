using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRig : MonoBehaviour
{
    public float speed = 3f;
    public Transform follow;
    Transform _transform;

    void Awake()
    {
        _transform = transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (follow)
        {
            _transform.position = Vector3.Lerp(_transform.position, follow.position, speed * Time.deltaTime);
        }
    }
}
