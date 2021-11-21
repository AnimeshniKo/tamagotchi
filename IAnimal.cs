using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tamagochi
{
    internal interface IAnimal
    {
        void Eat(Food food);
       // void KillAnimal();
        void Clean();
        void Play();
        void Drink();

    }
}
