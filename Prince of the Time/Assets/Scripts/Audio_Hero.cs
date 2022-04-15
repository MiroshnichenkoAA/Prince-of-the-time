using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_Hero : MonoBehaviour
{
    public AudioClip[] footSteps;
    private AudioSource playerAudio;
    // Start is called before the first frame update
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FootStep()
    {
        int randInd = Random.Range(0, footSteps.Length);
        playerAudio.PlayOneShot(footSteps[randInd]);
    }
}
