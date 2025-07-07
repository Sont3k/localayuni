# localayuni - lightweight localization solution for Unity

A lightweight, plug-and-play localization system for Unity projects, designed for easy integration with Unity UI and TextMeshPro. This system enables dynamic language switching, automatic UI text updates, and persistent user language preferences.

## Features

- **Multi-language support** using JSON-based dictionaries
- **Runtime language switching** via dropdown UI
- **Automatic UI updates** for localized text elements
- **Fallback to default language** if a key is missing
- **Persistent user language selection** using PlayerPrefs
- **Integration with TextMeshPro**

## How It Works

| Component                | Purpose                                                                                   |
|--------------------------|-------------------------------------------------------------------------------------------|
| `LocalizationController` | Manages available locales, loads dictionaries, handles current locale, and provides API   |
| `LanguageSelectorView`   | UI dropdown for language selection; updates language at runtime                           |
| `LocalizedText`          | Component for UI text elements; auto-updates text when language changes                   |

## Getting Started

### 1. Add Locales

- Prepare JSON files for each language, containing key-value pairs (including `"lang_name"`).
- Assign these JSON files and optional flag sprites to the `LocaleOption` array in the `LocalizationController`.

### 2. Set Up the Localization Controller

Attach the `LocalizationController` to a GameObject in your scene. It will:

- Deserialize each locale's JSON file into a dictionary
- Store locale names and dictionaries internally
- Set the current locale from PlayerPrefs or default to the first locale

### 3. Add Language Selection UI

- Place a `TMP_Dropdown` in your UI.
- Attach the `LanguageSelectorView` script and assign the dropdown reference.
- The dropdown will list all available locales (with names and optional sprites) and update language on selection.

### 4. Localize UI Text

- Add the `LocalizedText` component to any GameObject with a `TMP_Text` component.
- Set the `Key` property to the translation key.
- The text will update automatically when the language changes.

## Example Usage

```csharp
// Get a localized string in code
string greeting = _localizationController.GetLocalizedValue("GREETING");

// Change the current locale (e.g., from dropdown)
_localizationController.SetCurrentLocale(selectedIndex);
```

## Implementation Details

- **Locale Loading:** Each `LocaleOption` holds a JSON TextAsset and an optional Sprite. The JSON is deserialized into a dictionary on startup[2].
- **Language Switching:** The current locale index is stored in PlayerPrefs and triggers an update event for all localized components[2].
- **UI Integration:** The dropdown options are generated from the available locales, and the selected value is restored on startup[1].
- **LocalizedText:** Listens for locale changes and updates its `TMP_Text` content with the correct translation for its assigned key[3].

## Requirements

- Unity 2019.4 or newer
- TextMeshPro
- Newtonsoft.Json (for JSON parsing)

## Contributing

Contributions are welcome! Please open issues or pull requests for improvements or bug fixes.
