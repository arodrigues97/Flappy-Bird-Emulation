using Flappy_Bird.fb;
using Flappy_Bird.fb.Screen;
using Flappy_Bird_Emulation.fb.entity.pipe;
using Microsoft.Xna.Framework;
using System;

namespace Flappy_Bird_Emulation.fb.logic {
    public class PipeManager {

        private long pipeSpawn = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

        private long waitTime = 3000L;

        private Pipe current;
         
        public void Update() {
            if ((long)(DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond) - pipeSpawn > waitTime) {
                pipeSpawn = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                int widthBetween = FlappyBirdGame.Random.Next(120, 200);
                Console.WriteLine("Width between pipes=" + widthBetween);
                Pipe downPipe = new Pipe(PipeDirection.DOWN, new Vector2(0, 0), new Rectangle(288, 0 - (widthBetween / 2), 44, 252));
                Pipe upPipe = new Pipe(PipeDirection.UP, new Vector2(0, 0), new Rectangle(288, 265 + (widthBetween / 2),   44 , 252));
                GameManager.GetGame().GetEntityManager().AddEntity(downPipe);
                GameManager.GetGame().GetEntityManager().AddEntity(upPipe);
                if (waitTime == 5000L) {
                    waitTime = 2000L;
                }
                if (current == null) {
                    current = downPipe;
                }
            }

            if (current != null && current.GetRectangle().X < 35) {
                current = null;
                GameManager.GetGame().GetFlappyBird().IncrementScore();
                PlayScreen playScreen = (PlayScreen) GameManager.GetGame().GetGameScreen();
                playScreen.GetPointSfx().Play();
            }

        }

        public long GetPipeSpawnTimeStamp() {
            return pipeSpawn;
        }

    }
}
