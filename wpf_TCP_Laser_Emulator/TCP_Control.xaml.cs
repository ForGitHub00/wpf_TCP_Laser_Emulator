using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace wpf_TCP_Laser_Emulator {
    /// <summary>
    /// Логика взаимодействия для TCP_Control.xaml
    /// </summary>
    public partial class TCP_Control : UserControl {
        public TCP_Control() {
            InitializeComponent();
            _tcp = new TCP();
            p = new Path();
            
        }
        public TCP _tcp;
        Shape s1;
        Visual ctx;
        Path p;
        Laser_Viewer laser_control;
        Canvas c;
        public MainWindow mw;

        public void SetLaserControl(Laser_Viewer las) {
            laser_control = las;
           // mw = new MainWindow();
        }
        public void SetAngle(double angle) {
            _tcp.setAngle(angle);
            grid.RenderTransform = _tcp.Angle;
        }
        public void CorrectAngle(double corection) {
            _tcp.Angle = new RotateTransform(_tcp.Angle.Angle + corection);
            grid.RenderTransform = _tcp.Angle;

        }

        public void MonitoringOverlap(Visual ctx1,Shape sh1) {
            Thread thrd_overlap = new Thread(new ThreadStart(overlap));
            //thrd_overlap.Start();
        }
        private void overlap() {
           
        }

        public void StartMove() {
            Thread thrd_move = new Thread(new ThreadStart(move));
            thrd_move.Start();
        }
        private void move() {
            _tcp.TCP_X = 10;
            _tcp.TCP_Y = 10;
            
            while (true) {
                _tcp.TCP_X += 0.8;
                Dispatcher.Invoke(() => {
                    Canvas.SetLeft(this, _tcp.TCP_X);
                    Canvas.SetTop(this, _tcp.TCP_Y);
                    Canvas.SetTop(laser, _tcp.TCP_Y); //* (Math.Sin(_tcp.Angle.Angle * Math.PI / 180)));
                    _tcp.Laser_X = Canvas.GetLeft(this) + 100;
                    _tcp.Laser_Y = Canvas.GetTop(this) + 100;
                });
                Thread.Sleep(30);
            }            
        }    
    }


    public class TCP {
        public TCP() {
            Angle = new RotateTransform(0);
        }
        public double TCP_X;
        public double TCP_Y;

        public double Laser_X;
        public double Laser_Y;

        public double Laser_Size;

        public double Offset;

        public RotateTransform Angle;

        public void setAngle(double value) {
            Angle = new RotateTransform(value);
        }


    }
}
