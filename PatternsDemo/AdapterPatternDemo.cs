using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternsDemo
{
    public class AdapterPatternDemo
    {
        // Target interface 
        public interface IExerciseSource
        {
            List<string> GetExercises();
        }

        // Adaptee #1
        public class PersonalExerciseList
        {
            private List<string> items = new List<string>
        {
            "Отжимания",
            "Приседания",
            "Планка"
        };

            public List<string> GetRawPersonalItems()
            {
                return items;
            }
        }

        // Adaptee #2
        public class CommonExerciseList
        {
            private List<string> items = new List<string>
        {
            "Жим лёжа",
            "Становая тяга",
            "Подтягивания"
        };

            public List<string> GetRawCommonItems()
            {
                return items;
            }
        }

        // Adapter #1
        public class PersonalListAdapter : IExerciseSource
        {
            private PersonalExerciseList personalList;

            public PersonalListAdapter(PersonalExerciseList personalList)
            {
                this.personalList = personalList;
            }

            public List<string> GetExercises()
            {
                // Адаптация интерфейса
                return personalList.GetRawPersonalItems();
            }
        }

        // Adapter #2
        public class CommonListAdapter : IExerciseSource
        {
            private CommonExerciseList commonListSource;

            public CommonListAdapter(CommonExerciseList commonListSource)
            {
                this.commonListSource = commonListSource;
            }

            public List<string> GetExercises()
            {
                // Адаптация интерфейса
                return commonListSource.GetRawCommonItems();
            }
        }

        // Client (Gym)
        public class Gym
        {
            private IExerciseSource personalListSource;
            private IExerciseSource commonListSource;

            public Gym(
                IExerciseSource personalListSource,
                IExerciseSource commonListSource)
            {
                this.personalListSource = personalListSource;
                this.commonListSource = commonListSource;
            }

            public void GetAllExercises()
            {
                Console.WriteLine("Персональные упражнения:");
                PrintExercises(personalListSource.GetExercises());

                Console.WriteLine("\nОбщие упражнения:");
                PrintExercises(commonListSource.GetExercises());
            }

            private void PrintExercises(List<string> exercises)
            {
                foreach (var exercise in exercises)
                {
                    Console.WriteLine($"- {exercise}");
                }
            }
        }
        public static void AdapterMain()
        {
            // Adaptee
            var personalList = new PersonalExerciseList();
            var commonList = new CommonExerciseList();

            // Adapters
            IExerciseSource personalAdapter =
                new PersonalListAdapter(personalList);

            IExerciseSource commonAdapter =
                new CommonListAdapter(commonList);

            // Client
            var gym = new Gym(personalAdapter, commonAdapter);

            gym.GetAllExercises();
        }
    }
}
