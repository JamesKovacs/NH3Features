using System;

namespace Core {
    public class Cat : Animal {
        public virtual void Meow() {
            Console.WriteLine("Meow!");
        }
    }
}