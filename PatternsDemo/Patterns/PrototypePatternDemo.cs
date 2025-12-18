using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternsDemo.Patterns
{
    public class PrototypePatternDemo
    {

        // Prototype interface
        public interface IPrototype<T>
        {
            T Clone();
        }

        // Load strategy interface
        public interface ILoadStrategy : IPrototype<ILoadStrategy>
        {
            void Configure();
            double CalculateLoad();
        }

        // Concrete strategy: Strength
        public class StrengthLoadStrategy : ILoadStrategy
        {
            public void Configure()
            {
                
            }

            public double CalculateLoad()
            {
                return 100;
            }

            public ILoadStrategy Clone()
            {
                return new StrengthLoadStrategy();
            }
        }

        // Concrete strategy: Cardio
        public class CardioLoadStrategy : ILoadStrategy
        {
            public void Configure()
            {
                // настройка кардио нагрузки
            }

            public double CalculateLoad()
            {
                return 50;
            }

            public ILoadStrategy Clone()
            {
                return new CardioLoadStrategy();
            }
        }

        // Exercise parameters
        public class ExerciseParameters : IPrototype<ExerciseParameters>
        {
            public int Reps { get; set; }
            public int Sets { get; set; }
            public int Duration { get; set; }

            public ILoadStrategy LoadStrategy { get; set; }

            public ExerciseParameters Clone()
            {
                return new ExerciseParameters
                {
                    Reps = Reps,
                    Sets = Sets,
                    Duration = Duration,
                    LoadStrategy = LoadStrategy.Clone()
                };
            }
        }

        // Exercise
        public class Exercise : IPrototype<Exercise>
        {
            public string Name { get; set; }
            public ExerciseParameters Parameters { get; set; }

            public Exercise Clone()
            {
                return new Exercise
                {
                    Name = Name,
                    Parameters = Parameters.Clone()
                };
            }
        }

        // Training plan
        public class TrainingPlan : IPrototype<TrainingPlan>
        {
            public string Name { get; set; }
            public string Description { get; set; }

            public List<Exercise> Exercises { get; set; } = new();

            public TrainingPlan Clone()
            {
                var clone = new TrainingPlan
                {
                    Name = Name,
                    Description = Description
                };

                foreach (var ex in Exercises)
                {
                    clone.Exercises.Add(ex.Clone());
                }
                return clone;
            }
        }
        public static void PrototypeMain()
        {
            // Создание оригинального плана тренировки
            var originalPlan = new TrainingPlan
            {
                Name = "Базовый план",
                Description = "План для начинающих",
                Exercises = new List<Exercise>
                {
                    new Exercise
                    {
                        Name = "Приседания",
                        Parameters = new ExerciseParameters
                        {
                            Reps = 10,
                            Sets = 3,
                            LoadStrategy = new StrengthLoadStrategy()
                        }
                    },
                    new Exercise
                    {
                        Name = "Бег на месте",
                        Parameters = new ExerciseParameters
                        {
                            Duration = 15,
                            LoadStrategy = new CardioLoadStrategy()
                        }
                    }
                }
            };
            // Клонирование плана тренировки
            var clonedPlan = originalPlan.Clone();
            clonedPlan.Name = "Клон базового плана";
            // Вывод информации о планах
            Console.WriteLine($"Оригинальный план: {originalPlan.Name}, Описание: {originalPlan.Description}");
            foreach (var ex in originalPlan.Exercises)
            {
                Console.WriteLine($"- Упражнение: {ex.Name}, Параметры: Reps={ex.Parameters.Reps}, Sets={ex.Parameters.Sets}, Duration={ex.Parameters.Duration}, Load={ex.Parameters.LoadStrategy.CalculateLoad()}");
            }
            Console.WriteLine($"\nКлонированный план: {clonedPlan.Name}, Описание: {clonedPlan.Description}");
            foreach (var ex in clonedPlan.Exercises)
            {
                Console.WriteLine($"- Упражнение: {ex.Name}, Параметры: Reps={ex.Parameters.Reps}, Sets={ex.Parameters.Sets}, Duration={ex.Parameters.Duration}, Load={ex.Parameters.LoadStrategy.CalculateLoad()}");
            }
        }
    }
}
