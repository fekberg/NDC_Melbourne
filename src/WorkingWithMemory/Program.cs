using System;
using System.Linq;

namespace WorkingWithMemory
{
    class Program
    {
        public Memory<Person> NotWorking { get; set; }

        static void Main(string[] args)
        {
            var people = Enumerable.Range(0, 5000)
                .Select(x => new Person());

            var peopleArray = people.ToArray();

            Console.WriteLine(GC.GetTotalAllocatedBytes(true));

            //var subset1 = people.Take(1000).ToArray();

            Console.WriteLine(GC.GetTotalAllocatedBytes(true));

            var peopleArrayAsSpan = new ReadOnlySpan<Person>(); // peopleArray.AsSpan();

            Console.WriteLine(GC.GetTotalAllocatedBytes(true));

            var subSet = peopleArrayAsSpan[0..5000];

            peopleArrayAsSpan[0].Name = "Test";

            Console.WriteLine(GC.GetTotalAllocatedBytes(true));
        }
    }

    class Person
    {
        public string Name { get; set; }
    }
}
