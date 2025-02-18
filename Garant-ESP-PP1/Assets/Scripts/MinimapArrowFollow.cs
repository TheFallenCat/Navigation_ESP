using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapArrowFollow : FollowXZPlayer
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        transform.SetPositionAndRotation(transform.position, Quaternion.Euler(90, base.player.rotation.eulerAngles.y, 0));
    }
}
