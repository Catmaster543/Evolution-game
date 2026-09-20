using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoUrlFixer : MonoBehaviour
{
    static readonly bool AutoPlayWhenSwitchedOn = true;

    class Tracked
    {
        public VideoPlayer vp;
        public bool live;
    }
    readonly List<Tracked> tracked = new List<Tracked>();

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void Init()
    {
        Debug.Log("[VideoUrlFixer] Init - script is running in this build");
        var go = new GameObject("VideoUrlFixer");
        DontDestroyOnLoad(go);
        go.AddComponent<VideoUrlFixer>();
    }

    void Awake()
    {
        SceneManager.sceneLoaded += (scene, mode) =>
        {
            Debug.Log("[VideoUrlFixer] Scene loaded: " + scene.name);
            Scan();
        };
        Scan();
    }

    static bool IsLive(VideoPlayer vp)
    {
        return vp.enabled && vp.gameObject.activeInHierarchy;
    }

    void Scan()
    {
        int found = 0;
        foreach (var vp in Resources.FindObjectsOfTypeAll<VideoPlayer>())
        {
            if (!vp.gameObject.scene.IsValid()) continue; 
            found++;

            Debug.Log("[VideoUrlFixer] Found '" + vp.gameObject.name + "' | source=" + vp.source +
                      " | renderMode=" + vp.renderMode + " | playOnAwake=" + vp.playOnAwake +
                      " | componentEnabled=" + vp.enabled +
                      " | activeInHierarchy=" + vp.gameObject.activeInHierarchy +
                      " | url=" + vp.url);

            if (vp.source != VideoSource.Url)
            {
                Debug.LogWarning("[VideoUrlFixer] '" + vp.gameObject.name +
                                 "' uses Video Clip source - it will NOT play on WebGL. Switch it to URL.");
                continue;
            }

            if (!string.IsNullOrEmpty(vp.url))
            {
                vp.url = Application.streamingAssetsPath + "/" + Path.GetFileName(vp.url);
                Debug.Log("[VideoUrlFixer] '" + vp.gameObject.name + "' url set to: " + vp.url);
            }

            vp.errorReceived -= OnError; vp.errorReceived += OnError;
            vp.prepareCompleted -= OnPrepared; vp.prepareCompleted += OnPrepared;
            vp.started -= OnStarted; vp.started += OnStarted;

            bool already = false;
            foreach (var t in tracked) { if (t.vp == vp) { already = true; break; } }
            if (!already) tracked.Add(new Tracked { vp = vp, live = IsLive(vp) });
        }
        Debug.Log("[VideoUrlFixer] VideoPlayers found: " + found);
    }

    void Update()
    {
        for (int i = tracked.Count - 1; i >= 0; i--)
        {
            var t = tracked[i];
            if (t.vp == null) { tracked.RemoveAt(i); continue; }

            bool live = IsLive(t.vp);
            if (live == t.live) continue;
            t.live = live;

            Debug.Log("[VideoUrlFixer] '" + t.vp.gameObject.name + "' " + (live ? "SWITCHED ON" : "SWITCHED OFF"));
            if (live && AutoPlayWhenSwitchedOn && !t.vp.isPlaying)
            {
                Debug.Log("[VideoUrlFixer] Auto-play: calling Play() on '" + t.vp.gameObject.name + "'");
                t.vp.Play();
            }
        }
    }

    static void OnPrepared(VideoPlayer p) { Debug.Log("[VideoUrlFixer] PREPARED: " + p.gameObject.name); }
    static void OnStarted(VideoPlayer p) { Debug.Log("[VideoUrlFixer] STARTED: " + p.gameObject.name); }
    static void OnError(VideoPlayer p, string msg) { Debug.LogError("[VideoUrlFixer] ERROR on " + p.gameObject.name + ": " + msg); }
}