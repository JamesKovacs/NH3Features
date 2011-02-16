namespace Core {
    public abstract class Animal : UnmappedAnimal {
        public override string Name { get; set; }

        public override string ToString() {
            return string.Format("Name: {0}", Name);
        }
    }
}