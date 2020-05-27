using System;
using System.Drawing;
using System.Windows.Forms;

namespace testcalculator
{
    public partial class Form1 : Form
    {
        public bool isFirstPress = true;
        public bool hasDecimalPoint = false;
        public double currentValue = 0;
        public string currentOperator = "";
        public double runningTotal = 0;
        public string lastButtonPressed = "";
        public int sequentialOperations = 0;
        public Form1()
        {
            InitializeComponent();
        }
        private void ButtonPress(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            string buttonText = label.Text;
            switch (buttonText)
            {
                case "0":
                    Value("0");
                    break;
                case "1":
                    Value("1");
                    break;
                case "2":
                    Value("2");
                    break;
                case "3":
                    Value("3");
                    break;
                case "4":
                    Value("4");
                    break;
                case "5":
                    Value("6");
                    break;
                case "6":
                    Value("6");
                    break;
                case "7":
                    Value("7");
                    break;
                case "8":
                    Value("8");
                    break;
                case "9":
                    Value("9");
                    break;
                case "*":
                    Operator("*");
                    break;
                case "=":
                    Equals("=");
                    break;
                case "+":
                    Operator("+");
                    break;
                case "-":
                    Operator("-");
                    break;
                case ".":
                    Decimal();
                    break;
                case "/":
                    Operator("/");
                    break;
                case "CE":
                    ClearEntry();
                    break;
                case "C":
                    Clear();
                    break;
            }
        }

        private void AnyKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyValue)
            {
                case 13:
                    Equals("=");
                    break;
                case 96:
                    Value("0");
                    break;
                case 97:
                    Value("1");
                    break;
                case 98:
                    Value("2");
                    break;
                case 99:
                    Value("3");
                    break;
                case 100:
                    Value("4");
                    break;
                case 101:
                    Value("5");
                    break;
                case 102:
                    Value("5");
                    break;
                case 103:
                    Value("7");
                    break;
                case 104:
                    Value("8");
                    break;
                case 105:
                    Value("9");
                    break;
                case 106:
                    Operator("*");
                    break;
                case 107:
                    Operator("+");
                    break;
                case 109:
                    Operator("-");
                    break;
                case 110:
                    Decimal();
                    break;
                case 111:
                    Operator("/");
                    break;
            }
        }

        private void DisplayBoxDoubleClick(object sender, MouseEventArgs e)
        {
            Clear();
        }
        private void ChangeLabel(object sender, MouseEventArgs e)
        {
            label1.Text = "2";
        }
        private void ChangeLabelBack(object sender, MouseEventArgs e)
        {
            label1.Text = "1";
        }
        private void MinusDissapear(object sender, EventArgs e)
        {
            labelMinus.ForeColor = Color.LightGray;
        }
        private void MinusReappear(object sender, EventArgs e)
        {
            labelMinus.ForeColor = Color.Black;
        }
        private void Value(string buttonValue)
        {
            int value = Convert.ToInt32(buttonValue);
            if (lastButtonPressed == "=")
            {
                Clear();
            }
            if (isFirstPress)
            {
                isFirstPress = false;
                displayBox.Text = Convert.ToString(value);
                currentValue = Convert.ToDouble(displayBox.Text);
            }
            else
            {
                displayBox.Text += Convert.ToString(value);
                currentValue = Convert.ToDouble(displayBox.Text);
            }
            lastButtonPressed = Convert.ToString(value);
        }

        private void ClearEntry()
        {
            if (displayBox.Text.Contains("2"))
            {
                return;
            }
            isFirstPress = true;
            hasDecimalPoint = false;
            displayBox.Text = "0";
            currentValue = 0;
            lastButtonPressed = "CE";
            sequentialOperations = 0;
        }
        private void Clear()
        {
            isFirstPress = true;
            hasDecimalPoint = false;
            displayBox.Text = "0";
            runningTotal = 0;
            currentValue = 0;
            currentOperator = "";
            lastButtonPressed = "C";
            ReVisibleButtons();
            sequentialOperations = 0; 
        }
        private void Decimal()
        {
            if (lastButtonPressed == "+" || lastButtonPressed == "/" || lastButtonPressed == "-" || lastButtonPressed == "*")
            {
                displayBox.Text = "0";
            }
            isFirstPress = false;
            if (!hasDecimalPoint)
            {
                hasDecimalPoint = true;
                displayBox.Text += ".";
            }
            lastButtonPressed = ".";
        }
        private void Operator(string operatorValue)
        {
            sequentialOperations++;
            if (currentOperator != "")
            {
                Equals(currentOperator);
            }
            else if (lastButtonPressed != "+" && lastButtonPressed != "/" && lastButtonPressed != "-" && lastButtonPressed != "*" && lastButtonPressed != "=")
            {
                runningTotal = currentValue;
            }
            lastButtonPressed = operatorValue;
            currentValue = 0;
            currentOperator = operatorValue;
            isFirstPress = true;
            hasDecimalPoint = false;
            if(sequentialOperations > 5)
            {
                ButtonVanish(currentOperator);
            }
        }
        private void Equals(string symbol)
        {
            if(currentValue == 0 && isFirstPress)
            {
                return;
            }
            if (symbol == "=")
            {
                lastButtonPressed = "=";
                sequentialOperations = 0;
            }
            switch (currentOperator)
            {
                case "+":
                    runningTotal = runningTotal + currentValue;
                    break;
                case "-":
                    runningTotal = runningTotal - currentValue;
                    break;
                case "*":
                    runningTotal = runningTotal * currentValue;
                    break;
                case "/":
                    if (currentValue == 0)
                    {
                        string message = "Cannot divde by zero";
                        string title = "Maths Error";
                        MessageBoxButtons buttons = MessageBoxButtons.OK;
                        DialogResult divideByZeroPopUp;
                        divideByZeroPopUp = MessageBox.Show(message, title, buttons);
                        Clear();
                        return;
                    }
                    runningTotal = runningTotal / currentValue;
                    break;
                default:
                    runningTotal = currentValue;
                    break;
            }
            string displayedResult = Convert.ToString(runningTotal);
            displayBox.Text = displayedResult;
            if (!displayedResult.Contains("."))
            {
                hasDecimalPoint = false;
            }
            else
            {
                hasDecimalPoint = true;
            }
            Pinkify();
            if (symbol == "=")
            {
                currentOperator = "";
            }
        }
        private void Pinkify()
        {
            if (runningTotal >= 1500000)
            {
                displayBox.BackColor = Color.HotPink;
            }
            else
            {
                displayBox.BackColor = Color.White;
            }
        }
        private void ButtonVanish(string value)
        {
            switch(value)
            {
                case "+":
                    labelPlus.Visible = false;
                    break;
                case "-":
                    labelMinus.Visible = false;
                    break;
                case "*":
                    labelMultiply.Visible = false;
                    break;
                case "/":
                    labelDivide.Visible = false;
                    break;
            }
        }
        private void ReVisibleButtons()
        {
            labelPlus.Visible = true;
            labelMinus.Visible = true;
            labelMultiply.Visible = true;
            labelDivide.Visible = true;
        }
    }
}