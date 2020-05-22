using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Pilkarze_MVVM.Model
{
    class MorePlayers
    {
        public List<Player> playersList = new List<Player>();
        public string storageFileName = @"C:\Users\Sonia\Desktop\Zajecia\Programowanie obiektowe i graficzne\Repository\Pilkarze_MVVM\Pilkarze_MVVM\storage.txt";

        public MorePlayers()
        {
            this.playersList = Data.LoadFromTxt(storageFileName);
            if (this.playersList == null) this.playersList = new List<Player>();
        }

        public void Add(Player newOne)
        {
            playersList.Add(newOne);           
        }

        public void Update(Player oldOne, Player newOne)
        {
            foreach (var player in playersList)
            {
                if (player == oldOne) player.Update(newOne);
            }
        }

        public bool IsDuplicate(string name, string surname, uint age, uint weight)
        {
            
            foreach (var player in playersList)
            {
                if (player.Name == name)
                {
                    if (player.Surname == surname)
                    {
                        if (player.Age == age)
                        {
                            if (player.Weight == weight) return true;
                        }
                    }
                }
            }
            return false;
        }

        public void Remove(int thisOne)
        {
            playersList.RemoveAt(thisOne);
        }

        public void Save()
        {
            Data.SaveToTxt(this.playersList,storageFileName);            
        }
    }
}