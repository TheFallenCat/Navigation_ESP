using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectLoadingAgent : MonoBehaviour
{
    [SerializeField] Transform player;
    //List of GameObjects that are to be unloaded when far away
    [SerializeField] GameObject[] unloadables;
    [SerializeField] float loadRadius = 50.0f;
    [SerializeField] float loadTimer = 1f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CheckLoadRadius());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator CheckLoadRadius()
    {
        while (true)
        {
            foreach(GameObject unloadable in unloadables)
            {
                float distanceToPlayer = Vector3.Distance(player.position, unloadable.transform.position);
                
                if(distanceToPlayer <= loadRadius)
                {
                    unloadable.SetActive(true);
                }

                else
                {
                    unloadable.SetActive(false);
                }
            }

            yield return new WaitForSeconds(loadTimer);
        }
    }
}
