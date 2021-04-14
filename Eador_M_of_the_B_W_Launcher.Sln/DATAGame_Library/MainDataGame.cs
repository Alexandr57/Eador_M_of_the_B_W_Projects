using DATAGame_Library.DATA;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using WK.Libraries.BetterFolderBrowserNS;
using MessageService_Library;


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
        /// Интерфейс сервиса сообщений
        /// </summary>
        private readonly IMessageService _messageService;

        /// <summary>
        /// Данные игры
        /// </summary>
        public DATAGame dataGame = new DATAGame();



        public MainDataGame(IMessageService _messageService)
        {
            this._messageService = _messageService;

            try
            {
                if (!LoadDATAGame(Environment.CurrentDirectory + fileNameDATAGame))
                    throw new Exception($"Необходимые данные небыли загружены!{Environment.NewLine}Программа будет закрыта!");
            }
            catch (Exception ex)
            {
                _messageService.MessageError(ex.Message, "Ошибка загрузки данных");
                Environment.Exit(0);
            }
        }

        #region Загрузка данных

        private bool LoadDATAGame(string _filename)
        {
            if (File.Exists(_filename))
            {
                dataGame = JsonSerializer.Deserialize<DATAGame>(File.ReadAllText(_filename), new JsonSerializerOptions { IgnoreNullValues = true });

                if (dataGame.IsSetDATAGame)
                    SetDATAGame();

                return true;
            }
            else
                return SetDATAGame();
        }

        #endregion

        #region Получения данных от пользователя

        private bool SetDATAGame()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Multiselect = false,
                ValidateNames = true,
                CheckFileExists = true,
                CheckPathExists = true,
                DereferenceLinks = true,
            };

            openFileDialog.Filter = "launcher.exe|launcher.exe";
            openFileDialog.DefaultExt = "launcher.exe";
            openFileDialog.Title = "Выберите launcher.exe в папке с игрой";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(openFileDialog.FileName))
                    dataGame.FullNameGame = openFileDialog.FileName;
                else
                    return false;
            }
            else
            {
                return false;
            }

            var betterFolderBrowser = new BetterFolderBrowser
            {
                Multiselect = false,
                RootFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                Title = "Выберите папку с сохранениями"
            };

            if (betterFolderBrowser.ShowDialog() == DialogResult.OK)
            {
                if (Directory.Exists(betterFolderBrowser.SelectedFolder))
                    dataGame.SaveFolder = betterFolderBrowser.SelectedFolder;
                else
                    return false;
            }
            else
            {
                return false;
            }

            openFileDialog.Filter = "Сохранение(*.map)|*.map";
            openFileDialog.DefaultExt = "*.map";
            openFileDialog.Title = "Выберите постоянное сохранение с данными осколка";
            openFileDialog.InitialDirectory = dataGame.SaveFolder;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(openFileDialog.FileName))
                    dataGame.FileNameSaveOskolok = $@"\{Path.GetFileName(openFileDialog.FileName)}";
                else
                    dataGame.FileNameSaveOskolok = "";
            }
            else
                dataGame.FileNameSaveOskolok = "";

            openFileDialog.Title = "Выберите постоянное сохранение с данными Астрала";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(openFileDialog.FileName))
                    dataGame.FileNameSaveAstral = $@"\{Path.GetFileName(openFileDialog.FileName)}";
                else
                    dataGame.FileNameSaveAstral = "";
            }
            else
                dataGame.FileNameSaveAstral = "";

            openFileDialog.Filter = "HEX Редактор(*.exe)|*.exe";
            openFileDialog.DefaultExt = "*.exe";
            openFileDialog.Title = "Выберите hex редактор для редактирование сохранений вручную";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(openFileDialog.FileName))
                    dataGame.FullNameHexEditor = openFileDialog.FileName;
                else
                    dataGame.FullNameHexEditor = null;
            }

            dataGame.IsSetDATAGame = false;

            SaveDATAGame(Directory.GetCurrentDirectory() + fileNameDATAGame);

            return true;
        }

        #endregion

        #region Сохранение данных

        private void SaveDATAGame(string _filename)
        {
            File.WriteAllText(_filename, JsonSerializer.Serialize(dataGame, new JsonSerializerOptions { WriteIndented = true, IgnoreNullValues = true }));
        }

        #endregion

        #region Свойства

        public DATAGame DATAGame { get => dataGame; set => dataGame = value; }

        public bool IsGameSteam => dataGame.FullNameGame.Contains("steamapps");

        public bool IsFileNameSaveAstral => File.Exists(dataGame.SaveFolder + dataGame.FileNameSaveAstral);

        public bool IsFileNameSaveOskolok => File.Exists(dataGame.SaveFolder + dataGame.FileNameSaveOskolok);

        #endregion
    }
}
