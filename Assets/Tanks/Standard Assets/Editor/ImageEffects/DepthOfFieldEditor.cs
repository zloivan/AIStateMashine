using System;
using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
    [CustomEditor(typeof(DepthOfField))]
    internal class DepthOfFieldEditor : Editor
    {
        private SerializedObject serObj;

        private SerializedProperty visualizeFocus;
        private SerializedProperty focalLength;
        private SerializedProperty focalSize;
        private SerializedProperty aperture;
        private SerializedProperty focalTransform;
        private SerializedProperty maxBlurSize;
        private SerializedProperty highResolution;

        private SerializedProperty blurType;
        private SerializedProperty blurSampleCount;

        private SerializedProperty nearBlur;
        private SerializedProperty foregroundOverlap;

        private SerializedProperty dx11BokehThreshold;
        private SerializedProperty dx11SpawnHeuristic;
        private SerializedProperty dx11BokehTexture;
        private SerializedProperty dx11BokehScale;
        private SerializedProperty dx11BokehIntensity;

        private AnimBool showFocalDistance = new AnimBool();
        private AnimBool showDiscBlurSettings = new AnimBool();
        private AnimBool showDX11BlurSettings = new AnimBool();
        private AnimBool showNearBlurOverlapSize = new AnimBool();

        private bool useFocalDistance { get { return focalTransform.objectReferenceValue == null; } }
        private bool useDiscBlur { get { return blurType.enumValueIndex < 1; } }
        private bool useDX11Blur { get { return blurType.enumValueIndex > 0; } }
        private bool useNearBlur { get { return nearBlur.boolValue; } }


        private void OnEnable()
        {
            serObj = new SerializedObject(target);

            visualizeFocus = serObj.FindProperty("visualizeFocus");

            focalLength = serObj.FindProperty("focalLength");
            focalSize = serObj.FindProperty("focalSize");
            aperture = serObj.FindProperty("aperture");
            focalTransform = serObj.FindProperty("focalTransform");
            maxBlurSize = serObj.FindProperty("maxBlurSize");
            highResolution = serObj.FindProperty("highResolution");

            blurType = serObj.FindProperty("blurType");
            blurSampleCount = serObj.FindProperty("blurSampleCount");

            nearBlur = serObj.FindProperty("nearBlur");
            foregroundOverlap = serObj.FindProperty("foregroundOverlap");

            dx11BokehThreshold = serObj.FindProperty("dx11BokehThreshold");
            dx11SpawnHeuristic = serObj.FindProperty("dx11SpawnHeuristic");
            dx11BokehTexture = serObj.FindProperty("dx11BokehTexture");
            dx11BokehScale = serObj.FindProperty("dx11BokehScale");
            dx11BokehIntensity = serObj.FindProperty("dx11BokehIntensity");

            InitializedAnimBools();
        }

        private void InitializedAnimBools()
        {
            showFocalDistance.valueChanged.AddListener(Repaint);
            showFocalDistance.value = useFocalDistance;

            showDiscBlurSettings.valueChanged.AddListener(Repaint);
            showDiscBlurSettings.value = useDiscBlur;

            showDX11BlurSettings.valueChanged.AddListener(Repaint);
            showDX11BlurSettings.value = useDX11Blur;

            showNearBlurOverlapSize.valueChanged.AddListener(Repaint);
            showNearBlurOverlapSize.value = useNearBlur;
        }


        private void UpdateAnimBoolTargets()
        {
            showFocalDistance.target = useFocalDistance;
            showDiscBlurSettings.target = useDiscBlur;
            showDX11BlurSettings.target = useDX11Blur;
            showNearBlurOverlapSize.target = useNearBlur;
        }


        public override void OnInspectorGUI()
        {
            serObj.Update();

            UpdateAnimBoolTargets();

            EditorGUILayout.LabelField("Simulates camera lens defocus", EditorStyles.miniLabel);

            GUILayout.Label("Focal Settings");
            EditorGUILayout.PropertyField(visualizeFocus, new GUIContent(" Visualize"));
            EditorGUILayout.PropertyField(focalTransform, new GUIContent(" Focus on Transform"));

            if (EditorGUILayout.BeginFadeGroup(showFocalDistance.faded))
            {
                EditorGUILayout.PropertyField(focalLength, new GUIContent(" Focal Distance"));
            }
            EditorGUILayout.EndFadeGroup();

            EditorGUILayout.Slider(focalSize, 0.0f, 2.0f, new GUIContent(" Focal Size"));
            EditorGUILayout.Slider(aperture, 0.0f, 1.0f, new GUIContent(" Aperture"));

            EditorGUILayout.Separator();

            EditorGUILayout.PropertyField(blurType, new GUIContent("Defocus Type"));

            if (!(target as DepthOfField).Dx11Support() && blurType.enumValueIndex > 0)
            {
                EditorGUILayout.HelpBox("DX11 mode not supported (need shader model 5)", MessageType.Info);
            }

            if (EditorGUILayout.BeginFadeGroup(showDiscBlurSettings.faded))
            {
                EditorGUILayout.PropertyField(blurSampleCount, new GUIContent(" Sample Count"));
            }
            EditorGUILayout.EndFadeGroup();

            EditorGUILayout.Slider(maxBlurSize, 0.1f, 2.0f, new GUIContent(" Max Blur Distance"));
            EditorGUILayout.PropertyField(highResolution, new GUIContent(" High Resolution"));

            EditorGUILayout.Separator();

            EditorGUILayout.PropertyField(nearBlur, new GUIContent("Near Blur"));
            if (EditorGUILayout.BeginFadeGroup(showNearBlurOverlapSize.faded))
            {
                EditorGUILayout.Slider(foregroundOverlap, 0.1f, 2.0f, new GUIContent("  Overlap Size"));
            }
            EditorGUILayout.EndFadeGroup();

            EditorGUILayout.Separator();

            if (EditorGUILayout.BeginFadeGroup(showDX11BlurSettings.faded))
            {
                GUILayout.Label("DX11 Bokeh Settings");
                EditorGUILayout.PropertyField(dx11BokehTexture, new GUIContent(" Bokeh Texture"));
                EditorGUILayout.Slider(dx11BokehScale, 0.0f, 50.0f, new GUIContent(" Bokeh Scale"));
                EditorGUILayout.Slider(dx11BokehIntensity, 0.0f, 100.0f, new GUIContent(" Bokeh Intensity"));
                EditorGUILayout.Slider(dx11BokehThreshold, 0.0f, 1.0f, new GUIContent(" Min Luminance"));
                EditorGUILayout.Slider(dx11SpawnHeuristic, 0.01f, 1.0f, new GUIContent(" Spawn Heuristic"));
            }
            EditorGUILayout.EndFadeGroup();

            serObj.ApplyModifiedProperties();
        }
    }
}
