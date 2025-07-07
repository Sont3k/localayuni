using System;
using TMPro;
using Unity.Containers;
using UnityEngine;

namespace Unity.Localization
{
    [RequireComponent(typeof(TMP_Text))]
    public class LocalizedText : MonoBehaviour
    {
        [SerializeField] private TMP_Text _tmpText;
        [SerializeField] private string _key;
        
        private LocalizationController _localizationController;

        private void Awake()
        {
            _localizationController = GlobalServices.Get<LocalizationController>();
        }

        private void OnEnable()
        {
            SetLocalizedValue();
            LocalizationController.OnCurrentLocaleUpdate += SetLocalizedValue;
        }

        private void OnDisable()
        {
            LocalizationController.OnCurrentLocaleUpdate -= SetLocalizedValue;
        }

        private void SetLocalizedValue()
        {
            if (_localizationController == null)
            {
                Debug.LogError("Localization instance has not initialized yet");
                return;
            }
            
            if (_key == string.Empty)
            {
                Debug.LogWarning($"Key is not assigned for object: {name}");
                return;
            }
            
            var localizedValue = _localizationController.GetLocalizedValue(_key);
            if (localizedValue == string.Empty)
            {
                Debug.LogError($"No Value for key: {_key}");
                return;
            }
            
            _tmpText.text = localizedValue;
        }
    }
}