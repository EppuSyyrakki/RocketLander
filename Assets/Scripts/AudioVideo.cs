using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVideo : MonoBehaviour
{
    public GameObject player;
    AudioSource thruster;
    AudioSource rcs;
    AudioSource music;

    private void Start()
    {
        thruster = GameObject.Find("Thruster").GetComponent<AudioSource>();
        rcs = GameObject.Find("Rcs").GetComponent<AudioSource>();
        music = GameObject.Find("Music").GetComponent<AudioSource>();
        music.Play();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (!thruster.isPlaying)
                thruster.Play();
        }       
        else
            thruster.Stop();

        if (Input.GetAxis("Horizontal") != 0)
        {
            if (!rcs.isPlaying)
                rcs.Play();
        }
        else
            rcs.Stop();

        transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
    }

    public void Mute()
    {
        thruster.volume = 0f;
        rcs.volume = 0f;
        music.volume = 0f;
    }

    public void Unmute()
    {
        thruster.volume = 1f;
        rcs.volume = 1f;
        music.volume = 1f;
    }
}
