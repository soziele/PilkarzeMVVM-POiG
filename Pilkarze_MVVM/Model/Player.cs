using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pilkarze_MVVM.Model
{
   public class Player
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public uint Age { get; set; }
        public uint Weight { get; set; }

        public Player(string Name, string Surname, uint Age, uint Weigth)
        {
            this.Name = Name;
            this.Surname = Surname;
            this.Age = Age;
            this.Weight = Weigth;
        }

        public override string ToString()
        {
            return this.Name[0].ToString().ToUpper()+this.Name.Substring(1) + " " + this.Surname[0].ToString().ToUpper()+this.Surname.Substring(1) + " Wiek: " + this.Age.ToString() + " Waga: " + this.Weight.ToString();
        }


        public void Update(Player edited)
        {
            this.Name = edited.Name;
            this.Surname = edited.Surname;
            this.Age = edited.Age;
            this.Weight = edited.Weight;
        }

        

    }
}
