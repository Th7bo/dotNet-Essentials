using System;
using System.Windows;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EurosCents
{
    // opgave
    // 1. als de gebruiker alt+c ingeeft moet de muis gepositioneerd worden op het textvak centsTextbox
    // 2. bij klikken op de calculateButton moet in
    //    result1Label bedrag in euros en centen komen  bvb. 125 euros and 63 cents
    //    result2label bedrag in euros  bvb. 125,63
    // 3. opmaak aanpassen
    // 3.1 result1Label: minstens 6 posities voor de euros (rechts uitgelijnd) en 2 posities voor de centen
    
    // 3.2 result2Label: euro's met 2 cijfers na de komma en valutateken + scheidingsteken tussen 1000-tallen en 100-tallen
    
    // 3.3 result1Label: minstens 6 posities voor de euros (links uitgelijnd) 

    // 3.4 result2Label: euro's met 1 cijfer na de komma en valutateken + scheidingsteken tussen 1000-tallen en 100-tallen
    
    // 3.5 result1Label: minstens 6 posities voor de euros met voorloopnullen 
    
    // 3.6 result2Label: euro's met 0 cijfers na de komma zonder valutateken + scheidingsteken tussen 1000-tallen en
    // 100-tallen en minstens 10 posities rechts uitgelijnd 
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void calculateButton_Click(object sender, RoutedEventArgs e)
        {
            int centen = Convert.ToInt32(centsTextBox.Text);
            int rest = centen % 100;
            //2
            //result1Label.Content = $"{centen / 100} euros and {rest} cents";
            //result2Label.Content = $"{centen / 100.0}";
            //3.1
            //result1Label.Content = $"{centen / 100,6} euros and {rest,2} cents";
            //3.2
            //result2Label.Content = $"{centen / 100.0:C}";
            //3.3
            //result1Label.Content = $"{centen / 100,-6} euros and {rest} cents";
            //3.4
            //result2Label.Content = $"{centen / 100.0:C1}";
            //3.5
            result1Label.Content = $"{centen / 100:D6} euros and {rest} cents";
            //3.6
            result2Label.Content = $"{centen / 100.0,10:N0}";
        }
    }
}
