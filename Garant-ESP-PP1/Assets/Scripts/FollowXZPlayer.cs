using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowXZPlayer : MonoBehaviour
{
    
    [SerializeField] protected Transform player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        transform.position = new Vector3(player.position.x, transform.position.y, player.position.z) ;
    }
}
