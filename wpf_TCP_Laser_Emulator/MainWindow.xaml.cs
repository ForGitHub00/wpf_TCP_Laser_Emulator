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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace wpf_TCP_Laser_Emulator {
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            p = new Path();

        }
        Path p;
        private void button_Click(object sender, RoutedEventArgs e) {
            //tcp_cntrl.SetAngle(Convert.ToInt32(textBox.Text));
            tcp_cntrl.StartMove();
        }

        private void button1_Click(object sender, RoutedEventArgs e) {
            //tcp_cntrl.CorrectAngle(Convert.ToInt32(textBox.Text));        
            
            tcp_cntrl.mw = this;
            //tcp_cntrl.MonitoringOverlap(cnv, line);
            //tcp_cntrl.SetLaserControl(laser_cntrl);
            tcp_cntrl.SetAngle(Convert.ToInt32(textBox.Text));
            Calculate();

        }
        public void LaserSetPos(double pos) {
            laser_cntrl.SetPosition(pos);
        }

        public void Calculate() {
            Thread thrd_calc = new Thread(new ThreadStart(calc));
            thrd_calc.Start();
        }
        public void calc() {
            while (true) {
                Dispatcher.Invoke(() => {
                    var g = RenderedIntersect(cnv, line, tcp_cntrl.laser);
                    p = new Path() { Stroke = Brushes.Yellow, StrokeThickness = 2, Data = g };
                    if (!p.Data.Bounds.IsEmpty) {
                        double temp = Math.Abs(Canvas.GetTop(tcp_cntrl.laser) - p.Data.Bounds.Y);

                        temp -= 50 * (Math.Sin(tcp_cntrl._tcp.Angle.Angle * Math.PI / 180));
                        laser_cntrl.SetPosition(temp);
                        tcp_cntrl._tcp.Offset = temp;
                        listBox.Items.Add($"{temp}\n {temp * (-Math.Sin(tcp_cntrl._tcp.Angle.Angle * Math.PI / 180))} \n\n");
                    }
                });
            }
        }
        static CombinedGeometry RenderedIntersect(Visual ctx, Shape s1, Shape s2) {
            var p1 = new Pen(Brushes.Transparent, 0.01);
            var t1 = s1.TransformToAncestor(ctx) as Transform;
            var t2 = s2.TransformToAncestor(ctx) as Transform;
            var g1 = s1.RenderedGeometry;
            var g2 = s2.RenderedGeometry;
            if (s1 is Line)
                g1 = g1.GetWidenedPathGeometry(p1);
            if (s2 is Line)
                g2 = g2.GetWidenedPathGeometry(p1);
            g1.Transform = t1;
            g2.Transform = t2;
            return new CombinedGeometry(GeometryCombineMode.Intersect, g1, g2);
        }
    }
}
