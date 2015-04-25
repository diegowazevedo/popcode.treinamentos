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

namespace treinamentos {
    public partial class MainPage : PhoneApplicationPage {
        public Training training { get; set; }

        public MainPage() {
            InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e) {

            List<Training> trainings = await TrainingService.GetTrainingsAsync();
            listTraining.ItemsSource = trainings;
            listTraining.SelectionChanged += OnSelectionChanged;
            progress.Visibility = Visibility.Collapsed;

        }

        protected override void OnNavigatedFrom(NavigationEventArgs e) {
            TrainingDetails trainingDetails = e.Content as TrainingDetails;
            if (trainingDetails != null) {
                //Passa o objeto carro como prarametro
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