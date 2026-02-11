using GameUI;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Text.Json;
using Newtonsoft.Json;
using System.Threading.Tasks;


namespace GameUI
{
    public static class GameSaver
    {
        

        public static int getLastFileNo()
        {
            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string targetDirectory = currentDirectory + "Saves";
            int maxNumber = 1;
            if (Directory.Exists(targetDirectory))
            {
                var files = Directory.GetFiles(targetDirectory);
                foreach (string file in files)
                {
                    string filename = Path.GetFileNameWithoutExtension(file);

                    string digits = Regex.Match(filename, @"\d+").Value;

                    if (int.TryParse(digits, out int testNumber))
                    {
                        if(testNumber > maxNumber)
                        {
                            maxNumber = testNumber;
                        }
                    }
                }
                return maxNumber;
            } else
            {
                return maxNumber;
            }
        }

        // Save format:
        // line1: currentNumber
        // line2: currentLevel
        // line3: points
        // line4: lastRow,lastCol
        // next 5/7 lines: board rows with "." for empty
        // line10/12: userName(if any)
        // line11/13: saveDateTime(if any)
        public static async Task SaveGameToJson(string file, List<GameState> GameStates)
        {
            string jsonString = JsonConvert.SerializeObject(GameStates, Formatting.Indented);
            await Task.Run( () => File.WriteAllText(file, jsonString));
        }

        public static List<GameState> LoadGame(string file)
        {
            if (!File.Exists(file))
            {

            }
            string jsonString = File.ReadAllText(file);


            return JsonConvert.DeserializeObject<List<GameState>>(jsonString);
        }

        public static async Task SaveLeaderBoardToJsonAsync(GameState state, string filepath)
        {
            if (!File.Exists(filepath))
            {
                File.Create(filepath).Close();
            }


            string jsonString = JsonConvert.SerializeObject(state);

            await Task.Run(() => File.AppendAllText(filepath, jsonString));
        }

        public static List<GameState> GetLeaderBoardList(string filepath)
        {
            string jsonString = File.ReadAllText(filepath);

            return JsonConvert.DeserializeObject<List<GameState>>(jsonString);

        }




    }
}



