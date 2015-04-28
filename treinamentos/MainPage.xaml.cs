using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using treinamentos.Resources;
using treinamentos.Src.Classes;
using treinamentos.Src.Classes.Entity;
using System.Diagnostics;

namespace treinamentos {
    public partial class MainPage : PhoneApplicationPage {
        public Training training { get; set; }

        public MainPage() {
            InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e) {
            listTraining.Visibility = Visibility.Collapsed;
            progress.Visibility = Visibility.Visible;

            List<Training> trainings = new List<Training>();

            if (!HttpHelper.CheckInternetConnection()) {
                MessageBox.Show("Sem conexão com a internet.");
                Database db = new Database();
                if (db.DatabaseExists()) {
                    trainings = TrainingDB.getTrainings();
                }
            } else {
                trainings = await TrainingService.GetTrainingsAsync();
            }
            listTraining.ItemsSource = trainings;
            listTraining.SelectionChanged += OnSelectionChanged;
            progress.Visibility = Visibility.Collapsed;
            listTraining.Visibility = Visibility.Visible;

        }

        protected override void OnNavigatedFrom(NavigationEventArgs e) { 
            TrainingDetails trainingDetails = e.Content as TrainingDetails;
            if (trainingDetails != null) {
                trainingDetails.training = this.training;
            }
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e) {
            Training t = (sender as ListBox).SelectedItem as Training;
            if (t != null) {
                this.training = t;
                this.NavigationService.Navigate(new Uri("/TrainingDetails.xaml", UriKind.Relative));
            }
        }
    }
}