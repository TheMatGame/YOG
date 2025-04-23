using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{

    // CheckPoints
    Vector3 lastCheckPoint;

    // Music
    List<AudioSource> sources = new List<AudioSource>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddSound(AudioSource source) 
    {
        sources.Add(source);
        source.Play();
    }

    public void RemoveAllSounds()
    {
        sources.Clear();
    }

    public void SetCheckpoint(Vector3 checkpoint) 
    {
        lastCheckPoint = checkpoint;
    }

    public Vector3 GetLastCheckPoint() {return lastCheckPoint;}
}
