using System.Diagnostics;

namespace WinFormsApp1
{
    public class KRAResultItem
    {
        public Double a0 { get; set; }
        public Double a1 { get; set; }
        public Double elastic { get; set; }
        public Double r { get; set; }
        public Double rIndex { get; set; }
        public String regressionExpression { get; set; }
        public Double sigmaY { get; set; }
        public Double sigmaYX { get; set; }
        public Double bigR { get; set; }
        public Double yx { get; set; }
        public Double a { get; set; }
    };

    public class KRA
    {
        public double elasticAl;
        public double elasticCu;
        public double r;
        public double rCu;
        public double rIndex;
        public double n;
        public string regressionExpressionAl;
        public string regressionExpressionCu;
        private List<double> _alCreated;
        private List<double> _alBought;
        private List<double> _cuCreated;
        private List<double> _cuBought;
        private MathHelper _mathHelper = new MathHelper();
        public double countA0;
        public double countA1;
        public double countA0Cu;
        public double countA1Cu;
        public double sigmaY;
        public double sigmaYX;
        public double sigmaYCu;
        public double sigmaYXCu;
        public double bigR;
        public double bigRCu;

        public List<KRAResultItem> _result = new();
        public double kraA;
        public double bigRSquared;
        public double fisher;
        public double yx = 0;

        List<List<double>> _values;
        List<double> _y;

        public void setValues(List<List<double>> values, List<double> y) {
            this._values = values;
            this._y = y;
            // this._alCreated = alCreated; this._alBought = alBought; this._cuCreated = cuCreated; this._cuBought = cuBought;
        }

        public double countFisher()
        {
            double result = 0;

            return result;
        }

        public void startKra()
        {
            //double a0Al = this.a0(this._alCreated, this._alBought);
            //double a1Al = this.a1(this._alCreated, this._alBought);
            //double a0Cu = this.a0(this._cuCreated, this._cuBought);
            //double a1Cu = this.a1(this._cuCreated, this._cuBought);

            //this.countA0 = a0Al;
            //this.countA1 = a1Al;
            //this.sigmaY = this.calculateSigmaY(this._alBought);
            //this.sigmaYX = this.calculateSigmaYX(this._alCreated, this._alBought);
            //this.bigR = this.calculateRBig(this._alCreated, this._alBought);
            //this.elasticAl = this.calculateElastic(this._alCreated, this._alBought, a1Al);
            //this.r = this.calculateR(this._alCreated, this._alBought);
            //this.regressionExpressionAl = $"{this.countA0} + {this.countA1}x";

            //this.countA0Cu = a0Cu;
            //this.countA1Cu = a1Cu;
            //this.sigmaYCu = this.calculateSigmaY(this._cuBought);
            //this.elasticCu = this.calculateElastic(this._cuCreated, this._cuBought, a1Cu);
            //this.sigmaYXCu = this.calculateSigmaYX(this._cuCreated, this._cuBought);
            //this.bigRCu = this.calculateRBig(this._cuCreated, this._cuBought);
            //this.rCu = this.calculateR(this._cuCreated, this._cuBought);
            //this.regressionExpressionCu = $"{this.countA0Cu} + {this.countA1Cu}x";

            for (int i = 0; i < this._values.Count; i++)
            {
                KRAResultItem item = new();
                item.a0 = this.a0(this._values[i], this._y);
                item.a1 = this.a1(this._values[i], this._y);
                item.sigmaYX = this.calculateSigmaYX(this._values[i], this._y);
                item.elastic = this.calculateElastic(this._values[i], this._y, item.a1);
                item.r = this.calculateR(this._values[i], this._y);
                //item.bigR = Math.Abs(this.calculateRBig(this._values[i], this._y, item.a0, item.a1, item.sigmaY));
                item.bigR = Math.Pow(item.r, 2);
                item.regressionExpression = $"{item.a0} + {item.a1}x";
                _result.Add(item);
            }
            //this.kraA = this.calculateA(this._y, this._values);
            this.kraA = this._mathHelper.getAverageValue(this._y);
            for (int i = 0; i < this._result.Count; i++)
            {
                _result[i].yx = this.kraA
                    + (_result[i].a0 + _result[i].a1 * this._values[i][0])
                    + (_result[i].a0 + _result[i].a1 * this._values[i][1])
                    + (_result[i].a0 + _result[i].a1 * this._values[i][2])
                    + (_result[i].a0 + _result[i].a1 * this._values[i][3])
                    + (_result[i].a0 + _result[i].a1 * this._values[i][4]);
                this.yx = _result[i].yx;
            }
            this.sigmaY = this.calculateSigmaY(this._y);
            this.bigRSquared = this.calculateBigRSquared(this._y);
            this.fisher = this.calculateFisher();
        }

