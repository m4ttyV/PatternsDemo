using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternsDemo
{
    public class CommandPatternDemo
    {
        // Модель
        public class Exercise
        {
            public string Name { get; set; }

            public Exercise(string name)
            {
                Name = name;
            }

            public override string ToString() => Name;
        }


        // Receiver
        public class CommonExerciseList
        {
            private List<Exercise> exercises = new List<Exercise>();

            public void AddExercise(Exercise ex)
            {
                exercises.Add(ex);
            }

            public void RemoveExercise(Exercise ex)
            {
                exercises.Remove(ex);
            }

            public void ChangeExercise(Exercise oldEx, Exercise newEx)
            {
                int index = exercises.IndexOf(oldEx);
                if (index >= 0)
                    exercises[index] = newEx;
            }

            public void Print()
            {
                Console.WriteLine("Текущий список упражнений:");
                foreach (var ex in exercises)
                    Console.WriteLine($"- {ex}");
                Console.WriteLine();
            }
        }

        // Command interface
        public interface ICommand
        {
            void Execute();
            void Undo();
            void Redo();
        }

        // Concrete Command: Add
        public class AddExerciseCommand : ICommand
        {
            private CommonExerciseList list;
            private Exercise exercise;

            public AddExerciseCommand(CommonExerciseList list, Exercise exercise)
            {
                this.list = list;
                this.exercise = exercise;
            }

            public void Execute()
            {
                list.AddExercise(exercise);
            }

            public void Undo()
            {
                list.RemoveExercise(exercise);
            }

            public void Redo()
            {
                Execute();
            }
        }

        // Concrete Command: Remove
        public class RemoveExerciseCommand : ICommand
        {
            private CommonExerciseList list;
            private Exercise exercise;

            public RemoveExerciseCommand(CommonExerciseList list, Exercise exercise)
            {
                this.list = list;
                this.exercise = exercise;
            }

            public void Execute()
            {
                list.RemoveExercise(exercise);
            }

            public void Undo()
            {
                list.AddExercise(exercise);
            }

            public void Redo()
            {
                Execute();
            }
        }

        // Concrete Command: Change
        public class ChangeExerciseCommand : ICommand
        {
            private CommonExerciseList list;
            private Exercise oldLoad;
            private Exercise newLoad;

            public ChangeExerciseCommand(
                CommonExerciseList list,
                Exercise oldLoad,
                Exercise newLoad)
            {
                this.list = list;
                this.oldLoad = oldLoad;
                this.newLoad = newLoad;
            }

            public void Execute()
            {
                list.ChangeExercise(oldLoad, newLoad);
            }

            public void Undo()
            {
                list.ChangeExercise(newLoad, oldLoad);
            }

            public void Redo()
            {
                Execute();
            }
        }

        // Invoker
        public class CommandManager
        {
            private Stack<ICommand> history = new Stack<ICommand>();
            private Stack<ICommand> undone = new Stack<ICommand>();

            public void Execute(ICommand command)
            {
                command.Execute();
                history.Push(command);
                undone.Clear();
            }

            public void Undo()
            {
                if (history.Count == 0) return;

                var command = history.Pop();
                command.Undo();
                undone.Push(command);
            }

            public void Redo()
            {
                if (undone.Count == 0) return;

                var command = undone.Pop();
                command.Redo();
                history.Push(command);
            }
        }
        public static void CommandMain()
        {
            var list = new CommonExerciseList();
            var manager = new CommandManager();

            var squat = new Exercise("Приседания");
            var bench = new Exercise("Жим лёжа");
            var deadlift = new Exercise("Становая тяга");

            manager.Execute(new AddExerciseCommand(list, squat));
            manager.Execute(new AddExerciseCommand(list, bench));
            list.Print();

            manager.Execute(new ChangeExerciseCommand(
                list, bench, deadlift));
            list.Print();

            manager.Undo();
            list.Print();

            manager.Redo();
            list.Print();

            manager.Execute(new RemoveExerciseCommand(list, squat));
            list.Print();

            manager.Undo();
            list.Print();
        }
    }
}
