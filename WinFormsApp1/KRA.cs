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
        public List<double> smallRs = new();

        List<List<double>> _values;
        List<List<double>> valuesTransposed = new();
        List<double> transposedYMultiply = new();
        List<double> multiplyResut = new();
        List<List<double>> transposedMultiply = new();
        List<List<double>> transposedMultiplyBack = new();
        List<List<double>> initialValuesTransposed = new();
        public List<double> finalKoefs = new(); // коэфициенты уравнения регрессии

        public KraResult result = new(); //Средняя ошибка аппроксимации

        List<double> _y;
        public List<List<double>> pairCorrelations = new();

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
            for (int i = 0; i < this._values.Count; i++)
            {
                List<double> list = new();
                for (int j = 0; j < this._values.Count; j++)
                {
                    list.Add(this.calculatePairCorrelations(this._values[i], this._values[j]));
                }
                list.Add(this.calculatePairCorrelations(this._values[i], this._y));
                this.pairCorrelations.Add(list);
            }
            List<double> listY = new();
            for (int i = 0; i < this._values.Count; i++)
            {
                listY.Add(this.calculatePairCorrelations(this._y, this._values[i]));
            }
            listY.Add(this.calculatePairCorrelations(this._y, this._y));
            this.pairCorrelations.Add(listY); // парные кореляции

            for (int i = 0; i < this._values.Count; i++)
            {
                double sigmaY = Math.Sqrt(this._mathHelper.getAverageValueSquared(this._y) - Math.Pow(this._mathHelper.getAverageValue(this._y), 2));
                double sigmaX = Math.Sqrt(this._mathHelper.getAverageValueSquared(this._values[i]) - Math.Pow(this._mathHelper.getAverageValue(this._values[i]), 2));

                KRAResultItem item = new();
                //item.a0 = this.a0(this._values[i], this._y);
                //item.a1 = this.a1(this._values[i], this._y);
                //item.sigmaYX = this.calculateSigmaYX(this._values[i], this._y);
                //item.elastic = this.calculateElastic(this._values[i], this._y, item.a1);
                item.r = this.calculateR(this._values[i], this._y);
                ////item.bigR = Math.Abs(this.calculateRBig(this._values[i], this._y, item.a0, item.a1, item.sigmaY));
                //item.bigR = Math.Pow(item.r, 2);
                //item.regressionExpression = $"{item.a0} + {item.a1}x";
                _result.Add(item);
            }

            List<double> ones = new List<double>();
            for (int i = 0; i < _y.Count; i++)
            {
                ones.Add(1);
            }
            this.initialValuesTransposed = this._mathHelper.transposeMatrix(this._values);
            this._values.Insert(0, ones);
            this.valuesTransposed = this._mathHelper.transposeMatrix(this._values);
            //this.multiplyResut = this._mathHelper.multiplyMatrix(this.valuesTransposed, this._y);
            this.transposedMultiply = this._mathHelper.multiplyMatrixes(this.valuesTransposed, this._values);
            this.transposedYMultiply = this._mathHelper.multiplyMatrix(this.valuesTransposed, this._y);
            this.transposedMultiplyBack = this._mathHelper.backMatrix(this.transposedMultiply);
            this.finalKoefs = this._mathHelper.multiplyMatrix(this.transposedMultiplyBack, this.transposedYMultiply);
            this.result = this.calcKraResults();

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

        private KraResult calcKraResults()
        {
            double epsilonSquared = 0; // оценка дисперсии
            double epsilonY = 0; 

            for (int i = 0; i < this._y.Count; i++)
            {
                double predict = this.calcYPredict(this.initialValuesTransposed[i]);
                double epsilon = this._y[i] - predict;
                epsilonSquared += Math.Pow(epsilon, 2);
                epsilonY += Math.Abs(epsilon / this._y[i]);
            }

            double avgApproximation = epsilonY / this._y.Count * 100; // средняя ошибка аппроксимации
            double unshiftedDispersion = (1 / (this._y.Count - (this._values.Count - 1) - 1)) * epsilonSquared; // несмещенная оценка дисперсии 
            double avgSquaredShift = Math.Sqrt(unshiftedDispersion); // среднеквадратическое отклонение

            List<double> paramsDispersion = new(); // дисперсия параметров
            List<List<double>> covariantMatrix = this._mathHelper.multiplyMatrixToNumber(this.transposedMultiplyBack, unshiftedDispersion); // оценка ковариационной матрицы вектора

            for (int i = 0; i < covariantMatrix.Count; i++)
            {
                paramsDispersion.Add(Math.Sqrt(covariantMatrix[i][i]));
            }

            List<double> elastics = new(); // частные коэфициенты эластичности

            for (int i = 1; i < this._values.Count; i++)
            {
                elastics.Add(this.finalKoefs[i] * this._mathHelper.getAverageValue(this._values[i]) / this._mathHelper.getAverageValue(this._y));
            }

            List<double> studentCriterias = new(); // t - критерии стьюдента

            for (int i = 0; i < this.finalKoefs.Count; i++)
            {
                studentCriterias.Add(Math.Abs(finalKoefs[i] / paramsDispersion[i]));
            }
            double lower = this.calcRLower();
            double rSquared = Math.Round(1 - (epsilonSquared / (lower)), 4);

            double fisherUpper = rSquared / (1 - rSquared);

            double fisherLowerUpper = this._y.Count - (this._values.Count - 1) - 1;
            double fisherLowerLower = this._values.Count - 1;

            double fisherLower = fisherLowerUpper / fisherLowerLower;

            double fisher = fisherUpper * fisherLower;
            KraResult res = new();

            res.fisher = fisher;
            res.epsilonY = epsilonY;
            res.epsilonSquared = epsilonSquared;
            res.rSquared = rSquared;
            res.avgSquaredShift = avgSquaredShift;
            res.avgApproximation = avgApproximation;
            res.elastics = elastics;
            res.studentCriterias = studentCriterias;
            res.unshiftedDispersion = unshiftedDispersion;
            res.paramsDispersion = paramsDispersion;

            return res;
        }

        private double calcRLower()
        {
            double res = 0;
            double avgY = this._mathHelper.getAverageValue(this._y);

            for (int i = 0; i < this._y.Count; i++)
            {
                res += Math.Pow(this._y[i] - avgY, 2);
            }

            return res;
        }

        public double calcYPredict(List<double> x) //расчитать значение по уравнению регрессии
        {
            double res = this.finalKoefs[0];

            for (int i = 0; i < x.Count; i++)
            {
                res += x[i] * this.finalKoefs[i + 1];
            }

            return res;
        }

        private double a0(List<double> x, List<double> y) => (this._mathHelper.getSumValue(y) * this._mathHelper.getSumSquared(x) - this._mathHelper.getSumListsMultiplied(x, y) * this._mathHelper.getSumValue(x)) / (x.Count * this._mathHelper.getSumSquared(x) - Math.Pow(this._mathHelper.getSumValue(x), 2));

        private double a1(List<double> x, List<double> y) => (x.Count * this._mathHelper.getSumListsMultiplied(x, y) - this._mathHelper.getSumValue(x) * this._mathHelper.getSumValue(y)) / (x.Count * this._mathHelper.getSumSquared(x) - Math.Pow(this._mathHelper.getSumValue(x), 2));

        private double calculateElastic(List<double> x, List<double> y, double a1) => a1 * this._mathHelper.getAverageValue(x) / this._mathHelper.getAverageValue(y);

        private double calculateR(List<double> x, List<double> y) => (this._mathHelper.getSumListsMultiplied(x, y) - (this._mathHelper.getSumValue(x) * this._mathHelper.getSumValue(y) / x.Count)) / Math.Sqrt((this._mathHelper.getSumSquared(x) - (Math.Pow(this._mathHelper.getSumValue(x), 2)) / x.Count) * (this._mathHelper.getSumSquared(y) - (Math.Pow(this._mathHelper.getSumValue(y), 2)) / x.Count));

        private double calculateSigmaY(List<double> y) => (this._mathHelper.getSumSquared(y) / y.Count) - Math.Pow((this._mathHelper.getSumValue(y) / y.Count), 2);

        private double calculateSigmaYX(List<double> x, List<double> y) => this.sigmaY - (this._mathHelper.getSquaredDifference(x, y, this.countA0, this.countA1) / x.Count);

        private double calculateRBig(List<double> x, List<double> y, double a0, double a1, double sigmaY) => Math.Sqrt(1 - (this._mathHelper.getSquaredDifference(x, y, a0, a1) / x.Count) / sigmaY);
    
        private double calculatePairCorrelations(List<double> x, List<double> y)
        {    
            double upper = 0;
            for (int i = 0; i < x.Count; i++)
            {
                upper += (x[i] - this._mathHelper.getAverageValue(x)) * (y[i] - this._mathHelper.getAverageValue(y));
            }

            double lowerFirst = 0;
            for (int i = 0; i < x.Count; i++)
            {
                lowerFirst += Math.Pow(x[i] - this._mathHelper.getAverageValue(x), 2);
            }
            double lowerSecond = 0;
            for (int i = 0; i < x.Count; i++)
            {
                lowerSecond += Math.Pow(y[i] - this._mathHelper.getAverageValue(y), 2);
            }
            double lower = Math.Sqrt(lowerFirst * lowerSecond);

            return upper / lower;
        }

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

        private double calculateFisher()
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
