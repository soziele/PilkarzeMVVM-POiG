using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;


namespace Pilkarze_MVVM.ViewModel
{    
    using Model;
    using BaseClass;

   internal class PlayerViewModel :ViewModelBase
    {
        private MorePlayers players = new MorePlayers();
        

        public List<string> displayList { get => Display(players.playersList); }

        private List<string> Display(List<Player> list)
        {
            List<string> newList = new List<string>();
            for (int i = 0; i < list.Count; i++) newList.Add(list[i].ToString());
            return newList;
        }


        #region public interface

        private string name = "";
        private string surname = "";
        private uint age = 25;
        private uint weight = 70;
        public string Name
        {
            get => this.name;
            set
            {
                this.name = value;
                this.OnPropertyChanged();
            }
        }
        public string Surname
        {
            get => this.surname;
            set
            {
                this.surname = value;
                this.OnPropertyChanged();
            }
        }
        public uint Age {
            get => this.age;
            set
            {
                this.age = value;
                this.OnPropertyChanged();
            }
        }
        public uint Weight {
            get => this.weight;
            set
            {
                this.weight = value;
                this.OnPropertyChanged();
            }
        }
        public int Index { get; set; } = -1;

        #endregion

        #region commands

        private ICommand add = null;
        private ICommand update = null;
        private ICommand remove = null;

        public ICommand Add
        {
            get
            {
                if (add == null)
                {
                    add = new RelayCommand(arg =>
                    {
                        players.Add(new Player(Name, Surname, Age, Weight));
                        OnPropertyChanged(nameof(displayList));
                        players.Save();
                    },
                arg => (!string.IsNullOrEmpty(Name)) && (!string.IsNullOrEmpty(Surname))&& (Name.All(char.IsLetter))&&(Surname.All(char.IsLetter)) && !players.IsDuplicate(Name, Surname, Age, Weight)
                );
                }
                return add;
            }
        }
        
        public ICommand Remove
        {
            get
            {
                if (remove == null)
                    remove = new RelayCommand(
                        arg => {
                            var mb = MessageBox.Show("Czy na pewno chcesz usunąć tę osobę?", "Uwaga", MessageBoxButton.OKCancel);
                            if (mb == MessageBoxResult.OK)
                            {
                                players.Remove(Index);
                                OnPropertyChanged(nameof(displayList));
                                players.Save();
                            }
                        },
                        arg => (Index > -1)
                     );
                return remove;
            }
        }

        public ICommand Update
        {
            get
            {
                if (update == null)
                {
                    update = new RelayCommand(
                        arg =>
                        {
                            players.Update(players.playersList.ElementAt(Index), new Player(Name, Surname, Age, Weight));
                            OnPropertyChanged(nameof(displayList));
                            players.Save();
                        },
                        arg=>Index>-1&& (!string.IsNullOrEmpty(Name)) && (!string.IsNullOrEmpty(Surname)) && (Name.All(char.IsLetter)) && (Surname.All(char.IsLetter))&& !players.IsDuplicate(Name, Surname, Age, Weight)
                        );
                }
                return update;
            }
        }

     



        #endregion

    }
}
