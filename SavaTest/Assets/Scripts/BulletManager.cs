using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{

    private void Start()
    {
        StartCoroutine("DestroSelf");
    }

    //自动销毁
    IEnumerator DestroSelf()
    {
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
    }
}
