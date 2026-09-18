using UnityEngine;
using UnityEngine.Video;

public class PlayCutscene : MonoBehaviour
{
    public string fileName = "error_screen.mp4";

    void Start()
    {
        var vp = GetComponent<VideoPlayer>();
        vp.playOnAwake = false;
        vp.source = VideoSource.Url;
        vp.url = Application.streamingAssetsPath + "/" + fileName;
        vp.errorReceived += (src, msg) => Debug.LogError("Video error: " + msg);
        //vp.Play();
    }
}