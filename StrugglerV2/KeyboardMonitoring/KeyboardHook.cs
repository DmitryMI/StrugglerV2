using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StrugglerV2.KeyboardMonitoring
{
    public class KeyboardHook
    {
        private static int _hookCounter;

        private IntPtr _handle;
        private Keys _key;
        private List<Keys> _modifiers;
        private int _id;
        private bool _isRegistered = false;
        public KeyboardHook(Form form, Keys key, IEnumerable<Keys> modifiers)
        {
            _handle = form.Handle;
            _key = key;
            _id = _hookCounter;
            _modifiers = new List<Keys>();
            _modifiers.AddRange(modifiers);
            _hookCounter++;
        }

        public KeyboardHook(Form form, KeyCombination keyCombination)
        {
            _handle = form.Handle;
            _key = keyCombination.Key;
            _id = _hookCounter;
            _modifiers = new List<Keys>();
            _modifiers.AddRange(keyCombination.Modifiers);
            _hookCounter++;
        }

        public Keys Key
        {
            get => _key;
            set
            {
                if (_isRegistered)
                {
                    throw new KeyHookMustBeUnregisteredException();
                }
                _key = value;
            }

        }

        public Keys[] Modifiers
        {
            get => _modifiers.ToArray();
            set
            {
                if (_isRegistered)
                {
                    throw new KeyHookMustBeUnregisteredException();
                }
                _modifiers.Clear(); 
                _modifiers.AddRange(value);
            }
        }
        

        public bool Register()
        {
            var modifiersParsed = GetModifiers(_modifiers, out var modifiers);
            if (!modifiersParsed)
            {
                throw new KeyModifierNotSupportedException();
            }
            _isRegistered = KeyboardHookSettler.Register(_handle, _id, _key, modifiers);
            return _isRegistered;
        }

        public void Unregister()
        {
            KeyboardHookSettler.Unregister(_handle, _id);
            _isRegistered = false;
        }

        ~KeyboardHook()
        {
            Unregister();
        }

        private bool GetModifiers(IEnumerable<Keys> keys, out KeyModifiers keyModifiers)
        {
            keyModifiers = 0;
            foreach (var key in keys)
            {
                
                if (key == Keys.Shift)
                {
                    keyModifiers |= KeyModifiers.Shift;
                }
                else if (key == Keys.Control)
                {
                    keyModifiers |= KeyModifiers.Control;
                }
                else if (key == Keys.LWin || key == Keys.RWin)
                {
                    keyModifiers |= KeyModifiers.Win;
                }
                else if (key == Keys.Alt)
                {
                    keyModifiers |= KeyModifiers.Alt;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }
    }
}
