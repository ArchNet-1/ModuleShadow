using TMPro;
using UnityEngine;

namespace ArchNet.Module.Shadow
{
    /// <summary>
    /// [MODULE] - [ARCH NET] - [SHADOW]
    /// author : LOUIS PAKEL
    /// </summary>
    public class Shadow : MonoBehaviour
    {
        #region Serialized Fields

        [Header("Shadows dropdown")]
        // Shadow Level Dropdown
        [SerializeField, Tooltip("Shadow level dropdown")]
        private TMP_Dropdown _shadowLevelDropdown;

        // Shadow Quality Dropdown
        [SerializeField, Tooltip("Shadow quality dropdown")]
        private TMP_Dropdown _shadowQualityDropdown;

        #endregion

        #region Private Fields

        // Shadow level
        private ShadowQuality _shadowLevel;

        // Shadow Quality
        private ShadowResolution _shadowQuality;

        #endregion

        #region Public Methods

        /// <summary>
        /// Description : Force Save new value
        /// </summary>
        public void Save()
        {
            // UPDATE SHADOWS SETTINGS
            ValueChanged();
        }

        #endregion

        #region Private Methods
        // Start is called before the first frame update
        private void Start()
        {
            // Set Shadow level and shadow quality
            _shadowLevel = QualitySettings.shadows;
            _shadowQuality = QualitySettings.shadowResolution;

            // OPTIONAL
            // Force set dropdown component
            ForceSetModule();

            // Check if the module is available
            ModuleAvailable();

            // Init shadow module
            InitModule();
        }

        private void ForceSetModule()
        {
            // force set shadown level dropdown
            if (null == _shadowLevelDropdown)
            {
                _shadowLevelDropdown = transform.GetChild(0).GetComponent<TMP_Dropdown>();
            }

            // force set shadow quality dropdown
            if(null == _shadowQualityDropdown)
            {
                _shadowQualityDropdown = transform.GetChild(1).GetComponent<TMP_Dropdown>();
            }
        }

        /// <summary>
        /// Description : Check if the module is available for runtime
        /// </summary>
        private void ModuleAvailable()
        {
            // Shadow level dropdown and shadown quality dropdown must not be empty
            if (null == _shadowLevelDropdown
                || null == _shadowQualityDropdown)
            {
                Debug.Log(Constants.ERROR_411);
            }
        }

        /// <summary>
        /// Description : Initaite Shadow Module
        /// </summary>
        private void InitModule()
        {
            // Set shadow level dropdown value
            switch (_shadowLevel)
            {
                case ShadowQuality.Disable:
                    _shadowLevelDropdown.value = 0;
                    break;
                case ShadowQuality.HardOnly:
                    _shadowLevelDropdown.value = 1;
                    break;
                case ShadowQuality.All:
                    _shadowLevelDropdown.value = 2;
                    break;
            }

            // Set shadow quality dropdown value
            switch (_shadowQuality)
            {
                case ShadowResolution.Low:
                    _shadowQualityDropdown.value = 0;
                    break;
                case ShadowResolution.Medium:
                    _shadowQualityDropdown.value = 1;
                    break;
                case ShadowResolution.High:
                    _shadowQualityDropdown.value = 2;
                    break;
                case ShadowResolution.VeryHigh:
                    _shadowQualityDropdown.value = 3;
                    break;
            }
        }

        /// <summary>
        /// Description : Shadow's dropdown values have changed
        /// </summary>
        private void ValueChanged()
        {
            // SET NEW SHADOW MODE
            switch (_shadowLevelDropdown.value)
            {
                case 0:
                    QualitySettings.shadows = ShadowQuality.Disable;
                    break;
                case 1:
                    QualitySettings.shadows = ShadowQuality.HardOnly;
                    break;
                case 2:
                    QualitySettings.shadows = ShadowQuality.All;
                    break;
            }

            // SET SHADOW RESOLUTION
            switch (_shadowQualityDropdown.value)
            {
                case 0:
                    QualitySettings.shadowResolution = ShadowResolution.Low;
                    break;
                case 1:
                    QualitySettings.shadowResolution = ShadowResolution.Medium;
                    break;
                case 2:
                    QualitySettings.shadowResolution = ShadowResolution.High;
                    break;
                case 3:
                    QualitySettings.shadowResolution = ShadowResolution.VeryHigh;
                    break;
            }
        }

        #endregion
    }
}