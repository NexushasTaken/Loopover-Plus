

using Newtonsoft.Json;
using System;

namespace Loopover_Plus
{
    public class Save
    {
        [JsonProperty("Size")]
        public int Size { get; set; }

        [JsonProperty("X")]
        public int X { get; set; }

        [JsonProperty("Y")]
        public int Y { get; set; }

        [JsonProperty("Puzzle")]
        public int[,] Puzzle { get; set; }

        [JsonProperty("Date")]
        public string Date { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        public Save(int[,] puzzle, string name, int size, int x, int y)
        {
            Name = name;
            Date = DateTime.Now.ToString();
            Size = size;
            X = x;
            Y = y;
            Puzzle = puzzle;
        }
    }
}

