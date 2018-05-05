using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentBuilderPattern
{
    public class Person
    {
        public string Name { get; set; }
        public string Job { get; set; }

        public static Builder New => new Builder();

        public override string ToString()
        {
            return $"{nameof(Name)}:{Name} , {nameof(Job)}:{Job}";
        }

        public class Builder : PersonJobBuilder<Builder>
        {

        }
        
    }
    
    public abstract class PersonBuilder
    {
        protected Person person = new Person();

        public Person Build()
        {
            return person;
        }
    }

    public class PersonInfoBuilder<SELF> : PersonBuilder where SELF : PersonInfoBuilder<SELF>
    {

        public SELF Called(string name)
        {
            person.Name = name;
            return (SELF)this;

        }
    }

    public class PersonJobBuilder<SELF> : PersonInfoBuilder<PersonJobBuilder<SELF>> where SELF : PersonJobBuilder<SELF>
    {
        public SELF WorkAsA(string job)
        {
            person.Job = job;
            return (SELF)this;

        }
    }



    class Program
    {
        static void Main(string[] args)
        {

            var persona = Person.New.Build();
            Console.WriteLine(persona.ToString());

            var personaInfo = Person.New.Called("Inspector").Build();
            Console.WriteLine(personaInfo.ToString());

            var personaTrabajadora = Person.New.WorkAsA("Inspector de una linea de colectivos!").Build();
            Console.WriteLine(personaTrabajadora.ToString());

            var personaInfoTrabajadora = Person.New.Called("Inspector").WorkAsA("Atiende boludos!").Build();
            Console.WriteLine(personaInfoTrabajadora.ToString());

            Console.ReadKey();

        }
    }
}
