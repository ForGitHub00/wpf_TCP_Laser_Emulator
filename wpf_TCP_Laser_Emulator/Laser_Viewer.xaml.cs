using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace wpf_TCP_Laser_Emulator {
    /// <summary>
    /// Логика взаимодействия для Laser_Viewer.xaml
    /// </summary>
    public partial class Laser_Viewer : UserControl {
        public Laser_Viewer() {
            InitializeComponent();
        }
        public void SetPosition(double value) {
            Canvas.SetLeft(el_pos, 250 + value * 4);            
            cnv.Children.Remove(el_pos);
            cnv.Children.Add(el_pos);
        }
    }
}
