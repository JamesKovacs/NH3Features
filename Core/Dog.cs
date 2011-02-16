using System;

namespace Core {
    public class Dog : Animal {
        public virtual void Bark() {
            Console.WriteLine("Woof!");
        }
    }
}