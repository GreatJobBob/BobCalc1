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

        
       List<string> cells = new List<string>();
                
      

        public MainWindow()
        {
            InitializeComponent();
           // this.KeyDown += new KeyEventHandler(MainWindow_KeyDown);
            this.KeyUp += new KeyEventHandler(MainWindow_KeyUp);


        }

        void MainWindow_KeyUp(object sender, KeyEventArgs e)
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
                    e.Handled = true;
                    buttonEquals.PerformClick();
                   
                    break;

            }
          
        }

        // KeyDown event swallowed for enter key so use PreviewKeyDown instead
        private void BobCalc_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            return;
            if (e.Key == Key.Enter)
            {
                buttonEquals.PerformClick();
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
            HistoryDisplay.Text += number;
        }

        private void opButton_Click(object sender, RoutedEventArgs e)
        {

            // An operator was pressed; perform the last operation and store the new operator.
            string operation = (sender as Button).Content.ToString();
            if (operation == "C")
            {
                accumulator = 0;
                calcDisplay.Text = "0";
                HistoryDisplay.Text = "";
                calculationPending = false;
                return;

               
            }
            else
            {
                cells.Add(calcDisplay.Text);
                cells.Add(operation);
               
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
                HistoryDisplay.Text += operation;
                calcDisplay.Text = accumulator.ToString();
            }

            lastOperation = operation;
            //calcDisplay.Text = operation == "=" ? accumulator.ToString() : "0";
            if (operation == "=")
            {
                calcDisplay.Text = accumulator.ToString();
                HistoryDisplay.Text += accumulator.ToString();
                ReduceMultiplyAndDivide("x");
                ReduceMultiplyAndDivide("/");
            }
        }

        void ReduceMultiplyAndDivide(string operation)
        {
            bool mayBeMore = true;             // evaluate condense any multiply operations
            double result = 0;


            while (mayBeMore){
                int s = cells.FindIndex(x => x.ToString() == operation);
                if (s >= 0)
                {
                    if (operation == "x")
                    {
                        result = Convert.ToDouble(cells[s - 1]) * Convert.ToDouble(cells[s + 1]);
                    }
                    else if (operation == "/") {

                        result = Convert.ToDouble(cells[s - 1]) / Convert.ToDouble(cells[s + 1]);
                    }
                    cells[s] = result.ToString();
                    cells.RemoveAt(s + 1);
                    cells.RemoveAt(s - 1);
                }
                else mayBeMore = false;
            }

           




        }

       
    }
}
