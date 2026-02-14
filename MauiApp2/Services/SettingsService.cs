using MauiApp2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MauiApp2.Services
{
    public class SettingsService
    {
        private const string FileName = "settings.json";

        public AppSettings Load()
        {
            var path = Path.Combine(FileSystem.AppDataDirectory, FileName);

            if (!File.Exists(path))
                return new AppSettings();

            var json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<AppSettings>(json) ?? new AppSettings();
        }

        public void Save(AppSettings settings)
        {
            var path = Path.Combine(FileSystem.AppDataDirectory, FileName);

            var json = JsonSerializer.Serialize(settings, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(path, json);
        }
    }
}
