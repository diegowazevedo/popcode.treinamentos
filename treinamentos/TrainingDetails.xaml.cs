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
using treinamentos.Src.Classes.Entity;
using System.Diagnostics;

namespace treinamentos {
    public partial class TrainingDetails : PhoneApplicationPage {
        public Training training { get; set; }

        public TrainingDetails() {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {
            if (training != null) {
                Database db = new Database();
                Training tr = db.Training.Single(t => t.id == training.id);
                photo.Source = tr.imageLogo;
                tName.Text = tr.name;
                tDescription.Text = tr.description;
                tTarget.Text = tr.target;
                tRequirements.Text = tr.requirements;
                tMethodology.Text = tr.methodology;
                wTopics.NavigateToString(tr.topics);
                String te = "";
                foreach (var t in tr.teachers) {
                    te += t.name + "\n";
                }
                tTeachers.Text = te;

            }
        }
    }
}