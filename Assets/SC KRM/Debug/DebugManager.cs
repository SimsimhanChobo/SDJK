using Newtonsoft.Json;
using SCKRM.Input;
using SCKRM.SaveLoad;
using SCKRM.UI;
using SCKRM.UI.StatusBar;
using UnityEngine;

namespace SCKRM.DebugUI
{
    [AddComponentMenu("SC KRM/Debug/UI/Debug Manager")]
    public sealed class DebugManager : UIManager<DebugManager>
    {
        [GeneralSaveLoad]
        public class SaveData
        {
            static float _textRefreshDelay = 0.1f; [JsonProperty] public static float textRefreshDelay { get => _textRefreshDelay.Clamp(0); set => _textRefreshDelay = value.Clamp(0); }
            static float _graphRefreshDelay = 0.5f; [JsonProperty] public static float graphRefreshDelay { get => _graphRefreshDelay.Clamp(0); set => _graphRefreshDelay = value.Clamp(0); }



            static float _speed = 2;
            [JsonProperty] public static float graphSpeed { get => _speed.Clamp(1); set => _speed = value.Clamp(1); }



            [JsonProperty] public static bool textShow { get; set; } = true;
            [JsonProperty] public static bool graphShow { get; set; } = true;
        }

        

        public static bool isShow { get; set; } = false;



        [SerializeField] GameObject _textLayout; public GameObject textLayout => _textLayout;
        [SerializeField] GameObject _graphLayout; public GameObject graphLayout => _graphLayout;



        void Update()
        {
            rectTransform.offsetMin = StatusBarManager.cropedRect.min;
            rectTransform.offsetMax = StatusBarManager.cropedRect.max;

            if (InitialLoadManager.isInitialLoadEnd && InputManager.GetKey("debug_manager.toggle", InputType.Down, InputManager.inputLockDenyAllForce))
                isShow = !isShow;

            if (textLayout.activeSelf != (isShow && SaveData.textShow))
                textLayout.SetActive(isShow && SaveData.textShow);
            if (graphLayout.activeSelf != (isShow && SaveData.graphShow))
                graphLayout.SetActive(isShow && SaveData.graphShow);
        }
    }
}
