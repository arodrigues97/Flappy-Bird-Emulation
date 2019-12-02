using Microsoft.Xna.Framework;
using System;

namespace Flappy_Bird_Emulation.fb.logic.entity.flappybird {

    /// <summary>
    /// Represents the class used to handle the Physics of the Bird.
    /// <see cref="https://github.com/paulkr/Flappy-Bird/blob/master/lib/Bird.java"/> for physics reference.
    /// </summary>
    public class BirdPhysics {

        /// <summary>
        /// The upwards jump shift constant.
        /// </summary>
        private const int JUMP_SHIFT = 5;

        /// <summary>
        /// The gravity constant.
        /// </summary>
        private const double GRAVITY = .26;

        /// <summary>
        /// Represents the velocity of the bird.
        /// </summary>
        private double velocity;

        /// <summary>
        /// Represents the rotation of the bird falling downwards.
        /// </summary>
        private float rotation;

        /// <summary>
        /// Represents the delay for jumping.
        /// </summary>
        private double delay;

        private double upwardsRotate;

        /// <summary>
        /// Constructs the Bird Physics Instance.
        /// </summary>
        public BirdPhysics() {
        }

        /// <summary>
        /// Calculastes the new Physics state.
        /// </summary>
        /// <param name="location">The location to effect.</param>
        public void Calculate(ref Vector2 location) {
            velocity += GRAVITY;
            location.Y += (int)velocity;
            rotation = (float)((((90 * (velocity + 35) / 25) - 90) * Math.PI / 180) + upwardsRotate);
            rotation /= 2;
            rotation = (float)(rotation > Math.PI / 2 ? Math.PI / 2 : rotation);
            if (delay > 0) {
                delay--;
            }
        }

        /// <summary>
        /// Handles the Jump physics effect.
        /// </summary>
        public void Jump() {
            if (delay < 1) {
                velocity = -JUMP_SHIFT;
                upwardsRotate = -189.5;
            }
        }

        /// <summary>
        /// Sets the rotation of the physics state.
        /// </summary>
        /// <param name="rotation">The rotation.</param>
        public void SetRotation(float rotation) {
            this.rotation = rotation;
        }

        /// <summary>
        /// Gets the Rotation.
        /// </summary>
        /// <returns>The rotation.</returns>
        public float GetRotation() {
            return rotation;
        }

    }
}
