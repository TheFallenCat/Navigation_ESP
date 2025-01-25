using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinuousOcean : MonoBehaviour
{
    Transform t;
    [SerializeField] Transform player;
    // Start is called before the first frame update
    void Start()
    {
        t = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        t.position = new Vector3(player.position.x, 0, player.position.z);
    }
}
