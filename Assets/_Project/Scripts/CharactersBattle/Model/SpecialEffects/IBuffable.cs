namespace Model.SpecialEffects
{
    public interface IBuffable
    {
        public void ApplySpecialEffect(Character target);
        public void RemoveSpecialEffect(Character target);
    }
}