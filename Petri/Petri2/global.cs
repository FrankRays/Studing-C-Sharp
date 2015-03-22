using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Petri2
{
    static class global
    {
        public static int cell = 20;
        public static int hcell = cell / 2;
        public static int w = 2;
        public static int h = 3;

        public static ToolStripStatusLabel outtxt;
        public static ToolStripStatusLabel lasttxt;

        public static Graphics field;
        public static PictureBox panel;
        public static string[] words = new string[] 
        {
            /* 0*/ "Укажите на переход который хотите выполнить", 
            /* 1*/ "Во входных позициях не хватает фишок",
            /* 2*/ "Переход удачно выполнен",
            /* 3*/ "Выбран ничего не делающий инструмент :)",
            /* 4*/ "Ничего не произошло :)",
            /* 5*/ "Позиция. Выберете место для елемента.",
            /* 6*/ "Переход. Выберете место для елемента.",
            /* 7*/ "Стрелка. Выберете елемент \"от\"",
            /* 8*/ "Изменение колличества фишек: ЛКМ +1 ; ПКМ -1. Укажите на позицию",
            /* 9*/ "Удаление елемента: выберите елемент. Все его связи тоже пропадут.",
            /*10*/ "Запустить переход. Выберите переход для запуска.",
            /*11*/ "Новая схема.",
            /*12*/ "Сохраненная рание схема открыта.",
            /*13*/ "Схема сохранина",
            /*14*/ "Не сохранино",
            /*15*/ "Связь удалена.",
            /*16*/ "Для удаления укажите на елемент!",
            /*17*/ "Это место занято другим елементом.",
            /*18*/ "Создание связи отменено.",
            /*19*/ "Связь успешно создана.",
            /*20*/ "Построение связи начато. Укажите на позицию. Для отмены - ПКМ.",
            /*21*/ "Построение связи начато. Укажите на переход. Для отмены - ПКМ.",
            /*22*/ "Количество фишек изменено.",
            /*23*/ "Елемент удален Т_Т",
            /*24*/ "Елемент создан.",
            /*25*/ "Колличество фишек должно быть в диапазоне от 0 до 5",
            /*26*/ "",
            /*27*/ "",
            /*28*/ "",
            ""
        };
        public static void Out(int i)
        {
            lasttxt.Text = outtxt.Text;
            outtxt.Text = words[i];
        }
    }

    
    static class data
    {
        public static List<Transaction> TList = new List<Transaction>();
        public static List<Position> PList = new List<Position>();
        public static List<Arrow> AList = new List<Arrow>();
        public static void Clear()
        {
            AList.Clear();
            TList.Clear();
            PList.Clear();
            
        }
    }

    [Serializable()]
    class DataToSave
    {
        public List<Transaction> TList = new List<Transaction>();
        public List<Position> PList = new List<Position>();
        public List<Arrow> AList = new List<Arrow>();

        public void Serialize(string CurrentFile, DataToSave dts)
        {
            FileStream FS = new FileStream(CurrentFile,
                FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
            new BinaryFormatter().Serialize(FS, dts);
            FS.Close();
        }

        public void Deserialize(string CurrentFile)
        {
            object a1;
            FileStream FS = new FileStream(CurrentFile, FileMode.Open, FileAccess.Read, FileShare.Read);
            a1 = new BinaryFormatter().Deserialize(FS);
            FS.Close();

            data.TList = (a1 as DataToSave).TList;
            data.PList = (a1 as DataToSave).PList;
            data.AList = (a1 as DataToSave).AList;
        }
        
    }
}
