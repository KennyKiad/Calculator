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

            if (textBox.Text.Length > 0 && CheckOperatorsNear(textBox.Text))
            {
                
                textBox.Text += $" {currentButton.Text}";

            } else
            {
                textBox.Text += currentButton.Text;
            }
        }

        private void buttonOperatorsClick(object sender, EventArgs e)
        {
            var currentButton = sender as Button;

            if ( textBox.Text.Length > 0 && !CheckOperatorsNear(textBox.Text.Trim()))
            {

                textBox.Text += currentButton.Text;

            } else if (textBox.Text.Length <= 0)
            {
                char currentChar = currentButton.Text.Trim()[0];
                if (currentChar == '+' || currentChar == '-')
                {
                    textBox.Text += currentChar.ToString();
                }
            }
            
        }

        private bool CheckOperatorsNear(string currentText)
        {
           
            switch (currentText[currentText.Length - 1])
            {
                case '+':
                    return true;
                case '-':
                    return true;
                case '*':
                    return true;
                case '/':
                    return true;

                default:
                    return false;
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
                string expression = textBox.Text.Replace(" ", "");

                var result = dataTab.Compute(expression, null);
                if (result == DBNull.Value || result == null)
                {
                    throw new InvalidOperationException("Syntax error");

                }

                double resultDouble = Math.Round( Convert.ToDouble(result), 15);
                if (Double.IsNaN(resultDouble) || 
                    expression.Contains("/0"))
                {
                    throw new InvalidOperationException("Нельзя делить на ноль");

                }

                textBox.Text = resultDouble.ToString().Replace(',', '.');

            } catch (Exception ex) {

                HandlerError(ex);
            
            }

        }

        private void HandlerError(Exception ex)
        {
            switch (ex.Message.Contains("Syntax error") || ex.Message.Contains("Cannot") || ex.Message.Contains("correct format"))
            {
                case true:
                    MessageBox.Show($"Невозможно вычислить выражение: {textBox.Text}", "Синтаксическая ошибка", MessageBoxButtons.OK);
                    break;

                case false:

                    switch (ex.Message)
                    {
                        case "The expression has too many closing parentheses.":
                            MessageBox.Show($"Невозможно вычислить выражение: {textBox.Text}", "Слишком много закрывающих скобок", MessageBoxButtons.OK);
                            break;

                        case "The expression is missing the closing parenthesis.":
                            MessageBox.Show($"Невозможно вычислить выражение: {textBox.Text}", "Скобки не закрыты", MessageBoxButtons.OK);
                            break;

                        case "Нельзя делить на ноль":
                            MessageBox.Show($"Невозможно вычислить выражение: {textBox.Text}", $"{ex.Message}", MessageBoxButtons.OK);
                            break;

                    }
                    break;
            }
        }
    }
}