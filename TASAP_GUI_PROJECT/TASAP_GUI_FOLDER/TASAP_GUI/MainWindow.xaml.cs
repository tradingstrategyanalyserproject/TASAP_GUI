using LiveCharts;
using LiveCharts.Wpf;
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
        public List<RadioButton> rblist = new List<RadioButton>();

        public partial class SeriesChart : UserControl
        {
            SeriesChart cruves;
            String checkedOne;

            public SeriesChart(List<RadioButton> rblist, Greecs gr, Grid graphgrid)
            {
                foreach (RadioButton check in rblist)
                {
                    if ((bool)check.IsChecked)
                    {
                        checkedOne = check.Name;
                    }
                }

                CartesianChart mychart = new CartesianChart();
                mychart.Series = new SeriesCollection
                {
                    new LineSeries
                    {
                        Title = "Series 1",
                        Values = new ChartValues<double> { Convert.ToDouble(gr.greecsdico.Values.ElementAt(2)),Convert.ToDouble(gr.greecsdico.Values.ElementAt(1))}
                    }

                };

                graphgrid.Children.Add(mychart);

            }

        }

        public MainWindow()

        {
            InitializeComponent();
            rblist.Add(STPrb);
            rblist.Add(STKrb);
            rblist.Add(Volrb);
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
            Optionrequest optionrequest = new Optionrequest(optionType, (int)StockPriceSlider.Value, (int)StrikePriceSlider.Value, (int)MaturitySlider.Value, (int)RfRateSlider.Value, (int)VolatilitySlider.Value);
            Greecs answerGreecs = optionrequest.Rfq_Handler();
            //PayoffVal.Content = answerGreecs.greecsdico.Values.ElementAt(2).ToString();
            PayoffVal.Content = answerGreecs.greecsdico.Values.ElementAt(2).ToString();
            SeriesChart sch = new SeriesChart(rblist, answerGreecs, GraphGrid);
            //SeriesCollection SeriesCollectionxml = sch.SeriesCollection;
        }

        private void OptionType_Checked(object sender, RoutedEventArgs e)
        {
            optionType = "call";
        }

        private void OptionType_Unchecked(object sender, RoutedEventArgs e)
        {
            optionType = "put";

        }

        private void STPrb_Click(object sender, RoutedEventArgs e)
        {
            if((bool)STPrb.IsChecked)
            foreach(RadioButton rb in rblist)
                {
                    if(rb.Name != STPrb.Name)
                    {
                        rb.IsChecked = false;
                    }
                }
        }

        private void STKrb_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)STKrb.IsChecked)
                foreach (RadioButton rb in rblist)
                {
                    if (rb.Name != STKrb.Name)
                    {
                        rb.IsChecked = false;
                    }
                }
        }

        private void Volrb_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)Volrb.IsChecked)
                foreach (RadioButton rb in rblist)
                {
                    if (rb.Name != Volrb.Name)
                    {
                        rb.IsChecked = false;
                    }
                }
        }
    }
}
