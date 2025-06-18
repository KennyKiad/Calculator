using System.Data;

namespace Calculator
{
    public partial class Form1 : Form
    {

        private DataTable dataTab = new DataTable();
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonDigitsClick(object sender, EventArgs e)
        {
            var currentButton = sender as Button;
            textBox.Text += currentButton.Text.ToString();
        }

        private void buttonOperatorsClick(object sender, EventArgs e)
        {
            var currentButton = sender as Button;
            var currentText = textBox.Text.Trim();

            if (currentText.Length > 0)
            {
                switch (currentText[currentText.Length - 1])
                {
                    case '+':
                        break;
                    case '-':
                        break;
                    case '*':
                        break;
                    case '/':
                        break;

                    default:
                        textBox.Text += currentButton.Text.ToString();
                        break;
                }
            } else
            {
                char currentOperator = currentButton.Text.Trim()[0];
                if (currentOperator == '+' || currentOperator == '-')
                {
                    textBox.Text += currentOperator.ToString();
                }
            }
            
        }

        private void buttonClearClick(object sender, EventArgs e)
        {
            if (textBox.Text.Length > 0)
            {
                string text = textBox.Text.Trim();
                string textBoxMinus = text.Remove(text.Length - 1);
                
                textBox.Text = textBoxMinus.Trim();
            }
        }
        private void buttonAllClearClick(object sender, EventArgs e)
        {
            textBox.Text = "";
        }

        private void buttonResultClick(object sender, EventArgs e)
        {
            try
            {
                string expression = textBox.Text.Trim();
                textBox.Text = "";


                var result = dataTab.Compute(expression, null);
                if (result == DBNull.Value || result == null)
                {
                    throw new InvalidOperationException("Невозможно вычислить выражение");
                }

                double resultDouble = Math.Round( Convert.ToDouble(result), 15);

                textBox.Text = resultDouble.ToString().Replace(',', '.');

            } catch (Exception ex) {
                textBox.Text = $"Ошибка: {ex.Message}";
            }
        }
    }
}