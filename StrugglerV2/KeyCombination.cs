using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StrugglerV2
{
    public struct KeyCombination
    {
        public Keys Key { get; set; }
        public Keys[] Modifiers { get; set; }

        public KeyCombination(Keys key, Keys[] modifiers)
        {
            Key = key;
            if (modifiers == null)
            {
                Modifiers = new Keys[0];
            }
            else
            {
                Modifiers = modifiers;
            }
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(Key.ToString());
            
            foreach (var mod in Modifiers)
            {
                builder.Append('+');
                builder.Append(mod.ToString());
            }

            return builder.ToString();
        }

        public static bool TryParse(string text, out KeyCombination combination)
        {
            Keys mainKey = Keys.None;
            List<Keys> modifiers = new List<Keys>();
            string[] parts = text.Split('+');
            if (parts.Length == 0)
            {
                combination = default;
                return false;
            }
            for (var i = 0; i < parts.Length; i++)
            {
                var part = parts[i];
                bool ok = Enum.TryParse(part, out Keys key);
                if (!ok)
                {
                    combination = default;
                    return false;
                }

                if (i == 0)
                {
                    mainKey = key;
                }
                else
                {
                    modifiers.Add(key);
                }
            }

            combination = new KeyCombination(mainKey, modifiers.ToArray());
            return true;
        }

        public static bool operator ==(KeyCombination a, KeyCombination b)
        {
            if (a.Key != b.Key)
            {
                return false;
            }

            if (a.Modifiers.Length != b.Modifiers.Length)
            {
                return false;
            }

            for (int i = 0; i < a.Modifiers.Length; i++)
            {
                Keys aMod = a.Modifiers[i];
                int index = -1;
                for (int j = 0; j < b.Modifiers.Length; j++)
                {
                    Keys bMod = b.Modifiers[j];
                    if (aMod == bMod)
                    {
                        index = j;
                        break;
                    }
                }

                if (index == -1)
                {
                    return false;
                }
            }

            return true;
        }

        public static bool operator !=(KeyCombination a, KeyCombination b)
        {
            return !(a == b);
        }
    }
}
