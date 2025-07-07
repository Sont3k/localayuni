using System;
using TMPro;
using Unity.Containers;
using UnityEngine;

namespace Unity.Localization
{
    public class LanguageSelectorView : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown _dropdown;
        
        private LocalizationController _localizationController;

        private void Awake()
        {
            _localizationController = GlobalServices.Get<LocalizationController>();
        }

        private void OnEnable()
        {
            _dropdown.onValueChanged.AddListener(OnValueChanged);
        }

        private void OnDisable()
        {
            _dropdown.onValueChanged.RemoveAllListeners();
        }

        private void Start()
        {
            InitDropdown();
        }
        
        private void OnValueChanged(int index)
        {
            _localizationController.SetCurrentLocale(index);
        }

        private void InitDropdown()
        {
            _dropdown.ClearOptions();
            
            foreach (var locale in _localizationController.GeneratedLocales)
            {
                var optionData = new TMP_Dropdown.OptionData
                {
                    image = locale.sprite,
                    text = locale.LocaleName
                };
                
                _dropdown.options.Add(optionData);
            }

            var startValue = PlayerPrefs.GetInt(PlayerPrefsConstants.SelectedLocaleIndex, 0);
            _dropdown.SetValueWithoutNotify(startValue); // to not trigger listener
            _dropdown.RefreshShownValue();
        }
    }
}