using UnityEngine.Localization.Settings;
using System.Collections;
using UnityEngine;

namespace Game.Scripts.Localization
{
    public class LocalizationSelector : MonoBehaviour
    {
        private int activeId = 0;
        private bool isProcess = false;
        private string localizationKey = "LocalizationKey";

        private void Awake() {
            int id = PlayerPrefs.GetInt(localizationKey, 0);
            ChangeLocalization(id);
        }

        public void ChangeLocalization(int id) {
            if (id == activeId) return;
            if (isProcess) return;

            StartCoroutine(SetLocalization(id));
        }

        IEnumerator SetLocalization(int id) {
            isProcess = true;
            yield return LocalizationSettings.InitializationOperation;
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[id];
            PlayerPrefs.SetInt(localizationKey, id);
            activeId = id;
            isProcess = false;
        }
    }
}