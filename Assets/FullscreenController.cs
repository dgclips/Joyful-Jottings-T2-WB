using System.Runtime.InteropServices;
using UnityEngine;

public class FullscreenController : MonoBehaviour
{
    public static FullscreenController instance;

#if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void EnterFullscreen();

    [DllImport("__Internal")]
    private static extern void ExitFullscreen();

    [DllImport("__Internal")]
    private static extern void FocusUnityCanvas();
#endif

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void GoFullscreen()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        EnterFullscreen();
        Invoke(nameof(CallFocusCanvas), 0.2f); // slight delay to ensure canvas focus after transition
#else
        Debug.Log("Fullscreen only works in WebGL builds.");
#endif
    }

    public void ExitFullscreenMode()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        ExitFullscreen();
        Invoke(nameof(CallFocusCanvas), 0.2f);
#else
        Debug.Log("Exit fullscreen only works in WebGL builds.");
#endif
    }

    public void CallFocusCanvas()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        FocusUnityCanvas();
#endif
    }
}
