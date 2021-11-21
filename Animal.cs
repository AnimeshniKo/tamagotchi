using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tamagochi
{
    public abstract class Animal : IAnimal
    {
        private Timer starving;
        private Timer thirsting;
        private Timer boring;
        private Timer dirtying;
        private Timer die;

        private bool isStarving;
        private bool isThirsty;
        private bool isBoring;
        private bool isDirty;
        private bool isDie;

        readonly TimeSpan timeToStarving = TimeSpan.FromMinutes(1);
        readonly TimeSpan timeToThirsting = TimeSpan.FromMinutes(2);
        readonly TimeSpan timeToBoring = TimeSpan.FromMinutes(3);
        readonly TimeSpan timeToDirting = TimeSpan.FromMinutes(4);
        readonly TimeSpan timeToDeath = TimeSpan.FromMinutes(1);

        public Animal()
        {
           
        }
        public Animal(string name)
        {
            starving = new Timer(StartStarving, name + " хочет есть", timeToStarving, Timeout.InfiniteTimeSpan);  
            thirsting = new Timer(StartThirsty, name + " хочет пить", timeToThirsting, Timeout.InfiniteTimeSpan);
            boring = new Timer(StartBoring,  name + " скучно", timeToBoring, Timeout.InfiniteTimeSpan);
            dirtying = new Timer(SufferFromDirt, name + " плохо пахнет", timeToDirting, Timeout.InfiniteTimeSpan);
            die = new Timer(KillAnimal, "Dead", Timeout.InfiniteTimeSpan, Timeout.InfiniteTimeSpan);
            isDie = false;
            Name = name;
        }
        public abstract void Voice();
        private void SufferFromDirt(object? state)
        {
            isDirty = true;
            Console.WriteLine(state.ToString());

            die = new Timer(KillAnimal, "Смерть от вони", timeToDeath, Timeout.InfiniteTimeSpan);
        }

        private void StartBoring(object? state)
        {
            isBoring = true;
            Console.WriteLine(state.ToString());
            die = new Timer(KillAnimal, "Смерть от скуки", timeToDeath, Timeout.InfiniteTimeSpan);
        }
       
        private void StartThirsty(object? state)
        {
            isThirsty = true;
            Console.WriteLine(state.ToString());
            die = new Timer(KillAnimal, "Смерть от жажды", timeToDeath, Timeout.InfiniteTimeSpan);
        }

        private void StartStarving(object? state)
        {
            isStarving = true;
            Console.WriteLine(state.ToString());
            die = new Timer(KillAnimal, "Смерть от голода", timeToDeath, Timeout.InfiniteTimeSpan);
        }

        private void KillAnimal(object? state)
        {
            Console.WriteLine(state?.ToString());
            isDie = true;
            starving.Dispose();
            boring.Dispose();
            dirtying.Dispose();
            thirsting.Dispose();
            die.Dispose();

        }
        public bool isAlive()
        {
            return !isDie;
        }
        public string Name { get;  }
        public double Age { get; set; }

        public void Eat(Food food)
        {

            if (isStarving)
            {
                this.Voice();
                die.Dispose();
                isStarving = false;
                starving.Change(timeToStarving, Timeout.InfiniteTimeSpan);
            }
            else
            {
                Console.WriteLine("Я не голоден");
            }
        }

        public void Clean()
        {
            if (isDirty)
            {
                this.Voice();
                die.Dispose();
                isDirty = false;
                dirtying.Change(timeToDirting, Timeout.InfiniteTimeSpan);
            }
            else
            {
                Console.WriteLine("Я чист");
            }
        }

        public void Play()
        {
            if (isBoring)
            {
                this.Voice();
                die.Dispose();
                isBoring = false;
                thirsting.Change(timeToBoring, Timeout.InfiniteTimeSpan);
            }
            else
            {
                Console.WriteLine("Я не хочу играть");
            }
        }
        public void Drink()
        {
            if (isThirsty)
            {
                this.Voice();
                die.Dispose();
                isThirsty = false;
                thirsting.Change(timeToThirsting, Timeout.InfiniteTimeSpan);
            }
            else
            {
                Console.WriteLine("Я не хочу пить");
            }
        }
    }
}
