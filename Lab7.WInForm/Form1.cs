using System;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

using Task1Lib = ClassTask1.Class1;
using Task2Lib = ClassTask2.Class1;
using Task3Lib = ClassTask3.Class1;
using Task4Lib = ClassTask4.Class1;

namespace Lab7.WinForm
{
    public partial class Form1 : Form
    {
        private readonly Random _rnd = new Random();

        public Form1()
        {
            InitializeComponent();

            btn_t1_generate.Click += btn_t1_generate_Click;
            btn_t12_calc.Click += btn_t12_calc_Click;
            btn_t13_generate.Click += btn_t13_generate_Click;
            btn_t2_calc.Click += btn_t2_calc_Click;
            btn_t3_calc.Click += btn_t3_calc_Click;
            btn_t4_calc.Click += btn_t4_calc_Click;

            rb_t4_default.CheckedChanged += rb_t4_CheckedChanged;
            rb_t4_manual.CheckedChanged += rb_t4_CheckedChanged;

            rb_t4_default.Checked = true;
            UpdateT4InputsState();

            rtb_t1_out.Clear();
            rtb_t12_out.Clear();
            rtb_t13_out.Clear();
            rtb_t2_out.Clear();
            rtb_t3_out.Clear();
            rtb_t4_out.Clear();
        }

