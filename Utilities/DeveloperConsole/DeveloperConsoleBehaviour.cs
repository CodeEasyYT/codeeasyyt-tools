using CodeEasyYT.Utilities.DeveloperConsole.Commands;
using System.Linq;
using TMPro;
using UnityEngine;

namespace CodeEasyYT.Utilities.DeveloperConsole
{
    /// <summary>
    /// This class is the UI part of the developer console.
    /// However you can create your custom one.
    /// </summary>
    public class DeveloperConsoleBehaviour : MonoBehaviour
    {
        [Tooltip("ProTip! If you don't want any prefix, just leave empty!")]
        [SerializeField] private string prefix = string.Empty;
        [SerializeField] private ConsoleCommand[] commands = new ConsoleCommand[0];

        [Header("UI")]
        [Tooltip("ProTip! If your developer console is not seperate from chat, just leave empty!")]
        [SerializeField] private GameObject uiCanvas = null;

        private float pausedTimeScale;

        private static DeveloperConsoleBehaviour instance;

        private DeveloperConsole developerConsole;
        public DeveloperConsole DeveloperConsole
        {
            get
            {
                if (developerConsole != null) return developerConsole;
                return developerConsole = new DeveloperConsole(prefix, commands);
            }
        }

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;

            if(uiCanvas != null)
                DontDestroyOnLoad(gameObject);
        }

        private void OnDestroy()
        {
            if (uiCanvas != null)
                instance = null;
        }

        public void Toggle()
        {
            if (uiCanvas != null)
            {
                if(uiCanvas.activeSelf)
                {
                    uiCanvas.SetActive(false);
                    Time.timeScale = pausedTimeScale;
                }
                else
                {
                    pausedTimeScale = Time.timeScale;
                    Time.timeScale = 0f;
                }
            }
        }
    }
}