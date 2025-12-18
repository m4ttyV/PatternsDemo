using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternsDemo.Patterns
{
    public class AdapterPatternDemo
    {
        // Target interface
        public interface IExerciseSource
        {
            List<string> GetExercises();
        }
        // Adaptee #1 (личный список)
        public class PersonalExerciseList
        {
            private List<string> items = new()
        {
            "Приседания (любимое) [9/10]",
            "Отжимания (дом) [8/10]",
            "Планка (реабилитация) [7/10]"
        };


            public List<string> GetRawPersonalItems()
            {
                return items;
            }
        }

        // Adaptee #2 (общий список)
        public class CommonExerciseList
        {
            private List<string> items = new()
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
                var result = new List<string>();

                foreach (var raw in personalList.GetRawPersonalItems())
                {
                    var clean = raw.Split('(')[0].Trim();
                    result.Add(clean);
                }

                return result;
            }
        }

        // Adapter #2
        public class CommonListAdapter : IExerciseSource
        {
            private CommonExerciseList commonList;

            public CommonListAdapter(CommonExerciseList commonList)
            {
                this.commonList = commonList;
            }

            public List<string> GetExercises()
            {
                return commonList.GetRawCommonItems();
            }
        }

        // Client
        public class Gym
        {
            private IExerciseSource personalSource;
            private IExerciseSource commonSource;

            public Gym(
                IExerciseSource personalSource,
                IExerciseSource commonSource)
            {
                this.personalSource = personalSource;
                this.commonSource = commonSource;
            }

            public void PrintAllExercises()
            {
                Console.WriteLine("Итоговый список упражнений:\n");

                foreach (var ex in personalSource.GetExercises())
                    Console.WriteLine($"- {ex}");

                foreach (var ex in commonSource.GetExercises())
                    Console.WriteLine($"- {ex}");
            }
        }
         
        public static void AdapterMain()
        {
            // Adaptee
            var personalList = new PersonalExerciseList();
            var rawPersonalList = personalList.GetRawPersonalItems();
            var commonList = new CommonExerciseList();
            Console.WriteLine("Исходный список личных успражнений");
            for (int i = 0; i < rawPersonalList.Count; i++)
            {
                Console.WriteLine(rawPersonalList[i]);
            }
            var rawCommonList = commonList.GetRawCommonItems();
            Console.WriteLine("Исходный список общих успражнений");
            for (int i = 0; i < rawCommonList.Count; i++)
            {
                Console.WriteLine(rawCommonList[i]);
            }
            // Adapters
            IExerciseSource personalAdapter =
                new PersonalListAdapter(personalList); 
            IExerciseSource commonAdapter =
                new CommonListAdapter(commonList);
            // Client
            var gym = new Gym(personalAdapter, commonAdapter);

            gym.PrintAllExercises();
        }
    }
}
