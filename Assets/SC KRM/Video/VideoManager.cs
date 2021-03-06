using Cysharp.Threading.Tasks;
using K4.Threading;
using Newtonsoft.Json;
using SCKRM.ProjectSetting;
using SCKRM.SaveLoad;
using SCKRM.Threads;
using UnityEngine;

namespace SCKRM
{
    [AddComponentMenu("SC KRM/Video/Video Manager")]
    public sealed class VideoManager : Manager<VideoManager>
    {
        [ProjectSettingSaveLoad]
        public sealed class Data
        {
            static float _standardFPS = 60; [JsonProperty] public static float standardFPS { get => _standardFPS.Clamp(0); set => _standardFPS = value; }
            static int _notFocusFpsLimit = 30; [JsonProperty] public static int notFocusFpsLimit { get => _notFocusFpsLimit.Clamp(0); set => _notFocusFpsLimit = value; }
        }

        [GeneralSaveLoad]
        public sealed class SaveData
        {
            static bool _vSync = true;
            [JsonProperty]
            public static bool vSync
            {
                get => _vSync;
                set
                {
                    _vSync = value;

                    if (ThreadManager.isMainThread)
                        FpsRefresh(Application.isFocused);
                    else
                        K4UnityThreadDispatcher.Execute(() => FpsRefresh(Application.isFocused));

                }
            }

            static int _fpsLimit = 480;
            [JsonProperty]
            public static int fpsLimit
            {
                get => _fpsLimit.Clamp(1);
                set
                {
                    _fpsLimit = value;

                    if (ThreadManager.isMainThread)
                        FpsRefresh(Application.isFocused);
                    else
                        K4UnityThreadDispatcher.Execute(() => FpsRefresh(Application.isFocused));
                }
            }
        }



        async UniTaskVoid Awake()
        {
            while (!InitialLoadManager.isInitialLoadEnd)
            {
                QualitySettings.vSyncCount = 0;
                Application.targetFrameRate = 0;

                await UniTask.DelayFrame(1, PlayerLoopTiming.LastPostLateUpdate, this.GetCancellationTokenOnDestroy());
            }

            FpsRefresh(Application.isFocused);
        }

        void OnApplicationFocus(bool focus)
        {
            if (InitialLoadManager.isInitialLoadEnd)
                FpsRefresh(focus);
        }

        static void FpsRefresh(bool focus)
        {
            //FPS Limit
            //?????? ????????? ??????????????? ????????? ???????????? ???????????? ????????? ??????????????? ??????????????????
            if (focus || Application.isEditor)
            {
                //???????????????
                if (SaveData.vSync)
                {
                    QualitySettings.vSyncCount = 1;
                    Application.targetFrameRate = ScreenManager.currentResolution.refreshRate;
                }
                else
                {
                    QualitySettings.vSyncCount = 0;
                    Application.targetFrameRate = SaveData.fpsLimit;
                }
            }
            else //?????? ????????? ????????? ???????????? ?????????????????? ????????? ???????????? ?????? ??????????????? ??????????????????
            {
                Application.targetFrameRate = Data.notFocusFpsLimit;
                QualitySettings.vSyncCount = 0;
            }
        }
    }
}
