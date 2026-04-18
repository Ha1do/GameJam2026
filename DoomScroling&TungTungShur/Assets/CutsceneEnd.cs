using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class CutsceneEnd : MonoBehaviour
{
    public PlayableDirector director;

    void Update()
    {
        if (director.state != PlayState.Playing)
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
