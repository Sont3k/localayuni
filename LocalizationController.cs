using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Unity.Localization
{
    [Serializable]
    public class LocaleOption
    {
        public Sprite sprite;
        public TextAsset textAsset;
        
        public string LocaleName { get; set; }
        public Dictionary<string, string> LocaleDictionary { get; set; }
    }
    
    public class LocalizationController : MonoBehaviour
    {
        [SerializeField] private LocaleOption[] _locales;

        public List<LocaleOption> GeneratedLocales { get; } = new();
        private LocaleOption _currentLocale;

        public static event Action OnCurrentLocaleUpdate;
        
        private void Awake()
        {
            InitLocales();
            InitCurrentLocale();
        }

        private void InitLocales()
        {
            foreach (var locale in _locales)
            {
                var dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(locale.textAsset.ToString());
                locale.LocaleName = dictionary["lang_name"];
                locale.LocaleDictionary = dictionary;
                GeneratedLocales.Add(locale);
            }
        }
        
        private void InitCurrentLocale()
        {
            var localeIndex = PlayerPrefs.GetInt(PlayerPrefsConstants.SelectedLocaleIndex, 0);
            _currentLocale = GeneratedLocales[localeIndex];
            OnCurrentLocaleUpdate?.Invoke();
        }
        
        public void SetCurrentLocale(int index)
        {
            PlayerPrefs.SetInt(PlayerPrefsConstants.SelectedLocaleIndex, index);
            _currentLocale = GeneratedLocales[index];
            OnCurrentLocaleUpdate?.Invoke();
        }

        public string GetLocalizedValue(string key)
        {
            _currentLocale.LocaleDictionary.TryGetValue(key, out var localizedValue);
            if (localizedValue == null && GeneratedLocales[0] != null)
            {
                GeneratedLocales[0].LocaleDictionary.TryGetValue(key, out var fallbackValue);
                Debug.LogWarning($"[{_currentLocale.LocaleName}] locale don't have a key: {key}");
                return fallbackValue;
            }

            return localizedValue;
        }
    }
}