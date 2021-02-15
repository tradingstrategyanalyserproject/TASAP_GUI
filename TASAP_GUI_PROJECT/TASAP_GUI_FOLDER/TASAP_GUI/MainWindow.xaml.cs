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
using TASAP_COM;

namespace TASAP_GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string optionType = "put";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {

        }
        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void LV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Slider_DragCompletedSP(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            int strikePriceVal = (int)StrikePriceSlider.Value;
            StrikePriceVal.Text = strikePriceVal.ToString();
        }

        private void StockPriceSlider_DragCompletedSTP(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            int stockPriceVal = (int)StockPriceSlider.Value;
            StockPriceVal.Text = stockPriceVal.ToString();
        }

        private void MaturitySlider_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            int marturityVal = (int)MaturitySlider.Value;
            MaturityVal.Text = marturityVal.ToString();
        }

        private void RfRateSlider_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            int rfRateVal = (int)RfRateSlider.Value;
            RfRateVal.Text = rfRateVal.ToString();
        }

        private void VolatilitySlider_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            int volatilityVal = (int)VolatilitySlider.Value;
            VolatilityVal.Text = volatilityVal.ToString();
        }

        private void Launch_Click(object sender, RoutedEventArgs e)
        {
            // When clicking on Launch button, we call the OptionRequest Class we builded in the TASAP COM Project
            Optionrequest optionrequest = new Optionrequest(optionType, StockPriceSlider.Value,StrikePriceSlider.Value, (int)MaturitySlider.Value, RfRateSlider.Value, VolatilitySlider.Value);
            Greecs answerGreecs = optionrequest.Rfq_Handler();
        }

        private void ToggleButton_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            if ((bool)OptionType.IsChecked)
            {
                optionType =Call.Text ;
            }
            else
            {
                optionType = Put.Text ;
            }
        }
    }
}