        private double a0(List<double> x, List<double> y) => (this._mathHelper.getSumValue(y) * this._mathHelper.getSumSquared(x) - this._mathHelper.getSumListsMultiplied(x, y) * this._mathHelper.getSumValue(x)) / (x.Count * this._mathHelper.getSumSquared(x) - Math.Pow(this._mathHelper.getSumValue(x), 2));

        private double a1(List<double> x, List<double> y) => (x.Count * this._mathHelper.getSumListsMultiplied(x, y) - this._mathHelper.getSumValue(x) * this._mathHelper.getSumValue(y)) / (x.Count * this._mathHelper.getSumSquared(x) - Math.Pow(this._mathHelper.getSumValue(x), 2));

        private double calculateElastic(List<double> x, List<double> y, double a1) => a1 * this._mathHelper.getAverageValue(x) / this._mathHelper.getAverageValue(y);

        private double calculateR(List<double> x, List<double> y) => (this._mathHelper.getSumListsMultiplied(x, y) - (this._mathHelper.getSumValue(x) * this._mathHelper.getSumValue(y) / x.Count)) / Math.Sqrt((this._mathHelper.getSumSquared(x) - (Math.Pow(this._mathHelper.getSumValue(x), 2)) / x.Count) * (this._mathHelper.getSumSquared(y) - (Math.Pow(this._mathHelper.getSumValue(y), 2)) / x.Count));

        private double calculateSigmaY(List<double> y) => (this._mathHelper.getSumSquared(y) / y.Count) - Math.Pow((this._mathHelper.getSumValue(y) / y.Count), 2);

        private double calculateSigmaYX(List<double> x, List<double> y) => this.sigmaY - (this._mathHelper.getSquaredDifference(x, y, this.countA0, this.countA1) / x.Count);

        private double calculateRBig(List<double> x, List<double> y, double a0, double a1, double sigmaY) => Math.Sqrt(1 - (this._mathHelper.getSquaredDifference(x, y, a0, a1) / x.Count) / sigmaY);
    
        private double calculateA(List<double> y, List<List<double>> x)
        {
            double avgY = this._mathHelper.getAverageValue(y);
            double sum = 0;

            for(int i = 0; i < x.Count; i++)
            {
                sum += this._result[i].r + this._mathHelper.getAverageValue(x[i]);
            }

            return avgY - sum;
        }

        private double calculateBigRSquared(List<double> y)
        {
            double upper = 0;
            for (int i = 0; i < y.Count; i++)
            {
                upper += Math.Pow((y[i]) - this.yx, 2); 
            }
            double lower = 0;
            for (int i = 0; i < _result.Count; i++)
            {
                lower += Math.Pow(y[i] - this._mathHelper.getAverageValue(y), 2);
            }
            //return Math.Pow(1 - upper/lower, 2);
            return 1 - upper / lower;
        }

        public double calculateFisher()
        {
            //return (this.bigRSquared * (this._y.Count - this._values.Count)) / ((1 - Math.Pow(this.bigRSquared, 2)) - (this._values.Count - 1));
            double first = Math.Abs(this.bigRSquared / (1 - this.bigRSquared));
            double secondF = this._y.Count - this._values.Count - 1;
            double secondT = this._values.Count - 1;
            double second = secondF / secondT;
            return first * second;
        }
    }
}
