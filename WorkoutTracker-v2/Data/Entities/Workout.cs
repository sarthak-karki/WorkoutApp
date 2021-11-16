using System.Collections.Generic;

namespace WorkoutTracker_v2.Models
{
    public class Workout
    {
        public IEnumerable<Exercise> Exercises { get; set; }

        public Workout(IEnumerable<Exercise> exercises)
        {
            Exercises = exercises;
        }
    }
}
