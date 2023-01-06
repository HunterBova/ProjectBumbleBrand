using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getChildColliders : MonoBehaviour
{
    private Collider[] everythingIOwn;
    // Start is called before the first frame update
    void Start()
    {
        everythingIOwn = this.GetComponentsInChildren<Collider>();
        StartCoroutine(startTest());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator startTest()
    {
        //mapManager.find.AddToNoFlyZone(everythingIOwn);
        yield return new WaitForSeconds(10);
        //mapManager.find.RemoveFromNoFlyZone(everythingIOwn);
        Debug.Log("Complete");
    }
}
