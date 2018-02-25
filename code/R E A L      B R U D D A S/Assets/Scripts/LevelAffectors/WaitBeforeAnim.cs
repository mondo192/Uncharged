using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitBeforeAnim : MonoBehaviour {
    Animator anim;

    // Use this for initialization
    void Awake () {
        StartCoroutine(MyCoroutine());       
    }

    IEnumerator MyCoroutine()
    {
        anim = GetComponentInChildren<Animator>();
        yield return new WaitForSeconds(5);
        anim.SetTrigger("Blink");
    }
}
