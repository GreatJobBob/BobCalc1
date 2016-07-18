using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BobCalc1
{
    
    
    /// <summary>
         /// Interaction logic for MainWindow.xaml
         /// </summary>
    public partial class MainWindow : Window
    {
        private double accumulator = 0;
        private string lastOperation;
        private bool calculationPending;

        public MainWindow()
        {
            InitializeComponent();
            this.KeyDown += new KeyEventHandler(MainWindow_KeyDown);

        }

        void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {

            switch (e.Key)
            {
                case Key.NumPad0:
                    buttonZero.PerformClick();
                    break;
                case Key.NumPad1:
                    buttonOne.PerformClick();
                    break;
                case Key.NumPad2:
                    buttonTwo.PerformClick();
                    break;
                case Key.NumPad3:
                    buttonThree.PerformClick();
                    break;
                case Key.NumPad4:
                    buttonFour.PerformClick();
                    break;
                case Key.NumPad5:
                    buttonFive.PerformClick();
                    break;
                case Key.NumPad6:
                    buttonSix.PerformClick();
                    break;
                case Key.NumPad7:
                    buttonSeven.PerformClick();
                    break;
                case Key.NumPad8:
                    buttonEight.PerformClick();
                    break;
                case Key.NumPad9:
                    buttonNine.PerformClick();
                    break;
                case Key.Add:
                    buttonPlus.PerformClick();
                    break;
                case Key.Enter:
                    buttonEquals.PerformClick();
                    break;
            }
          
        }

        private void numButton_Click(object sender, RoutedEventArgs e)
        {
            if (calculationPending)
            {

                calcDisplay.Text = "";
                calculationPending = false;
            }
            string number = (sender as Button).Content.ToString();
            calcDisplay.Text = calcDisplay.Text == "0" ? number : calcDisplay.Text + number;
        }

        private void opButton_Click(object sender, RoutedEventArgs e)
        {

            // An operator was pressed; perform the last operation and store the new operator.
            string operation = (sender as Button).Content.ToString();
            if (operation == "C")
            {
                accumulator = 0;
                calcDisplay.Text = "0";
            }
            else
            {
                calculationPending = true;

                double currentValue = double.Parse(calcDisplay.Text);
                switch (lastOperation)
                {
                    case "+": accumulator += currentValue; break;
                    case "-": accumulator -= currentValue; break;
                    case "x": accumulator *= currentValue; break;
                    case "/": accumulator /= currentValue; break;
                    default: accumulator = currentValue; break;
                }
            }

            lastOperation = operation;
            //calcDisplay.Text = operation == "=" ? accumulator.ToString() : "0";
            if (operation == "=")
                calcDisplay.Text = accumulator.ToString();

        }

      
    }
}
