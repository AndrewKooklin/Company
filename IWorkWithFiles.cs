using System;

namespace Company
{
    interface IWorkWithFiles
    {
        /// <summary>
        /// Запись json строки в файл
        /// </summary>
        public void WriteToFile(string root, string fileName);
        /// <summary>
        /// Чтение json строки из файла
        /// </summary>
        public string ReadFromFile(string fileName);
    }
}
