using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using org.mariuszgromada.math.mxparser;
using Expression = org.mariuszgromada.math.mxparser.Expression;

namespace Calculator
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    Double resultValue = 0;
    string operation = "";

    public MainWindow()
    {
      InitializeComponent();
      this.Loaded += (s, e) =>
      {
        Buttons.Children.OfType<Button>().ToList().ForEach(x => x.Click += (sender, r) =>
        {
          if (Answer.Text.Length <= 8 && ((Button)sender).Tag != null)
            Answer.Text += ((Button)sender).Tag.ToString();

          //if((Example.Text.Length + Answer.Text.Length) <= 18)

        });
      };

      GetAnswer();
    }


    private void GetAnswer()
    {
      string example = Example.Text;
      Expression expression = new Expression(example);
      var answer = expression.calculate();

      if (answer < 0)
      {
        Answer.Text = $"-{Convert.ToString(-answer)}";
      }
      else
      {
        Answer.Text = Convert.ToString(answer);
      }
      
      History.Text = null;
      History.Text += $"{Example.Text} = {Answer.Text}\r\n";

      Answer.Text = null;
      Example.Text = null;
    }

    private void Button_Menu(object sender, RoutedEventArgs e)
    {
      Form_Calculator.Children.Clear();
      //Window MainWindow = new Window();
    }

    // Clear answer and example in TextBlock
    // Insert answer and example in history
    private void Button_Clear(object sender, RoutedEventArgs e)
    {
      Answer.Text = null;
    }

    private void Button_Reverce(object sender, RoutedEventArgs e)
    {
      int number;
      if (Answer.Text != "")
      {
        number = Convert.ToInt32(Answer.Text);
        number = -number;
        Answer.Text = Convert.ToString(number);
      }
    }

    private void Button_Percent(object sender, RoutedEventArgs e)
    {
      string text = Answer.Text;
      int i = 0;
      if (Answer.Text != "")
      {
        foreach (char ch in text)
        {
          if (ch == '.')
            i++;
        }
        if (i <= 0)
          Answer.Text += "%";
      }
    }

    private void Button_Division(object sender, RoutedEventArgs e)
    {
      if (Example.Text != "" && Answer.Text == "")
      {
        if (Example.Text[Example.Text.Length - 1] == '+' || Example.Text[Example.Text.Length - 1] == '-' || Example.Text[Example.Text.Length - 1] == '*')
        {
          Example.Text = Example.Text.Remove(Example.Text.Length - 1);
          Example.Text += "÷";
        }
        
      }
      else if ((Example.Text.Length + Answer.Text.Length) <= 15 && Answer.Text != "" || Example.Text != "")
      {
        Example.Text += Answer.Text + "÷";
        Answer.Text = null;
      }
    }

    private void Button_Multiplication(object sender, RoutedEventArgs e)
    {
      if (Example.Text != "" && Answer.Text == "")
      {
        if (Example.Text[Example.Text.Length - 1] == '+' || Example.Text[Example.Text.Length - 1] == '-' || Example.Text[Example.Text.Length - 1] == '÷')
        {
          Example.Text = Example.Text.Remove(Example.Text.Length - 1);
          Example.Text += "*";
        }
      }
      else if ((Example.Text.Length + Answer.Text.Length) <= 15 && Answer.Text != "" || Example.Text != "")
      {
        Example.Text += Answer.Text + "*";
        Answer.Text = null;
      }
    }

    private void Button_Minus(object sender, RoutedEventArgs e)
    {
      if ((Example.Text != "" && Answer.Text == ""))
      {
        if (Example.Text[Example.Text.Length - 1] == '+' || Example.Text[Example.Text.Length - 1] == '÷' || Example.Text[Example.Text.Length - 1] == '*')
        {
          Example.Text = Example.Text.Remove(Example.Text.Length - 1);
          Example.Text += "-";
        }
      }
      else if ((Example.Text.Length + Answer.Text.Length) <= 15 && Answer.Text != "" || Example.Text != "")
      {
        Example.Text += Answer.Text + "-";
        Answer.Text = null;
      }
    }

    private void Button_Plus(object sender, RoutedEventArgs e)
    {
      if (Example.Text != "" && Answer.Text == "")
      {
        if (Example.Text[Example.Text.Length - 1] == '÷' || Example.Text[Example.Text.Length - 1] == '-' || Example.Text[Example.Text.Length - 1] == '*')
        {
          Example.Text = Example.Text.Remove(Example.Text.Length - 1);
          Example.Text += "+";
        }        
      }
      else if ((Example.Text.Length + Answer.Text.Length) <= 15 && Answer.Text != "" || Example.Text != "")
      {
        Example.Text += Answer.Text + "+";
        Answer.Text = null;
      }
    }

    private void Button_Dot(object sender, RoutedEventArgs e)
    {
      string text = Answer.Text;
      int i = 0;
      if (Answer.Text != "")
      {
        foreach (char ch in text)
        {
          if (ch == '.')
            i++;
        }
        if (i <= 0)
          Answer.Text += ".";
      }
    }

    private void Button_Equal(object sender, RoutedEventArgs e)
    {
      if (Answer.Text != "" && (Example.Text.Length + Answer.Text.Length) <= 15)
      {
        Example.Text += Answer.Text;
        Answer.Text = null;
        GetAnswer();
      }
    }
  }
}
