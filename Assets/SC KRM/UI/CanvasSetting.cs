using SCKRM.UI.StatusBar;
using System;
using UnityEngine;

namespace SCKRM.UI
{
    [ExecuteAlways]
    [AddComponentMenu("SC KRM/UI/Canvas Setting")]
    public sealed class CanvasSetting : UI
    {
        [SerializeField] bool _customSetting; public bool customSetting { get => _customSetting; set => _customSetting = value; }
        [SerializeField] bool _customGuiSize; public bool customGuiSize { get => _customGuiSize; set => _customGuiSize = value; }
        [SerializeField] bool _worldRenderMode; public bool worldRenderMode { get => _worldRenderMode; set => _worldRenderMode = value; }
        [SerializeField] float _planeDistance; public float planeDistance { get => _planeDistance; set => _planeDistance = value; }

        [NonSerialized] Canvas _canvas; public Canvas canvas => _canvas = this.GetComponentFieldSave(_canvas, ComponentTool.GetComponentMode.destroyIfNull);

        [SerializeField, HideInInspector] RectTransform safeScreen;

        DrivenRectTransformTracker tracker;

        protected override void OnEnable() => Canvas.preWillRenderCanvases += Refresh;
        protected override void OnDisable()
        {
            if (!Kernel.isPlaying)
                tracker.Clear();

            Canvas.preWillRenderCanvases -= Refresh;
        }

        void Refresh()
        {
            if (canvas == null)
                return;

            if (!customGuiSize)
            {
                if (Kernel.isPlaying)
                    canvas.scaleFactor = UIManager.currentGuiSize;
                else
                    canvas.scaleFactor = 1;
            }

            if (!customSetting)
            {
                if (!worldRenderMode)
                {
                    if (Kernel.isPlaying)
                    {
                        RectTransform taskBarManager = StatusBarManager.instance.rectTransform;

                        float guiSize = 1;
                        if (customGuiSize)
                            guiSize = UIManager.currentGuiSize / canvas.scaleFactor;

                        if (canvas.renderMode == RenderMode.ScreenSpaceOverlay)
                        {
                            SafeScreenSetting();

                            safeScreen.offsetMin = StatusBarManager.cropedRect.min * guiSize;
                            safeScreen.offsetMax = StatusBarManager.cropedRect.max * guiSize;
                        }
                        else
                            SafeScreenDestroy();
                    }
                    else
                    {
                        tracker.Clear();

                        if (canvas.renderMode == RenderMode.ScreenSpaceOverlay)
                            SafeScreenSetting();
                        else
                            SafeScreenDestroy();
                    }
                }
                else
                {
                    SafeScreenDestroy();
                    WorldRenderCamera();
                }
            }
        }

        void SafeScreenSetting()
        {
            if (safeScreen == null)
            {
                if (Kernel.isPlaying)
                {
                    if (Kernel.emptyRectTransform == null)
                        return;

                    safeScreen = Instantiate(Kernel.emptyRectTransform, transform.parent);
                }
                else
                    safeScreen = new GameObject().AddComponent<RectTransform>();

                safeScreen.name = "Safe Screen";
            }

            if (!Kernel.isPlaying)
            {
                tracker.Clear();
                tracker.Add(this, safeScreen, DrivenTransformProperties.All);
            }

            if (safeScreen.parent != transform)
                safeScreen.SetParent(transform);

            safeScreen.anchorMin = Vector2.zero;
            safeScreen.anchorMax = Vector2.one;

            safeScreen.offsetMin = Vector2.zero;
            safeScreen.offsetMax = Vector2.zero;

            safeScreen.pivot = Vector2.zero;

            safeScreen.localEulerAngles = Vector3.zero;
            safeScreen.localScale = Vector3.one;

            int childCount = transform.childCount;
            for (int i = 0; i < childCount; i++)
            {
                Transform childtransform = transform.GetChild(i);
                if (childtransform != safeScreen)
                {
                    childtransform.SetParent(safeScreen);

                    i--;
                    childCount--;
                }
            }
        }

        void SafeScreenDestroy()
        {
            if (safeScreen == null)
                return;

            int childCount = safeScreen.childCount;
            for (int i = 0; i < childCount; i++)
            {
                Transform childtransform = safeScreen.GetChild(i);
                if (childtransform != safeScreen)
                {
                    childtransform.SetParent(transform);

                    i--;
                    childCount--;
                }
            }

            DestroyImmediate(safeScreen.gameObject);
        }

        void WorldRenderCamera()
        {
            if (!Kernel.isPlaying)
            {
                tracker.Clear();
                tracker.Add(this, rectTransform, DrivenTransformProperties.All);
            }


            UnityEngine.Camera camera = canvas.worldCamera;
            if (camera == null)
            {
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                return;
            }
            else
                canvas.renderMode = RenderMode.WorldSpace;



            transform.rotation = camera.transform.rotation;
            transform.position = camera.transform.position + (transform.forward * planeDistance);
            


            float width = camera.pixelWidth * (1 / UIManager.currentGuiSize);
            float height = camera.pixelHeight * (1 / UIManager.currentGuiSize);

            rectTransform.sizeDelta = new Vector2(width, height);
            rectTransform.pivot = Vector2.one * 0.5f;
            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.zero;



            float screenX, screenY;

            if (camera.orthographic)
            {
                screenY = camera.orthographicSize * 2;
                screenX = screenY / height * width;
            }
            else
            {
                screenY = Mathf.Tan(camera.fieldOfView * 0.5f * Mathf.Deg2Rad) * 2.0f * planeDistance;
                screenX = screenY / height * width;
            }

            transform.localScale = new Vector3(screenX / width, screenY / height, screenX / width);
        }
    }
}