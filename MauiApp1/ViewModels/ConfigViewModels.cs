using MauiApp1.Models;
using MauiApp1.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiApp1.ViewModels
{
    public class ConfigViewModel : INotifyPropertyChanged
    {
        private readonly SettingsService _settingsService;
        private AppSettings _settings;

        public string BrokerHost { get; set; }
        public string QueueIn { get; set; }
        public string QueueOut { get; set; }

        public string LlmUri { get; set; }
        public string Model { get; set; }
        public double Temperature { get; set; }
        public int MaxTokens { get; set; }
        public int MaxTurns { get; set; }
        public string SystemPrompt { get; set; }

        public ICommand SaveCommand { get; }

        public ConfigViewModel()
        {
            _settingsService = new SettingsService();
            _settings = _settingsService.Load();

            BrokerHost = _settings.BrokerHost;
            QueueIn = _settings.QueueIn;
            QueueOut = _settings.QueueOut;
            LlmUri = _settings.LlmUri;
            Model = _settings.Model;
            Temperature = _settings.Temperature;
            MaxTokens = _settings.MaxTokens;
            MaxTurns = _settings.MaxTurns;
            SystemPrompt = _settings.SystemPrompt;

            SaveCommand = new Command(Save);
        }

        private async void Save()
        {
            _settings.BrokerHost = BrokerHost;
            _settings.QueueIn = QueueIn;
            _settings.QueueOut = QueueOut;
            _settings.LlmUri = LlmUri;
            _settings.Model = Model;
            _settings.Temperature = Temperature;
            _settings.MaxTokens = MaxTokens;
            _settings.MaxTurns = MaxTurns;
            _settings.SystemPrompt = SystemPrompt;

            _settingsService.Save(_settings);

            await Application.Current.MainPage.DisplayAlert(
                "Configuración",
                "Guardado correctamente",
                "OK");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
