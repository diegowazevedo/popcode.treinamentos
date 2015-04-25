using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using treinamentos.Src.Classes;

namespace treinamentos {
    public partial class TrainingDetails : PhoneApplicationPage {
        public Training training { get; set; }

        public TrainingDetails() {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {
            if (training != null) {
                photo.Source = training.imageLogo;
                tName.Text = training.name;
                tDescription.Text = training.description;
                tTarget.Text = training.target;
                tRequirements.Text = training.requirements;
                tMethodology.Text = training.methodology;
                tTopics.Text = training.topics;
                String teachers = "";
                foreach(Teacher teacher in training.teacher){
                    teachers += teacher.name+"\n";
                }
                tTeachers.Text = teachers;

            }
        }
    }
}