        private void ShowError(string message, Control focusCtrl = null)
        {
            MessageBox.Show(
                message,
                "Ошибка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );

            if (focusCtrl != null)
            {
                focusCtrl.BackColor = Color.LightPink;
                focusCtrl.Focus();

                if (focusCtrl is TextBox tb)
                    tb.SelectAll();
            }
        }

        private void ClearError(Control ctrl)
        {
            if (ctrl != null)
                ctrl.BackColor = SystemColors.Window;
        }

        private void AppendLineColor(RichTextBox rtb, string text, Color color)
        {
            rtb.SelectionStart = rtb.TextLength;
            rtb.SelectionLength = 0;
            rtb.SelectionColor = color;

            rtb.AppendText(text + Environment.NewLine);

            rtb.SelectionColor = rtb.ForeColor;
        }

        private bool TryReadInt(TextBox tb, string name, out int value)
        {
            if (!int.TryParse((tb.Text ?? "").Trim(), out value))
            {
                ShowError($"Введите целое число ({name}).", tb);
                return false;
            }

            ClearError(tb);
            return true;
        }

        private bool TryReadIntRange(
            TextBox tb, string name,
            int min, int max,
            out int value)
        {
            if (!TryReadInt(tb, name, out value))
                return false;

            if (value < min || value > max)
            {
                ShowError(
                    $"Некорректное значение ({name}). Допустимо: [{min}; {max}].",
                    tb
                );

                return false;
            }

            ClearError(tb);
            return true;
        }

        private bool TryReadDouble(TextBox tb, string name, out double value)
        {
            string s = ((tb.Text ?? "").Trim()).Replace(',', '.');

            if (!double.TryParse(
                s,
                NumberStyles.Float,
                CultureInfo.InvariantCulture,
                out value)
                || double.IsNaN(value)
                || double.IsInfinity(value))
            {
                ShowError($"Введите действительное число ({name}).", tb);
                return false;
            }

            ClearError(tb);
            return true;
        }

        private bool TryReadMinMax(
            TextBox tbMin,
            TextBox tbMax,
            out int minV,
            out int maxV)
        {
            minV = 0;
            maxV = 0;

            if (!TryReadIntRange(tbMin, "min", -1000, 1000, out minV))
                return false;

            if (!TryReadIntRange(tbMax, "max", minV, 1000, out maxV))
                return false;

            return true;
        }


        private static bool IsRowSortedDesc(int[,] a, int row)
        {
            int n = a.GetLength(1);

            for (int j = 0; j < n - 1; j++)
            {
                if (a[row, j] < a[row, j + 1])
                    return false;
            }

            return true;
        }

        private void btn_t1_generate_Click(object sender, EventArgs e)
        {
            if (!TryReadIntRange(tb_t1_m, "m", 1, 30, out int m))
                return;

            if (!TryReadIntRange(tb_t1_n, "n", 1, 30, out int n))
                return;

            if (!TryReadMinMax(
                tb_t1_min, tb_t1_max,
                out int minV, out int maxV))
                return;

            int[,] a = new int[m, n];

            for (int i = 0; i < m; i++)
                for (int j = 0; j < n; j++)
                    a[i, j] = _rnd.Next(minV, maxV + 1);

            int count = Task1Lib.CountRowsSortedDesc(a);

            rtb_t1_out.Clear();

            AppendLineColor(
                rtb_t1_out,
                $"Матрица {m}x{n} (min={minV}, max={maxV}):",
                Color.DimGray
            );

            AppendLineColor(rtb_t1_out, "", Color.DimGray);

            for (int i = 0; i < m; i++)
            {
                bool ok = IsRowSortedDesc(a, i);

                var sb = new StringBuilder();

                for (int j = 0; j < n; j++)
                    sb.AppendFormat("{0,6}", a[i, j]);

                AppendLineColor(
                    rtb_t1_out,
                    sb.ToString(),
                    ok ? Color.Green : Color.Black
                );
            }

            AppendLineColor(rtb_t1_out, "", Color.DimGray);

            AppendLineColor(
                rtb_t1_out,
                $"RESULT: Кол-во строк = {count}",
                Color.Green
            );
        }

        private void btn_t12_calc_Click(object sender, EventArgs e)
        {
            if (!TryReadDouble(tb_t12_x, "x", out double x))
                return;

            if (!TryReadDouble(tb_t12_y, "y", out double y))
                return;

            if (!TryReadDouble(tb_t12_z, "z", out double z))
                return;

            double a = Task1Lib.CalcA(x, y);
            double b = Task1Lib.CalcB(x, z);

            rtb_t12_out.Clear();

            AppendLineColor(
                rtb_t12_out,
                "Задание 1.2. Математические формулы варианта №6",
                Color.DimGray
            );

            AppendLineColor(rtb_t12_out, "", Color.DimGray);

            AppendLineColor(
                rtb_t12_out,
                $"x = {x}, y = {y}, z = {z}",
                Color.DimGray
            );

            double numerator =
                Math.Sqrt(Math.Abs(x - 1)) - Math.Sqrt(Math.Abs(y));

            double denominator =
                1 + Math.Pow(x, 2) / 2 + Math.Pow(y, 2) / 4;

            AppendLineColor(
                rtb_t12_out,
                $"Числитель a = {numerator:F6}",
                Color.DimGray
            );

            AppendLineColor(
                rtb_t12_out,
                $"Знаменатель a = {denominator:F6}",
                Color.DimGray
            );

            AppendLineColor(
                rtb_t12_out,
                $"arctg(z) = {Math.Atan(z):F6}",
                Color.DimGray
            );

            AppendLineColor(
                rtb_t12_out,
                $"arctg^3(z) = {Math.Pow(Math.Atan(z), 3):F6}",
                Color.DimGray
            );

            AppendLineColor(
                rtb_t12_out,
                $"e = {Math.E:F6}",
                Color.DimGray
            );

            AppendLineColor(rtb_t12_out, "", Color.DimGray);

            AppendLineColor(
                rtb_t12_out,
                $"RESULT: a = {a:F6}",
                Color.Green
            );

            AppendLineColor(
                rtb_t12_out,
                $"RESULT: b = {b:F6}",
                Color.Green
            );
        }

        private void btn_t13_generate_Click(object sender, EventArgs e)
        {
            if (!TryReadIntRange(tb_t13_n, "n", 1, 100, out int n))
                return;

            if (!TryReadMinMax(
                tb_t13_min, tb_t13_max,
                out int minV, out int maxV))
                return;

            int[] a = new int[n];

            for (int i = 0; i < n; i++)
                a[i] = _rnd.Next(minV, maxV + 1);

            int count = Task1Lib.CountNegative(a);

            rtb_t13_out.Clear();

            AppendLineColor(
                rtb_t13_out,
                $"Массив (n={n}, min={minV}, max={maxV}):",
                Color.DimGray
            );

            AppendLineColor(rtb_t13_out, "", Color.DimGray);

            for (int i = 0; i < n; i++)
            {
                rtb_t13_out.SelectionStart = rtb_t13_out.TextLength;
                rtb_t13_out.SelectionLength = 0;

                rtb_t13_out.SelectionColor =
                    a[i] < 0 ? Color.Green : Color.Black;

                rtb_t13_out.AppendText($"{a[i],6}");
            }

            rtb_t13_out.AppendText(Environment.NewLine);

            rtb_t13_out.SelectionColor = rtb_t13_out.ForeColor;

            AppendLineColor(rtb_t13_out, "", Color.DimGray);

            AppendLineColor(
                rtb_t13_out,
                $"RESULT: Кол-во отрицательных элементов = {count}",
                Color.Green
            );

            rtb_t13_out.SelectionStart = 0;
            rtb_t13_out.SelectionLength = 0;
            rtb_t13_out.ScrollToCaret();
        }
        private void btn_t2_calc_Click(object sender, EventArgs e)
        {
            if (!TryReadDouble(tb_t2_a, "a", out double a))
                return;

            if (!TryReadDouble(tb_t2_b, "b", out double b))
                return;

            Task2Lib.CalcU(in a, in b, out double u);
            Task2Lib.CalcV(in a, in b, out double v);

            double k = 0;
            Task2Lib.CalcK(in u, in v, ref k);

            rtb_t2_out.Clear();

            AppendLineColor(
                rtb_t2_out,
                $"a = {a}, b = {b}",
                Color.DimGray
            );

            AppendLineColor(
                rtb_t2_out,
                $"b - a = {b - a}",
                Color.DimGray
            );

            AppendLineColor(
                rtb_t2_out,
                $"a*b = {a * b}",
                Color.DimGray
            );

            AppendLineColor(
                rtb_t2_out,
                $"a + b = {a + b}",
                Color.DimGray
            );

            AppendLineColor(rtb_t2_out, "", Color.DimGray);

            AppendLineColor(
                rtb_t2_out,
                $"u = max(a, b-a) = {u}",
                Color.DimGray
            );

            AppendLineColor(
                rtb_t2_out,
                $"v = min(a*b, a+b) = {v}",
                Color.DimGray
            );

            AppendLineColor(
                rtb_t2_out,
                $"u + 2*v = {u + 2 * v}",
                Color.DimGray
            );

            AppendLineColor(
                rtb_t2_out,
                $"k = min(u + 2*v, 3.14) = {k}",
                Color.DimGray
            );

            AppendLineColor(rtb_t2_out, "", Color.DimGray);

            AppendLineColor(rtb_t2_out, $"RESULT: u = {u}", Color.Green);
            AppendLineColor(rtb_t2_out, $"RESULT: v = {v}", Color.Green);
            AppendLineColor(rtb_t2_out, $"RESULT: k = {k}", Color.Green);
        }

        private void btn_t3_calc_Click(object sender, EventArgs e)
        {
            if (!TryReadIntRange(
                tb_t3_a, "a",
                -1000000, 1000000,
                out int a))
                return;

            if (!TryReadIntRange(
                tb_t3_b, "b",
                -1000000, 1000000,
                out int b))
                return;

            if (!TryReadIntRange(
                tb_t3_sum, "сумма цифр",
                0, 200,
                out int sum))
                return;

            Task3Lib.BuildNumbers(
                ref a, ref b,
                in sum,
                out int[] result
            );

            rtb_t3_out.Clear();

            AppendLineColor(
                rtb_t3_out,
                $"Диапазон: [{a}; {b}]",
                Color.DimGray
            );

            AppendLineColor(
                rtb_t3_out,
                $"Сумма цифр: {sum}",
                Color.DimGray
            );

            AppendLineColor(rtb_t3_out, "", Color.DimGray);

            if (result.Length == 0)
            {
                AppendLineColor(
                    rtb_t3_out,
                    "RESULT: NO",
                    Color.Green
                );

                return;
            }

            AppendLineColor(
                rtb_t3_out,
                $"RESULT: Найдено чисел = {result.Length}",
                Color.Green
            );

            AppendLineColor(rtb_t3_out, "", Color.DimGray);

            var sb = new StringBuilder();

            for (int i = 0; i < result.Length; i++)
            {
                sb.Append(result[i]);
                sb.Append(' ');

                if ((i + 1) % 10 == 0)
                    sb.AppendLine();
            }

            AppendLineColor(
                rtb_t3_out,
                sb.ToString(),
                Color.Green
            );
        }


        private void rb_t4_CheckedChanged(object sender, EventArgs e)
        {
            UpdateT4InputsState();
        }

        private void UpdateT4InputsState()
        {
            bool manual = rb_t4_manual.Checked;

            tb_t4_x1.Enabled = manual;
            tb_t4_y1.Enabled = manual;

            tb_t4_x2.Enabled = manual;
            tb_t4_y2.Enabled = manual;

            tb_t4_x3.Enabled = manual;
            tb_t4_y3.Enabled = manual;
        }

        private void btn_t4_calc_Click(object sender, EventArgs e)
        {
            double x1 = 0, y1 = 0;
            double x2 = 2, y2 = 0;
            double x3 = 1, y3 = 2;

            if (rb_t4_manual.Checked)
            {
                if (!TryReadDouble(tb_t4_x1, "x1", out x1)) return;
                if (!TryReadDouble(tb_t4_y1, "y1", out y1)) return;

                if (!TryReadDouble(tb_t4_x2, "x2", out x2)) return;
                if (!TryReadDouble(tb_t4_y2, "y2", out y2)) return;

                if (!TryReadDouble(tb_t4_x3, "x3", out x3)) return;
                if (!TryReadDouble(tb_t4_y3, "y3", out y3)) return;
            }

            double per = rb_t4_default.Checked
                ? Task4Lib.PERIMETR()
                : Task4Lib.PERIMETR(x1, y1, x2, y2, x3, y3);

            double area = Task4Lib.TriangleArea(
                x1, y1,
                x2, y2,
                x3, y3
            );

            rtb_t4_out.Clear();

            AppendLineColor(
                rtb_t4_out,
                $"Точка A: ({x1}; {y1})",
                Color.DimGray
            );

            AppendLineColor(
                rtb_t4_out,
                $"Точка B: ({x2}; {y2})",
                Color.DimGray
            );

            AppendLineColor(
                rtb_t4_out,
                $"Точка C: ({x3}; {y3})",
                Color.DimGray
            );

            AppendLineColor(rtb_t4_out, "", Color.DimGray);

            if (area <= 0)
            {
                AppendLineColor(
                    rtb_t4_out,
                    "ERROR: Треугольник вырожденный (площадь = 0).",
                    Color.Red
                );

                return;
            }

            AppendLineColor(
                rtb_t4_out,
                $"RESULT: Периметр = {per:F6}",
                Color.Green
            );

            AppendLineColor(
                rtb_t4_out,
                $"RESULT: Площадь = {area:F6}",
                Color.Green
            );
        }
    }
}