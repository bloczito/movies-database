using MoviesDatabase.Commands;
using MoviesDatabase.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MoviesDatabase.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private List<Movie> _movies;
        public List<Movie> Movies
        {
            get { return _movies; }
            set {
                _movies = value; 
                OnPropertyChanged();
            }
        }


        private Movie? _selectedMovie;
        public Movie? SelectedMovie
        {
            get { return _selectedMovie; }
            set { 
                _selectedMovie = value;
                OnPropertyChanged();

                if(value != null)
                {
                    Title = value.Title;
                    Description = value.Description;
                }
            }
        }

        private string? _title;
        public string? Title
        {
            get { return _title; }
            set { 
                _title = value;
                OnPropertyChanged();
            }
        }

        private string? _description;
        public string? Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }

        private string _newTitle = "";
        public string NewTitle
        {
            get { return _newTitle; }
            set { 
                _newTitle = value; 
                OnPropertyChanged();
            }
        }

        private string _newDescription = "";
        public string NewDescription
        {
            get { return _newDescription; }
            set { 
                _newDescription = value; 
                OnPropertyChanged();
            }
        }

        public ICommand AddButtonCommand { get; set; }

        public ICommand SaveButtonCommand { get; set; }
        
        public ICommand RemoveButtonCommand { get; set; }

        private void AddMovie(object sender)
        {
            if (NewTitle == null || NewTitle.Length == 0)
            {
                MessageBox.Show("Title is required");
                return;
            }

            var newList = new List<Movie>(Movies);

            newList.Add(new Movie(GetNewId(), NewTitle, NewDescription ));

            Movies = newList.OrderBy(x => x.Title).ToList();

            NewTitle = "";
            NewDescription = "";
        }

        private int GetNewId()
        {
            if (Movies == null || Movies.Count == 0) return 1;
           
            return _movies.Max(x => x.Id) + 1;
        }

        private void SaveMovie(object sender)
        {
            if (SelectedMovie != null && Title != null)
            {
                Movies = Movies
                    .Select(m =>
                    {
                        if (m.Id == SelectedMovie.Id)
                        {
                            return new Movie(m.Id, Title, Description);
                        }
                        else
                        {
                            return m;
                        }
                    })
                    .OrderBy(x => x.Title)
                    .ToList();
            }
        } 

        private void RemoveMovie(object sender)
        {
            if (SelectedMovie != null)
            {
                Movies = Movies.Where(x => x.Id != SelectedMovie.Id).ToList();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MainViewModel()
        {
            AddButtonCommand = new RelayCommand(AddMovie);
            SaveButtonCommand = new RelayCommand(SaveMovie);
            RemoveButtonCommand = new RelayCommand(RemoveMovie);

            _movies = new List<Movie>()
            {
                new Movie(1, "Movie 1", "Description 1"),
                new Movie(2, "Movie 2", "Description 2"),
                new Movie(3, "Movie 3", "Description 3"),
                new Movie(4, "Movie 4", "Description 4"),
                new Movie(5, "Movie 5", "Description 5"),
            };

        }

        
    }

}
