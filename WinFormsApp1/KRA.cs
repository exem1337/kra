using System.Diagnostics;

namespace WinFormsApp1
{
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

        public void setValues(List<double> alCreated, List<double> alBought, List<double> cuCreated, List<double> cuBought) {
            this._alCreated = alCreated; this._alBought = alBought; this._cuCreated = cuCreated; this._cuBought = cuBought;
        }

        public void startKra()
        {
            double a0Al = this.a0(this._alCreated, this._alBought);
            double a1Al = this.a1(this._alCreated, this._alBought);
            double a0Cu = this.a0(this._cuCreated, this._cuBought);
            double a1Cu = this.a1(this._cuCreated, this._cuBought);

            this.countA0 = a0Al;
            this.countA1 = a1Al;
            this.sigmaY = this.calculateSigmaY(this._alBought);
            this.sigmaYX = this.calculateSigmaYX(this._alCreated, this._alBought);
            this.bigR = this.calculateRBig(this._alCreated, this._alBought);
            this.elasticAl = this.calculateElastic(this._alCreated, this._alBought, a1Al);
            this.r = this.calculateR(this._alCreated, this._alBought);
            this.regressionExpressionAl = $"{this.countA0} + {this.countA1}x";

            this.countA0Cu = a0Cu;
            this.countA1Cu = a1Cu;
            this.sigmaYCu = this.calculateSigmaY(this._cuBought);
            this.elasticCu = this.calculateElastic(this._cuCreated, this._cuBought, a1Cu);
            this.sigmaYXCu = this.calculateSigmaYX(this._cuCreated, this._cuBought);
            this.bigRCu = this.calculateRBig(this._cuCreated, this._cuBought);
            this.rCu = this.calculateR(this._cuCreated, this._cuBought);
            this.regressionExpressionCu = $"{this.countA0Cu} + {this.countA1Cu}x";
        }

        private double a0(List<double> x, List<double> y) => (this._mathHelper.getSumValue(y) * this._mathHelper.getSumSquared(x) - this._mathHelper.getSumListsMultiplied(x, y) * this._mathHelper.getSumValue(x)) / (x.Count * this._mathHelper.getSumSquared(x) - Math.Pow(this._mathHelper.getSumValue(x), 2));

        private double a1(List<double> x, List<double> y) => (x.Count * this._mathHelper.getSumListsMultiplied(x, y) - this._mathHelper.getSumValue(x) * this._mathHelper.getSumValue(y)) / (x.Count * this._mathHelper.getSumSquared(x) - Math.Pow(this._mathHelper.getSumValue(x), 2));

        private double calculateElastic(List<double> x, List<double> y, double a1) => a1 * this._mathHelper.getAverageValue(x) / this._mathHelper.getAverageValue(y);

        private double calculateR(List<double> x, List<double> y) => (this._mathHelper.getSumListsMultiplied(x, y) - (this._mathHelper.getSumValue(x) * this._mathHelper.getSumValue(y) / x.Count)) / Math.Sqrt((this._mathHelper.getSumSquared(x) - (Math.Pow(this._mathHelper.getSumValue(x), 2)) / x.Count) * (this._mathHelper.getSumSquared(y) - (Math.Pow(this._mathHelper.getSumValue(y), 2)) / x.Count));

        private double calculateSigmaY(List<double> y) => (this._mathHelper.getSumSquared(y) / y.Count) - Math.Pow((this._mathHelper.getSumValue(y) / y.Count), 2);

        private double calculateSigmaYX(List<double> x, List<double> y) => this.sigmaY - (this._mathHelper.getSquaredDifference(x, y, this.countA0, this.countA1) / x.Count);

        private double calculateRBig(List<double> x, List<double> y) => Math.Sqrt(1 - (this._mathHelper.getSquaredDifference(x, y, this.countA0, this.countA1) / x.Count) / this.sigmaY);
    }
}
