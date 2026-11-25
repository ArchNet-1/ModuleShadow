// ArchNet · Shadow Module
// Shadow settings for an options menu: shadow type (off, hard, soft) and shadow
// resolution. Changes apply at once and are saved between sessions.
// Built-in Render Pipeline: these are the QualitySettings shadows. URP / HDRP read shadows
// from the pipeline asset instead: use the Video Module's quality level there.
// Docs: https://archnet-plateform.fr/#ModuleShadow

using TMPro;
using UnityEngine;

namespace ArchNet.Module
{
    [AddComponentMenu("ArchNet/Shadow Module")]
    public class ShadowModule : MonoBehaviour
    {
        const string LevelKey = "ArchNet.Shadow.Level";
        const string ResolutionKey = "ArchNet.Shadow.Resolution";

        // Dropdown option order in the prefab.
        static readonly ShadowQuality[] Levels = { ShadowQuality.Disable, ShadowQuality.HardOnly, ShadowQuality.All };
        static readonly ShadowResolution[] Resolutions = { ShadowResolution.Low, ShadowResolution.Medium, ShadowResolution.High, ShadowResolution.VeryHigh };

        [Tooltip("Options: Off, Hard, Soft.")]
        [SerializeField] TMP_Dropdown _shadowLevelDropdown;
        [Tooltip("Options: Low, Medium, High, Very High.")]
        [SerializeField] TMP_Dropdown _shadowQualityDropdown;
        [Tooltip("Apply as soon as a dropdown changes. Off = wait for Save().")]
        [SerializeField] bool _applyOnChange = true;

        void Start()
        {
            // Saved choice first, current settings otherwise.
            QualitySettings.shadows = Levels[PlayerPrefs.GetInt(LevelKey, IndexOf(Levels, QualitySettings.shadows))];
            QualitySettings.shadowResolution = Resolutions[PlayerPrefs.GetInt(ResolutionKey, IndexOf(Resolutions, QualitySettings.shadowResolution))];

            Init(_shadowLevelDropdown, IndexOf(Levels, QualitySettings.shadows));
            Init(_shadowQualityDropdown, IndexOf(Resolutions, QualitySettings.shadowResolution));
        }

        /// <summary>Applies and saves the dropdown choices.</summary>
        public void Save()
        {
            if (_shadowLevelDropdown != null) SetLevel(_shadowLevelDropdown.value);
            if (_shadowQualityDropdown != null) SetResolution(_shadowQualityDropdown.value);
            PlayerPrefs.Save();
        }

        /// <summary>0 = off, 1 = hard shadows, 2 = hard and soft shadows.</summary>
        public void SetLevel(int index)
        {
            index = Mathf.Clamp(index, 0, Levels.Length - 1);
            QualitySettings.shadows = Levels[index];
            PlayerPrefs.SetInt(LevelKey, index);
        }

        /// <summary>0 = low … 3 = very high.</summary>
        public void SetResolution(int index)
        {
            index = Mathf.Clamp(index, 0, Resolutions.Length - 1);
            QualitySettings.shadowResolution = Resolutions[index];
            PlayerPrefs.SetInt(ResolutionKey, index);
        }

        void Init(TMP_Dropdown dropdown, int index)
        {
            if (dropdown == null)
            {
                Debug.LogWarning("[ArchNet] Shadow Module: a dropdown is not set.", this);
                return;
            }
            dropdown.SetValueWithoutNotify(index);
            dropdown.onValueChanged.AddListener(_ => { if (_applyOnChange) Save(); });
        }

        static int IndexOf<T>(T[] values, T value) => Mathf.Max(0, System.Array.IndexOf(values, value));
    }
}
