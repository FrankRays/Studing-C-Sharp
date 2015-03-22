using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace chm_lab_1
{
    static class Log
    {
        public static DictionaryBindingList<TKey, TValue> ToBindingList<TKey, TValue>(this IDictionary<TKey, TValue> data)
        {
            return new DictionaryBindingList<TKey, TValue>(data);
        }
        public sealed class Pair<TKey, TValue>
        {
            private readonly TKey key;
            private readonly IDictionary<TKey, TValue> data;
            public Pair(TKey key, IDictionary<TKey, TValue> data)
            {
                this.key = key;
                this.data = data;
            }
            public TKey Key { get { return key; } }
            public TValue Value
            {
                get
                {
                    TValue value;
                    data.TryGetValue(key, out value);
                    return value;
                }
                set { data[key] = value; }
            }
        }
        public class DictionaryBindingList<TKey, TValue> : BindingList<Pair<TKey, TValue>>
        {
            private readonly IDictionary<TKey, TValue> data;
            public DictionaryBindingList(IDictionary<TKey, TValue> data)
            {
                this.data = data;
                Reset();
            }
            public void Reset()
            {
                bool oldRaise = RaiseListChangedEvents;
                RaiseListChangedEvents = false;
                try
                {
                    Clear();
                    foreach (TKey key in data.Keys)
                    {
                        Add(new Pair<TKey, TValue>(key, data));
                    }
                }
                finally
                {
                    RaiseListChangedEvents = oldRaise;
                    ResetBindings();
                }
            }

        }

        private static LogWindow window = new LogWindow();
        private static TextBox log = window.log;


        public static LogWindow Output
        {
            get
            {
                return window;
            }
        }

        public static void WriteLn(string str)
        {
            Debug.WriteLine(str);
            log.Text += str + Environment.NewLine;
        }

        public static void Show()
        {
            log.Parent.Show();
        }
    }
}
