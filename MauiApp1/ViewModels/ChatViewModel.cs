using MauiApp1.Models;
using MauiApp1.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace MauiApp1.ViewModels;

public class ChatViewModel : INotifyPropertyChanged
{
    public ObservableCollection<ChatMessage> Messages { get; } = new();

    private readonly RabbitMqService _rabbit;
    private readonly LlmService _llm;
    private readonly SettingsService _settingsService;

    private AppSettings _settings;

    private string inputText;
    private int _turnCounter = 0;
    private bool _conversationEnded = false;

    private const string END_MESSAGE = "__END_CONVERSATION__";

    public string InputText
    {
        get => inputText;
        set { inputText = value; OnPropertyChanged(); }
    }

    public string CurrentLlmName => _settings.Model;

    public ICommand SendCommand { get; }

    public ChatViewModel()
    {
        _settingsService = new();
        _settings = _settingsService.Load();

        _rabbit = new();
        _llm = new();

        _rabbit.Connect(
            _settings.BrokerHost,
            _settings.Username,
            _settings.Password,
            _settings.QueueIn,
            _settings.QueueOut
        );

        _rabbit.MessageReceived += OnMessageReceived;

        SendCommand = new Command(SendMessage);
    }

    void SendMessage()
    {
        if (_conversationEnded) return;
        if (string.IsNullOrWhiteSpace(InputText)) return;

        if (_turnCounter >= _settings.MaxTurns)
        {
            EndConversation();
            return;
        }

        var text = InputText;

        Messages.Add(new ChatMessage
        {
            Author = "Yo",
            Text = text,
            IsMine = true
        });

        InputText = "";
        _turnCounter++;

        _rabbit.SendMessage(text);
    }

    private async void OnMessageReceived(string message)
    {
        if (_conversationEnded) return;

        // 🔴 Si el otro envió fin
        if (message == END_MESSAGE)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                AddSystemMessage("🏁 Fin del debate.");
            });

            _conversationEnded = true;
            return;
        }

        if (_turnCounter >= _settings.MaxTurns)
        {
            EndConversation();
            return;
        }

        // Mostrar mensaje recibido
        MainThread.BeginInvokeOnMainThread(() =>
        {
            Messages.Add(new ChatMessage
            {
                Author = "Llamita",
                Text = message,
                IsMine = false
            });

            _turnCounter++;
        });

        if (_turnCounter >= _settings.MaxTurns)
        {
            EndConversation();
            return;
        }

        // Generar respuesta
        _settings = _settingsService.Load();

        var response = await _llm.GetResponseAsync(
            message,
            _settings.SystemPrompt,
            _settings.Model,
            _settings.Temperature,
            _settings.MaxTokens,
            _settings.LlmUri
        );


        MainThread.BeginInvokeOnMainThread(() =>
        {
            Messages.Add(new ChatMessage
            {
                Author = "Mi IA",
                Text = response,
                IsMine = true
            });

            _turnCounter++;
        });

        _rabbit.SendMessage(response);
    }

    private void EndConversation()
    {
        if (_conversationEnded) return;

        _conversationEnded = true;

        MainThread.BeginInvokeOnMainThread(() =>
        {
            AddSystemMessage("🏁 Fin del debate.");
        });

        _rabbit.SendMessage(END_MESSAGE);
    }

    private void AddSystemMessage(string text)
    {
        Messages.Add(new ChatMessage
        {
            Author = "Sistema",
            Text = text,
            IsMine = false
        });
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    void OnPropertyChanged([CallerMemberName] string name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
