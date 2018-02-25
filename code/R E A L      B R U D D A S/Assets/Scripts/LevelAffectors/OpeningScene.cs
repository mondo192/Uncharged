using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningScene : MonoBehaviour
{
        public PlayerController playerMove;
        public EnemyMovement enemyMove;
        private ParticleSystem ps;
        Animator anim;
        AudioSource thunder;
        // Use this for initialization
        void Awake()
        {
            thunder = GetComponent<AudioSource>();
            StartCoroutine(MyCoroutine());
        }

        IEnumerator MyCoroutine()
        {
            thunder.Play();
            anim = GetComponentInChildren<Animator>();
            yield return new WaitForSeconds(1);
            anim.SetTrigger("gone");
        }
    }
