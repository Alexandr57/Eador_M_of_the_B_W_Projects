using DATAGame_Library.DATA;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATAGame_Library
{
    public interface IMainDataGame
    {
        //Данные игры
        DATAGame DATAGame { get; set; }

        //Проверка на (Является ли игра Steam версией)
        bool IsGameSteam { get; }
        //Проверка на наличия файла сохранения Астрала (Если выбран файл по умолчанию)
        bool IsFileNameSaveAstral { get; }
        //Проверка на наличия файла сохранения Осколка (Если выбран файл по умолчанию)
        bool IsFileNameSaveOskolok { get; }
    }

    public class MainDataGame : IMainDataGame
    {
        /// <summary>
        /// Имя файла сохранения / загрузки данных игры
        /// </summary>
        private const string fileNameDATAGame = @"\DataGame.json";

        /// <summary>
        /// Данные игры
        /// </summary>
        public DATAGame dataGame = new DATAGame();

        #region Свойства

        public DATAGame DATAGame { get => dataGame; set => dataGame = value; }

        public bool IsGameSteam => dataGame.FullNameGame.Contains("steamapps");

        public bool IsFileNameSaveAstral => File.Exists(dataGame.SaveFolder + dataGame.FileNameSaveAstral);

        public bool IsFileNameSaveOskolok => File.Exists(dataGame.SaveFolder + dataGame.FileNameSaveOskolok);

        #endregion
    }
}
