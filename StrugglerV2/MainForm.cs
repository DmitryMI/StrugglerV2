using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StrugglerV2.KeyboardMonitoring;
using StrugglerV2.Preferences;
using StrugglerV2.StrugglerActing;

namespace StrugglerV2
{
    public partial class MainForm : Form
    {
        public const string PreferencesRelativePath = "StrugglerPrefs.xml";
        private KeyHandler _keyHandler;
        private readonly StrugglerActuator _actuator;
        private readonly PreferencesContainer _preferences;

        #region State

        private SelectedButton _selectedButton = SelectedButton.None;
        private bool _settingKey = false;
        private bool _listeningEnabled = false;

        #endregion

        public MainForm()
        {
            InitializeComponent();
            ClearStatus();

            _preferences = new PreferencesContainer();
            LoadPreferences();
            _actuator = new KeyboardEventActuator(_preferences.TargetKey, _preferences.PeriodOuterMs, _preferences.PeriodInnerMs);
            UpdateUi();
        }

        private void LoadPreferences()
        {
            if (File.Exists(PreferencesRelativePath))
            {
                try
                {
                    _preferences.LoadFromFile(PreferencesRelativePath);
                }
                catch (PreferencesContainer.PreferencesFileReadingException ex)
                {
                    PrintErrorToStatus(ex.GetUserMessage());
                }
            }
            else
            {
                PrintMessageToStatus("Using default settings");
            }
        }

        private void PrintErrorToStatus(string message)
        {
            StatusLabel.Text = message;
            StatusLabel.ForeColor = Color.Red;
        }

        private void PrintMessageToStatus(string message)
        {
            StatusLabel.Text = message;
            StatusLabel.ForeColor = Color.Black;
        }

        private void ClearStatus()
        {
            StatusLabel.Text = "OK";
            StatusLabel.ForeColor = Color.DarkGreen;
        }

        private void HandleHotKey()
        {
            if (_actuator.IsRunning)
            {
                _actuator.StopActuator();
            }
            else
            {
                _actuator.StartActuator();
            }
            SystemSounds.Beep.Play();
            UpdateIndicators();
            UpdateSelectors();
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == Constants.WM_HOTKEY_MSG_ID)
                HandleHotKey();
            base.WndProc(ref m);
        }

        private void StartListeningButton_Click(object sender, EventArgs e)
        {
            ClearStatus();
            if (_listeningEnabled)
            {
                _keyHandler.Unregister();
                _listeningEnabled = false;
            }
            else
            {
                _keyHandler = new KeyHandler(_preferences.ToggleKey, this);
                _keyHandler.Register();
                _actuator.StopActuator();
                _listeningEnabled = true;
            }

            UpdateIndicators();
            UpdateSelectors();
        }

        private void UpdateIndicators()
        {
            if (_listeningEnabled)
            {
                ListeningIndicator.BackColor = Color.LawnGreen;
            }
            else
            {
                ListeningIndicator.BackColor = Color.Red;
            }

            if (_actuator.IsRunning)
            {
                ActuatingIndicator.BackColor = Color.LawnGreen;
            }
            else
            {
                ActuatingIndicator.BackColor = Color.Red;
            }
        }

        private void UpdateKeyLabels()
        {
            TargetKeyBox.Text = _preferences.TargetKey.ToString();
            ToggleKeyBox.Text = _preferences.ToggleKey.ToString();
        }

        private void UpdateSelectors()
        {
            StartListeningButton.Enabled = true;
            SetToggleKeyButton.Enabled = true;
            SetToggleKeyButton.BackColor = SystemColors.Control;
            SetTargetKeyButton.Enabled = true;
            SetTargetKeyButton.BackColor = SystemColors.Control;
            Focus();
            if (_selectedButton == SelectedButton.None)
            {
                KeyPreview = false;
            }
            else if(_selectedButton == SelectedButton.TargetKey)
            {
                SetToggleKeyButton.Enabled = false;
                SetTargetKeyButton.BackColor = Color.LightGreen;
                KeyPreview = true;
                StartListeningButton.Enabled = false;
            }
            else if (_selectedButton == SelectedButton.ToggleKey)
            {
                SetTargetKeyButton.Enabled = false;
                SetToggleKeyButton.BackColor = Color.LightGreen;
                KeyPreview = true;
                StartListeningButton.Enabled = false;
            }

            if (_actuator != null && _actuator.IsRunning)
            {
                SetTargetKeyButton.Enabled = false;
                SetToggleKeyButton.Enabled = false;
            }

            if (_listeningEnabled)
            {
                SetTargetKeyButton.Enabled = false;
                SetToggleKeyButton.Enabled = false;
            }
        }

        private void UpdateUi()
        {
            UpdateSelectors();
            UpdateIndicators();
            UpdateKeyLabels();
        }

        private void SetKeyButton_Click(object sender, EventArgs e)
        {
            if (sender == SetTargetKeyButton)
            {
                if (_selectedButton == SelectedButton.TargetKey)
                {
                    _selectedButton = SelectedButton.None;
                }
                else
                {
                    _selectedButton = SelectedButton.TargetKey;
                }
            }
            else if (sender == SetToggleKeyButton)
            {
                if (_selectedButton == SelectedButton.ToggleKey)
                {
                    _selectedButton = SelectedButton.None;
                }
                else
                {
                    _selectedButton = SelectedButton.ToggleKey;
                }
            }
            else
            {
                _selectedButton = SelectedButton.None;
            }

            UpdateSelectors();
        }
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            Keys pressedKey = e.KeyCode;

            if (_selectedButton == SelectedButton.TargetKey)
            {
                _preferences.TargetKey = pressedKey;
                _actuator.TargetButton = pressedKey;
            }
            else if (_selectedButton == SelectedButton.ToggleKey)
            {
                _preferences.ToggleKey = pressedKey;
            }

            _selectedButton = SelectedButton.None;


            UpdateSelectors();
            UpdateKeyLabels();
        }

        private void SavePreferences()
        {
            bool arePrefsDefault = !_preferences.IsNotDefault;
            bool prefsFileExists = File.Exists(PreferencesRelativePath);
            if (arePrefsDefault && !prefsFileExists)
                return;
                
            try
            {
                _preferences.SaveToFile(PreferencesRelativePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Preferences not saved: " + ex.Message);
            }

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SavePreferences();
        }
    }
}
