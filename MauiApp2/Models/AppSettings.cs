using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp2.Models
{
    public class AppSettings
    {
        // RabbitMQ
        public string BrokerHost { get; set; } = "localhost";
        public string QueueIn { get; set; } = "llm1_input";
        public string QueueOut { get; set; } = "llm2_input";
        public string Username { get; set; } = "admin";
        public string Password { get; set; } = "admin";


        // LLM
        public string LlmUri { get; set; } = "http://127.0.0.1:1234/v1/chat/completions";
        public string Model { get; set; } = "meta-llama-3.1-8b-instruct";
        public double Temperature { get; set; } = 0.7;
        public int MaxTokens { get; set; } = 150;
        public int MaxTurns { get; set; } = 6;

        public string SystemPrompt { get; set; } =
            "Eres un defensor radical de Java. Responde en máximo 3 frases.";
    }
}
