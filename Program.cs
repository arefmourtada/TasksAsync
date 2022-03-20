using System;
using System.Threading;
using System.Threading.Tasks;

namespace TasksAsync
{
    class Program
    {
        static async Task  Main(string[] args)
        {
            //Synchronous version of making tea.
            Console.WriteLine(" >>> Synchronous version of making tea");
            MakeTea();
            Console.WriteLine(" **** **** ***** ");
            
            //Asynchronous version of making tea.
            Console.WriteLine(" >>> Asynchronous version of making tea");
            await MakeTeaAsync();
            /*
             * Try to remove the above `await` and see how the program will terminate before the 
             * the thread of boiling water is finished :( tea is not done 
             */
            Console.WriteLine("Back in main");
        }

        static string MakeTea()
        {
            Console.WriteLine("Tea Process >>> Fill Kettle");
            string water = BoilWater();
            Console.WriteLine("Tea Process >>> Take cup out");
            Console.WriteLine("Tea Process >>> Add Tea to cup");
            string tea = $"Pour {water} in cup";
            Console.WriteLine(tea);

            return tea;
        }

        static string BoilWater()
        {
            Console.WriteLine("Boil Process >>> Start the kettle");
            Console.WriteLine("Boil Process >>> Waiting for the kettle");
            Task.Delay(3000); //Delay representing the delay boiling water
            Console.WriteLine("Boil Process >>> Finished boiling");
            return "Boil Process >>> Hot water";

        }

        //Note the use of keyword async to create `asynchronous` methods
        //async methods use "Task" as a return type. The task object can hold the actual return type.
        //In the example below, the thread will return a string through the task.
        static async Task<string> MakeTeaAsync()
        {
            Console.WriteLine("Tea Process >>> Fill Kettle");
            var water =  BoilWaterAsync(); //start the process of boiling water..
            
            //things to do while the tea is processed...
            Console.WriteLine("Tea Process >>> Take cup out");
            Console.WriteLine("Tea Process >>> Add Tea to cup");
            Console.WriteLine("Tea Process >>> Add sugar to cup");
            Console.WriteLine("Tea Process >>> wait ... ");
            Console.WriteLine("Tea Process >>> wait ....");
            
            //at this point we need to wait for the boiling process to finish before we pour water..
            var hotWater = await water;
            string tea = $"Tea Process >>> Pour {hotWater} in cup";
            Console.WriteLine(tea);

            return tea;
        }

        static async Task<string> BoilWaterAsync()
        {
            Console.WriteLine("Boil Process >>> Start the kettle");
            Console.WriteLine("Boil Process >>> Waiting for the kettle");
            await Task.Delay(3000);
            /* `await` above will give control to the calling thread to take over and 
             * not wait for the boiling to finish. The thread will work on its own..
             */
            Console.WriteLine("Boil Process >>> Finished boiling");
            return "Hot water";

        }
    }
}
