using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectableCounter : MonoBehaviour
{
    public AudioClip collectionSound;
    public AudioSource audioSource;
    public float volume;
    public static CollectableCounter instance;
    public Text counter;
    public string format = "0000";
    public int currentCount =0;
    private void OnEnable()
    {
       if(instance ==null) instance = this;
    }
    private void Awake()
    {
        currentCount = 0;
        if(audioSource==null)audioSource = GetComponent<AudioSource>();
    }
    private void Update(){
        counter.text = currentCount.ToString(format);
    }
    public void AddToCount(){
        currentCount++;
        if(audioSource && collectionSound){
            audioSource.PlayOneShot(collectionSound,volume);
        }
    }
}
