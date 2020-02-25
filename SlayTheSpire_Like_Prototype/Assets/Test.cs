using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Calls the PlayBackgroundMusic method in SoundManager to play
        //Background music using the first audio clip.
        SoundManager.instance.PlayBackgroundMusic(0);   
    }
}
