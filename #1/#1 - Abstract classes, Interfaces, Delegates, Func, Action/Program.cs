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
        public int Health { get; set; } = 10;
        public delegate void HealthChanged(Attacker attacker, int damage, Attacker target);
        public HealthChanged? healthChanged;

        public void ReceiveDamage(Attacker attacker, int damage, Attacker target)
        {
            Health -= damage;
            healthChanged?.Invoke(attacker, damage, target);
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
        public HealthComponent HealthComponent { get; set; } = new();

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



















//using System.Reflection.Metadata;

//namespace Exercise_1___Class__Abstract_Class__Interface
//{
//    internal class Program
//    {
//        static void Main(string[] args)
//        {
//            IDamageable samurai = new Samurai();
//            IDamageable marksman = new Marksman();
//            Console.WriteLine("The combat between samurai and marksman starts now");

//            do
//            {
//                samurai.(marksman);
//                marksman.Attack(samurai);

//                Console.WriteLine($"samurai life is {samurai.healthComponent.Health}, " +
//                                    $"marksman life is {marksman.healthComponent.Health}");

//            } while (samurai.healthComponent.Health > 0 && marksman.healthComponent.Health > 0);

//        }
//    }

//    public interface IDamageable
//    {
//        void ReceiveDamage(IDamageable target, int damage) { }
//    }

//    public class HealthComponent : IDamageable
//    {
//        public int Health { get; set; } = 10;

//    }

//    public abstract class Entity
//    {
//        protected HealthComponent HealthComponent { get; set; } = new();
//    }

//    public abstract class Attacker : Entity, IDamageable
//    {
//        public int Damage { get; protected set; } = 1;

//        public abstract void Attack (IDamageable target);
//    }

//    public class Samurai : Attacker
//    {

//        public Samurai()
//        {
//            Damage = 1;
//        }

//        public override void Attack(IDamageable target)
//        {
//            ReceiveDamage(target, Damage);
//        }
//    }

//    public class Marksman : Attacker
//    {
//        public Bullet bullet = new();

//        public void Attack(IDamageable target)
//        {
//            ReceiveDamage(target, Damage);
//        }


//    }

//    public class Bullet
//    {
//        public int Damage
//        {
//            get
//            {
//                Random rand = new Random();
//                if (rand.Next(1, 4) == 2)
//                    return 3;
//                return 0;
//            }
//            set { }
//        }
//    }
//}

//Veo que lo has hecho bastante bien, pero hay fallos, por ejemplo, el único IDamageable debería ser el
// HealthComponent, ya que tenemos ese componente en cada Entity, no hace falta que Samurai o Marksman
//implementen la interfaz. Otra cosa que estás haciendo es que estás declarando la clase Attacker como abstract pero
// el método Attack no lo has declarado como tal. Respecto a este mismo método, no debería recibir como argumento
//un Entity? sino un IDamageable, no hace falta la ? ya que es una interfaz y puede ser null por defecto,
// no hace falta hacerla nullable. También sobre el método Attack veo que estás haciendo
//entity.HealthComponent.Health -= Damage, esto no es correcto, para algo tenemos el método ReceiveDamage 
//en IDamageable, utiliza ese método, y en este caso no necesitamos que ReceiveDamage pida una referencia al 
// Attacker ni a ninguna Entity, solo un float o int representando el daño. Además, ya que accedemos a la
// vida a través de la interfaz, no queremos hacer que HealthComponent sea público, así que mejor ponlo en
//protected. También decirte que Bullet no es un Attacker, sino una herramienta para atacar, así que con la variable
//de Damage nos basta, no hace falta que herede de nada.

//internal class Program
//{
//    static void Main(string[] args)
//    {
//        Samurai samurai = new();
//        Marksman marksman = new();
//        Console.WriteLine("The combat between samurai and marksman starts now");

//        do
//        {
//            samurai.Attack(marksman);
//            marksman.Attack(samurai);
//            Console.WriteLine($"samurai life is {samurai.HealthComponent.Health}, " +
//                                $"marksman life is {marksman.HealthComponent.Health}");

//        } while (samurai.HealthComponent.Health > 0 && marksman.HealthComponent.Health > 0);

//    }
//}


//public class Entity
//{
//    public HealthComponent HealthComponent { get; set; } = new HealthComponent();
//}

//public class HealthComponent : IDamageable
//{
//    public int Health { get; set; } = 10;
//}

//public interface IDamageable
//{
//    void ReceiveDamage(Attacker attacker, Entity entity) { }
//}


//public abstract class Attacker : Entity
//{
//    public int Damage { get; protected set; } = 1;

//    public virtual void Attack(Entity? entity)
//    {
//    }
//}

//public class Samurai : Attacker, IDamageable
//{
//    public Samurai()
//    {
//        Damage = 1;
//    }

//    public override void Attack(Entity entity)
//    {
//        entity.HealthComponent.Health -= Damage;
//    }
//}

//public class Marksman : Attacker, IDamageable
//{
//    private Bullet bullet = new();
//    private HealthComponent healthComponent = new();

//    public Marksman()
//    {
//        healthComponent.Health = 20;
//    }

//    public override void Attack(Entity entity)
//    {
//        Random rand = new Random();
//        if (rand.Next(1, 3) == 2)
//        {
//            entity.HealthComponent.Health -= bullet.Damage;
//        }
//    }
//}

//public class Bullet : Attacker
//{
//    public Bullet()
//    {
//        Damage = 2;
//    }
//}