namespace WinFormsApp1
{
    public class MathHelper
    {
        public double getSumListsMultiplied(List<double> x, List<double> y)
        {
            double sum = 0;

            for (int i = 0; i < x.Count; i++)
            {
                sum += x[i] * y[i];
            }

            return sum;
        }

        public double getAverageValue(List<double> values)
        {
            double sum = 0;

            foreach (double v in values)
            {
                sum += v;
            }

            return sum / values.Count;
        }

        public double getSumValue(List<double> values)
        {
            double sum = 0;

            foreach (double v in values)
            {
                sum += v;
            }

            return sum;
        }

        public List<double> multiplyVectorToValue(List<double> values, double multiplier)
        {
            List<double> sum = new List<double>();

            foreach (double v in values)
            {
                sum.Add(v * multiplier);
            }

            return sum;
        }

        public double getSumSquared(List<double> values)
        {
            double sum = 0;

            foreach (double v in values)
            {
                sum += Math.Pow(v, 2);
            }

            return sum;
        }

        public double getSquaredDifference(List<double> x, List<double> y, double a0, double a1)
        {
            double sum = 0;

            for (int i = 0; i < x.Count; i++)
            {
                sum += Math.Pow(y[i] - (a0 + a1 * x[i]) , 2);
            }
            
            return sum;
        }
    }
}
