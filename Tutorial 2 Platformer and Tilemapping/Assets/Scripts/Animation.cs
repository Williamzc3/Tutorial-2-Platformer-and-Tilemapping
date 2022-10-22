using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
public AudioClip musicClipOne;

public AudioClip musicClipTwo;

public AudioSource musicSource;

Animator anim;

// Start is called before the first frame update

void Start()

{

  anim = GetComponent<Animator>();

}

// Update is called once per frame

void Update()

{

     if (Input.GetKeyDown(KeyCode.D))

        {

          musicSource.clip = musicClipOne;

          musicSource.Play();

          anim.SetInteger("State", 1);

         }

     if (Input.GetKeyUp(KeyCode.W))

        {

          musicSource.Stop();

          anim.SetInteger("State", 0);

         }

     if (Input.GetKeyDown(KeyCode.W))

        {

          musicSource.clip = musicClipTwo;

          musicSource.Play();

          anim.SetInteger("State", 2);

         }

     if (Input.GetKeyUp(KeyCode.R))

        {

          musicSource.Stop();

          anim.SetInteger("State", 0);

         }

    }
}
