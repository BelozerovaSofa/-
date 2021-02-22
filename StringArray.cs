using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace Array
{
    class StringArray : IComparable<StringArray>
    {
        private const string toStringMessage = "The object of type Array.StringArray.";
        public static int instancesNumber;//число массивов
        private int length;//длинна массива
        private string[] storage;//элементы массива
        private string separator = "";//разделитель между элементами

        public string Separator
        {
            set
            {
                if(value.Length > 51)
                {
                    return;//если выполняется это условие, то выходим 
                }
                separator = value;
            }
            get
            {
                return separator;
            }
        }

        public string ResultString
        {
            set
            {
                storage = value.Split(separator);//разделяем наш массив
                length = storage.Length;
            }
            get
            {
                string result = "";
                for(int i = 0; i < length; i++)
                {
                    result += storage[i] + separator;//в массиве (элемент_1 +разделитель,элемент_2 + разделитель
                }
                if(separator.Equals(string.Empty))
                {
                    return result;
                }
                return result.Remove(result.Length - separator.Length);
                /*возвращаем результат с разделителями,но после последнего элемента убираем его*/
            }
        }
        public int Lenght
        {
            set
            {
                if(value >0 && value<length)
                {
                    length = value;
                    string[] temp = storage;//в новый массив записывается наш начальный массив
                    storage = new string[length];//но уже с новой размерностью, попадающей в наше условие
                    copyArray(storage, temp, 0, storage.Length);
                    //куда, какие элементы, с какого, по какой
                }
                
            }
            //get
            //{
            //    return length;
            //}
        }

        public string[] Storage
        {
            set
            {
                storage = value;
                length = value.Length;
            }
            get
            {
                return storage;
            }
        }
        //конструкторы
        public StringArray(int length)
        {
            this.length = length;
            this.storage = new string[length];
            instancesNumber++;//считается кол-во наших массивов
        }
        public StringArray(params string[] strs)//позволяет методу передавать переменное кол-во элементов одного типа
        {
            this.storage = strs;
            length = storage.Length;
            instancesNumber++;
        }

        public string Get(int index)
        {
            if(index >= length || index < 0)
            {
                return null;//возращает пустую ссылку, которая не ссылается на объект
            }
            return storage[index];//возвращает размерность массива по конечному индексу, который мы указали
        }

        public bool Set(int index, string value)
            /*устанавливаем новое значение и проверяем, входит ли в рамки массива*/
        {
            if(index >= length || index < 0)
            {
                return false;
            }
            storage[index] = value;
            return true;
        }

        public void Union(StringArray array)
        {
            length = length + array.length;
            string[] result = new string[length];
            /*создаем новый массив, в который поместим наш, и объединим его с новым*/
            copyArray(result, storage, 0, storage.Length);
            /*куда, какие элементы, с какого начиная, по какой*/
            copyArray(result, array.storage, storage.Length, array.length);

            storage = result;//выводим новый массив
        } 

        private static void copyArray(string[] to, string[] from, int toStartIndex, int fromEndIndex)
            //только внутри класса используется
        {
            for(int i = 0; i < fromEndIndex; i++)
            {
                to[i + toStartIndex] = from[i];
                //указываем с какого по какой элемент, идя по циклу
            }
        }

        public int CompareTo(StringArray anotherArray)//сравнивает текущий массив с anotherArray
        {
            if (storage[0].Length < anotherArray.storage[0].Length) return -1;
            if (storage[0].Length > anotherArray.storage[0].Length) return 1;
            return 0;
          //  return storage[0].CompareTo(anotherArray.storage[0]);
        }

        public override string ToString()//предоставление сообщения о типе 
        {
            return toStringMessage;
        }

       public static bool operator true(StringArray myArr)
       {
            return IsHasNullValues(myArr);
       }

        public static bool operator false(StringArray myArr)
        {
            return IsHasNullValues(myArr);
        }

        public static StringArray operator ++(StringArray myArr)
        {
            int newLenght = myArr.length + 1;//увеличение длины моего массива
            string[] result = new string[newLenght];
            /*новый массив, в который помещается старый,
             но c 1 свободной ячейкой*/
            copyArray(result, myArr.storage, 0, myArr.length);
            /*куда, какие элементы, с какого начиная, по какой*/
            myArr.storage = result;
            myArr.length = newLenght;
            return myArr;
        }
        public static StringArray operator --(StringArray myArr)
        {
            if(myArr.length <= 0)
            {
                return myArr;
            }
            int newLenght = myArr.length - 1;
            string[] result = new string[newLenght];
            copyArray(result, myArr.storage, 0, newLenght);
            myArr.storage = result;
            myArr.length = newLenght;
            return myArr;
        }

        //отдельный метод для перегрузки true/false
        private static bool IsHasNullValues(StringArray myArr)
        {
            foreach (string str in myArr.storage)
            {
                if (str == null)//если есть хоть одна пустая ячейка
                {
                    return false;
                }
            }
            return true;
        }

        public bool IsPresent(string str)//в качестве параметра строка
        {
            return storage.Contains(str);//строка, которая содержится в массиве 
        }
    }
}

