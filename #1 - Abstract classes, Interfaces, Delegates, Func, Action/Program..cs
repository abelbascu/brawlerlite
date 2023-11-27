using System;

namespace Exercise_1___Class__Abstract_Class__Interface
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Attacker> attackers = new List<Attacker>();
            Samurai samurai = new Samurai();
            Marksman marksman = new Marksman();
            attackers.Add(samurai);
            attackers.Add(marksman);
            HealthTracker healthTracker = new HealthTracker(attackers);
            Console.WriteLine("The combat between samurai and marksman starts now");
            Random randomSeed = new Random();

            do
            {

                int diceThrow = randomSeed.Next(1, 7);
                if (diceThrow % 2 == 0)
                    samurai.Attack(marksman);
                else
                    marksman.Attack(samurai);

            } while (((Attacker)samurai).GetHealth() > 0 && ((Attacker)marksman).GetHealth() > 0);
        }
    }

    public interface IDamageable
    {
        void ReceiveDamage(Attacker attacker, int damage, Attacker target);
    }

    public class HealthComponent : IDamageable
    {
        public int Health { get; private set; } = 10;
        public delegate void HealthChanged(Attacker attacker, int damage, Attacker target);
        public HealthChanged? healthChanged;
        //public Action<Attacker, int, Attacker> HealthChanged;

        public void ReceiveDamage(Attacker attacker, int damage, Attacker target)
        {
            Health -= damage;
            healthChanged?.Invoke(attacker, damage, target);
            //HealthChanged?.Invoke(attacker, damage, target);  //to use with Action
        }
    }

    public class HealthTracker
    {
        public List<Attacker> attackersList = new List<Attacker>();
        public HealthTracker(List<Attacker> listOfAttackers)
        {
            attackersList = listOfAttackers;
            SubscribeToHealthChanged();
        }

        public void SubscribeToHealthChanged()
        {
            foreach (Attacker attacker in attackersList)
            {
                attacker.HealthComponent.healthChanged += OnHealthChanged;
            }
        }

        public void OnHealthChanged(Attacker attacker, int damage, Attacker target)
        {
            if (target.GetHealth() <= 0)
            {
                Console.WriteLine($"{attacker.GetType().Name} deals {damage} to {target.GetType().Name} and kills him. Game Over.");
            }
            else if (damage == 0)
                Console.WriteLine($"{attacker.GetType().Name} misses the shot. {target.GetType().Name} receives {damage} damage.");
            else
                Console.WriteLine($"{attacker.GetType().Name} deals {damage} to {target.GetType().Name}. " +
                    $"{target.GetType().Name} health lowers to {target.GetHealth()}.");
        }
    }


    public abstract class Entity : IDamageable
    {
        public HealthComponent HealthComponent { get; private set; } = new(); //doesn't seem to make sense to instantiate as property

        public int GetHealth()
        {
            return HealthComponent.Health;
        }

        public void ReceiveDamage(Attacker attacker, int damage, Attacker target)
        {
            HealthComponent.ReceiveDamage(attacker, damage, target);
        }
    }

    public abstract class Attacker : Entity
    {
        public int Damage { get; set; } = 1;
        public abstract void Attack(Attacker target);
    }

    public class Samurai : Attacker
    {
        public override void Attack(Attacker target)
        {
            target.ReceiveDamage(this, Damage, target);
        }
    }

    public class Marksman : Attacker
    {
        public Bullet Bullet { get; } = new Bullet();

        public override void Attack(Attacker target)
        {
            target.ReceiveDamage(this, Bullet.Damage, target);
        }
    }

    public class Bullet
    {
        public int Damage
        {
            get
            {
                Random rand = new Random();
                if (rand.Next(1, 4) == 2)
                    return 3;
                return 0;
            }
        }
    }
}