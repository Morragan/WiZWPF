using System.Collections.Generic;
using System.Text;
using Wiz.ViewModels;

namespace Wiz.Helpers
{
    internal static class CommandHelper
    {
        private static readonly Dictionary<string, string> commandDictionary = new()
        {
            { "Red", "r" },
            { "Green", "g" },
            { "Blue", "b" },
            { "Brightness", "dimming" },
            { "Speed", "speed" },
            { "Scene", "sceneId" },
            { "Temperature", "temp" },
            { "Enabled", "state" }
        };

        public static readonly List<Scene> scenesList = new()
        {
            new Scene("Ocean", 1),
            new Scene("Romance", 2),
            new Scene("Sunset", 3),
            new Scene("Party", 4),
            new Scene("Fireplace", 5),
            new Scene("Cozy", 6),
            new Scene("Forest", 7),
            new Scene("Pastel Colors", 8),
            new Scene("Wake up", 9),
            new Scene("Bedtime", 10),
            new Scene("Warm White", 11),
            new Scene("Daylight", 12),
            new Scene("Cool white", 13),
            new Scene("Night light", 14),
            new Scene("Focus", 15),
            new Scene("Relax", 16),
            new Scene("True colors", 17),
            new Scene("TV time", 18),
            new Scene("Plantgrowth", 19),
            new Scene("Spring", 20),
            new Scene("Summer", 21),
            new Scene("Fall", 22),
            new Scene("Deepdive", 23),
            new Scene("Jungle", 24),
            new Scene("Mojito", 25),
            new Scene("Club", 26),
            new Scene("Christmas", 27),
            new Scene("Halloween", 28),
            new Scene("Candlelight", 29),
            new Scene("Golden white", 30),
            new Scene("Pulse", 31),
            new Scene("Steampunk", 32)
        };

        public static bool IsWizParam(string name)
        {
            return commandDictionary.ContainsKey(name);
        }

        public static string BuildParams(Dictionary<string, object> commands, MainWindowViewModel viewModel)
        {
            if (commands.Count > 0 && !commands.ContainsKey("Enabled")) commands["Enabled"] = true;
            if (commands.ContainsKey("Speed") && !commands.ContainsKey("Scene")) commands["Scene"] = viewModel.Scene.ID;

            var builder = new StringBuilder();
            builder.Append('{');
            foreach (KeyValuePair<string, object> command in commands)
            {
                if (!commandDictionary.ContainsKey(command.Key)) continue;
                var paramName = commandDictionary[command.Key];
                builder.Append($"\"{paramName}\":{command.Value.ToString()!.ToLower()},");
            }
            builder.Length--; // remove last comma
            builder.Append('}');
            return builder.ToString();
        }

        private static string? GetCommandName(string key)
        {
            commandDictionary.TryGetValue(key, out string? value);
            return value;
        }
    }

    public class Scene
    {
        public string Name { get; set; }
        public int ID { get; set; }

        public Scene(string name, int id)
        {
            Name = name;
            ID = id;
        }
    }
}
