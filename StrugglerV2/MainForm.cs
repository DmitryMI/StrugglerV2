﻿using System;
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
        private readonly StrugglerActuator _actuator;
        private readonly PreferencesContainer _preferences;
        private KeyboardHook _keyboardHook;
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
            _keyboardHook = new KeyboardHook(this, _preferences.ToggleKey);
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
            _actuator.StopActuator();
            if (_listeningEnabled)
            {
                _keyboardHook.Unregister();
                _listeningEnabled = false;
            }
            else
            {
                _keyboardHook.Unregister();
                _keyboardHook.Key = _preferences.ToggleKey.Key;
                _keyboardHook.Modifiers = _preferences.ToggleKey.Modifiers;
                bool hookRegistered = _keyboardHook.Register();
                if (!hookRegistered)
                {
                    PrintErrorToStatus("Unable to set keyboard hook. Try another key combination");
                }
                else
                {
                    _listeningEnabled = true;
                }
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

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (_selectedButton != SelectedButton.None)
            {
                _selectedButton = SelectedButton.None;

                UpdateSelectors();
                UpdateKeyLabels();
            }
        }
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            Keys pressedKey = e.KeyCode;
            Keys modifierFlags = e.Modifiers;
            List<Keys> modifierList = new List<Keys>();
            if (((int)modifierFlags & (int)Keys.Shift) != 0)
            {
                modifierList.Add(Keys.Shift);
            }
            if (((int)modifierFlags & (int)Keys.Control) != 0)
            {
                modifierList.Add(Keys.Control);
            }
            if (((int)modifierFlags & (int)Keys.Alt) != 0)
            {
                modifierList.Add(Keys.Alt);
            }
            if (((int)modifierFlags & (int)Keys.LWin) != 0)
            {
                modifierList.Add(Keys.LWin);
            }
            if (((int)modifierFlags & (int)Keys.RWin) != 0)
            {
                modifierList.Add(Keys.RWin);
            }

            if (_selectedButton == SelectedButton.TargetKey)
            {
                _preferences.TargetKey = pressedKey;
                _actuator.TargetButton = pressedKey;
            }
            else if (_selectedButton == SelectedButton.ToggleKey)
            {
                _preferences.ToggleKey = new KeyCombination(pressedKey, modifierList.ToArray());
            }

            //_selectedButton = SelectedButton.None;


            //UpdateSelectors();
            //UpdateKeyLabels();
        }
    }
}
