using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkoutTracker_v2.Models
{
    public class Exercise
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string MuscleGroup { get; set; }
        public string BodyPart { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }

        public Exercise(int id, string name, string type, string muscleGroup, string bodyPart, int sets, int reps)
        {
            ID = id;
            Name = name;
            Type = type;
            MuscleGroup = muscleGroup;
            BodyPart = bodyPart;
            Sets = sets;
            Reps = reps;
        }
    }
}
