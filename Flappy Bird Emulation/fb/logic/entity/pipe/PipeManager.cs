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
                int widthBetween = FlappyBirdGame.Random.Next(100, 200);
                int heightOffsetDown = (widthBetween) * -1;
                Pipe downPipe = new Pipe(PipeDirection.DOWN, new Vector2(0, 0), new Rectangle(288, heightOffsetDown, 55, 321));
                Pipe upPipe = new Pipe(PipeDirection.UP, new Vector2(0, 0), new Rectangle(288, 265 + (widthBetween / 2),   55 , 321));
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

                Console.WriteLine(current.GetRectangle().X);
                current = null;
                GameManager.GetGame().GetFlappyBird().IncrementScore();
                PlayScreen playScreen = (PlayScreen) GameManager.GetGame().GetGameScreen();
                playScreen.GetPointSfx().Play();
            }

        }

        public void Clear() {
            current = null;
        }
       
        public Pipe GetPipe() {
            return current;
        }
        
        public long GetPipeSpawnTimeStamp() {
            return pipeSpawn;
        }

    }
}
