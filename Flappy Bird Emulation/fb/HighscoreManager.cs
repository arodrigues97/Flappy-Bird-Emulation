using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flappy_Bird_Emulation.fb {
    public class HighscoreManager {

        /// <summary>
        /// The file name to perform I/O opeerations to.
        /// </summary>
        public const String FILE_NAME = "flappyhighscores.txt";

        /// <summary>
        /// Represents the highscores.
        /// </summary>
        private List<int> highscores = new List<int>();

        /// <summary>
        /// Constructs a new Highscore Manager.
        /// </summary>
        public HighscoreManager() {
            Read();
        }

        /// <summary>
        /// Reds the highscores file.
        /// </summary>
        public void Read() {
            if (!File.Exists(FILE_NAME)) {
                return;
            }
            String line;
            int score;
            try {
                StreamReader sr = new StreamReader(FILE_NAME);
                line = sr.ReadLine();
                while (line != null) {
                    line = sr.ReadLine();
                     if (!Int32.TryParse(line, out score)) {
                        continue;
                     }
                    Console.WriteLine("Reading score " + score);
                    highscores.Add(score);
                }
                sr.Close();
                Console.ReadLine();
            } catch (Exception e) {
                Console.WriteLine("Exception: " + e.Message);
            }
            highscores.Sort();
        }

        /// <summary>
        /// Writes the highscores fil.
        /// </summary>
        public void Write() {
            Console.WriteLine("Wrote highscores");
            try {
                StreamWriter sw = new StreamWriter(FILE_NAME);
                for (int i = 0; i < highscores.Count; i++) {
                    sw.WriteLine(highscores[i]);
                }
                sw.Close();
            } catch (Exception e) {
                Console.WriteLine("Exception: " + e.Message);
            } finally {
                Console.WriteLine("Executing finally block.");
            }
        }

        /// <summary>
        /// Adds a score.
        /// </summary>
        /// <param name="score"></param>
        public void AddScore(int score) {
            if (highscores.Contains(score) || score < 1) {
                return;
            }
            Console.WriteLine("Adding score" + score);
            highscores.Add(score);
            highscores.Sort();
            Write();
        }

        /// <summary>
        /// Gets the best score.
        /// </summary>
        /// <returns></returns>
        public int GetBestScore() {
            int highest = -1 ;
            foreach (int i in highscores) {
                if (i > highest) {
                    highest = i;
                }
            }
            return highest;
        }

    }
}
