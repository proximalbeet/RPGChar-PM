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

namespace RPGChar_PM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Random _rng = new Random();
        private RPGCharacter _character;
        
        public MainWindow()
        {
            InitializeComponent();
            _character = new RPGCharacter(_rng);
            updateStats();
            
        }

        private void buttonUpdateName_Click(object sender, RoutedEventArgs e)
        {
            _character.Name = textBoxName.Text;
            
        }

        private void buttonReroll_Click(object sender, RoutedEventArgs e)
        {
            _character.Roll();
            updateStats();
            double odds = 0.5f;
            _character.PartyMembers.Clear();
            foreach (ListBoxItem i in ListPotentialMembers.Items) 
            { if (_rng.NextDouble() > odds)
                {
                    RPGCharacter r = new RPGCharacter(_rng)
                    {
                        Name = i.Content.ToString()
                    };
                    _character.PartyMembers.Add(r);
                }
            }
            if (_character.PartyMembers.Count > 0) 
            {
                _character.PartyMembers[0].FavoriteColor = Brushes.Gold;
            }

            // Adding party mambers to party members list box.
            ListPartyNumbers.Items.Clear();
            Brush color1 = Brushes.Aqua;
            Brush color2 = Brushes.OrangeRed;
            foreach (RPGCharacter r in _character.PartyMembers) 
            { 
              ListBoxItem i = new ListBoxItem();
                if (r.FavoriteColor != null) 
                { 
                 i.Background = r.FavoriteColor;
                }
                // Alternate background colors
                else if (ListPartyNumbers.Items.Count % 2 == 0)
                {
                    i.Background = color1;
                }
                else
                { 
                 i.Background = color2;
                }
                i.Content = $"{r.Name} \n STR: {r.Strength} \n INT: {r.Intelligence}"; 
              ListPartyNumbers.Items.Add(i);
            }
        }

        
        private void updateStats()
        {
            textStrength.Text = _character.Strength.ToString();
            textIntelligence.Text = _character.Intelligence.ToString();
            textDexterity.Text = _character.Dexterity.ToString();
            textStamina.Text = _character.Stamina.ToString();
            textWisdom.Text = _character.Wisdom.ToString();
            textCharisma.Text = _character.Charisma.ToString();
        }

       
    }
}
