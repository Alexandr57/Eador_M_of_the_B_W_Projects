using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATAGame_Library.DATA
{
    class DATAGame
    {
        /// <summary>
        /// Задать настройки по новой
        /// </summary>
        public bool IsSetDATAGame { get; set; }

        /// <summary>
        /// Файл с сохранением в Астрале
        /// </summary>
        public string FileNameSaveAstral { get; set; }
        /// <summary>
        /// Файл с сохранением на Осколке
        /// </summary>
        public string FileNameSaveOskolok { get; set; }
        /// <summary>
        /// Файл игры (Полный путь и имя файла)
        /// </summary>
        public string FullNameGame { get; set; }
        /// <summary>
        /// Папка с сохранениями
        /// </summary>
        public string SaveFolder { get; set; }
        /// <summary>
        /// Файл hex редактора (Полный путь и имя файла)
        /// </summary>
        public string FullNameHexEditor { get; set; }

        public DATAGame()
        {
            IsSetDATAGame = true;
        }
    }
}
